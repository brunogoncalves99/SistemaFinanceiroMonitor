using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.Interface
{
    public interface ICotacaoService
    {
        Task<CotacaoDTO> ObterUltimaCotacaoAsync(TipoMoeda tipoMoeda);
        Task<IEnumerable<CotacaoDTO>> ObterCotacoesPorPeriodoAsync(TipoMoeda tipoMoeda, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<CotacaoDTO>> ObterHistoricoUltimos30DiasAsync(TipoMoeda tipoMoeda);
        Task<bool> AtualizarCotacoesAsync();
    }
}
