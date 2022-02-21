using Microsoft.EntityFrameworkCore;

namespace Company.Course.Models
{
    public class CourseDbContext : DbContext, ICourseDbContext
    {
        public CourseDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; } = null!;
    }
}
