using SistemaFinanceiroMonitor.Domain.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace SistemaFinanceiroMonitor.Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task EnviarEmailAlertaAsync(string destinatario, string assunto, string mensagem)
        {
            try
            {
                using var smtpClient = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = _settings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = assunto,
                    Body = mensagem,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(destinatario);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                throw;
            }
        }

        public async Task EnviarRelatorioSemanalAsync(string destinatario, string conteudoHtml)
        {
            try
            {
                using var smtpClient = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = _settings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = "📊 Relatório Semanal - Sistema Financeiro Monitor",
                    Body = conteudoHtml,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(destinatario);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar relatório semanal: {ex.Message}");
                throw;
            }
        }
    }
}
