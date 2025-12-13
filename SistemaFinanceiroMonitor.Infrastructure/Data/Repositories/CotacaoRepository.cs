using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Repositories
{
    public class CotacaoRepository : ICotacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public CotacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cotacao> ObterPorIdAsync(int id)
        {
            return await _context.Cotacoes.FindAsync(id);
        }

        public async Task<Cotacao> ObterUltimaCotacaoAsync(TipoMoeda tipoMoeda)
        {
            return await _context.Cotacoes
                .Where(c => c.TipoMoeda == tipoMoeda)
                .OrderByDescending(c => c.DataCotacao)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cotacao>> ObterCotacoesPorPeriodoAsync(TipoMoeda tipoMoeda, DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Cotacoes
                .Where(c => c.TipoMoeda == tipoMoeda
                    && c.DataCotacao >= dataInicio
                    && c.DataCotacao <= dataFim)
                .OrderBy(c => c.DataCotacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cotacao>> ObterTodasAsync()
        {
            return await _context.Cotacoes
                .OrderByDescending(c => c.DataCotacao)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Cotacao cotacao)
        {
            await _context.Cotacoes.AddAsync(cotacao);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarVariasAsync(IEnumerable<Cotacao> cotacoes)
        {
            await _context.Cotacoes.AddRangeAsync(cotacoes);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cotacao cotacao)
        {
            _context.Cotacoes.Update(cotacao);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteCotacaoAsync(TipoMoeda tipoMoeda, DateTime dataCotacao)
        {
            return await _context.Cotacoes
                .AnyAsync(c => c.TipoMoeda == tipoMoeda
                    && c.DataCotacao.Date == dataCotacao.Date);
        }
    }
}
