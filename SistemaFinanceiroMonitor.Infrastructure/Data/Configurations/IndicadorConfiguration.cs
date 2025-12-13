using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFinanceiroMonitor.Domain.Entities;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Configurations
{
    public class IndicadorConfiguration : IEntityTypeConfiguration<IndicadorEconomico>
    {
        public void Configure(EntityTypeBuilder<IndicadorEconomico> builder)
        {
            builder.ToTable("Indicadores");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .UseIdentityColumn();

            builder.Property(i => i.TipoIndicador)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(i => i.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(i => i.DataReferencia)
                .IsRequired();

            builder.Property(i => i.DataRegistro)
                .IsRequired();

            builder.HasIndex(i => new { i.TipoIndicador, i.DataReferencia });
        }
    }
}
