using System;
using System.Web;
using System.Web.Mvc;

namespace CarajWeb.Controllers
{
    public class AuthenticationController : Controller
    {
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
        public ActionResult GetUserLogin(string username, string password)
        {
            /*AuthServiceSoapClient client = new AuthServiceSoapClient();
           //oginResponseDto response = client.CustomerLogin(username, password);
            if (response != null)
            {
                Session["BirthDate"] = response.BirthDate.ToString("yyyy-MM-dd");
                Response.Cookies.Add(CreateUserCookie(response.CustomerName));
                Session["CustomerID"] = response.CustomerID;
                return RedirectToAction("Index", "Home", "");
            }
            else
            {
                TempData.Add("Error","Wrong username or password");
                return RedirectToAction("Login", "Authentication", "");
            }*/
            return RedirectToAction("Login", "Authentication", "");
        }

        [HttpPost]
        public ActionResult RegisterUser(string dto)
        {


            return RedirectToAction("Login", "Authentication", "");
        }

        [HttpPost]
        public ActionResult RegisterCompany(string dto)
        {


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
        public ActionResult GetCompanyLogin(string username, string password)
        {
           
            if ("" != null)
            {
                //Response.Cookies.Add(CreateUserCookie(response.CompanyID.ToString()));
                //Session["CompanyID"] = response.CompanyID;
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