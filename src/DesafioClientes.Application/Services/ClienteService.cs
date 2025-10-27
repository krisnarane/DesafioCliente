using AutoMapper;
using DesafioClientes.Application.DTOs;
using DesafioClientes.Application.Interfaces;
using DesafioClientes.Domain.Entities;
using DesafioClientes.Domain.Interfaces;

namespace DesafioClientes.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IViaCepService _viaCepService;
    private readonly IMapper _mapper;

    public ClienteService(
        IClienteRepository clienteRepository,
        IViaCepService viaCepService,
        IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _viaCepService = viaCepService;
        _mapper = mapper;
    }

    public async Task<ClienteDTO?> ObterPorIdAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);
        return cliente == null ? null : _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<PagedResultDTO<ClienteDTO>> ObterTodosAsync(int pagina, int tamanhoPagina)
    {
        var clientes = await _clienteRepository.ObterTodosAsync(pagina, tamanhoPagina);
        var total = await _clienteRepository.ObterTotalAsync();

        return new PagedResultDTO<ClienteDTO>
        {
            Items = _mapper.Map<IEnumerable<ClienteDTO>>(clientes),
            TotalItems = total,
            PageNumber = pagina,
            PageSize = tamanhoPagina
        };
    }

    public async Task<IEnumerable<ClienteDTO>> PesquisarPorNomeAsync(string nome)
    {
        var clientes = await _clienteRepository.PesquisarPorNomeAsync(nome);
        return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
    }

    public async Task<ClienteDTO> CriarAsync(CriarClienteDTO dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        cliente.DataCadastro = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        if (dto.Endereco != null && !string.IsNullOrEmpty(dto.Endereco.Cep))
        {
            var viaCepResponse = await _viaCepService.ObterEnderecoPorCepAsync(dto.Endereco.Cep);

            if (viaCepResponse != null)
            {
                cliente.Endereco = new Endereco
                {
                    Cep = viaCepResponse.Cep,
                    Logradouro = string.IsNullOrEmpty(dto.Endereco.Logradouro)
                        ? viaCepResponse.Logradouro
                        : dto.Endereco.Logradouro,
                    Cidade = string.IsNullOrEmpty(dto.Endereco.Cidade)
                        ? viaCepResponse.Localidade
                        : dto.Endereco.Cidade,
                    Numero = dto.Endereco.Numero,
                    Complemento = string.IsNullOrEmpty(dto.Endereco.Complemento)
                        ? viaCepResponse.Complemento
                        : dto.Endereco.Complemento
                };
            }
        }

        var clienteCriado = await _clienteRepository.AdicionarAsync(cliente);
        return _mapper.Map<ClienteDTO>(clienteCriado);
    }

    public async Task<ClienteDTO?> AtualizarAsync(int id, AtualizarClienteDTO dto)
    {
        var clienteExistente = await _clienteRepository.ObterPorIdAsync(id);
        if (clienteExistente == null)
            return null;

        clienteExistente.Nome = dto.Nome;

        if (dto.Endereco != null && !string.IsNullOrEmpty(dto.Endereco.Cep))
        {
            var viaCepResponse = await _viaCepService.ObterEnderecoPorCepAsync(dto.Endereco.Cep);

            if (viaCepResponse != null)
            {
                if (clienteExistente.Endereco == null)
                {
                    clienteExistente.Endereco = new Endereco();
                }

                clienteExistente.Endereco.Cep = viaCepResponse.Cep;
                clienteExistente.Endereco.Logradouro = string.IsNullOrEmpty(dto.Endereco.Logradouro)
                    ? viaCepResponse.Logradouro
                    : dto.Endereco.Logradouro;
                clienteExistente.Endereco.Cidade = string.IsNullOrEmpty(dto.Endereco.Cidade)
                    ? viaCepResponse.Localidade
                    : dto.Endereco.Cidade;
                clienteExistente.Endereco.Numero = dto.Endereco.Numero;
                clienteExistente.Endereco.Complemento = string.IsNullOrEmpty(dto.Endereco.Complemento)
                    ? viaCepResponse.Complemento
                    : dto.Endereco.Complemento;
            }
        }

        clienteExistente.Contatos = _mapper.Map<ICollection<Contato>>(dto.Contatos);

        var clienteAtualizado = await _clienteRepository.AtualizarAsync(clienteExistente);
        return _mapper.Map<ClienteDTO>(clienteAtualizado);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        return await _clienteRepository.ExcluirAsync(id);
    }
}
