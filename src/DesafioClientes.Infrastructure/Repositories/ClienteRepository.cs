using DesafioClientes.Domain.Entities;
using DesafioClientes.Domain.Interfaces;
using DesafioClientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioClientes.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> ObterPorIdAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync(int pagina, int tamanhoPagina)
    {
        return await _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .OrderBy(c => c.Id)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }

    public async Task<int> ObterTotalAsync()
    {
        return await _context.Clientes.CountAsync();
    }

    public async Task<IEnumerable<Cliente>> PesquisarPorNomeAsync(string nome)
    {
        return await _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .Where(c => c.Nome.Contains(nome))
            .ToListAsync();
    }

    public async Task<Cliente> AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> AtualizarAsync(Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;

        if (cliente.Endereco != null)
        {
            if (cliente.Endereco.Id == 0)
            {
                _context.Entry(cliente.Endereco).State = EntityState.Added;
            }
            else
            {
                _context.Entry(cliente.Endereco).State = EntityState.Modified;
            }
        }

        var contatosExistentes = await _context.Contatos
            .Where(c => c.ClienteId == cliente.Id)
            .ToListAsync();

        _context.Contatos.RemoveRange(contatosExistentes);

        if (cliente.Contatos != null && cliente.Contatos.Any())
        {
            foreach (var contato in cliente.Contatos)
            {
                contato.ClienteId = cliente.Id;
                await _context.Contatos.AddAsync(contato);
            }
        }

        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == id);
    }
}
