 
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DTC_Dental.Models
{
	public class Appointment
	{
		public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Please enter/select an appointment date.")]
        public DateTime AppointmentDate { get; set; } 

        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Please select a dentist.")]
        public int DentistID { get; set; }
        [ValidateNever]
        public Dentist Dentist { get; set; } = null!;

        [Required(ErrorMessage = "Please select a patient.")]
        public int PatientID { get; set; }
        [ValidateNever]
        public Patient Patient { get; set; } = null!;

        [Required(ErrorMessage = "Please select an appointment type.")]
        public int AppointmentTypeID { get; set; }
        [ValidateNever]
        public AppointmentType AppointmentType { get; set; } = null!;
    }
}

