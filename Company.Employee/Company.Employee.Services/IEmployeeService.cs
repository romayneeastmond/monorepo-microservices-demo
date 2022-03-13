namespace Company.Employee.Services
{
    public interface IEmployeeService
    {
        Task<List<Models.Employee>> Get();

        Task<Models.Employee> Get(Guid id);

        Task<Models.Employee> GetByEmailAddress(string emailAddress);

        Task<Models.Employee> Insert(Models.Employee employee);

        Task Update(Guid id, Models.Employee employee);

        Task Delete(Guid id);

        Task Rebuild();
    }
}
