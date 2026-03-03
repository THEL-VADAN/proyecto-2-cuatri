using proyecto_2do_parcial.Models;

namespace proyecto_2do_parcial.Services
{
    public class PlanificadorService
    {
        public List<string> Simular(string algoritmo, List<Proceso> procesos, int quantum = 2)
        {
            var log = new List<string>();

            if (algoritmo.ToLower() == "fifo")
            {
                foreach (var p in procesos.OrderBy(p => p.Id))
                {
                    p.Estado = EstadoProceso.Ejecucion;
                    log.Add($"[FIFO] Ejecutando {p.Nombre} (ID: {p.Id}) hasta terminar.");
                    p.TiempoRestante = 0;
                    p.Estado = EstadoProceso.Terminado;
                }
            }
            else if (algoritmo.ToLower() == "rr") // Round Robin
            {
                while (procesos.Any(p => p.TiempoRestante > 0))
                {
                    foreach (var p in procesos.Where(p => p.TiempoRestante > 0))
                    {
                        p.Estado = EstadoProceso.Ejecucion;
                        int ejecutado = Math.Min(p.TiempoRestante, quantum);
                        log.Add($"[RR] {p.Nombre} ejecuta {ejecutado}ms.");

                        p.TiempoRestante -= ejecutado;

                        if (p.TiempoRestante > 0)
                        {
                            p.Estado = EstadoProceso.Listo;
                            log.Add($"[RR] {p.Nombre} vuelve a 'Listo'. Resta: {p.TiempoRestante}");
                        }
                        else
                        {
                            p.Estado = EstadoProceso.Terminado;
                            log.Add($"[RR] {p.Nombre} ha TERMINADO.");
                        }
                    }
                }
            }
            return log;
        }
    }
}