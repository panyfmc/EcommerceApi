namespace EcommerceApi.DTOs;

public class PedidoResponseDto
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; } // Adicionado conforme sua regra de ID único de usuário
    public string Comprador { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<PedidoItemResponseDto> Itens { get; set; } = new();
    public decimal ValorTotal => Itens.Sum(i => i.ValorTotal);
}

public class PedidoItemResponseDto
{
    public Guid ProdutoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal => Quantidade * ValorUnitario;
}