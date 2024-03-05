

using System;
namespace DTC_Dental.Models
{
	public class Visit
	{
		public int VisitID { get; set; }

		public DateTime VisitDate { get; set; }

        public int DentistID { get; set; }

		public Dentist Dentist { get; set; } = null!;    

		public int PatientID { get; set; }

        public Patient Patient { get; set; } = null!;
    }
}

