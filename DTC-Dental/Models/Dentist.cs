


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace DTC_Dental.Models
{
	public class Dentist
	{
		public int DentistID { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a hire date.")]
        public DateTime HireDate { get; set; }

        public string FullName => FirstName + " " + LastName;   // read-only property
    }
}

