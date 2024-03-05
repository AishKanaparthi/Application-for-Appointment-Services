﻿


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace DTC_Dental.Models
{
	public class AppointmentType
	{
		public int AppointmentTypeID { get; set; }

        [Required(ErrorMessage = "Please enter an appointment name.")]
        public string AppointmentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a duration.")]
        public int Duration { get; set; }
    }
}
