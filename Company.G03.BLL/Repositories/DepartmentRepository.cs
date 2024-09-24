using Company.G03.DAL.Data.Context;
using Company.G03.DAL.Interfaces;
using Company.G03.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApppDbContext context) : base(context)
        {
        }
    }
}
