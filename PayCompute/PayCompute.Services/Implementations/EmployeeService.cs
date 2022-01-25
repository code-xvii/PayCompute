using PayCompute.Entities;
using PayCompute.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == employeeId);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }
        public async Task CreateAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee newEmployee)
        {
            _context.Update(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int employeeId)
        {
            var employee = GetEmployeeById(employeeId);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int employeeId)
        {
            var employee =  GetEmployeeById(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }



        public decimal StudentLoanRepaymentAmount(int employeeId, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

        public decimal UnionFees(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
