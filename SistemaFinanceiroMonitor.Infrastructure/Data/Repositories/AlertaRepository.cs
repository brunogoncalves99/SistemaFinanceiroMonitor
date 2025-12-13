using Microsoft.EntityFrameworkCore;
using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Infrastructure.Data.Context;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Repositories
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly ApplicationDbContext _context;

        public AlertaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Alerta> ObterPorIdAsync(int id)
        {
            return await _context.Alertas
                .Include(a => a.Usuario)
                .Include(a => a.Historico)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Alerta>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.Alertas
                .Include(a => a.Historico)
                .Where(a => a.UsuarioId == usuarioId)
                .OrderByDescending(a => a.DataCriacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> ObterAlertasAtivosAsync()
        {
            return await _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.Status == StatusAlerta.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> ObterAlertasAtivosPorMoedaAsync(TipoMoeda tipoMoeda)
        {
            return await _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.Status == StatusAlerta.Ativo && a.TipoMoeda == tipoMoeda)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Alerta alerta)
        {
            await _context.Alertas.AddAsync(alerta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Alerta alerta)
        {
            _context.Alertas.Update(alerta);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var alerta = await ObterPorIdAsync(id);
            if (alerta != null)
            {
                _context.Alertas.Remove(alerta);
                await _context.SaveChangesAsync();
            }
        }
    }
}