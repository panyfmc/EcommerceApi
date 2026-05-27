using EcommerceApi.Enums;
namespace EcommerceApi.Models;

// estrutura do pedido
public class Pedido
{
    public Guid Id {get; set;}
    public string Comprador {get; set;} = string.Empty;
    public StatusPedido Status {get; set;}
    public List<Produto> Produtos {get; set;} = new();
}