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
            if (ModelState.IsValid 
                && Session["UserID"] != null
                && Session["UserType"].ToString().Equals("2")
                && !string.IsNullOrEmpty(parkingLog.PlateNumber))
            {
                using (ParkingEntities db = new ParkingEntities())
                {
                    parkingLog.Status = "OnGoing";
                    parkingLog.SessionID = Session.SessionID;
                    parkingLog.TimeStamp = DateTime.Now;
                    parkingLog.INAgentMACID = Session["UserName"].ToString();
                    db.ParkingLogs.Add(parkingLog);
                    db.SaveChanges();
                }
            }
            ModelState.Clear();
            return View();
        }

        
        public ActionResult OutGate()
        {
            return View();
        }
    }
}