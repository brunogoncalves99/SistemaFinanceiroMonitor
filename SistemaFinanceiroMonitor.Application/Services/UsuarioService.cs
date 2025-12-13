using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Domain.ValueObjects;

namespace SistemaFinanceiroMonitor.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioDTO> ObterPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            return usuario != null ? ConverterParaDTO(usuario) : null;
        }

        public async Task<UsuarioDTO> ObterPorEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
            return usuario != null ? ConverterParaDTO(usuario) : null;
        }

        public async Task<IEnumerable<UsuarioDTO>> ObterTodosAsync()
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            return usuarios.Select(u => ConverterParaDTO(u)).ToList();
        }

        public async Task<int> CriarUsuarioAsync(string nome, string email)
        {
            if (await _usuarioRepository.ExisteEmailAsync(email))
                throw new ArgumentException("Email já cadastrado");

            var emailVO = new Email(email);
            var usuario = new Usuario(nome, emailVO);

            await _usuarioRepository.AdicionarAsync(usuario);
            return usuario.Id;
        }

        private UsuarioDTO ConverterParaDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email.Endereco,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo,
                TotalAlertas = usuario.Alertas?.Count ?? 0
            };
        }



    }
}
