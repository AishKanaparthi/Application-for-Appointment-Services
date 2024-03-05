

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTC_Dental.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTC_Dental.Controllers
{
    public class PatientController : Controller
    {
        private DentalContext context { get; set; }
        public PatientController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the List view
        [Route("[controller]s")]
        public IActionResult List()
        {
            var patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            return View(patients);
        }

        //saves the new/edited patient record/object to the DTC Dental database (Patients table).
        [HttpPost]
        public IActionResult Save(Patient patient)
        {
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            bool uniqueEmail = !context.Patients.Any(p => p.PatientID != patient.PatientID && p.Email == patient.Email);
            string message = string.Empty;

            if (ModelState.IsValid && uniqueEmail) //if required fields are passed through and successfully validated the record/object is saved/updated to the designated table and the user is redirected to the List view.
            {
                if (patient.PatientID == 0) //if ID is 0 the record doesn't currently exist meaning new record/object is being created
                {
                    context.Patients.Add(patient);
                    TempData["message"] = patient.FullName + " was added.";
                }
                else //else an existing object/record is being edited/updated
                {
                    //allows users to edit exsisting patients and change their HOH ID.
                    var existingPatient = context.Patients.Find(patient.PatientID);
                    if (existingPatient != null)
                    {
                        context.Entry(existingPatient).State = EntityState.Detached;
                    }

                    context.Patients.Update(patient);
                    TempData["message"] = patient.FullName + " was updated.";
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }

            else if (!uniqueEmail) //else if not unique email displays specified error message
            {
                ModelState.AddModelError("Email", "Email already in use.");
                return View("AddEdit", patient);
            }

            else //else the entered data has validation errors and cannot be saved
            {
                if (patient.PatientID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View("AddEdit", patient);
            }
        }

        //opens the AddEdit view when the add button is clicked and creates a new empty patient object/record
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            return View("AddEdit", new Patient());
        }

        //opens the AddEdit view when the edit button is clicked and finds the specific patient object/record by its ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Patients = context.Patients.OrderBy(p => p.FirstName).ToList();
            var patient = context.Patients.Find(id);
            return View("AddEdit", patient);
        }

        //opens the delete view for the specified object/record by using its ID to find it.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var patient = context.Patients.Find(id);
            return View(patient);
        }

        //removes the patient record/object from the database (Patients table) and redirects the user to the List view.
        [HttpPost]
        public IActionResult Delete(Patient patient)
        {
            var patientToDelete = context.Patients.Find(patient.PatientID);
            if (patientToDelete != null)
            {
                TempData["message"] = patientToDelete.FullName + " was deleted.";
                context.Patients.Remove(patientToDelete);
                context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
