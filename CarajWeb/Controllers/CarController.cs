using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CarajWeb.Controllers
{
    public class CarController : Controller
    {
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

            return View(1);
        }

        public ActionResult List()
        {
            return View(1);
        }

        public ActionResult Reservations()
        {
            //CompanyServiceSoapClient client = new CompanyServiceSoapClient();
            //List<RentDetailsResponseDto> model = client.GetRentDetails((int)Session["CompanyID"]).ToList();
            return View(1);
        }

        [HttpPost]
        public ActionResult Create(string dto)
        {
            //CarServiceSoapClient client = new CarServiceSoapClient();
            if (ModelState.IsValid) { 
              //  client.AddCar(dto, (int)Session["CompanyID"]);
                TempData.Add("Message","Car Created");
                return RedirectToAction("List", "Car", "");
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