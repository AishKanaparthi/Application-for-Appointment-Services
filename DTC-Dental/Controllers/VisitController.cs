

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
    public class VisitController : Controller
    {
        private DentalContext context { get; set; }
        public VisitController(DentalContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        //opens the List view
        [Route("[controller]s")]
        public IActionResult List()
        {
            var services = context.Services.OrderBy(s => s.Description).ToList();
            return View(services);
        }
        
    }
}

