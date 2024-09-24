using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Context;
using Company.G03.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly ApppDbContext _context;
        public GenericRepository(ApppDbContext context)
        {
              _context = context; 
        }
        public async Task<IEnumerable<T>> GetAllAsync() 
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Set<T>().ToListAsync();
            }
        }
        public async Task<T> GetAsync(int? id)
        {
           return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> AddAsync(T entity)
        {
           await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
            return await  _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
     
    }
}
