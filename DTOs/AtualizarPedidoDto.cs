using EcommerceApi.Enums;
namespace EcommerceApi.DTOs;

// atualiza os status do pedido
public class AtualizarPedidoDto
{
    public StatusPedido Status {get; set;}
}