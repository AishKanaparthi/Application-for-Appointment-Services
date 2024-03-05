


using System.ComponentModel.DataAnnotations;
using DTC_Dental.Models;

public class VisitConfirmationViewModel
{
    public int SubTotal { get; set; }

    [Required(ErrorMessage = "Please select a dentist")]
    public string Dentist { get; set; } = null!;

    [Required(ErrorMessage = "Please select a dentist")]
    public string Patient { get; set; } = null!;

    public List<Service> Services { get; set; }

}
