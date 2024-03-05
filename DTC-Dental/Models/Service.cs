

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace DTC_Dental.Models
{
	public class Service
	{
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a cost.")]
        public int Cost { get; set; }
    }
}

