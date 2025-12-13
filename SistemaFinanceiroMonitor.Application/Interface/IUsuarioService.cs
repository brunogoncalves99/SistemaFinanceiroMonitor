using SistemaFinanceiroMonitor.Application.DTOs;

namespace SistemaFinanceiroMonitor.Application.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> ObterPorIdAsync(int id);
        Task<UsuarioDTO> ObterPorEmailAsync(string email);
        Task<IEnumerable<UsuarioDTO>> ObterTodosAsync();
        Task<int> CriarUsuarioAsync(string nome, string email);
    }
}
