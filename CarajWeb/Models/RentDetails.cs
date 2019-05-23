using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarajWeb.Models
{
    public class RentDetails
    {
        public int CustomerID { get; set; }
        public int CarID { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public int StartingKilometer { get; set; }
        public int EndingKilometer { get; set; }
        public decimal Pricing { get; set; }
    }
}