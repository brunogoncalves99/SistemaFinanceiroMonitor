using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Domain.Interfaces.Repositories
{
    public interface ICotacaoRepository
    {
        Task<Cotacao> ObterPorIdAsync(int id);
        Task<Cotacao> ObterUltimaCotacaoAsync(TipoMoeda tipoMoeda);
        Task<IEnumerable<Cotacao>> ObterCotacoesPorPeriodoAsync(TipoMoeda tipoMoeda, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<Cotacao>> ObterTodasAsync();
        Task AdicionarAsync(Cotacao cotacao);
        Task AdicionarVariasAsync(IEnumerable<Cotacao> cotacoes);
        Task AtualizarAsync(Cotacao cotacao);
        Task<bool> ExisteCotacaoAsync(TipoMoeda tipoMoeda, DateTime dataCotacao);
    }
}
