using Company.G03.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
    public class EmployeeViewModel 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [Range(25, 60, ErrorMessage = "Age must be between 25 and 60")]
        public int? Age { get; set; }
        public DateTime BirthDate { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

    }
}
