using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.G03.DAL.Model
{
    public class Employee :BaseEntity
    { 
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string? ImageName { get; set; }

        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor {  get; set; }

    }
}
