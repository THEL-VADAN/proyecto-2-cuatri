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

        // 1. BOOT: Para la pantalla de carga inicial
        [HttpGet("boot")]
        public IActionResult BootSystem()
        {
            var existentes = _context.Procesos.ToList();
            _context.Procesos.RemoveRange(existentes);

            var arranque = new List<Proceso>
            {
                new Proceso(1, "Kernel.sys", 5, 5),
                new Proceso(2, "System32.dll", 8, 5), // Agregué este para que se vea más "pro"
                new Proceso(3, "Drivers_Video", 4, 4),
                new Proceso(4, "Explorer.exe", 6, 3)
            };

            _context.Procesos.AddRange(arranque);
            _context.SaveChanges();

            return Ok(arranque);
        }

        // 2. REGISTRAR: Para cuando den clic al icono del Navegador en el escritorio
        [HttpPost("registrar")]
        public IActionResult RegistrarProceso([FromBody] Proceso nuevoProceso)
        {
            _context.Procesos.Add(nuevoProceso);
            _context.SaveChanges();
            return Ok(new { mensaje = "App abierta y registrada", id = nuevoProceso.Id });
        }

        // 3. EJECUTAR: Para mostrar la simulación en la ventana del simulador
        [HttpGet("ejecutar/{algoritmo}")]
        public IActionResult EjecutarSimulacion(string algoritmo)
        {
            var procesos = _context.Procesos.ToList();
            if (!procesos.Any()) return BadRequest("No hay procesos para ejecutar.");

            var servicio = new PlanificadorService();
            // Pasamos el tipo (rr o fifo) y la lista de la DB
            var log = servicio.Simular(algoritmo, procesos);

            _context.SaveChanges();
            return Ok(log);
        }

        // 4. REINICIAR: El botón de "Terminar" o "Apagar"
        [HttpDelete("reiniciar")]
        public IActionResult ReiniciarSimulacion()
        {
            var todosLosProcesos = _context.Procesos.ToList();
            _context.Procesos.RemoveRange(todosLosProcesos);
            _context.SaveChanges();

            return Ok(new { mensaje = "Sistema apagado correctamente." });
        }
    }
}