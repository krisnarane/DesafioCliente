using DesafioClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioClientes.Infrastructure.Data.Configurations;

public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Tipo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Texto)
            .IsRequired()
            .HasMaxLength(100);
    }
}
