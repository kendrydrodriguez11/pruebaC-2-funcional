using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class OrdenProductosRepository : IOrdenProductosRepository
    {
        private readonly AppDbContext _context;

        public OrdenProductosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenProducto>> GetAllAsync()
        {
            return await _context.OrdenProductos
                .Include(op => op.Producto)
                .Include(op => op.Orden)
                .ToListAsync();
        }

        public async Task<OrdenProducto> GetByIdAsync(Guid id)
        {
            return await _context.OrdenProductos
                .Include(op => op.Producto)
                .Include(op => op.Orden)
                .FirstOrDefaultAsync(op => op.Id == id);
        }

        public async Task AddAsync(OrdenProducto ordenProducto)
        {
            await _context.OrdenProductos.AddAsync(ordenProducto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrdenProducto ordenProducto)
        {
            _context.OrdenProductos.Update(ordenProducto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var op = await _context.OrdenProductos.FindAsync(id);
            if (op != null)
            {
                _context.OrdenProductos.Remove(op);
                await _context.SaveChangesAsync();
            }
        }
    }
}