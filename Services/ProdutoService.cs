using EcommerceApi.Data;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services;

public class ProdutoService
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    // Buscar por ID
    public async Task<Produto?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Listar produtos
    public async Task<List<Produto>> ListarAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    // Criar produto
    public async Task<ProdutoResponseDto> CriarAsync(CriarProdutoDto dto)
    {
        if (dto.Valor <= 0)
            throw new Exception("O valor precisa ser maior que zero");

        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Valor = dto.Valor
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }

    // Editar produto por ID
    public async Task<ProdutoResponseDto?> EditarProdutoAsync(Guid id, decimal valor)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
            return null;

        if (valor <= 0)
            throw new Exception("O valor precisa ser maior que zero");

        produto.Valor = valor;

        await _context.SaveChangesAsync();

        return new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }

    // Deletar por ID
    public async Task<bool> DeletarAsync(Guid id)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
            return false;

        _context.Produtos.Remove(produto);

        await _context.SaveChangesAsync();

        return true;
    }
}