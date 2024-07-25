using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Spend.Business;
using Spend.Models;


namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class CREDENCEController : Controller
    {
        // GET: CREDENCET

        [HttpGet]
        [Authorize(Roles = "CREDENCETBL_Select")]
        public ActionResult Index(int? Page, string search)
        {
            

            CREDENCETBL_Business b = new CREDENCETBL_Business();

            List<CREDENCETBL_Model> model = new List<CREDENCETBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.CRED_NAME.Contains(search)).OrderByDescending(x => x.ID_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.ID_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


       





        [HttpGet]
        [Authorize(Roles = "CREDENCETBL_Create")]
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CREDENCETBL_Create")]
        public ActionResult Create(CREDENCETBL_Model model)
        {
            if (ModelState.IsValid)
            {
                CREDENCETBL_Business cREDENCETBL_B = new CREDENCETBL_Business();
                int reusrt = cREDENCETBL_B.Create(model);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }


            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "CREDENCETBL_Update")]
        public ActionResult Edit(int id)
        {
            CREDENCETBL_Business cREDENCETBL_B = new CREDENCETBL_Business();
            var model = cREDENCETBL_B.GetPyID(id);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "CREDENCETBL_Update")]
        public ActionResult Edit(CREDENCETBL_Model model)
        {
            if (ModelState.IsValid)
            {
                CREDENCETBL_Business cREDENCETBL_B = new CREDENCETBL_Business();
                int reusrt = cREDENCETBL_B.Update(model);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }


            }
            return View(model);
        }
        [Authorize(Roles = "CREDENCETBL_Deleted")]
        public bool delete(int? id)
        {
            bool return_result = false;
            CREDENCETBL_Business cREDENCETBL_B = new CREDENCETBL_Business();

            int result = cREDENCETBL_B.delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
    }
}