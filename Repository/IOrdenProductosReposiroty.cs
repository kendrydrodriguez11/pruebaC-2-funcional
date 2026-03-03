using WebApplication1.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repository
{
    public interface IOrdenProductosRepository
    {
        Task<List<OrdenProducto>> GetAllAsync();
        Task<OrdenProducto> GetByIdAsync(Guid id);
        Task AddAsync(OrdenProducto ordenProducto);
        Task UpdateAsync(OrdenProducto ordenProducto);
        Task DeleteAsync(Guid id);
    }
}