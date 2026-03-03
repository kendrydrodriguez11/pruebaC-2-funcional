using WebApplication1.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repository
{
    public interface IOrdenRepository
    {
        Task<List<Orden>> GetAllAsync();
        Task<Orden> GetByIdAsync(Guid id);
        Task AddAsync(Orden orden);
        Task UpdateAsync(Orden orden);
        Task DeleteAsync(Guid id);
    }
}