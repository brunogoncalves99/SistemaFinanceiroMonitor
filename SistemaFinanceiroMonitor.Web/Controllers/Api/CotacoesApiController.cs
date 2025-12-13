using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacoesApiController : ControllerBase
    {
        private readonly ICotacaoService _cotacaoService;

        public CotacoesApiController(ICotacaoService cotacaoService)
        {
            _cotacaoService = cotacaoService;
        }

        // GET: api/CotacoesApi/Ultima/Dolar
        [HttpGet("Ultima/{tipoMoeda}")]
        public async Task<IActionResult> ObterUltimaCotacao(TipoMoeda tipoMoeda)
        {
            try
            {
                var cotacao = await _cotacaoService.ObterUltimaCotacaoAsync(tipoMoeda);

                if (cotacao == null)
                    return NotFound(new { mensagem = "Cotação não encontrada" });

                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao obter cotação: {ex.Message}" });
            }
        }

        // GET: api/CotacoesApi/Historico/Dolar?dias=30
        [HttpGet("Historico/{tipoMoeda}")]
        public async Task<IActionResult> ObterHistorico(TipoMoeda tipoMoeda, int dias = 30)
        {
            try
            {
                var dataFim = DateTime.Now;
                var dataInicio = dataFim.AddDays(-dias);

                var cotacoes = await _cotacaoService.ObterCotacoesPorPeriodoAsync(
                    tipoMoeda,
                    dataInicio,
                    dataFim
                );

                return Ok(cotacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao obter histórico: {ex.Message}" });
            }
        }

        // GET: api/CotacoesApi/GraficoDolar
        [HttpGet("GraficoDolar")]
        public async Task<IActionResult> ObterDadosGraficoDolar(int dias = 30)
        {
            try
            {
                var historico = await _cotacaoService.ObterHistoricoUltimos30DiasAsync(TipoMoeda.Dolar);

                var dados = new
                {
                    labels = historico.Select(c => c.DataCotacao.ToString("dd/MM")).ToList(),
                    valores = historico.Select(c => c.ValorVenda).ToList()
                };

                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro: {ex.Message}" });
            }
        }
    }
}