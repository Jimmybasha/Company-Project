using Company.G03.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        //    public IEnumerable<Employee> GetAll();

        //    public Employee Get(int? id);

        //    int Add(Employee entity);

        //    public int Update(Employee entity);

        //    public int Delete(Employee entity);

        Task<IEnumerable<Employee>> GetByNameAsync(string name);

    }
}
