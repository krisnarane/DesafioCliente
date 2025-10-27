using DesafioClientes.Domain.Entities;

namespace DesafioClientes.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObterPorIdAsync(int id);
    Task<IEnumerable<Cliente>> ObterTodosAsync(int pagina, int tamanhoPagina);
    Task<int> ObterTotalAsync();
    Task<IEnumerable<Cliente>> PesquisarPorNomeAsync(string nome);
    Task<Cliente> AdicionarAsync(Cliente cliente);
    Task<Cliente> AtualizarAsync(Cliente cliente);
    Task<bool> ExcluirAsync(int id);
    Task<bool> ExisteAsync(int id);
}
