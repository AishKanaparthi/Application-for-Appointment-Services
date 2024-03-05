

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DTC_Dental.Models
{
	public class Patient
	{
		public int PatientID { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an address.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a city.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a state.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a zip.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Zip code must be 5 or 9 digits/numbers. (Format: 12345 or 12345-6789).")]
        public string Zip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format 999-999-9999.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a SSN.")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$",ErrorMessage = "Social Security Number must be in the format 999-99-9999.")]

        public string SSN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a DOB.")]
        public string DOB { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a minor status.")]
        public Boolean Minor { get; set; }

        public int? PatientHOHID { get; set; }

        public Patient? HOH { get; set; }

        public ICollection<Patient> Dependents { get; set; } = new List<Patient>();

        public string FullName => FirstName + " " + LastName;   // read-only property
    }
}

