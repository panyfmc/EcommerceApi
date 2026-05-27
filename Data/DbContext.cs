using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
namespace EcommerceApi.Data;
// conecta o projeto ao banco de dados (sql server)
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Produto> Produtos => Set<Produto>();
}