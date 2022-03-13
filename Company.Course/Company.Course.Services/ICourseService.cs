namespace Company.Course.Services
{
    public interface ICourseService
    {
        Task<List<Models.Course>> Get();

        Task<Models.Course> Get(Guid id);

        Task<Models.Course> Insert(Models.Course course);

        Task Update(Guid id, Models.Course course);

        Task Delete(Guid id);

        Task Rebuild();
    }
}
