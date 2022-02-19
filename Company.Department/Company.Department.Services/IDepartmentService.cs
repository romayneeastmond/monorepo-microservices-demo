namespace Company.Department.Services
{
    public interface IDepartmentService
    {
        Task<List<Models.Department>> Get();

        Task<Models.Department> Get(string id);

        Task<Models.Department> Insert(Models.Department department);

        Task Update(string code, Models.Department department);

        Task Delete(string code);
    }
}
