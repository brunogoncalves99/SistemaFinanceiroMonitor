using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Web.Models;
using System.Text.Json;

namespace SistemaFinanceiroMonitor.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = await _dashboardService.ObterDadosDashboardAsync();

            var viewModel = new DashboardViewModel
            {
                UltimaCotacaoDolar = dashboardData.UltimaCotacaoDolar,
                UltimaCotacaoEuro = dashboardData.UltimaCotacaoEuro,
                UltimaSelic = dashboardData.UltimaSelic,
                UltimoIPCA = dashboardData.UltimoIPCA,
                HistoricoDolar30Dias = dashboardData.HistoricoDolar30Dias,
                HistoricoSelic12Meses = dashboardData.HistoricoSelic12Meses,
                TotalAlertasAtivos = dashboardData.TotalAlertasAtivos,
                TotalAlertasDisparados = dashboardData.TotalAlertasDisparados,
                DataAtualizacao = dashboardData.DataAtualizacao
            };

            // Preparar dados para gráficos (JSON)
            if (dashboardData.HistoricoDolar30Dias.Any())
            {
                var labels = dashboardData.HistoricoDolar30Dias
                    .Select(c => c.DataCotacao.ToString("dd/MM"))
                    .ToList();
                var valores = dashboardData.HistoricoDolar30Dias
                    .Select(c => c.ValorVenda)
                    .ToList();

                viewModel.GraficoDolarLabels = JsonSerializer.Serialize(labels);
                viewModel.GraficoDolarValores = JsonSerializer.Serialize(valores);
            }

            if (dashboardData.HistoricoSelic12Meses.Any())
            {
                var labels = dashboardData.HistoricoSelic12Meses
                    .Select(i => i.DataReferencia.ToString("MMM/yy"))
                    .ToList();
                var valores = dashboardData.HistoricoSelic12Meses
                    .Select(i => i.Valor)
                    .ToList();

                viewModel.GraficoSelicLabels = JsonSerializer.Serialize(labels);
                viewModel.GraficoSelicValores = JsonSerializer.Serialize(valores);
            }

            return View(viewModel);
        }
    }
}