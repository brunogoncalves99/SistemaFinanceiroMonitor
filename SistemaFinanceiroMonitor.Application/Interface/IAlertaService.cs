using SistemaFinanceiroMonitor.Application.DTOs;

namespace SistemaFinanceiroMonitor.Application.Interface
{
    public interface IAlertaService
    {
        Task<AlertaDTO> ObterPorIdAsync(int id);
        Task<IEnumerable<AlertaDTO>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<AlertaDTO>> ObterAlertasAtivosAsync();
        Task<int> CriarAlertaAsync(CriarAlertaDTO dto);
        Task AtualizarAlertaAsync(int id, CriarAlertaDTO dto);
        Task RemoverAlertaAsync(int id);
        Task PausarAlertaAsync(int id);
        Task ReativarAlertaAsync(int id);
        Task VerificarEDispararAlertasAsync();
        Task<IEnumerable<HistoricoAlertaDTO>> ObterHistoricoAlertasAsync(int alertaId);
    }
}
