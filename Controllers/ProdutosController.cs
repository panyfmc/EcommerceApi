using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;
    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _produtoService.ListarAsync();

        return Ok(produtos);
    }

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
}