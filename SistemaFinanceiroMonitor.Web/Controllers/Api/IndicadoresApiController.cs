using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadoresApiController : ControllerBase
    {
        private readonly IIndicadorService _indicadorService;

        public IndicadoresApiController(IIndicadorService indicadorService)
        {
            _indicadorService = indicadorService;
        }

        // GET: api/IndicadoresApi/Ultimo/Selic
        [HttpGet("Ultimo/{tipoIndicador}")]
        public async Task<IActionResult> ObterUltimoIndicador(TipoIndicador tipoIndicador)
        {
            try
            {
                var indicador = await _indicadorService.ObterUltimoIndicadorAsync(tipoIndicador);

                if (indicador == null)
                    return NotFound(new { mensagem = "Indicador não encontrado" });

                return Ok(indicador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao obter indicador: {ex.Message}" });
            }
        }

        // GET: api/IndicadoresApi/Historico/Selic?meses=12
        [HttpGet("Historico/{tipoIndicador}")]
        public async Task<IActionResult> ObterHistorico(TipoIndicador tipoIndicador, int meses = 12)
        {
            try
            {
                var dataFim = DateTime.Now;
                var dataInicio = dataFim.AddMonths(-meses);

                var indicadores = await _indicadorService.ObterIndicadoresPorPeriodoAsync(
                    tipoIndicador,
                    dataInicio,
                    dataFim
                );

                return Ok(indicadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao obter histórico: {ex.Message}" });
            }
        }

        // GET: api/IndicadoresApi/GraficoSelic
        [HttpGet("GraficoSelic")]
        public async Task<IActionResult> ObterDadosGraficoSelic(int meses = 12)
        {
            try
            {
                var historico = await _indicadorService.ObterHistoricoUltimos12MesesAsync(TipoIndicador.Selic);

                var dados = new
                {
                    labels = historico.Select(i => i.DataReferencia.ToString("MMM/yy")).ToList(),
                    valores = historico.Select(i => i.Valor).ToList()
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