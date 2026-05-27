namespace EcommerceApi.Models;
// criar o produto
public class Produto
{
    public Guid Id {get; set;}  // GUID (Globally Unique Identifier)
    public string Nome {get; set;} = string.Empty;
    public decimal Valor {get; set;}
}