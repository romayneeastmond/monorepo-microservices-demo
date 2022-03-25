namespace Company.Employee.Services
{
    public interface IEmployeeService
    {
        Task<List<Models.Employee>> Get();

        Task<Models.Employee> Get(Guid id);

        Task<List<Models.Employee>> GetByDepartment(Guid departmentId);

        Task<Models.Employee> GetByEmailAddress(string emailAddress);

        Task<List<Models.Employee>> GetByStatus(bool isActive);

        Task<Models.Employee> Insert(Models.Employee employee);

        Task Update(Guid id, Models.Employee employee);

        Task Delete(Guid id);

        Task Rebuild();
    }
}
