using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signin(UserProfile objUser)
        {
            TempData["errorMsg"] = string.Empty;

            // Validate UserName & Password
            if (ModelState.IsValid
                && !string.IsNullOrEmpty(objUser.UserName)
                && !string.IsNullOrEmpty(objUser.Password))
            {
                using (ParkingEntities db = new ParkingEntities())
                {
                    var obj = db.UserProfiles
                        .Where(a => a.UserName.Equals(objUser.UserName) 
                            && a.Password.Equals(objUser.Password))
                            .FirstOrDefault();

                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        Session["UserType"] = obj.UserType.ToString();
                        if (obj.UserType == 1)
                        {
                            TempData["statusMsg"] = string.Empty;
                            return RedirectToAction("AdminUserDashBoard");
                        }
                        else if (obj.UserType == 2)
                        {
                            return RedirectToAction("EntryGate", "Parking");
                        }
                        else if (obj.UserType == 3)
                        {
                            return RedirectToAction("OutGate", "Parking");
                        }
                    }
                    else {
                        TempData["errorMsg"] = "UserName / Password is incorrect";
                    }
                }
            }

            // When user is not logged-in OR Authorization fails.
            Session.Clear();
            return View(objUser);
        }

        public ActionResult AdminUserDashBoard()
        {
            // Admin User Dashboard.
            if (Session["UserID"] != null
                && Session["UserType"].ToString().Equals("1"))
            {
                var parkingGateList = new List<SelectListItem>();
                parkingGateList.Add(new SelectListItem
                {
                    Text = "Entry Gate",
                    Value = "2",
                    Selected = true
                });
                parkingGateList.Add(new SelectListItem
                {
                    Text = "Out Gate",
                    Value = "3"
                });
                ModelState.Clear();
                ViewData["GateList"] = parkingGateList;
                return View();
            }
            else
            {
                // When user is not logged-in.
                Session.Clear();
                return RedirectToAction("Signin");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Signin");
        }

        public ActionResult CreateGate(UserProfile objUser)
        {
            TempData["errorMsg"] = string.Empty;
            TempData["statusMsg"] = string.Empty;

            if (ModelState.IsValid 
                && Session["UserID"] != null
                && Session["UserType"].ToString().Equals("1"))
            {
                using (ParkingEntities db = new ParkingEntities())
                {
                    var obj = db.UserProfiles
                        .Where(a => a.UserName.Equals(objUser.UserName))
                        .FirstOrDefault();

                    if (obj == null)
                    {
                        objUser.IsActive = true;
                        objUser.CreatedBy = Session["UserName"].ToString();
                        db.UserProfiles.Add(objUser);
                        db.SaveChanges();
                        TempData["statusMsg"] = "Account is created successfully.";
                        return RedirectToAction("AdminUserDashBoard");
                    }
                    else {
                        TempData["errorMsg"] = "GateID is already exists, Please create with seperate name";
                        return RedirectToAction("AdminUserDashBoard");
                    }
                }
                
            }
            // When user is not logged-in.
            Session.Clear();
            return RedirectToAction("Signin");
        }
    }
}