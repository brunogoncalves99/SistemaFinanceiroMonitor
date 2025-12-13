using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Domain.Interfaces.Repositories
{
    public interface IIndicadorRepository
    {
        Task<IndicadorEconomico> ObterPorIdAsync(int id);
        Task<IndicadorEconomico> ObterUltimoIndicadorAsync(TipoIndicador tipoIndicador);
        Task<IEnumerable<IndicadorEconomico>> ObterIndicadoresPorPeriodoAsync(TipoIndicador tipoIndicador, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<IndicadorEconomico>> ObterTodosAsync();
        Task AdicionarAsync(IndicadorEconomico indicador);
        Task AdicionarVariosAsync(IEnumerable<IndicadorEconomico> indicadores);
        Task<bool> ExisteIndicadorAsync(TipoIndicador tipoIndicador, DateTime dataReferencia);
    }
}
