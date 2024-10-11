using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositroy.Interfacies;
using Talabat.Repositroy.Data;

namespace Talabat.Repositroy.Repositries
{
    public class GenaricRepositroy<T> : IGenaricInterface<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenaricRepositroy(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();

            }
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _context.Set<Product>().Where(p=>p.Id==id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            }
            return await _context.Set<T>().FindAsync(id);
        }
    }
} 
    

