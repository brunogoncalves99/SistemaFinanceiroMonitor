using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFinanceiroMonitor.Domain.Entities;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Configurations
{
    public class HistoricoAlertaConfiguration : IEntityTypeConfiguration<HistoricoAlerta>
    {
        public void Configure(EntityTypeBuilder<HistoricoAlerta> builder)
        {
            builder.ToTable("HistoricoAlertas");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                .UseIdentityColumn();

            builder.Property(h => h.AlertaId)
                .IsRequired();

            builder.Property(h => h.ValorNoMomento)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(h => h.DataDisparo)
                .IsRequired();

            builder.Property(h => h.EmailEnviado)
                .IsRequired();

            builder.Property(h => h.DataEnvioEmail);

            builder.HasIndex(h => h.AlertaId);
        }
    }
}
