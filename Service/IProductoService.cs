using WebApplication1.Dto.Producto;
using WebApplication1.Model;

namespace WebApplication1.Service
{
    public interface IProductoService
    {
        Task<List<ResponseProductoDto>> GetAllAsync();

        Task<ResponseProductoDto?> GetByIdAsync(Guid id);

        Task SaveAsync(RequestProductoDto producto);

        Task UpdateAsync(Guid id, RequestProductoDto producto);

        Task DeleteAsync(Guid id);
    }
}
