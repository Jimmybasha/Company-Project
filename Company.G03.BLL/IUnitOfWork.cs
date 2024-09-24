using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository  EmployeeRepository { get;}
        public IDepartmentRepository  DepartmentRepository { get;}

    }
}
