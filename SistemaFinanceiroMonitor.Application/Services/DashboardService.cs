using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ICotacaoService _cotacaoService;
        private readonly IIndicadorService _indicadorService;
        private readonly IAlertaService _alertaService;

        public DashboardService(ICotacaoService cotacaoService, IIndicadorService indicadorService, IAlertaService alertaService)
        {
            _cotacaoService = cotacaoService;
            _indicadorService = indicadorService;
            _alertaService = alertaService;
        }


        public async Task<DashboardDTO> ObterDadosDashboardAsync()
        {
            var dashboard = new DashboardDTO
            {
                DataAtualizacao = DateTime.Now
            };

            // Buscar última cotação do Dólar
            dashboard.UltimaCotacaoDolar = await _cotacaoService.ObterUltimaCotacaoAsync(TipoMoeda.Dolar);

            // Buscar última cotação do Euro
            dashboard.UltimaCotacaoEuro = await _cotacaoService.ObterUltimaCotacaoAsync(TipoMoeda.Euro);

            // Buscar último valor da SELIC
            dashboard.UltimaSelic = await _indicadorService.ObterUltimoIndicadorAsync(TipoIndicador.Selic);

            // Buscar último IPCA
            dashboard.UltimoIPCA = await _indicadorService.ObterUltimoIndicadorAsync(TipoIndicador.IPCA);

            // Buscar histórico do Dólar (últimos 30 dias)
            var historicoDolar = await _cotacaoService.ObterHistoricoUltimos30DiasAsync(TipoMoeda.Dolar);
            dashboard.HistoricoDolar30Dias = historicoDolar.ToList();

            // Buscar histórico da SELIC (últimos 12 meses)
            var historicoSelic = await _indicadorService.ObterHistoricoUltimos12MesesAsync(TipoIndicador.Selic);
            dashboard.HistoricoSelic12Meses = historicoSelic.ToList();

            // Contar alertas ativos
            var alertasAtivos = await _alertaService.ObterAlertasAtivosAsync();
            dashboard.TotalAlertasAtivos = alertasAtivos.Count();
            dashboard.TotalAlertasDisparados = alertasAtivos.Count(a => a.Status == StatusAlerta.Disparado);

            return dashboard;
        }
    }
}