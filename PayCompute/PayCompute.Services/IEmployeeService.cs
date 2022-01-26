using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayCompute.Services
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
        Task CreateAsync(Employee newEmployee);
        Task UpdateAsync(Employee newEmployee);
        Task UpdateAsync(int employeeId);
        Task DeleteAsync(int employeeId);
        decimal UnionFees(int employeeId);
        decimal StudentLoanRepaymentAmount(int employeeId, decimal totalAmount);

    }
}
