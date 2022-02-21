using Microsoft.EntityFrameworkCore;

namespace Company.Course.Models
{
    public interface ICourseDbContext
    {
        DbSet<Course> Courses { get; set; }
    }
}
