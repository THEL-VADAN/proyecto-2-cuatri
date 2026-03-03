using Microsoft.EntityFrameworkCore;
using proyecto_2do_parcial.Models;

namespace proyecto_2do_parcial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Esta línea le dice a la BD que cree una tabla llamada "Procesos"
        public DbSet<Proceso> Procesos { get; set; }
    }
}