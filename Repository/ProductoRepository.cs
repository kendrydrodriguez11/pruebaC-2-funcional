using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Producto?>> GetAllAsync()
        {
            return await _appDbContext.Productos.ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(Producto producto)
        {
            await _appDbContext.Productos.AddAsync(producto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Producto producto)
        {
            var productoDb = await _appDbContext.Productos.FindAsync(id);
            if (productoDb is null)
                return;

            productoDb.Nombre = producto.Nombre;
            productoDb.Precio = producto.Precio;
            productoDb.CategoriaId = producto.CategoriaId;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var producto = await _appDbContext.Productos.FindAsync(id);
            if (producto is null)
                return;

            _appDbContext.Productos.Remove(producto);
            await _appDbContext.SaveChangesAsync();
        }
    }
}