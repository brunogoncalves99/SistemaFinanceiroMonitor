using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiroMonitor.Application.Interface;

namespace SistemaFinanceiroMonitor.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardApiController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardApiController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("Dados")]
        public async Task<IActionResult> ObterDadosDashboard()
        {
            try
            {
                var dados = await _dashboardService.ObterDadosDashboardAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao obter dados: {ex.Message}" });
            }
        }
    }
}