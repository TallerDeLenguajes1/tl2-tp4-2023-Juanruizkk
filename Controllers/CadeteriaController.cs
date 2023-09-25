using EspacioCadeteria;
using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_Juanruizkk.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cad;
    private readonly ILogger<CadeteriaController> _logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cad = Cadeteria.Instance;
    }

    [HttpGet]
    [Route("Pedidos")]
    public IActionResult GetPedidos()
    {
        List<Pedido> listadoPedidos = cad.getListadoPedido();
        return Ok(listadoPedidos);
        /* return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray(); */
    }

    [HttpGet]
    [Route("Cadetes")]

    public IActionResult GetCadetes()
    {
        List<Cadete> listadoCadetes = cad.getListadoCadete();
        return Ok(listadoCadetes);
    }

    [HttpGet("get-Informe", Name = "obtenerInforme")]
    public IActionResult GetInforme()
    {
        Informe nuevoInforme = cad.GenerarInforme();
        return Ok(nuevoInforme);
    }

    [HttpPost(Name = "AgregarPedido")]
    public IActionResult AddPedido(int numero, string obs, Estado estado, string nombreCliente, string direccionCliente, double telefonoCliente, string observacionCliente)
    {
        cad.CrearPedido(numero, obs, estado, nombreCliente, direccionCliente, telefonoCliente, observacionCliente);
        var p = cad.getListadoPedido().FirstOrDefault(p => p.Numero == numero);
        if (p != null)
        {
            return Ok(p);
        }
        else
        {
            return StatusCode(500, "No se pudo cargar el pedido");
        }
    }
    [HttpPut("asignar-pedido", Name = "AsignarPedido")]
    public IActionResult AsignarPedido(int idCadete, int numPedido)
    {
        try
        {
            cad.AsignarCadeteAPedido(idCadete, numPedido);
            return Ok(); // Devuelve un OkResult si la asignación se realiza con éxito
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Devuelve un BadRequest con un mensaje de error si hay algún problema
        }
    }

    [HttpPut("cambiar-estado-pedido", Name = "CambiarEstadoPedido")]
    public IActionResult CambiarEstadoPedido(int numPedido, Estado estado)
    {
        try
        {
            cad.CambiarEstadoPedido(numPedido, estado);
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("cambiar-cadete-pedido", Name = "CambiarCadetePedido")]
    public IActionResult CambiarCadetePedido(int numPedido, int idCad)
    {
        try
        {
            cad.reasignarPedido(numPedido, idCad);
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

}
