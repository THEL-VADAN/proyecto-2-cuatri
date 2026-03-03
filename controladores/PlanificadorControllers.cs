using Microsoft.AspNetCore.Mvc;
using proyecto_2do_parcial.Data;
using proyecto_2do_parcial.Models;
using proyecto_2do_parcial.Services;

namespace proyecto_2do_parcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanificadorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlanificadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("simular-desde-db")]
        public IActionResult SimularDesdeDB()
        {
            // Obtenemos los procesos guardados en la tabla
            var procesos = _context.Procesos.ToList();

            var servicio = new PlanificadorService();
            var log = servicio.Simular("rr", procesos);

            // Guardamos los cambios de estado en la DB
            _context.SaveChanges();

            return Ok(log);
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarProceso([FromBody] Proceso nuevoProceso)
        {
            // 1. Agregamos el objeto a la lista que maneja Entity Framework
            _context.Procesos.Add(nuevoProceso);

            // 2. Confirmamos los cambios para que se escriban en el archivo .db
            _context.SaveChanges();

            return Ok(new { mensaje = "Proceso guardado con éxito", id = nuevoProceso.Id });
        }
    }
    }