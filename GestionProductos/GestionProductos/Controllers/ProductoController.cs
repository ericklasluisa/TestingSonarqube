using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly AppDBContext _appDBContext;
        private string? password;

        public ProductoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return Ok(await _appDBContext.Productos.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducts(Producto producto)
        {
            if (producto.precio < 0) return BadRequest("El precio no puede ser negativo");
            _appDBContext.Productos.Add(producto);
            await _appDBContext.SaveChangesAsync();
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.id) return BadRequest("El ID del producto no coincide");

            _appDBContext.Entry(producto).State = EntityState.Modified;

            try
            {
                await _appDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appDBContext.Productos.Any(e => e.id == id))
                {
                    return NotFound("Producto no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _appDBContext.Productos.FindAsync(id);
            if (producto == null) return NotFound("Producto no encontrado");

            _appDBContext.Productos.Remove(producto);
            await _appDBContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
