using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync();

        Task<Producto?> GetByIdAsync(Guid id);

        Task SaveAsync(Producto producto);

        Task UpdateAsync(Guid id, Producto producto);

        Task DeleteAsync(Guid id);
    }
}