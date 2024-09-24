using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Data.Context;
using Company.G03.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApppDbContext context;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDepartmentRepository departmentRepository;
        public UnitOfWork(ApppDbContext context)
        {
            this.context = context;
            employeeRepository = new EmployeeRepository(context);
            departmentRepository = new DepartmentRepository(context);

        }
        public IEmployeeRepository EmployeeRepository => employeeRepository;

        public IDepartmentRepository DepartmentRepository => departmentRepository;
    }
}
