using Microsoft.AspNetCore.Mvc.Rendering;
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
        private decimal studentLoanAmount;
        private decimal unionFee;

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
            var employee = GetEmployeeById(employeeId);
            if (employee.StudentLoan == Enums.StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanAmount = 15m;
            }
            else if (employee.StudentLoan == Enums.StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
            {
                studentLoanAmount = 38m;
            }
            else if (employee.StudentLoan == Enums.StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                studentLoanAmount = 60m;
            }
            else if (employee.StudentLoan == Enums.StudentLoan.Yes && totalAmount >= 2500)
            {
                studentLoanAmount = 83m;
            }
            else
            {
                studentLoanAmount = 0m;
            }


            return studentLoanAmount;
        }

        public decimal UnionFees(int employeeId)
        {
            var employee = GetEmployeeById(employeeId);
            unionFee = employee.UnionMember == Enums.UnionMember.Yes ? 10m : 0m;
            return unionFee;
        }

        public IEnumerable<SelectListItem> GetAllEmployeesForPayroll()
        {
            return GetEmployees().Select(e => new SelectListItem
            {
                Text = e.FullName,
                Value = e.Id.ToString(),
            });
        }
    }
}
