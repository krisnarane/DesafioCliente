namespace DesafioClientes.Application.DTOs;

public class ClienteDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;
    public EnderecoDTO? Endereco { get; set; }
    public ICollection<ContatoDTO> Contatos { get; set; } = new List<ContatoDTO>();
}
