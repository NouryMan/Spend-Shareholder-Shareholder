using Spend.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class ProvidersController : Controller
    {
        // GET: Providers
        public ActionResult Index()
        {
            
           
            return View();
        }
    }
}