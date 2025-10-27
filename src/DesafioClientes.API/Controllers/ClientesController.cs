using DesafioClientes.Application.DTOs;
using DesafioClientes.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DesafioClientes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IValidator<CriarClienteDTO> _criarClienteValidator;
    private readonly IValidator<AtualizarClienteDTO> _atualizarClienteValidator;

    public ClientesController(
        IClienteService clienteService,
        IValidator<CriarClienteDTO> criarClienteValidator,
        IValidator<AtualizarClienteDTO> atualizarClienteValidator)
    {
        _clienteService = clienteService;
        _criarClienteValidator = criarClienteValidator;
        _atualizarClienteValidator = atualizarClienteValidator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDTO<ClienteDTO>>> ObterTodos(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        var resultado = await _clienteService.ObterTodosAsync(pagina, tamanhoPagina);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> ObterPorId(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);

        if (cliente == null)
            return NotFound($"Cliente com ID {id} não encontrado");

        return Ok(cliente);
    }

    [HttpGet("pesquisar")]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> Pesquisar([FromQuery] string nome)
    {
        var clientes = await _clienteService.PesquisarPorNomeAsync(nome);
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> Criar([FromBody] CriarClienteDTO dto)
    {
        var validationResult = await _criarClienteValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var cliente = await _clienteService.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteDTO>> Atualizar(int id, [FromBody] AtualizarClienteDTO dto)
    {
        var validationResult = await _atualizarClienteValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var cliente = await _clienteService.AtualizarAsync(id, dto);

        if (cliente == null)
            return NotFound($"Cliente com ID {id} não encontrado");

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        var sucesso = await _clienteService.ExcluirAsync(id);
        return NoContent();
    }
}
