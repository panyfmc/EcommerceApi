using EcommerceApi.DTOs;
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

    // busca um produto pelo id
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var produto = await _produtoService.BuscarPorIdAsync(id);
        if (produto == null) return NotFound("produto não encontrado.");
        return Ok(produto); 
    }

    // adiciona um novo produto
    [HttpPost]
    public async Task<IActionResult> Criar(CriarProdutoDto dto)
    {
        try
        {
            var novoProduto = await _produtoService.CriarAsync(dto);
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoProduto.Id }, novoProduto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // atualizar o valor do produto
    [HttpPatch("{id}/valor")] 
    public async Task<IActionResult> EditarProduto(Guid id, [FromBody] decimal valor)
    {
        try
        {
            var produto = await _produtoService.EditarProdutoAsync(id, valor);
            if (produto is null) return NotFound("Produto não encontrado.");
            return Ok(produto);    
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // delete do produto
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var deletado = await _produtoService.DeletarAsync(id);
        if (!deletado) return NotFound("Produto não encontrado.");
        return NoContent(); 
    }
}