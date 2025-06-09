namespace MicroNet.Employee.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Entities.Employee> GetByIdAsync(Guid id);
        Task<List<Entities.Employee>> GetAllAsync();
        Task AddAsync(Entities.Employee employee);
        Task UpdateAsync(Entities.Employee employee);
        Task DeleteAsync(Entities.Employee employee);
        Task<bool> ExistsAsync(string employeeNumber);
    }
}
