

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTC_Dental.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTC_Dental.Controllers
{
    public class AppointmentTypeController : Controller
    {
        private DentalContext context { get; set; }
        public AppointmentTypeController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the List view
        [Route("[controller]s")]
        public IActionResult List()
        {
            var at = context.AppointmentTypes.OrderBy(a => a.AppointmentName).ToList();
            return View(at);
        }

        //saves the new/edited appointment type record/object to the DTC Dental database (Appointment Types table).
        [HttpPost]
        public IActionResult Save(AppointmentType at)
        {
            if (ModelState.IsValid) //if required fields are passed through and successfully validated the record/object is saved/updated to the designated table and the user is redirected to the List view.
            {
                if (at.AppointmentTypeID == 0) //if ID is 0 the record doesn't currently exist meaning new record/object is being created
                {
                    context.AppointmentTypes.Add(at);
                }
                else //else an existing object/record is being edited/updated
                {
                    context.AppointmentTypes.Update(at);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else //else the entered data has validation errors and cannot be saved
            {
                if (at.AppointmentTypeID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View("AddEdit", at);
            }
        }

        //opens the AddEdit view when the add button is clicked and creates a new empty appointment type object/record
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new AppointmentType());
        }

        //opens the AddEdit view when the edit button is clicked and finds the specific appointment type object/record by its ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var at = context.AppointmentTypes.Find(id);
            return View("AddEdit", at);
        }

        //opens the delete view for the specified object/record by using its ID to find it.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var at = context.AppointmentTypes.Find(id);
            return View(at);
        }

        //removes the appointment type record/object from the database (Appointment Types table) and redirects the user to the List view.
        [HttpPost]
        public IActionResult Delete(AppointmentType at)
        {
            context.AppointmentTypes.Remove(at);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}

