using EcommerceApi.DTOs;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceApi.Controllers;

[ApiController] // definido como 'pedidos'
[Route("api/[controller]")]  // api/pedidos
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;
    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]   
    public async Task<IActionResult> Listar()
    {
        var pedidos = await _pedidoService.ListarAsync();
        return Ok(pedidos); // retorna status HTTP OK
    }

    [HttpGet("{id}")]
    // guarda o id da rota 
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var pedido = await _pedidoService.BuscarPorIdAsync(id);
        if (pedido == null) return NotFound("Pedido não encontrado.");
        return Ok(pedido); 
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarPedidoDto dto)
    {
        try
        {
            var pedido = await _pedidoService.CriarAsync(dto);
            return CreatedAtAction(     // retorna status http Created
                nameof(BuscarPorId),
                new {id = pedido.Id},
                pedido
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);  // retorna status HTTP Bad Request
        }
    }


    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(Guid id, AtualizarStatusDto dto)
    {
        var pedido = await _pedidoService.AtualizarStatusAsync(id, dto);
        if (pedido is null) return NotFound();
        return Ok(pedido);
    }
}