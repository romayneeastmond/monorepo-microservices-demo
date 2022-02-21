using System.ComponentModel.DataAnnotations;

namespace Company.Employee.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; } = new Guid()!;

        public Guid DepartmentId { get; set; } = new Guid()!;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]        
        public string EmailAddress { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
