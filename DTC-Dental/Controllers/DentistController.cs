

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTC_Dental.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTC_Dental.Controllers
{
    public class DentistController : Controller
    {
        private DentalContext context { get; set; }
        public DentistController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the List view
        [Route("[controller]s")]
        public IActionResult List()
        {
            var dentists = context.Dentists.OrderBy(d => d.FirstName).ToList();
            return View(dentists);
        }

        //saves the new/edited dentist record/object to the DTC Dental database (Dentists table).
        [HttpPost]
        public IActionResult Save(Dentist dentist)
        {
            if (ModelState.IsValid) //if required fields are passed through and successfully validated the record/object is saved/updated to the designated table and the user is redirected to the List view.
            {
                if (dentist.DentistID == 0) //if ID is 0 the record doesn't currently exist meaning new record/object is being created
                {
                    context.Dentists.Add(dentist);
                }
                else //else an existing object/record is being edited/updated
                {
                    context.Dentists.Update(dentist);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else //else the entered data has validation errors and cannot be saved
            {
                if (dentist.DentistID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View("AddEdit", dentist);
            }
        }

        //opens the AddEdit view when the add button is clicked and creates a new empty dentist object/record
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Dentist());
        }

        //opens the AddEdit view when the edit button is clicked and finds the specific dentist object/record by its ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var dentist = context.Dentists.Find(id);
            return View("AddEdit", dentist);
        }

        //opens the delete view for the specified object/record by using its ID to find it.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dentist = context.Dentists.Find(id);
            return View(dentist);
        }

        //removes the dentist record/object from the database (Dentists table) and redirects the user to the List view.
        [HttpPost]
        public IActionResult Delete(Dentist dentist)
        {
            context.Dentists.Remove(dentist);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}

