using System.ComponentModel.DataAnnotations;

namespace proyecto_2do_parcial.Models
{
    public enum EstadoProceso { Listo, Ejecucion, Bloqueado, Terminado }

    public class Proceso
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int TiempoRestante { get; set; }
        public EstadoProceso Estado { get; set; }
        public Proceso() { }

    }
}