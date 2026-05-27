using EcommerceApi.DTOs;
namespace EcommerceApi.DTOs;

public class AtualizarPedidoDto
{
    public string? Comprador {get; set;}      
    public List<PedidoItemDto>? Itens {get; set;}
}