using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarajWeb.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Profile()
        {
            ViewBag.CustomerID = Convert.ToInt32(Session["CustomerID"]);
            return View();
        }

        public ActionResult CompanyProfile()
        {
            ViewBag.CompanyID = Convert.ToInt32(Session["CompanyID"]);
            return View();
        }

        [HttpPost]
        public ActionResult ReservationAddCheck(string location, string startDate, string startTime, string finishDate, string finishTime,string carID,string carName, string rent, string age)
        {
            DateTime first = DateTime.ParseExact(startDate + " " + startTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime last = DateTime.ParseExact(finishDate + " " + finishTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            double price = Math.Ceiling((last - first).TotalDays);
            price *= Double.Parse(rent);

            TempData.Remove("CarReservationLocation");
            TempData.Remove("CarReservationFirstDate");
            TempData.Remove("CarReservationLastDate");
            TempData.Remove("CarReservationCarID");
            TempData.Remove("CarReservationCarName");
            TempData.Remove("CarReservationPrice");
            TempData.Remove("CarReservationAge");

            TempData.Add("CarReservationLocation", location);
            TempData.Add("CarReservationFirstDate", first);
            TempData.Add("CarReservationLastDate", last);
            TempData.Add("CarReservationCarID", carID);
            TempData.Add("CarReservationCarName", carName);
            TempData.Add("CarReservationPrice", price);
            TempData.Add("CarReservationAge", age);

            return RedirectToAction("Reservation", "Dashboard", "");
        }

        public ActionResult AddReservation(string dto)
        {
            //CompanyServiceSoapClient client = new CompanyServiceSoapClient();
            if (ModelState.IsValid) { 
                //client.CreateRent(dto);
            }
            return RedirectToAction("Accepted", "Dashboard", "");
        }

        public ActionResult Reservation()
        {
            string BirthDate = Session["BirthDate"].ToString();
            DateTime dt = DateTime.ParseExact(BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var today = DateTime.Today;
            var age = today.Year -dt.Year;
            if (dt.Date > today.AddYears(-age)) age--;
            TempData.Add("Age", age);
            return View();
        }
        public ActionResult Accepted()
        {
            return View();
        }
    }
}