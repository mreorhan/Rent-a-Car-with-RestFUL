using CarajWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarajWeb.Controllers
{
    public class DashboardController : Controller
    {
        string link = "http://165.22.91.48/api/";
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
        public ActionResult ReservationAddCheck(string location, string startDate, string startTime, string finishDate, string finishTime, string carID, string carName, string rent, string age)
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

        public async Task<ActionResult> AddReservation(RentDetails dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serializeddto = JsonConvert.SerializeObject(dto);
                var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("company/CreateRent", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Accepted", "Dashboard", "");
                }
            }
            return View();
        }

        public ActionResult Reservation()
        {
            string BirthDate = Session["BirthDate"].ToString();
            DateTime dt = DateTime.ParseExact(BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var today = DateTime.Today;
            var age = today.Year - dt.Year;
            if (dt.Date > today.AddYears(-age)) age--;
            TempData.Add("Age", age);
            return View();
        }
        public ActionResult Accepted()
        {
            return View();
        }
        public ActionResult MonthlyCarExceedKMChart()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("company/economicreport?companyid=" + Session["CompanyID"]).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<List<JObject>>().Result;

                    List<Chart> chart = new List<Chart>();
                    for (int i = 0; i < model.Count; i++)
                    {
                        chart.Add(new Chart());
                        chart[i].income = float.Parse(model[i]["income"].ToString());
                        chart[i].monthlyCarRental = float.Parse(model[i]["monthlyCarRental"].ToString());
                        chart[i].monthlyCarExceedKM = float.Parse(model[i]["monthlyCarExceedKM"].ToString());
                    }
                    return View(chart);
                }
                else
                    return View();
            }
        }
        public ActionResult MonthlyCarRentalChart()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("company/economicreport?companyid=" + Session["CompanyID"]).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<List<JObject>>().Result;

                    List<Chart> chart = new List<Chart>();
                    for (int i = 0; i < model.Count; i++)
                    {
                        chart.Add(new Chart());
                        chart[i].income = float.Parse(model[i]["income"].ToString());
                        chart[i].expense = float.Parse(model[i]["expense"].ToString());
                        chart[i].monthlyCarRental = float.Parse(model[i]["monthlyCarRental"].ToString());
                        chart[i].monthlyCarExceedKM = float.Parse(model[i]["monthlyCarExceedKM"].ToString());
                    }
                    return View(chart);
                }
                else
                    return View();
            }
        }
        public ActionResult Chart()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("company/economicreport?companyid=" + Session["CompanyID"]).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<List<JObject>>().Result;

                    List<Chart> chart = new List<Chart>();
                    for (int i = 0; i < model.Count; i++)
                    {
                        chart.Add(new Chart());
                        chart[i].income = float.Parse(model[i]["income"].ToString());
                        chart[i].expense = float.Parse(model[i]["expense"].ToString());
                    }
                    return View(chart);
                }
                else
                    return View();
            }
        }
    }
}