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
    public class USER_UTLIST_PROJECTController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "USER_UTLIST_PROJECT_Select")]
       
        public ActionResult Index(int? Page, string search)
        {

            USER_UTLIST_PROJECTTBL_Business b = new USER_UTLIST_PROJECTTBL_Business();

            List<USER_UTLIST_PROJECTTBL_Model> model = new List<USER_UTLIST_PROJECTTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.NOTE.Contains(search) || x.UTLISTTBL_Model.UTL_NAME.Contains(search) || x.USERSTBL_Model.PERSONAL_INFOTBL_Model.AR_NAME.Contains(search) || x.PROJECTTBL_Model.PROJECT_NAME.Contains(search)).OrderByDescending(x => x.ID).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.ID).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        [Authorize(Roles = "USER_UTLIST_PROJECT_Create")]
        public ActionResult Create()
        {
            USERSTBL_Business user = new USERSTBL_Business();
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            ViewBag.user = user.getall();
            ViewBag.project = projb.getall();
            ViewBag.utlist = utl_b.getall();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "USER_UTLIST_PROJECT_Create")]
        public ActionResult Create(USER_UTLIST_PROJECTTBL_Model model,string UTLIST_LIST)
        {
            if (ModelState.IsValid)
            {
                int reusrt = 0;
                if (UTLIST_LIST.Length > 0)
                {
                  var listUltist=  UTLIST_LIST.Split(',');
                  
                    foreach (var item  in listUltist )
                    {
                        USER_UTLIST_PROJECTTBL_Business USER_UTLIST_PROJECTTBL_B = new USER_UTLIST_PROJECTTBL_Business();

                        model.UTLIST_ID =Convert.ToInt32(item);
                        reusrt = USER_UTLIST_PROJECTTBL_B.Create(model);
                    }

                }
               
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }


            }
            USERSTBL_Business user = new USERSTBL_Business();
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            ViewBag.user = user.getall();
            ViewBag.project = projb.getall();
            ViewBag.utlist = utl_b.getall();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "USER_UTLIST_PROJECT_Update")]
        public ActionResult Edit(int id)
        {
            USERSTBL_Business user = new USERSTBL_Business();
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            ViewBag.user = user.getall();
            ViewBag.project = projb.getall();
            ViewBag.utlist = utl_b.getall();

            USER_UTLIST_PROJECTTBL_Business USER_UTLIST_PROJECTTBL_B = new USER_UTLIST_PROJECTTBL_Business();
            var model = USER_UTLIST_PROJECTTBL_B.GetPyID(id);
            return View(model);
        }
        [Authorize(Roles = "USER_UTLIST_PROJECT_Update")]
        public ActionResult Edit(USER_UTLIST_PROJECTTBL_Model model)
        {
            if (ModelState.IsValid)
            {
                USER_UTLIST_PROJECTTBL_Business USER_UTLIST_PROJECTTBL_B = new USER_UTLIST_PROJECTTBL_Business();
                int reusrt = USER_UTLIST_PROJECTTBL_B.Update(model);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }


            }
            USERSTBL_Business user = new USERSTBL_Business();
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            ViewBag.user = user.getall();
            ViewBag.project = projb.getall();
            ViewBag.utlist = utl_b.getall();
            return View(model);
        }
        [Authorize(Roles = "USER_UTLIST_PROJECT_Deleted")]
        public bool delete(int? id)
        {
            bool return_result = false;
            USER_UTLIST_PROJECTTBL_Business USER_UTLIST_PROJECTTBL_B = new USER_UTLIST_PROJECTTBL_Business();

            int result = USER_UTLIST_PROJECTTBL_B.delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
    }
}