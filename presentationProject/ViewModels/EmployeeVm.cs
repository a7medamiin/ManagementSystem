using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace presentationProject.ViewModels
{
    public class EmployeeVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }

        //[Range(1,100)]
        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
