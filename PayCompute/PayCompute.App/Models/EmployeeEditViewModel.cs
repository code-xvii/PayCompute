using Microsoft.AspNetCore.Http;
using PayCompute.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PayCompute.App.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee number is required")]
        [RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "First name is required"), StringLength(50, MinimumLength = 2), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }

        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "First name is required"), StringLength(50, MinimumLength = 2), Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }
       
        public string Gender { get; set; }

        [DataType(DataType.Date), Display(Name ="Date Of Birth")]
        public DateTime DOB { get; set; }


        [Required, StringLength(150)]
        public string Address { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(10)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [DataType(DataType.Date), Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }

        [Required(ErrorMessage ="Job role is  required"), StringLength(100)]
        public string Designation { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name ="Photo")]
        public IFormFile ImageUrl { get; set; }


        [Required, StringLength(50), Display(Name ="NI No.")]
        [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
        public string NationalInsuranceNo { get; set; } //SSN 000-00-0000 @"^\d{3}-\d{2}-\d{4}$"

        [Display(Name ="Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }

        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
    }
}
