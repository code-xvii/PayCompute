using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayCompute.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeNo { get; set; }

        [Required,MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }


        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }


        [Required, MaxLength(150)]
        public string Address { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(10)]
        public string PostalCode { get; set; }

        public DateTime DateJoined { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }


        [Required, MaxLength(50)]
        public string NationalInsuranceNo { get; set; } //SSN

        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan StudentLoan { get; set; }
        public UnionMember UnionMember { get; set; }

        public IEnumerable<PaymentRecord>  PaymentRecords { get; set; }
    }
}
