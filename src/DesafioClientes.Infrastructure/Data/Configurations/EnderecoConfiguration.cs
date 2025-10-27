using DesafioClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioClientes.Infrastructure.Data.Configurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Cep)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.Logradouro)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Complemento)
            .HasMaxLength(200);
    }
}
