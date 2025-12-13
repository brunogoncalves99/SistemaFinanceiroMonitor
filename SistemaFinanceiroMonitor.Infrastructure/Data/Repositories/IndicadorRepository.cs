using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Repositories
{
    public class IndicadorRepository : IIndicadorRepository
    {
        private readonly ApplicationDbContext _context;

        public IndicadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IndicadorEconomico> ObterPorIdAsync(int id)
        {
            return await _context.Indicadores.FindAsync(id);
        }

        public async Task<IndicadorEconomico> ObterUltimoIndicadorAsync(TipoIndicador tipoIndicador)
        {
            return await _context.Indicadores
                .Where(i => i.TipoIndicador == tipoIndicador)
                .OrderByDescending(i => i.DataReferencia)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<IndicadorEconomico>> ObterIndicadoresPorPeriodoAsync(TipoIndicador tipoIndicador, DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Indicadores
                .Where(i => i.TipoIndicador == tipoIndicador
                    && i.DataReferencia >= dataInicio
                    && i.DataReferencia <= dataFim)
                .OrderBy(i => i.DataReferencia)
                .ToListAsync();
        }

        public async Task<IEnumerable<IndicadorEconomico>> ObterTodosAsync()
        {
            return await _context.Indicadores
                .OrderByDescending(i => i.DataReferencia)
                .ToListAsync();
        }

        public async Task AdicionarAsync(IndicadorEconomico indicador)
        {
            await _context.Indicadores.AddAsync(indicador);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarVariosAsync(IEnumerable<IndicadorEconomico> indicadores)
        {
            await _context.Indicadores.AddRangeAsync(indicadores);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteIndicadorAsync(TipoIndicador tipoIndicador, DateTime dataReferencia)
        {
            return await _context.Indicadores
                .AnyAsync(i => i.TipoIndicador == tipoIndicador
                    && i.DataReferencia.Date == dataReferencia.Date);
        }
    }
}