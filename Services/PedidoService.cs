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
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .ToListAsync();
    }

    public async Task<Pedido?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pedido> CriarAsync(CriarPedidoDto dto)
    {
        if (dto.Itens.Count == 0) throw new Exception("Pedido deve possuir ao menos um produto.");

        // busca os produtos pelos IDs 
        var produtoIds = dto.Itens.Select(i => i.ProdutoId).ToList();
        var produtos = await _context.Produtos
            .Where(p => produtoIds.Contains(p.Id))
            .ToListAsync();

        // estrutura os itens do pedido
        var itens = dto.Itens.Select(i =>
        {
            var produto = produtos.FirstOrDefault(p => p.Id == i.ProdutoId)
                ?? throw new Exception($"Produto {i.ProdutoId} não encontrado.");

            return new PedidoItem
            {
                Id = Guid.NewGuid(),
                ProdutoId = produto.Id,
                Produto = produto,
                Quantidade = i.Quantidade,
                ValorUnico = produto.Valor
            };
        }).ToList();

        var pedido = new Pedido
        {
            Id = Guid.NewGuid(),
            Comprador = dto.Comprador,
            Status = StatusPedido.Pendente,
            Itens = itens
        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        
        return pedido;
    }

    // Atualiza apenas o status do pedido
    public async Task<Pedido?> AtualizarStatusAsync(Guid id, AtualizarStatusDto dto)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

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
        pedido.Status = dto.Status;
        await _context.SaveChangesAsync();

        return pedido;
    }
}