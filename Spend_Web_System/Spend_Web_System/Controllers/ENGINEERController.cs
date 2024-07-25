using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class ENGINEERController : Controller
    {
        public ActionResult Index(int? Page, string search)
        {

            ENGINEERTBL_Business b = new ENGINEERTBL_Business();

            List<ENGINEERTBL_Model> model = new List<ENGINEERTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.ENG_NAME.Contains(search)).OrderByDescending(x => x.ENG_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.ENG_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        public ActionResult Create()
        {
            ENGINEERTBL_Business b = new ENGINEERTBL_Business();
           
            ViewBag.Eng_NO = b.GetMaxID();



          

            return View();
        }


        [HttpPost]
        public ActionResult Create(ENGINEERTBL_Model model)
        {

            ENGINEERTBL_Business b = new ENGINEERTBL_Business();

            try
            {
                long reusert = b.Create(model);
                if (reusert > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }


            ViewBag.Eng_NO = b.GetMaxID();



            return View(model);

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ENGINEERTBL_Business b = new ENGINEERTBL_Business();
            var model=b.GetPyID(id);

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ENGINEERTBL_Model model)
        {

            ENGINEERTBL_Business b = new ENGINEERTBL_Business();

            try
            {
                long reusert = b.Update(model);
                if (reusert > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch{ }

            return View(model);

        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            ENGINEERTBL_Business b = new ENGINEERTBL_Business();
            var model = b.GetPyID(id);

            return View(model);
        }
        public bool delete(int? id)
        {
            bool return_result = false;
            ENGINEERTBL_Business engB = new ENGINEERTBL_Business();

            int result = engB.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
    }
}