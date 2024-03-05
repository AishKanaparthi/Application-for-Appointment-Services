

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTC_Dental.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTC_Dental.Controllers
{
    public class ServiceController : Controller
    {
        private DentalContext context { get; set; }
        public ServiceController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the List view
        [Route("[controller]s")]
        public IActionResult List()
        {
            var services = context.Services.OrderBy(s => s.Description).ToList();
            return View(services);
        }

        //saves the new/edited service record/object to the DTC Dental database (Services table).
        [HttpPost]
        public IActionResult Save(Service service)
        {
            if (ModelState.IsValid) //if required fields are passed through and successfully validated the record/object is saved/updated to the designated table and the user is redirected to the List view.
            {
                if (service.ServiceID == 0) //if ID is 0 the record doesn't currently exist meaning new record/object is being created
                {
                    context.Services.Add(service);
                }
                else //else an existing object/record is being edited/updated
                {
                    context.Services.Update(service);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else //else the entered data has validation errors and cannot be saved
            {
                if (service.ServiceID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View("AddEdit", service);
            }
        }

        //opens the AddEdit view when the add button is clicked and creates a new empty service object/record
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Service());
        }

        //opens the AddEdit view when the edit button is clicked and finds the specific service object/record by its ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var service = context.Services.Find(id);
            return View("AddEdit", service);
        }

        //opens the delete view for the specified object/record by using its ID to find it.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var service = context.Services.Find(id);
            return View(service);
        }

        //removes the service record/object from the database (Services table) and redirects the user to the List view.
        [HttpPost]
        public IActionResult Delete(Service service)
        {
            context.Services.Remove(service);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}

