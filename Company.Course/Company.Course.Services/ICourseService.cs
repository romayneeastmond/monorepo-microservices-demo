namespace Company.Course.Services
{
    public interface ICourseService
    {
        Task<List<Models.Course>> Get();

        Task<Models.Course> Get(string id);

        Task<Models.Course> Insert(Models.Course course);

        Task Update(string id, Models.Course course);

        Task Delete(string id);
    }
}
