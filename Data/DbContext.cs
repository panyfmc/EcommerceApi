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
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Mapeamento Explícito de Nomes de Tabela ---
        modelBuilder.Entity<Pedido>().ToTable("Pedidos");
        modelBuilder.Entity<Produto>().ToTable("Produtos");
        modelBuilder.Entity<PedidoItem>().ToTable("PedidoItens"); 

        // corrige o aviso do decimal
        modelBuilder.Entity<Produto>()
            .Property(p => p.Valor)
            .HasPrecision(18, 2); // 18 dígitos, 2 casas decimais

        modelBuilder.Entity<PedidoItem>()
            .Property(p => p.ValorUnico)
            .HasPrecision(18, 2); 

        // Relacionamento Um-para-Muitos (Pedido -> Itens)
        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Itens)
            .WithOne(i => i.Pedido)
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}