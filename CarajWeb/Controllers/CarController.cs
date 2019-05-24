using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using CarajWeb.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace CarajWeb.Controllers
{
    public class CarController : Controller
    {
        string link = "http://165.22.91.48/api/";

        // GET: Car
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string carID)
        {
            ViewBag.Message = "Car Details";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("car/GetCar?id=" + carID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<JObject>().Result;
                    return View(model);
                }
                else
                    return View();
            }
        }

        public ActionResult Brings()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Fix
                HttpResponseMessage response = client.GetAsync("company/outside?CompanyID=" + Session["CompanyID"]).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<JArray>().Result;
                    return View(model);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Bring()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Bring(string endKM,string carID)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Car/RentCarReturn?endKm=" + endKM + "&CarID=" + carID);
                if (response.IsSuccessStatusCode)
                {
                    return View();
                }
                return View();
            }
        }
        public ActionResult List()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("car/GetCars?id=" + Session["CompanyID"]).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<List<JObject>>().Result;
                    return View(model);
                }
                else
                    return View();
            }

        }

        public ActionResult Reservations()
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("company/GetRentDetails?id=" + Session["CompanyID"]).Result;
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<List<JObject>>().Result;
                    return View(model);
                }
                else
                    return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CarRequest dto)
        {
            dto.CompanyID = int.Parse(Session["CompanyID"].ToString());
            if (ModelState.IsValid)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(link);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var serializeddto = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("car/addcar", content);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("List", "Car", "");
                }
            }
            return View();
        }
    

    [HttpPost]
    public async Task<ActionResult> Edit(CarUpdate dto, string carID)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(link);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var serializeddto = JsonConvert.SerializeObject(dto + carID);
            var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("car/updatecar?id="+carID, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", "Car", "");
            }
        }

        return View();

    }
    /*
    public ActionResult Delete(string carID)
    {
       // CarServiceSoapClient client = new CarServiceSoapClient();
        if (ModelState.IsValid)
           // client.DeleteCar(int.Parse(carID));

        return RedirectToAction("List", "Car", "");
    }*/
}
}