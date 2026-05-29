using EcommerceApi.Data;
using EcommerceApi.DTOs;
using EcommerceApi.Enums;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
namespace EcommerceApi.Services;
using EcommerceApi.Converters;

public class PedidoService
{
    private readonly AppDbContext _context;
    public PedidoService(AppDbContext context)
    {
        _context = context;
    }       

    // exibe todos os pedidos registrados (com detalhes)
    public async Task<List<PedidoResponseDto>> ListarAsync()
    {
        var pedidos = await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .ToListAsync();

        return pedidos.Select(p => p.ToResponseDto()).ToList();
    }

    public async Task<PedidoResponseDto?> BuscarPorIdAsync(Guid id)
    {
        var pedido = await _context.Pedidos
        .Include(p => p.Itens)
        .ThenInclude(i => i.Produto)
        .FirstOrDefaultAsync(p => p.Id == id);

        return pedido?.ToResponseDto();
    }
        
        // fazer um novo pedido  
    public async Task<PedidoResponseDto> CriarAsync(CriarPedidoDto dto)
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
            UsuarioId = Guid.NewGuid(),
            Comprador = dto.Comprador,
            Status = StatusPedido.Iniciado,
            Itens = itens
        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        
        return pedido.ToResponseDto();
    }


        // atualiza comprador e/ou produtos do pedido
    public async Task<PedidoResponseDto?> AtualizarAsync(Guid id, AtualizarPedidoDto dto)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido is null) return null;

        // apenas pedidos não processados podem ser alterados
        if (pedido.Status != StatusPedido.Iniciado)
            throw new Exception($"Apenas pedidos com status 'Iniciado' podem ser alterados.");

        // atualiza comprador se informado
        if (!string.IsNullOrWhiteSpace(dto.Comprador))
            pedido.Comprador = dto.Comprador;

        // atualiza itens se informado
        if (dto.Itens is not null)
        {
            var produtoIds = dto.Itens.Select(i => i.ProdutoId).ToList();
            var produtos = await _context.Produtos
                .Where(p => produtoIds.Contains(p.Id))
                .ToListAsync();

            // 1. Remove do banco os itens antigos que NÃO vieram no DTO novo
            var itensParaRemover = pedido.Itens
                .Where(antigo => !dto.Itens.Any(novo => novo.ProdutoId == antigo.ProdutoId))
                .ToList();
                
            _context.PedidoItens.RemoveRange(itensParaRemover);

            // 2. Atualiza os itens que restaram ou adiciona os novos
            foreach (var i in dto.Itens)
            {
                var produto = produtos.FirstOrDefault(p => p.Id == i.ProdutoId)
                    ?? throw new Exception($"Produto {i.ProdutoId} não encontrado.");

                var itemExistente = pedido.Itens.FirstOrDefault(item => item.ProdutoId == i.ProdutoId);

                if (itemExistente != null)
                {
                    // Se o produto já estava no pedido, só atualizamos a quantidade e valor
                    // Mantendo o mesmo ID de item que o banco já conhece!
                    itemExistente.Quantidade = i.Quantidade;
                    itemExistente.ValorUnico = produto.Valor;
                }
                else
                {
                    // Se é um produto novo entrando no pedido, adicionamos um novo PedidoItem
                    pedido.Itens.Add(new PedidoItem
                    {
                        Id = Guid.NewGuid(),
                        PedidoId = pedido.Id,
                        ProdutoId = produto.Id,
                        Quantidade = i.Quantidade,
                        ValorUnico = produto.Valor
                    });
                }
            }
        }

        await _context.SaveChangesAsync();
        return pedido.ToResponseDto();
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
            StatusPedido.Iniciado => dto.Status is StatusPedido.Processado or StatusPedido.Cancelado,
            StatusPedido.Processado => dto.Status is StatusPedido.Enviado or StatusPedido.Cancelado,
            StatusPedido.Enviado => false,
            StatusPedido.Cancelado => false,
            _ => false

        };
            // saída para quando a regra de transição de status nao condiz com as métricas 
        if (!RegrasTransicao) throw new Exception($"Não é possível alterar o status de '{pedido.Status}' para '{dto.Status}'.");
        pedido.Status = dto.Status;
        await _context.SaveChangesAsync();

        return pedido;
    }
    // Busca o pedido, deleta e retorna bool
    public async Task<bool> DeletarAsync(Guid id) 
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido is null) return false;

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();

        return true;
    }
}