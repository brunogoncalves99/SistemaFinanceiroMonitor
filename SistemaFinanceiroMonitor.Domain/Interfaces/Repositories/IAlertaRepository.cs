using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Domain.Interfaces.Repositories
{
    public interface IAlertaRepository
    {
        Task<Alerta> ObterPorIdAsync(int id);
        Task<IEnumerable<Alerta>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<Alerta>> ObterAlertasAtivosAsync();
        Task<IEnumerable<Alerta>> ObterAlertasAtivosPorMoedaAsync(TipoMoeda tipoMoeda);
        Task AdicionarAsync(Alerta alerta);
        Task AtualizarAsync(Alerta alerta);
        Task RemoverAsync(int id);
    }
}
