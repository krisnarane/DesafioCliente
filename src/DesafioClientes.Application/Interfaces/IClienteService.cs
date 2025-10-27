using DesafioClientes.Application.DTOs;

namespace DesafioClientes.Application.Interfaces;

public interface IClienteService
{
    Task<ClienteDTO?> ObterPorIdAsync(int id);
    Task<PagedResultDTO<ClienteDTO>> ObterTodosAsync(int pagina, int tamanhoPagina);
    Task<IEnumerable<ClienteDTO>> PesquisarPorNomeAsync(string nome);
    Task<ClienteDTO> CriarAsync(CriarClienteDTO dto);
    Task<ClienteDTO?> AtualizarAsync(int id, AtualizarClienteDTO dto);
    Task<bool> ExcluirAsync(int id);
}
