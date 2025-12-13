using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.Interface
{
    public interface IIndicadorService
    {
        Task<IndicadorDTO> ObterUltimoIndicadorAsync(TipoIndicador tipoIndicador);
        Task<IEnumerable<IndicadorDTO>> ObterIndicadoresPorPeriodoAsync(TipoIndicador tipoIndicador, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<IndicadorDTO>> ObterHistoricoUltimos12MesesAsync(TipoIndicador tipoIndicador);
        Task<bool> AtualizarIndicadoresAsync();
    }
}
