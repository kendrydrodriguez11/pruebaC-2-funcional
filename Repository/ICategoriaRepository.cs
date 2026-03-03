using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetAllAsync();

        Task<Categoria?> GetByIdAsync(Guid id);

        Task SaveAsync(Categoria categoria);

        Task UpdateAsync(Guid id, Categoria categoria);

        Task DeleteAsync(Guid id);
    }
}