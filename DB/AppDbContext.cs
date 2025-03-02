using Microsoft.EntityFrameworkCore;
using Proyecto_CRUD.Models;

namespace Proyecto_CRUD.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Alumnos>Alumnos { get; set; }
    }
}
