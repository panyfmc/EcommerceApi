using EcommerceApi.Data;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
namespace EcommerceApi.Services;

public class ProdutoService
{
    private readonly AppDbContext _produto;
    public ProdutoService(AppDbContext produto)
    {
        _produto = produto;
    }

    // funcao para filtrar o produto pelo nome
    public async Task<Produto?> BuscarPorNome(string nome)
    {
        return await _produto.Produtos .FirstOrDefaultAsync(p => p.Nome == nome);
    }

    // funcao para listar todos os produtos
    public async Task<List<Produto>> ListarAsync()
    {
        return await _produto.Produtos.ToListAsync();
    }
    
    // funcao para criar um novo produto
    public async Task<Produto> CriarAsync(Produto produto)
    {
        if(produto.Valor <= 0) throw new Exception("O valor precisa ser maior que zero");
        produto.Id = Guid.NewGuid();
        _produto.Produtos.Add(produto);
        await _produto.SaveChangesAsync();
        return produto;
    }

    // funcao para editar o valor de um produto pelo nome
    public async Task<ProdutoResponseDto?> EditarProdutoAsync(string nome, decimal valor)
    {
        var produto = await _produto.Produtos.FirstOrDefaultAsync(p => p.Nome == nome);
        if (produto is null) return null;

        if (valor <= 0) throw new Exception("O valor precisa ser maior que zero");

        produto.Valor = valor;
        await _produto.SaveChangesAsync();

        return new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }

    // funcao para deletar um produto pelo nome
    public async Task<bool> DeletarAsync(string nome) 
    {
        var produto = await _produto.Produtos
            .FirstOrDefaultAsync(p => p.Nome == nome);
        if (produto is null) return false;
        _produto.Produtos.Remove(produto);
        await _produto.SaveChangesAsync();

        return true;
    }
    
}