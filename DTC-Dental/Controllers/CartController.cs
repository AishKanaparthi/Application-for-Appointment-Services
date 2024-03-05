

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTC_Dental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTC_Dental.Controllers
{
    public class CartController : Controller
    {
        private DentalContext context { get; set; }
        public CartController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the cart list view
        [Route("[controller]s")]
        public IActionResult List()
        {
            var session = new DTCSession(HttpContext.Session);
            var model = new ServicesViewModel
            {
                Services = session.GetCartServices()
            };

            return View(model);
        }

        //adds a service from the visit list view to the cart view using sessions
        [HttpPost]
        public IActionResult Add(Service service)
        {
            service = context.Services
                .Where(s => s.ServiceID == service.ServiceID)
                .FirstOrDefault() ?? new Service();

            var session = new DTCSession(HttpContext.Session);
            var services = session.GetCartServices();

            //if the service doesn't already exsist in the cart it adds it to the cart
            if (!services.Any(s => s.ServiceID == service.ServiceID))
            {
                
                services.Add(service);
                session.AddToCart(services);
                //Displays a message on the Cart page that the selected item was added to the cart (not required but added for increased functionality/understandability)
                TempData["message"] = $"{service.Description}  was added to your cart.";
            }
            //the item already exsists in the cart and display a message stating so.
            else
            {
                TempData["message"] = $"{service.Description} is already in your cart.";
            }

            //redirects users to the cart view
            return RedirectToAction("List", "Cart");
        }

        //deletes a singular chosen service from the cart based on its ID
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var session = new DTCSession(HttpContext.Session);
            var services = session.GetCartServices();
            var deleteService = services.FirstOrDefault(s => s.ServiceID == id);

            //if the service being deleted isn't null 
            if (deleteService != null)
            {
                services.Remove(deleteService);
                session.AddToCart(services);
                TempData["message"] = $"{deleteService.Description} was deleted from your cart.";
            }

            return RedirectToAction("List", "Cart");
        }
        
        //clears/removes all of the services in the cart
        [HttpPost]
        public IActionResult Clear()
        {
            var session = new DTCSession(HttpContext.Session);
            session.RemoveCartServices();

            TempData["message"] = "Cart cleared.";

            return RedirectToAction("List", "Cart");
        }

        //adds the functionality of the finalize visit button and displays order summary (i.e. services and subtotal) and addition option choices.
        [HttpGet]
        public IActionResult Finalize()
        { 
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            ViewBag.Dentists = context.Dentists.OrderBy(d => d.FirstName).ToList();

            var session = new DTCSession(HttpContext.Session);
            var services = session.GetCartServices();

            var subTotal = services.Sum(service => service.Cost);

            var model = new FinalizeVisitViewModel
            {
                Services = services,
                SubTotal = subTotal
            };

            return View(model);
        }

        //saves user choices from Finalize view/page into the VisitConfirmationViewModel model and redirects the user to the confirmation page/vieww
        [HttpPost]
        public IActionResult Finalize(FinalizeVisitViewModel FVModel)
        {
            var session = new DTCSession(HttpContext.Session);
            var services = session.GetCartServices();

            var subTotal = services.Sum(service => service.Cost);

            var selectedDentist = context.Dentists.Find(FVModel.SelectedDentistID);
            var selectedPatient = context.Patients.Find(FVModel.SelectedPatientID);

            var model = new VisitConfirmationViewModel
            {
                Dentist = selectedDentist?.FullName,
                Patient = selectedPatient?.FullName,
                Services = services,
                SubTotal = subTotal
            };

            session.RemoveCartServices();

            //redirects user to the Confirmation view
            return RedirectToAction("Confirmation", model);
        }

        //gets the model information ver to the confirmation view/page
        [HttpGet]
        public IActionResult Confirmation(VisitConfirmationViewModel model)
        {
            return View(model);
        }
    }
}

