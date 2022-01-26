using PayCompute.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PayCompute.App.Models
{
    public class PaymentRecordCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string FullName { get; set; }
        public string NiNo { get; set; }


        [Display(Name = "Pay Date"), DataType(DataType.Date)]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Month"), DataType(DataType.Date)]
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();

        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }

        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; } = "1250L";

        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Hourls Worked")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; } = 144m;
        public decimal OvertimeHours { get; set; }
        public decimal ContractualEarnings { get; set; }
        public decimal OvertimeEarnings { get; set; }      
        public decimal Tax { get; set; }     
        public decimal NIC { get; set; } //Social Security Contribution       
        public decimal? UnionFee { get; set; } // optional     
        public decimal? SLC { get; set; } // Student loan contribution -- Optional      
        public decimal TotalEarnings { get; set; }      
        public decimal TotalDeductions { get; set; }       
        public decimal NetPayment { get; set; }

    }
}
