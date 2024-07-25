using Microsoft.AspNet.Identity;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shareholder_System.Controllers
{
    public class FISCAL_YEARController : Controller
    {
        // GET: FISCAL_YEAR
        public ActionResult Index()
        {
            FISCAL_YEAR_Business fISCAL_YEAR_Business = new FISCAL_YEAR_Business(); 
            var model=fISCAL_YEAR_Business.GetAll();
            return View(model);
        }


        public ActionResult Create()
        {
          
            return View();
        }


        [HttpPost]
        public ActionResult Create(FISCAL_YEAR_Model model)
        {
            if (ModelState.IsValid)
            {
                model.USER_CR =Convert.ToInt16(User.Identity.GetUserId());
                FISCAL_YEAR_Business fiscalYearBusiness = new FISCAL_YEAR_Business();
               int reusert= fiscalYearBusiness.Create(model);
                if (reusert > 0)
                {
                  
                    return RedirectToAction("Index");
                }

            }

            ViewBag.msg = "يجب تعبئة جميع الحقول المطلوبة";
            return View(model);
        }

        public ActionResult Update(int id)
        {
            FISCAL_YEAR_Business fiscalYearBusiness = new FISCAL_YEAR_Business();
            var model = fiscalYearBusiness.GetPyID(id);
            return View(model);
        }


        [HttpPost]
        public ActionResult Update(FISCAL_YEAR_Model model)
        {
            if (ModelState.IsValid)
            {
                model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
                FISCAL_YEAR_Business fiscalYearBusiness = new FISCAL_YEAR_Business();
                int reusert = fiscalYearBusiness.Update(model);
                if (reusert > 0)
                {

                    return RedirectToAction("Index");
                }

            }

            ViewBag.msg = "يجب تعبئة جميع الحقول المطلوبة";
            return View(model);
        }

        public bool delete(int? id)
        {
            bool return_result = false;
            var b = new FISCAL_YEAR_Business();

            int result = b.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }

            return return_result;
        }
    }
}