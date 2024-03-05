
using System;
namespace DTC_Dental.Models
{
	public class ServicesViewModel
	{
		public Service Service { get; set; } = new Service();
        public List<Service> Services { get; set; } = new List<Service>();
    }
}

