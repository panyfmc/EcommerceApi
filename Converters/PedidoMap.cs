using EcommerceApi.Models;
using EcommerceApi.DTOs;

namespace EcommerceApi.Converters; // O namespace é muito importante aqui!

public static class PedidoMap
{
    public static PedidoResponseDto ToResponseDto(this Pedido pedido)
    {
        return new PedidoResponseDto
        {
            Id = pedido.Id,
            UsuarioId = pedido.UsuarioId, 
            Comprador = pedido.Comprador,
            Status = pedido.Status.ToString(),
            Itens = pedido.Itens.Select(item => new PedidoItemResponseDto
            {
                ProdutoId = item.ProdutoId,
                Nome = item.Produto?.Nome ?? "Produto não informado",
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnico
            }).ToList()
        };
    }
}