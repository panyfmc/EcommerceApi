using EcommerceApi.DTOs;
using EcommerceApi.Services;
using EcommerceApi.Converters;
using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Enums;
namespace EcommerceApi.Controllers;

[ApiController] // definido como 'pedidos'
[Route("api/pedidos")]  // api/pedidos
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;
    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    // busca completa de todos os pedidos
    [HttpGet]   
    public async Task<IActionResult> Listar()
    {
        var pedidos = await _pedidoService.ListarAsync();
        return Ok(pedidos); // retorna status HTTP OK
    }

    // busca um pedido pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var pedido = await _pedidoService.BuscarPorIdAsync(id);
        if (pedido == null) return NotFound("Pedido não encontrado.");
        return Ok(pedido); 
    }

    // criar um novo pedido
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

    // atualizar comprador e/ou produtos do pedido
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, AtualizarPedidoDto dto)
    {
        try
        {
            var pedido = await _pedidoService.AtualizarAsync(id, dto);
            if (pedido is null) return NotFound("Pedido não encontrado");
            return Ok(pedido);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // patch status do pedido
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(Guid id, AtualizarStatusDto dto)
    {

        if (!ModelState.IsValid)
            return BadRequest($"Status inválido. Valores permitidos: {string.Join(", ", Enum.GetNames<StatusPedido>())}");

        try
        {
            var pedido = await _pedidoService.AtualizarStatusAsync(id, dto);
            if (pedido is null) return NotFound("Pedido não encontrado");
            return Ok(pedido);    
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // delete pedido
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var deletado = await _pedidoService.DeletarAsync(id);
        if (!deletado) return NotFound("Pedido não encontrado.");
        return NoContent(); 
    }
}