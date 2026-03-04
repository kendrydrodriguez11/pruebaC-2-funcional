using WebApplication1.Dto.Categoria;

namespace WebApplication1.Service
{
    public interface ICategoriaService
    {
        Task<List<ResponseCategoriaDto>> GetAllAsync();
        Task<ResponseCategoriaDto?> GetByIdAsync(Guid id);
        Task SaveAsync(RequestCategoriaDto categoria);
        Task UpdateAsync(Guid id, RequestCategoriaDto categoria);
        Task DeleteAsync(Guid id);
    }
}
