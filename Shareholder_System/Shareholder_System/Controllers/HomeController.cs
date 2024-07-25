using Microsoft.AspNet.Identity;
using Spend.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Shareholder_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
         

            SALES_INVDTTBL_Business b1 = new SALES_INVDTTBL_Business();
            var t1 = b1.getall();



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