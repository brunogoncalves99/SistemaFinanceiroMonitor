using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Web.Models;

namespace SistemaFinanceiroMonitor.Web.Controllers
{
    public class CotacoesController : Controller
    {
        private readonly ICotacaoService _cotacaoService;

        public CotacoesController(ICotacaoService cotacaoService)
        {
            _cotacaoService = cotacaoService;
        }

        public async Task<IActionResult> Index(TipoMoeda? tipoMoeda)
        {
            var tipoMoedaSelecionada = tipoMoeda ?? TipoMoeda.Dolar;

            var ultimaCotacao = await _cotacaoService.ObterUltimaCotacaoAsync(tipoMoedaSelecionada);
            var historico = await _cotacaoService.ObterHistoricoUltimos30DiasAsync(tipoMoedaSelecionada);

            var viewModel = new CotacaoViewModel
            {
                TipoMoedaSelecionada = tipoMoedaSelecionada,
                UltimaCotacao = ultimaCotacao,
                Cotacoes = historico.ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Historico(TipoMoeda tipoMoeda, DateTime? dataInicio, DateTime? dataFim)
        {
            if (!dataInicio.HasValue)
                dataInicio = DateTime.Now.AddMonths(-3);

            if (!dataFim.HasValue)
                dataFim = DateTime.Now;

            var cotacoes = await _cotacaoService.ObterCotacoesPorPeriodoAsync(
                tipoMoeda,
                dataInicio.Value,
                dataFim.Value
            );

            var viewModel = new CotacaoViewModel
            {
                TipoMoedaSelecionada = tipoMoeda,
                Cotacoes = cotacoes.ToList(),
                DataInicio = dataInicio,
                DataFim = dataFim
            };

            return View(viewModel);
        }
    }
}