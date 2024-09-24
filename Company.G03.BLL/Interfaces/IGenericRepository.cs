using Company.G03.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetAsync(int? id);

        public Task<int> AddAsync(T entity);

        public Task<int> UpdateAsync(T entity);

        public Task<int> DeleteAsync(T entity);


    }
}
