using DesafioClientes.Application.DTOs;

namespace DesafioClientes.Application.Interfaces;

public interface IViaCepService
{
    Task<ViaCepResponseDTO?> ObterEnderecoPorCepAsync(string cep);
}
