using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFinanceiroMonitor.Domain.Entities;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Configurations
{
    public class AlertaConfiguration : IEntityTypeConfiguration<Alerta>
    {
        public void Configure(EntityTypeBuilder<Alerta> builder)
        {
            builder.ToTable("Alertas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .UseIdentityColumn();

            builder.Property(a => a.UsuarioId)
                .IsRequired();

            builder.Property(a => a.TipoAlerta)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(a => a.TipoMoeda)
                .HasConversion<int?>();

            builder.Property(a => a.TipoIndicador)
                .HasConversion<int?>();

            builder.Property(a => a.ValorGatilho)
                .HasColumnType("decimal(18,4)");

            builder.Property(a => a.PercentualGatilho)
                .HasColumnType("decimal(18,2)");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.DataCriacao)
                .IsRequired();

            builder.Property(a => a.DataUltimoDisparo);

            builder.HasMany(a => a.Historico)
                .WithOne(h => h.Alerta)
                .HasForeignKey(h => h.AlertaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.Status);
        }
    }
}
