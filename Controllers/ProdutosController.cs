using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;    

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    // lista todos os produtos registrados
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _produtoService.ListarAsync();

        return Ok(produtos);
    }

    // busca um produto pelo nome
    [HttpGet("{nome}")]
    public async Task<IActionResult> BuscarPornome(string nome)
    {
        var produto = await _produtoService.BuscarPorNome(nome);
        if (produto == null) return NotFound("produto não encontrado.");
        return Ok(produto); 
    }

    // adiciona um novo produto
    [HttpPost]
    public async Task<IActionResult> Criar(Produto produto)
    {
        try
        {
            var novoProduto = await _produtoService.CriarAsync(produto);

            return CreatedAtAction(
                nameof(Listar),
                new {id = novoProduto.Id},
                novoProduto
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // atualizar o valor do produto
    [HttpPatch("{nome}/valor")] 
    public async Task<IActionResult> EditarProduto(string nome, [FromBody] decimal valor)
    {
        try
        {
            var produto = await _produtoService.EditarProdutoAsync(nome, valor);
            if (produto is null) return NotFound("Produto não encontrado.");
            return Ok(produto);    
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // delete do produto
    [HttpDelete("{nome}")]
    public async Task<IActionResult> Deletar(string nome)
    {
        var deletado = await _produtoService.DeletarAsync(nome);
        if (!deletado) return NotFound("Produto não encontrado.");
        return NoContent(); 
    }
}