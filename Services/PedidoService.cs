using EcommerceApi.Data;
using EcommerceApi.DTOs;
using EcommerceApi.Enums;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services;

public class PedidoService
{
    private readonly AppDbContext _context;
    public PedidoService(AppDbContext context)
    {
        _context = context;
    }  

    public async Task<List<Pedido>> ListarAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Produtos)
            .ToListAsync();
    }
    public async Task<Pedido?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Pedidos
            .Include(p => p.Produtos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Pedido> CriarAsync(CriarPedidoDto dto)
    {
        if (dto.ProdutoId.Count == 0) throw new Exception("Pedido deve possuir ao menos um produto.");

        var produtos = await _context.Produtos
            .Where(p => dto.ProdutoId.Contains(p.Id))
            .ToListAsync();

        var pedido = new Pedido
        {
            Id = Guid.NewGuid(),
            Comprador = dto.Comprador,
            Status = StatusPedido.Iniciado,
            Produtos = produtos
        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        
        return pedido;
    }

    // Atualiza apenas o status do pedido
    public async Task<Pedido?> AtualizarStatusAsync(Guid id, AtualizarPedidoDto dto)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido is null) return null;
        var RegrasTransicao = pedido.Status switch
        {
            StatusPedido.Pendente => dto.Status is StatusPedido.Iniciado or StatusPedido.Cancelado,
            StatusPedido.Iniciado => dto.Status is StatusPedido.Processado or StatusPedido.Cancelado,
            StatusPedido.Processado => dto.Status is StatusPedido.Enviado or StatusPedido.Cancelado,
            StatusPedido.Enviado => false,
            StatusPedido.Cancelado => false,
            _ => false

        };
        if (!RegrasTransicao) throw new Exception($"Não é possível alterar o status de '{pedido.Status}' para '{dto.Status}'.");

        await _context.SaveChangesAsync();

        return pedido;
    }
}