using Microsoft.EntityFrameworkCore;
using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Infrastructure.Data.Configurations;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cotacao> Cotacoes { get; set; }
        public DbSet<IndicadorEconomico> Indicadores { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<HistoricoAlerta> HistoricoAlertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new CotacaoConfiguration());
            modelBuilder.ApplyConfiguration(new IndicadorConfiguration());
            modelBuilder.ApplyConfiguration(new AlertaConfiguration());
            modelBuilder.ApplyConfiguration(new HistoricoAlertaConfiguration());
        }
    }
}
