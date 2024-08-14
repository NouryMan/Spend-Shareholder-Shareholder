using Microsoft.AspNet.Identity;
using Spend.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Contributions_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        public HomeController()
        {
           
            nfi.CurrencyDecimalDigits = 2;
            nfi.NumberDecimalDigits = 2;
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencySymbol = "";
            nfi.NegativeSign = "-";
            nfi.NumberNegativePattern = 1;
            nfi.CurrencyNegativePattern = 1;

        }

        [Authorize]

        public ActionResult Index()
        {
         

            //SALES_INVDTTBL_Business b1 = new SALES_INVDTTBL_Business();
            //var t1 = b1.getall();
             ACC_HOLDERTBL_Business accHoldeB = new ACC_HOLDERTBL_Business();
            var holders = accHoldeB.getall();
            ViewBag.AccHolder = holders.Count();
            ViewBag.Balence = holders.Sum(x=>x.BALANCE).ToString("C", nfi);

            ACCH_PROJECT_Business ProjectB = new ACCH_PROJECT_Business();
            ViewBag.Project = ProjectB.GetAllAsync(1).Result.Count();

            USERSTBL_Business userB = new USERSTBL_Business();
            ViewBag.User = userB.getall().Count();


            return View();
        }

       
        public ActionResult About()
        {
          
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}