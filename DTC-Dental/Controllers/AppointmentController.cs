
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTC_Dental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTC_Dental.Controllers
{
    public class AppointmentController : Controller
    {
        private DentalContext context { get; set; }
        public AppointmentController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the List view
        [Route("[controller]s")]
        public IActionResult List()
        {
            IQueryable<Appointment> appointments = context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.AppointmentType)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.Patient.FirstName);

            return View(appointments);
        }

        //saves the new/edited appointment record/object to the DTC Dental database (Appointment table).
        [HttpPost]
        public IActionResult Save(Appointment appointment)
        {
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            ViewBag.Dentists = context.Dentists.OrderBy(d => d.FirstName).ToList();
            ViewBag.AppointmentTypes = context.AppointmentTypes.OrderBy(a => a.AppointmentName).ToList();

            if (ModelState.IsValid) //if required fields are passed through and successfully validated the record/object is saved/updated to the designated table and the user is redirected to the List view.
            {
                if (appointment.AppointmentID == 0) //if ID is 0 the record doesn't currently exist meaning new record/object is being created
                {
                    context.Appointments.Add(appointment);
                }
                else //else an existing object/record is being edited/updated
                {
                    context.Appointments.Update(appointment);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else //else the entered data has validation errors and cannot be saved
            {
                if (appointment.AppointmentID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View("AddEdit", appointment);
            }
        }

        //opens the AddEdit view when the add button is clicked and creates a new empty appointment object/record
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            ViewBag.Dentists = context.Dentists.OrderBy(d => d.FirstName).ToList();
            ViewBag.AppointmentTypes = context.AppointmentTypes.OrderBy(a => a.AppointmentName).ToList();
            return View("AddEdit", new Appointment());
        }

        //opens the AddEdit view when the edit button is clicked and finds the specific appointment object/record by its ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            ViewBag.Dentists = context.Dentists.OrderBy(d => d.FirstName).ToList();
            ViewBag.AppointmentTypes = context.AppointmentTypes.OrderBy(a => a.AppointmentName).ToList();
            var appointment = context.Appointments.Find(id);
            return View("AddEdit", appointment);
        }

        //opens the delete view for the specified object/record by using its ID to find it.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var appointment = context.Appointments.Find(id);
            return View(appointment);
        }

        //removes the appointment record/object from the database (Appointments table) and redirects the user to the List view.
        [HttpPost]
        public IActionResult Delete(Appointment appointment)
        {
            context.Appointments.Remove(appointment);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}

