namespace Company.Department.Services
{
    public interface IDepartmentService
    {
        Task<List<Models.Department>> Get();

        Task<Models.Department> Get(Guid id);

        Task<Models.Department> Insert(Models.Department department);

        Task Update(Guid id, Models.Department department);

        Task Delete(Guid id);

        Task Rebuild();
    }
}
