using System.ComponentModel.DataAnnotations;

namespace proyecto_2do_parcial.Models
{
    public enum EstadoProceso { Listo, Ejecucion, Bloqueado, Terminado }

        public class Proceso
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Prioridad { get; set; }
            public int TiempoRestante { get; set; }
            public EstadoProceso Estado { get; set; }

            // Constructor vacío (Obligatorio para la Base de Datos)
            public Proceso() { }

            // Constructor con 4 parámetros (El que está causando el error)
            public Proceso(int id, string nombre, int tiempo, int prioridad)
            {
                Id = id;
                Nombre = nombre;
                TiempoRestante = tiempo;
                Prioridad = prioridad;
                Estado = EstadoProceso.Listo; // Por defecto inician en "Listo"
            }
        }
    }
