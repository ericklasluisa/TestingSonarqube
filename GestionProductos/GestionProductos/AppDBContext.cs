using Microsoft.EntityFrameworkCore;

namespace GestionProductos
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}
        public DbSet<Producto> Productos { get; set; }
    }

    public class Producto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
    }
}
