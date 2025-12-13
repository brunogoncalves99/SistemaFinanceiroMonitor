using SistemaFinanceiroMonitor.Domain.Entities;

namespace SistemaFinanceiroMonitor.Domain.Interfaces.Repositories
{
    public interface IHistoricoAlertaRepository
    {
        Task<IEnumerable<HistoricoAlerta>> ObterPorAlertaAsync(int alertaId);
        Task<IEnumerable<HistoricoAlerta>> ObterPorUsuarioAsync(int usuarioId);
        Task AdicionarAsync(HistoricoAlerta historico);
        Task AlterarHistoricoAsync(HistoricoAlerta historico);
    }
}
