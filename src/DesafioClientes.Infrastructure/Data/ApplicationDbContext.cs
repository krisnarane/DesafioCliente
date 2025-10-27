using DesafioClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioClientes.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
