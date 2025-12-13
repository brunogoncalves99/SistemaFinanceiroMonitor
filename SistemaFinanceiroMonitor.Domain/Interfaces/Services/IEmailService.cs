namespace SistemaFinanceiroMonitor.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task EnviarEmailAlertaAsync(string destinatario, string assunto, string mensagem);
        Task EnviarRelatorioSemanalAsync(string destinatario, string conteudoHtml);
    }
}
