using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace presentationProject.ViewModels
{
    public class DepartmentVm
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int Code { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
