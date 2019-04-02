using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSystem.Controllers
{
    public class ParkingController : Controller
    {
        public ActionResult EntryGate(ParkingLog parkingLog)
        {
            if (ModelState.IsValid)
            {

            }
                return View(parkingLog);
           
        }

        
        public ActionResult OutGate()
        {
            return View();
        }
    }
}