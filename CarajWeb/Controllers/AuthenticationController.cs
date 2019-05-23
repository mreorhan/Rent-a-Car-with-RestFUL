using CarajWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarajWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        string link = "http://165.22.91.48/api/";

        private HttpCookie CreateUserCookie(string customerName)
        {
            HttpCookie UserCookies = new HttpCookie("UserCookies");
            UserCookies.Value = customerName;
            UserCookies.Expires = DateTime.Now.AddHours(10);
            return UserCookies;
        }

        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public ActionResult CompanyLogin()
        {
            ViewBag.Title = "Company Login";
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Register";
            return View();
        }
        public ActionResult CompanyRegister()
        {
            ViewBag.Title = "Register";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetUserLogin(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    link+ "auth/CustomerLogin?username="+ username + "&password="+ password,
                     new StringContent(username,Encoding.UTF8, "application/json"));
               
                if (response.StatusCode.GetHashCode() == 200)
                {
                    var model = response.Content.ReadAsAsync<JObject>().Result;
                    //Session["BirthDate"] = model.BirthDate.ToString("yyyy-MM-dd");
                    Response.Cookies.Add(CreateUserCookie(model["customerName"].ToString()));
                    Session["CustomerID"] = model["customerID"];
                    return RedirectToAction("Index", "Home", "");
                }
                else
                {
                    TempData.Add("Error", "Wrong username or password");
                    return RedirectToAction("Login", "Authentication", "");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(Customer dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serializeddto = JsonConvert.SerializeObject(dto);
                var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("auth/CustomerRegister", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Authentication", "");
                }
            }
            return RedirectToAction("Login", "Authentication", "");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCompany(Company dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var serializeddto = JsonConvert.SerializeObject(dto);
                var content = new StringContent(serializeddto, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("auth/CompanyRegister", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CompanyLogin", "Authentication", "");
                }
            }

            return RedirectToAction("CompanyLogin", "Authentication", "");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            if (Request.Cookies["UserCookies"] != null)
            {
                Response.Cookies["UserCookies"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home", "");
        }

        [HttpPost]
        public async Task<ActionResult> GetCompanyLogin(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    link + "auth/CompanyLogin?companyname=" + username + "&password=" + password,
                     new StringContent(username, Encoding.UTF8, "application/json"));

                if (response.StatusCode.GetHashCode() == 200)
                {
                    var model = response.Content.ReadAsAsync<JObject>().Result;

                    //TODO: Change companyid with companyname
                    Response.Cookies.Add(CreateUserCookie(model["companyID"].ToString()));
                    Session["CompanyID"] = model["companyID"];
                    return RedirectToAction("Index", "Home", "");
                }
                else
                {
                    TempData.Add("Error", "Wrong username or password");
                    return RedirectToAction("CompanyLogin", "Authentication", "");
                }
            }
        }
    }
}