using EcommerceApi.Enums;
namespace EcommerceApi.DTOs;

// atualiza os status do pedido
public class AtualizarPedidoDto
{
    public string Comprador {get; set;} = string.Empty;
    public StatusPedido Status {get; set;}
}