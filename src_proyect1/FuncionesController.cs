using Microsoft.AspNetCore.Mvc;

namespace DemoMinimalApi;

[ApiController]
[Route("[controller]")]
public class FuncionesController: ControllerBase
{
    // ENDPOINTS
    
    /// <summary>
    /// Endpoint para saludar al mundo.
    /// </summary>
    /// <returns>string</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Saludar()
    {
        var saludo = $"Hola, World!";
        return Ok(saludo);
    }
    
    /// <summary>
    /// Endpoint para saludar al mundo.
    /// </summary>
    /// <returns>string</returns>
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult SaludarByNombre([FromQuery] string nombre = "mundo")
    {
        var saludo = $"Hola, {nombre}!";
        return Ok(saludo);
    }
    
        /// <summary>
    // /// Endpoint para saludar al mundo.
    // /// </summary>
    // /// <returns>string</returns>
     [HttpGet("[action]")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Despedirse()
    {
        var despedida = $"Adiós, mundo!";
        return Ok(despedida);
    }
}