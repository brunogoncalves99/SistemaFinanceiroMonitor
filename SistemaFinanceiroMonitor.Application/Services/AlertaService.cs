using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Domain.Interfaces.Services;

namespace SistemaFinanceiroMonitor.Application.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IHistoricoAlertaRepository _historicoRepository;
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;

        public AlertaService(IAlertaRepository alertaRepository, IHistoricoAlertaRepository historicoRepository, ICotacaoRepository cotacaoRepository, IUsuarioRepository usuarioRepository, IEmailService emailService)
        {
            _alertaRepository = alertaRepository;
            _historicoRepository = historicoRepository;
            _cotacaoRepository = cotacaoRepository;
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        public async Task<AlertaDTO> ObterPorIdAsync(int id)
        {
            var alerta = await _alertaRepository.ObterPorIdAsync(id);
            return alerta != null ? ConverterParaDTO(alerta) : null;
        }

        public async Task<IEnumerable<AlertaDTO>> ObterPorUsuarioAsync(int usuarioId)
        {
            var alertas = await _alertaRepository.ObterPorUsuarioAsync(usuarioId);
            return alertas.Select(a => ConverterParaDTO(a)).ToList();
        }

        public async Task<IEnumerable<AlertaDTO>> ObterAlertasAtivosAsync()
        {
            var alertas = await _alertaRepository.ObterAlertasAtivosAsync();
            return alertas.Select(a => ConverterParaDTO(a)).ToList();
        }

        public async Task<int> CriarAlertaAsync(CriarAlertaDTO dto)
        {
            Alerta alerta;

            if (dto.TipoMoeda.HasValue)
            {
                alerta = new Alerta(
                    dto.UsuarioId,
                    dto.TipoAlerta,
                    dto.TipoMoeda.Value,
                    dto.ValorGatilho ?? 0,
                    dto.Descricao
                );
            }
            else if (dto.TipoIndicador.HasValue)
            {
                alerta = new Alerta(
                    dto.UsuarioId,
                    dto.TipoAlerta,
                    dto.TipoIndicador.Value,
                    dto.ValorGatilho ?? 0,
                    dto.Descricao
                );
            }
            else
            {
                throw new ArgumentException("Deve informar TipoMoeda ou TipoIndicador");
            }

            await _alertaRepository.AdicionarAsync(alerta);
            return alerta.Id;
        }

        public async Task AtualizarAlertaAsync(int id, CriarAlertaDTO dto)
        {
            var alerta = await _alertaRepository.ObterPorIdAsync(id);

            if (alerta == null)
                throw new ArgumentException("Alerta não encontrado");

            await _alertaRepository.AtualizarAsync(alerta);
        }

        public async Task RemoverAlertaAsync(int id)
        {
            await _alertaRepository.RemoverAsync(id);
        }

        public async Task PausarAlertaAsync(int id)
        {
            var alerta = await _alertaRepository.ObterPorIdAsync(id);

            if (alerta == null)
                throw new ArgumentException("Alerta não encontrado");

            alerta.Pausar();
            await _alertaRepository.AtualizarAsync(alerta);
        }

        public async Task ReativarAlertaAsync(int id)
        {
            var alerta = await _alertaRepository.ObterPorIdAsync(id);

            if (alerta == null)
                throw new ArgumentException("Alerta não encontrado");

            alerta.Reativar();
            await _alertaRepository.AtualizarAsync(alerta);
        }

        public async Task VerificarEDispararAlertasAsync()
        {
            var alertasAtivos = await _alertaRepository.ObterAlertasAtivosAsync();

            foreach (var alerta in alertasAtivos)
            {
                if (alerta.TipoMoeda.HasValue)
                {
                    var cotacao = await _cotacaoRepository.ObterUltimaCotacaoAsync(alerta.TipoMoeda.Value);

                    if (cotacao != null && alerta.VerificarSeDeveDisparar(cotacao.ValorVenda.Valor))
                    {
                        await DispararAlertaAsync(alerta, cotacao.ValorVenda.Valor);
                    }
                }
            }
        }

        private async Task DispararAlertaAsync(Alerta alerta, decimal valorAtual)
        {
            var historico = new HistoricoAlerta(alerta.Id, valorAtual);
            await _historicoRepository.AdicionarAsync(historico);

            // Obter usuário para enviar email
            var usuario = await _usuarioRepository.ObterPorIdAsync(alerta.UsuarioId);

            if (usuario != null)
            {
                var assunto = $"🔔 Alerta Ativado - {alerta.Descricao}";
                var mensagem = $@"
                    Olá {usuario.Nome},
                    
                    Alerta no Sistema de Monitaramento de Moeda!
                    
                    Descrição: {alerta.Descricao}
                    Valor atual: R$ {valorAtual:F4}
                    Data/Hora: {DateTime.Now:dd/MM/yyyy HH:mm}
                    
                    Acesse o dashboard para mais detalhes.
                ";

                try
                {
                    await _emailService.EnviarEmailAlertaAsync(usuario.Email.Endereco, assunto, mensagem);
                    historico.RegistrarEnvioEmail();

                    await _historicoRepository.AlterarHistoricoAsync(historico);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                }
            }

            alerta.Disparar();
            await _alertaRepository.AtualizarAsync(alerta);
        }

        public async Task<IEnumerable<HistoricoAlertaDTO>> ObterHistoricoAlertasAsync(int alertaId)
        {
            var historico = await _historicoRepository.ObterPorAlertaAsync(alertaId);

            return historico.Select(h => new HistoricoAlertaDTO
            {
                Id = h.Id,
                AlertaId = h.AlertaId,
                ValorNoMomento = h.ValorNoMomento,
                DataDisparo = h.DataDisparo,
                EmailEnviado = h.EmailEnviado,
                DataEnvioEmail = h.DataEnvioEmail
            }).ToList();
        }

        private AlertaDTO ConverterParaDTO(Alerta alerta)
        {
            return new AlertaDTO
            {
                Id = alerta.Id,
                UsuarioId = alerta.UsuarioId,
                NomeUsuario = alerta.Usuario?.Nome,
                TipoAlerta = alerta.TipoAlerta,
                TipoAlertaDescricao = ObterDescricaoTipoAlerta(alerta.TipoAlerta),
                TipoMoeda = alerta.TipoMoeda,
                NomeMoeda = alerta.TipoMoeda.HasValue ? alerta.TipoMoeda.Value.ToString() : null,
                TipoIndicador = alerta.TipoIndicador,
                NomeIndicador = alerta.TipoIndicador.HasValue ? alerta.TipoIndicador.Value.ToString() : null,
                ValorGatilho = alerta.ValorGatilho,
                PercentualGatilho = alerta.PercentualGatilho,
                Status = alerta.Status,
                StatusDescricao = alerta.Status.ToString(),
                Descricao = alerta.Descricao,
                DataCriacao = alerta.DataCriacao,
                DataUltimoDisparo = alerta.DataUltimoDisparo
            };
        }

        private string ObterDescricaoTipoAlerta(TipoAlerta tipo)
        {
            return tipo switch
            {
                TipoAlerta.CotacaoAcimaDe => "Cotação Acima De",
                TipoAlerta.CotacaoAbaixoDe => "Cotação Abaixo De",
                TipoAlerta.VariacaoPercentual => "Variação Percentual",
                TipoAlerta.RelatorioSemanal => "Relatório Semanal",
                _ => tipo.ToString()
            };
        }

    }
}
