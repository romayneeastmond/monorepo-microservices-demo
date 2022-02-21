namespace Company.Employee.Services
{
    public interface IEmployeeService
    {
        Task<List<Models.Employee>> Get();

        Task<Models.Employee> Get(string id);

        Task<Models.Employee> GetByEmailAddress(string emailAddress);

        Task<Models.Employee> Insert(Models.Employee employee);

        Task Update(string id, Models.Employee employee);

        Task Delete(string id);
    }
}
