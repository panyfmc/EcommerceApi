namespace EcommerceApi.DTOs;
// direciona os dados que serão trafegados
public class CriarPedidoDto
{
    public string Comprador {get; set;} = string.Empty;
    public List<Guid> ProdutoId {get; set;} = new();    
}