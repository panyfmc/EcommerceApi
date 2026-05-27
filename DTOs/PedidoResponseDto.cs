using EcommerceApi.Enums;
namespace EcommerceApi.DTOs;

public class PedidoResponseDto
{
    public Guid Id {get; set;}
    public string Comprador {get; set;} = string.Empty;
    public StatusPedido Status {get; set;}
    public List<ProdutoResponseDto> Produtos {get; set;} = new();
    public decimal ValorTotal {get; set;}
    
}

