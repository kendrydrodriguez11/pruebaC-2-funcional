using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _appDbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Categorias.FindAsync(id);
        }

        public async Task SaveAsync(Categoria categoria)
        {
            await _appDbContext.Categorias.AddAsync(categoria);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Categoria categoria)
        {
            var categoriaDb = await _appDbContext.Categorias.FindAsync(id);
            if (categoriaDb is null)
                return;

            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Descripcion = categoria.Descripcion;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var categoria = await _appDbContext.Categorias.FindAsync(id);
            if (categoria is null)
                return;

            _appDbContext.Categorias.Remove(categoria);
            await _appDbContext.SaveChangesAsync();
        }
    }
}