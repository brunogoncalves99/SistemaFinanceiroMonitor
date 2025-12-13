using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Repositories
{
    public class HistoricoAlertaRepository : IHistoricoAlertaRepository
    {
        private readonly ApplicationDbContext _context;

        public HistoricoAlertaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricoAlerta>> ObterPorAlertaAsync(int alertaId)
        {
            return await _context.HistoricoAlertas
                .Where(h => h.AlertaId == alertaId)
                .OrderByDescending(h => h.DataDisparo)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistoricoAlerta>> ObterPorUsuarioAsync(int usuarioId)
        {
            return await _context.HistoricoAlertas
                .Include(h => h.Alerta)
                .Where(h => h.Alerta.UsuarioId == usuarioId)
                .OrderByDescending(h => h.DataDisparo)
                .ToListAsync();
        }

        public async Task AdicionarAsync(HistoricoAlerta historico)
        {
            await _context.HistoricoAlertas.AddAsync(historico);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarHistoricoAsync(HistoricoAlerta historico)
        {
            _context.HistoricoAlertas.Update(historico);
            await _context.SaveChangesAsync();
        }
    }
}