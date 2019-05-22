using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CarajWeb.Controllers
{
    public class HomeController : Controller
    {
        string link = "http://165.22.91.48/api/";
        public ActionResult Index()
        {

            ViewBag.Title = "Home";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("car/GetAllCars").Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<List<JObject>>().Result;
                    return View(model);
                }
                else
                    return View();
            }

        }

        public ActionResult CarDetails(int id)
        {
            ViewBag.Message = "Car Details";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("car/getcar?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<JObject>().Result;
                    return View(model);
                }
                else
                    return View();
            }
        }


        public ActionResult FilterResult(string location,string startDate,string startTime, string finishDate,string finishTime)
        {
            if (location=="" || startTime == "" || startDate == "" || finishTime == "" || finishDate == "")
                return RedirectToAction("Index");
            DateTime first = DateTime.ParseExact(startDate+" "+ startTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime last = DateTime.ParseExact(finishDate + " " + finishTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("car/GetAvailableCar?first=" + first+"&last="+last).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<JArray>().Result;
                    return View(model);
                }
                else
                    return View();
            }
        }
    }
}