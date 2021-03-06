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
    public class PayComputaionService : IPayComputationService
    {
        private readonly ApplicationDbContext _context;
        private decimal contractualEarnings;
        private decimal overTimeHours;

        public PayComputaionService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }
            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() 
            => _context.PaymentRecords.OrderBy(p => p.EmployeeId);

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYears = _context.TaxYears.Select(t => new SelectListItem
            {
                Text = t.YearOfTax,
                Value = t.Id.ToString()
            });

            return allTaxYears;
        }

        public PaymentRecord GetById(int id) 
            => _context.PaymentRecords.FirstOrDefault(p => p.Id == id);

        public decimal NetPayment(decimal totalEarnings, decimal totalDeduction) 
            => totalEarnings - totalDeduction;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) 
            => overtimeHours * overtimeRate;

        public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
            {
                overTimeHours = 0.00m;
            }
            else
            {
                overTimeHours = hoursWorked - contractualHours;
            }

            return overTimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) 
            => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees) 
            => tax + nic + studentLoanRepayment + unionFees;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings) 
            => overtimeEarnings + contractualEarnings;

        public TaxYear GetTaxYearById(int id) => _context.TaxYears.FirstOrDefault(ty => ty.Id == id);
    }
}
