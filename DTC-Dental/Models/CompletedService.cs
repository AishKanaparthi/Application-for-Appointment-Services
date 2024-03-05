

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace DTC_Dental.Models
{
	public class CompletedService
	{
		public int ServiceID { get; set; }				// foreign key property

		public Service Service { get; set; } = null!;   // navigation property

        public int VisitID { get; set; }                // foreign key property

		public Visit Visit { get; set; } = null!;		// navigation property
    }
}

