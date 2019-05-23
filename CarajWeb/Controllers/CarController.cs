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
        public async Task<ActionResult> Edit(CarUpdate dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serializeddto = JsonConvert.SerializeObject(dto);
                var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("car/UpdateCar", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Car", "");
                }
            }
            ViewBag.Message = "Car Details";

            return View(1);
        }

        public ActionResult List()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("car/GetCars?id="+ Session["CompanyID"]).Result;
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
            //CompanyServiceSoapClient client = new CompanyServiceSoapClient();
            //List<RentDetailsResponseDto> model = client.GetRentDetails((int)Session["CompanyID"]).ToList();
            return View(1);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CarRequest dto)
        {
            dto.CompanyID = int.Parse(Session["CompanyID"].ToString());
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serializeddto = JsonConvert.SerializeObject(dto);
                var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("car/addcar", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Car", "");
                }
            }

            return View();
        }

       /* [HttpPost]
        public ActionResult Edit(string dto, string carID)
        {
           // CarServiceSoapClient client = new CarServiceSoapClient();
            if (ModelState.IsValid)
                //client.UpdateCar(dto,int.Parse(carID));
            return RedirectToAction("List", "Car", "");
           
        }

        public ActionResult Delete(string carID)
        {
           // CarServiceSoapClient client = new CarServiceSoapClient();
            if (ModelState.IsValid)
               // client.DeleteCar(int.Parse(carID));

            return RedirectToAction("List", "Car", "");
        }*/
    }
}