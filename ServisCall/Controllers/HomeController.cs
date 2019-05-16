using Caraj.EntitiesLayer.DTOs;
using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServisCall.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ////install-package Microsoft.AspNet.WebApi.Client kurulacak Package Manager Console


            return View();
        }

        public ActionResult About()
        {
            using (HttpClient client = new HttpClient()) //Fix
            {
                client.BaseAddress = new Uri("http://165.22.91.48/api/"); //Fix
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Fix
                HttpResponseMessage response = client.GetAsync("car/getcar?id=8").Result; 
                //End Point Name https://localhost:44300/car/getcars Query String Example bla bla car/getcars?blabla
                if (response.IsSuccessStatusCode)
                {
                    var model = response.Content.ReadAsAsync<JObject>().Result; 
                    return View(model);
                }
                else
                {
                    //Error Bla Bla Veri Gelmezse Gibisine Yada Status Code Uymazsa  
                    return View();
                }
            }
          

            //Is Poss ? Diff Exp
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}