using DesafioClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioClientes.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.DataCadastro)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(c => c.Endereco)
            .WithOne(e => e.Cliente)
            .HasForeignKey<Endereco>(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Contatos)
            .WithOne(ct => ct.Cliente)
            .HasForeignKey(ct => ct.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
