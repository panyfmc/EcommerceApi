using EcommerceApi.Enums;
namespace EcommerceApi.DTOs;

// atualiza os status do pedido
public class AtualizarStatusDto
{
    public StatusPedido Status {get; set;}
}