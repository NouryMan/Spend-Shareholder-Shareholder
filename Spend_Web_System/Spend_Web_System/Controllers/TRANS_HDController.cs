using Spend.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    public class TRANS_HDController : Controller
    {
        // GET: TRANS_HD
        public ActionResult Index()
        {
            TRANS_HD_Business b = new TRANS_HD_Business();
            var model = b.GetAll().OrderByDescending(x=>x.JOURNAL_NO);
            return View(model);
        }
       
        public ActionResult Details(int id)
        {
            TRANS_HD_Business b = new TRANS_HD_Business();
           
            var model = b.GetPyID(id);
            return View(model);
        }
        public ActionResult TransPrint(int? id)
        {
            if (id != null && id > 0)
            {
                TRANS_HD_Business trb = new TRANS_HD_Business();
              
                return PartialView("_TransPrint", trb.GetPyID(id.Value));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "");

        }
    }
}