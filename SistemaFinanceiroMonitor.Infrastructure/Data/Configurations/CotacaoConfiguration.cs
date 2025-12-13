using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFinanceiroMonitor.Domain.Entities;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Configurations
{
    public class CotacaoConfiguration : IEntityTypeConfiguration<Cotacao>
    {
        public void Configure(EntityTypeBuilder<Cotacao> builder)
        {
            builder.ToTable("Cotacoes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(c => c.TipoMoeda)
                .IsRequired()
                .HasConversion<int>();

            // Mapeamento do Value Object ValorCompra
            builder.OwnsOne(c => c.ValorCompra, valor =>
            {
                valor.Property(v => v.Valor)
                    .IsRequired()
                    .HasColumnType("decimal(18,4)")
                    .HasColumnName("ValorCompra");

                valor.Property(v => v.Moeda)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("MoedaCompra");
            });

            builder.OwnsOne(c => c.ValorVenda, valor =>
            {
                valor.Property(v => v.Valor)
                    .IsRequired()
                    .HasColumnType("decimal(18,4)")
                    .HasColumnName("ValorVenda");

                valor.Property(v => v.Moeda)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("MoedaVenda");
            });

            builder.Property(c => c.DataCotacao)
                .IsRequired();

            builder.Property(c => c.DataRegistro)
                .IsRequired();

            builder.HasIndex(c => new { c.TipoMoeda, c.DataCotacao });
        }
    }
}
