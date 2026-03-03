using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly AppDbContext _context;

        public OrdenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orden>> GetAllAsync()
        {
            return await _context.Ordens.ToListAsync();
        }

        public async Task<Orden> GetByIdAsync(Guid id)
        {
            return await _context.Ordens.FindAsync(id);
        }

        public async Task AddAsync(Orden orden)
        {
            await _context.Ordens.AddAsync(orden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Orden orden)
        {
            _context.Ordens.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var orden = await _context.Ordens.FindAsync(id);
            if (orden != null)
            {
                _context.Ordens.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}