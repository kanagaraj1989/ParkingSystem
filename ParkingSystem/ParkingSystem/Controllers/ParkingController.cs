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
            TempData["statusMsg"] = string.Empty;

            if (ModelState.IsValid 
                && Session["UserID"] != null
                && Session["UserType"].ToString().Equals("2"))
            {
                if (!string.IsNullOrEmpty(parkingLog.PlateNumber))
                {
                    using (ParkingEntities db = new ParkingEntities())
                    {
                        parkingLog.Status = "OnGoing";
                        parkingLog.InGateSessionID = Session.SessionID;
                        parkingLog.InTimeStamp = DateTime.Now;
                        parkingLog.INAgentMACID = Session["UserName"].ToString();
                        db.ParkingLogs.Add(parkingLog);
                        db.SaveChanges();
                        TempData["statusMsg"] = "Successfully Submitted Your IN Entry.";
                    }
                }
                ModelState.Clear();
                return View();
            }

            // When user is not logged-in
            Session.Clear();
            return RedirectToAction("Signin", "Login");
        }

        
        public ActionResult OutGate(ParkingLog parkingLog)
        {
            TempData["statusMsg"] = string.Empty;
            TempData["errorMsg"] = string.Empty;

            if (ModelState.IsValid
                && Session["UserID"] != null
                && Session["UserType"].ToString().Equals("3"))
            {
                if (!string.IsNullOrEmpty(parkingLog.PlateNumber))
                {
                    using (ParkingEntities db = new ParkingEntities())
                    {
                        var obj = db.ParkingLogs
                            .Where(car
                               => car.PlateNumber.Equals(parkingLog.PlateNumber)
                                  && car.Status.Equals("OnGoing"))
                                  .OrderByDescending(car => car.InTimeStamp)
                                  .FirstOrDefault();

                        if (obj != null)
                        {
                            obj.Status = "Completed";
                            obj.OutGateSessionID = Session.SessionID;
                            obj.OutTimeStamp = DateTime.Now;
                            obj.OUTAgentMACID = Session["UserName"].ToString();
                            db.SaveChanges();
                            TempData["statusMsg"] = "Sucessfully submited your OUT Entry.";
                        }
                        else
                        {
                            TempData["errorMsg"] = "Sorry We don't have your records in our server.";
                        }
                    }
                }
                ModelState.Clear();
                return View();
            }

            //When user is not logged-in
            Session.Clear();
            return RedirectToAction("Signin", "Login");
        }
    }
}