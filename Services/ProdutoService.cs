using EcommerceApi.Data;
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
    public async Task<Produto?> BuscarPorId(Guid id)
    {
        return await _context.Produtos .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<List<Produto>> ListarAsync()
    {
        return await _context.Produtos.ToListAsync();
    }
    public async Task<Produto> CriarAsync(Produto produto)
    {
        if(produto.Valor <= 0) throw new Exception("O valor precisa ser maior que zero");
        produto.Id = Guid.NewGuid();
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }
}