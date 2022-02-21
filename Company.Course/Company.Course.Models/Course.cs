using System.ComponentModel.DataAnnotations;

namespace Company.Course.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; } = new Guid()!;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
