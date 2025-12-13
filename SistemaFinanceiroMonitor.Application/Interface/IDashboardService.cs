using SistemaFinanceiroMonitor.Application.DTOs;

namespace SistemaFinanceiroMonitor.Application.Interface
{
    public interface IDashboardService
    {
        Task<DashboardDTO> ObterDadosDashboardAsync();
    }
}
