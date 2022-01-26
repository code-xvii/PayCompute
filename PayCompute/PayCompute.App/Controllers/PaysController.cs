using Microsoft.AspNetCore.Mvc;
using PayCompute.App.Models;
using PayCompute.Entities;
using PayCompute.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute.App.Controllers
{
    public class PaysController : Controller
    {
        private readonly IPayComputationService _payComputationService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaxService _taxService;
        private readonly INationalInsuranceContributionServie _nicService;

        private decimal overTimeHours;
        private decimal contractualEarnings;
        private decimal overtimEarnings;
        private decimal totalEarnings;
        private decimal tax;
        private decimal unionFee;
        private decimal nic;
        private decimal studentLoan;
        private decimal totalDeduction;

        public PaysController(IPayComputationService payComputationService, IEmployeeService employeeService, ITaxService taxService, INationalInsuranceContributionServie nicService)
        {
            _payComputationService = payComputationService;
            _employeeService = employeeService;
            _taxService = taxService;
            _nicService = nicService;
        }
        public IActionResult Index()
        {
            var model = _payComputationService.GetAll().Select(p => new PaymentRecordIndexViewModel
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                Employee = p.Employee,
                FullName = p.FullName,
                PayDate = p.PayDate,
                PayMonth = p.PayMonth,
                TaxYearId = p.TaxYearId,
                Year = _payComputationService.GetTaxYearById(p.TaxYearId).YearOfTax,
                TotalEarnings = p.TotalEarnings,
                TotalDeduction = p.TotalDeductions,
                NetPayment = p.NetPayment,
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            var model = new PaymentRecordCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payRecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeService.GetEmployeeById(model.EmployeeId).FullName,
                    NiNo = _employeeService.GetEmployeeById(model.EmployeeId).NationalInsuranceNo,
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OvertimeHours = overTimeHours = _payComputationService.OverTimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contractualEarnings = _payComputationService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings = overtimEarnings = _payComputationService.OvertimeEarnings(_payComputationService.OvertimeRate(model.HourlyRate),overTimeHours),
                    TotalEarnings = totalEarnings =_payComputationService.TotalEarnings(overtimEarnings,contractualEarnings),
                    Tax = tax = _taxService.TaxAmount(totalEarnings),
                    UnionFee = unionFee =_employeeService.UnionFees(model.EmployeeId),
                    SLC = studentLoan= _employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings),
                    NIC = nic =_nicService.NIContribution(totalEarnings),
                    TotalDeductions = totalDeduction = _payComputationService.TotalDeduction(tax,nic,studentLoan,unionFee),
                    NetPayment = _payComputationService.NetPayment(totalEarnings,totalDeduction)

                };

                await _payComputationService.CreateAsync(payRecord);
                return RedirectToAction(nameof(Index));

            }

            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var paymentRecord = _payComputationService.GetById(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }

            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                Employee = paymentRecord.Employee,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payComputationService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxYear = paymentRecord.TaxYear,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeHours = paymentRecord.OvertimeHours,
                OvertimeRate = _payComputationService.OvertimeRate(paymentRecord.HourlyRate),
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                NIC = paymentRecord.NIC,
                UnionFee = paymentRecord.UnionFee,
                SLC = paymentRecord.SLC,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeductions = paymentRecord.TotalDeductions,
                NetPayment = paymentRecord.NetPayment,
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult PaySlip(int id)
        {
            var paymentRecord = _payComputationService.GetById(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }

            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                Employee = paymentRecord.Employee,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payComputationService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxYear = paymentRecord.TaxYear,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeHours = paymentRecord.OvertimeHours,
                OvertimeRate = _payComputationService.OvertimeRate(paymentRecord.HourlyRate),
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                NIC = paymentRecord.NIC,
                UnionFee = paymentRecord.UnionFee,
                SLC = paymentRecord.SLC,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeductions = paymentRecord.TotalDeductions,
                NetPayment = paymentRecord.NetPayment,
            };
            return View(model);
        }
    }
}
