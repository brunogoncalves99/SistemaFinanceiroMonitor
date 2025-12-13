using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFinanceiroMonitor.Domain.Entities;

namespace SistemaFinanceiroMonitor.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .UseIdentityColumn();

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Email");

                email.HasIndex(e => e.Endereco)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuarios_Email");
            });

            builder.Property(u => u.DataCadastro)
                .IsRequired();

            builder.Property(u => u.Ativo)
                .IsRequired();

            builder.HasMany(u => u.Alertas)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
