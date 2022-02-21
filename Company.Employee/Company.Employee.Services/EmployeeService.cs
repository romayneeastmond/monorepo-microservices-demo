namespace Company.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Models.Employee>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employee> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employee> GetByEmailAddress(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employee> Insert(Models.Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, Models.Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
