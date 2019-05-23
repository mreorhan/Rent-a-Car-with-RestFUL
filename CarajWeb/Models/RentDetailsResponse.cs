using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarajWeb.Models
{
    public class RentDetailsResponse
    {
        public int RentalRequestID { get; set; }
        public int CustomerID { get; set; } //TODO:FOR Details
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public DateTime CustomerBirthDate { get; set; }
        public int CarID { get; set; } //For Details
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
    }
}