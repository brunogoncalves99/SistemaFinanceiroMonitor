using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Web.Models;

namespace SistemaFinanceiroMonitor.Web.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly IIndicadorService _indicadorService;

        public IndicadoresController(IIndicadorService indicadorService)
        {
            _indicadorService = indicadorService;
        }

        public async Task<IActionResult> Index(TipoIndicador? tipoIndicador)
        {
            var tipoIndicadorSelecionado = tipoIndicador ?? TipoIndicador.Selic;

            var ultimoIndicador = await _indicadorService.ObterUltimoIndicadorAsync(tipoIndicadorSelecionado);
            var historico = await _indicadorService.ObterHistoricoUltimos12MesesAsync(tipoIndicadorSelecionado);

            var viewModel = new IndicadorViewModel
            {
                TipoIndicadorSelecionado = tipoIndicadorSelecionado,
                UltimoIndicador = ultimoIndicador,
                Indicadores = historico.ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Historico(TipoIndicador tipoIndicador, DateTime? dataInicio, DateTime? dataFim)
        {
            if (!dataInicio.HasValue)
                dataInicio = DateTime.Now.AddYears(-2);

            if (!dataFim.HasValue)
                dataFim = DateTime.Now;

            var indicadores = await _indicadorService.ObterIndicadoresPorPeriodoAsync(
                tipoIndicador,
                dataInicio.Value,
                dataFim.Value
            );

            var viewModel = new IndicadorViewModel
            {
                TipoIndicadorSelecionado = tipoIndicador,
                Indicadores = indicadores.ToList(),
                DataInicio = dataInicio,
                DataFim = dataFim
            };

            return View(viewModel);
        }
    }
}