using AutoMapper;
using Company.G03.DAL.Model;
using Company.G03.PL.ViewModels;

namespace Company.G03.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

        }
    }
}
