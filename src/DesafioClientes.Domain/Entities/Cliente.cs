namespace DesafioClientes.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;

    public Endereco? Endereco { get; set; }
    public ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}
