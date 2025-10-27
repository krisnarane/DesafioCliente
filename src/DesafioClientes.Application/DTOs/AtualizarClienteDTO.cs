namespace DesafioClientes.Application.DTOs;

public class AtualizarClienteDTO
{
    public string Nome { get; set; } = string.Empty;
    public EnderecoDTO? Endereco { get; set; }
    public ICollection<ContatoDTO> Contatos { get; set; } = new List<ContatoDTO>();
}
