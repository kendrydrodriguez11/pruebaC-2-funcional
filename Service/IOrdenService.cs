using WebApplication1.Dto.Orden;
using WebApplication1.Model;

namespace WebApplication1.Service
{
    public interface IOrdenService
    {
        Task<List<ResponseOrdenDto>> GetAllAsync();
        Task<ResponseOrdenDto> GetByIdAsync(Guid id);
        Task AddAsync(RequestOrdenDto orden);
        Task UpdateAsync(Guid Id, RequestOrdenDto orden);
        Task DeleteAsync(Guid id);
    }
}
