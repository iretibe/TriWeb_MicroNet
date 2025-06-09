using MicroNet.Employee.Core.Repositories;
using MicroNet.Employee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Employee.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Core.Entities.Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Core.Entities.Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string employeeNumber)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeNumber == employeeNumber);
        }

        public async Task<List<Core.Entities.Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Core.Entities.Employee> GetByIdAsync(Guid id)
        {
            var list = await _context.Employees.FindAsync(id);

            return list!;
            //return list ?? throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }

        public async Task UpdateAsync(Core.Entities.Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
