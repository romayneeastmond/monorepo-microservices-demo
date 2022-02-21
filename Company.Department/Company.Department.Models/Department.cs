using System.ComponentModel.DataAnnotations;

namespace Company.Department.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; } = new Guid()!;

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = null!;        
    }
}
