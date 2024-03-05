using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace DTC_Dental.Models
{
    public class DTCSession
    {
        private const string ServicesKey = "cart_services";
        private const string CountKey = "service_count";

        private ISession session { get; set; }
        public DTCSession(ISession session) => this.session = session;

        public void AddToCart(List<Service> services)
        {
            session.SetObject(ServicesKey, services);
            session.SetInt32(CountKey, services.Count);
        }

        public List<Service> GetCartServices() =>
            session.GetObject<List<Service>>(ServicesKey) ?? new List<Service>();

        public int? GetCartServiceCount() => session.GetInt32(CountKey);

        public void RemoveCartServices()
        {
            session.Remove(ServicesKey);
            session.Remove(CountKey);
        }
    }
}

