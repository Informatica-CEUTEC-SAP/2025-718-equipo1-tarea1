using Microsoft.AspNetCore.Mvc;

namespace DemoMinimalApi;

[ApiController]
[Route("api/[controller]")]
public class FuncionesController : ControllerBase
{
    // Datos en memoria (simulación de base de datos)
    private static List<Evento> eventos = new List<Evento>
    {
        new Evento
        {
            Id = 1,
            Nombre = "Concierto Rock",
            Ciudad = "Madrid",
            Categoria = "Música",
            Fecha = new DateTime(2025, 05, 10),
            Participantes = new List<Participante>
            {
                new Participante { DNI = "12345678A", Nombre = "Juan Perez", Email = "juan@example.com" }
            }
        },
        new Evento
        {
            Id = 2,
            Nombre = "Feria de Libros",
            Ciudad = "Barcelona",
            Categoria = "Cultura",
            Fecha = new DateTime(2025, 06, 15),
            Participantes = new List<Participante>()
        }
    };

    // ===========================
    // ENDPOINTS de Eventos
    // ===========================

    [HttpGet("eventos")]
    public IActionResult ObtenerEventos()
    {
        return Ok(eventos);
    }

    [HttpGet("eventos/{id}")]
    public IActionResult ObtenerEventoPorId(int id)
    {
        var evento = eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return NotFound();
        return Ok(evento);
    }

    [HttpPost("eventos")]
    public IActionResult CrearEvento([FromBody] Evento nuevoEvento)
    {
        nuevoEvento.Id = eventos.Max(e => e.Id) + 1;
        eventos.Add(nuevoEvento);
        return CreatedAtAction(nameof(ObtenerEventoPorId), new { id = nuevoEvento.Id }, nuevoEvento);
    }

    [HttpPut("eventos/{id}")]
    public IActionResult EditarEvento(int id, [FromBody] Evento eventoActualizado)
    {
        var evento = eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return NotFound();

        evento.Nombre = eventoActualizado.Nombre;
        evento.Ciudad = eventoActualizado.Ciudad;
        evento.Categoria = eventoActualizado.Categoria;
        evento.Fecha = eventoActualizado.Fecha;
        return NoContent();
    }

    [HttpDelete("eventos/{id}")]
    public IActionResult EliminarEvento(int id)
    {
        var evento = eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return NotFound();

        eventos.Remove(evento);
        return NoContent();
    }

    [HttpGet("eventos/categoria/{nombre}")]
    public IActionResult FiltrarPorCategoria(string nombre)
    {
        var eventosCategoria = eventos.Where(e => e.Categoria.Equals(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(eventosCategoria);
    }

    [HttpGet("eventos/ciudad/{nombre}")]
    public IActionResult FiltrarPorCiudad(string nombre)
    {
        var eventosCiudad = eventos.Where(e => e.Ciudad.Equals(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(eventosCiudad);
    }

    [HttpGet("eventos/fecha")]
    public IActionResult FiltrarPorFecha([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
    {
        var eventosFecha = eventos.Where(e => e.Fecha >= desde && e.Fecha <= hasta).ToList();
        return Ok(eventosFecha);
    }

    // ===========================
    // ENDPOINTS de Participantes
    // ===========================

    [HttpGet("eventos/{id}/participantes")]
    public IActionResult ObtenerParticipantes(int id)
    {
        var evento = eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return NotFound();

        return Ok(evento.Participantes);
    }

    [HttpPost("eventos/{id}/participantes")]
    public IActionResult AgregarParticipante(int id, [FromBody] Participante participante)
    {
        var evento = eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return NotFound();

        
        if (evento.Participantes.Any(p => p.DNI == participante.DNI))
        {
            return BadRequest("El DNI del participante ya está registrado en este evento.");
        }

        evento.Participantes.Add(participante);
        return Ok(participante);
    }

    [HttpDelete("eventos/{id}/participantes/{dni}")]
    public IActionResult EliminarParticipante(int id, string dni)
    {
        var evento = eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return NotFound();

        var participante = evento.Participantes.FirstOrDefault(p => p.DNI == dni);
        if (participante == null)
            return NotFound();

        evento.Participantes.Remove(participante);
        return NoContent();
    }
}

// ===========================
// MODELOS
// ===========================

public class Evento
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ciudad { get; set; }
    public string Categoria { get; set; }
    public DateTime Fecha { get; set; }
    public List<Participante> Participantes { get; set; }
}

public class Participante
{
    public string DNI { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
}
