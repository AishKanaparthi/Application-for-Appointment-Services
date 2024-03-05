


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTC_Dental.Models;

public class FinalizeVisitViewModel
{
    public int SubTotal { get; set; }

    public int SelectedDentistID { get; set; }

    public int SelectedPatientID { get; set; }

    public List<Service> Services { get; set; }

}
