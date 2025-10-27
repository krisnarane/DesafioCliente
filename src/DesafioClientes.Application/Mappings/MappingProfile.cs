using AutoMapper;
using DesafioClientes.Application.DTOs;
using DesafioClientes.Domain.Entities;

namespace DesafioClientes.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Contato, ContatoDTO>().ReverseMap();
        CreateMap<Endereco, EnderecoDTO>().ReverseMap();
        CreateMap<CriarClienteDTO, Cliente>();
        CreateMap<AtualizarClienteDTO, Cliente>();
    }
}
