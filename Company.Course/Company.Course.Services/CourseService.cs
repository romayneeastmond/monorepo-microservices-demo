using Company.Course.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Course.Services
{
    public class CourseService : ICourseService
    {
        private readonly CourseDbContext _db;

        public CourseService(CourseDbContext db)
        {
            _db = db;
        }

        public async Task Delete(Guid id)
        {
            var course = await _db.Courses.FindAsync(id);

            if (course == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            _db.Courses.Remove(course);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Models.Course>> Get()
        {
            return await _db.Courses.ToListAsync();
        }

        public async Task<Models.Course> Get(Guid id)
        {
            return await _db.Courses.FindAsync(id);
        }

        public async Task<Models.Course> Insert(Models.Course course)
        {
            _db.Courses.Add(course);

            await _db.SaveChangesAsync();

            return course;
        }

        public async Task Update(Guid id, Models.Course course)
        {
            var courseItem = await _db.Courses.FindAsync(id);

            if (courseItem == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            courseItem.Name = course.Name;
            courseItem.Description = course.Description;

            await _db.SaveChangesAsync();
        }

        public async Task Rebuild()
        {
            var courses = await _db.Courses.ToListAsync();

            _db.Courses.RemoveRange(courses);

            await _db.SaveChangesAsync();

            CourseInitalizer.Initialize(_db);
        }
    }
}
