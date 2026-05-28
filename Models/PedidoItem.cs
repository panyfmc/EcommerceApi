namespace EcommerceApi.Models;

public class PedidoItem
{
    public Guid Id {get; set;}
    public Guid PedidoId {get; set;}
    public Pedido Pedido {get; set;} = null!;
    public Guid ProdutoId {get; set;}
    public Produto Produto {get; set;} = null!;
    public int Quantidade {get; set;}
    public decimal ValorUnico {get; set;}
    public decimal ValorTotal => Quantidade * ValorUnico; 
}