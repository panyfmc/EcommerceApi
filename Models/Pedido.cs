using EcommerceApi.Enums;
namespace EcommerceApi.Models;

// estrutura do pedido
public class Pedido
{
    public Guid Id {get; set;}
    public Guid UsuarioId {get; set;}
    public string Comprador {get; set;} = string.Empty;
    public StatusPedido Status {get; set;}
    public List<PedidoItem> Itens {get; set;} = new();
    public decimal ValorTotal => Itens.Sum(i => i.ValorTotal);
}