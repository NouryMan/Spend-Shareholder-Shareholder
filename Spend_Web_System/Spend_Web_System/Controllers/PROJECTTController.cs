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
    public class PROJECTTController : Controller
    {
        public ActionResult Index(int? Page, string search)
        {

            PROJECTTBL_Business b = new PROJECTTBL_Business();

            List<PROJECTTBL_Model> model = new List<PROJECTTBL_Model>();


            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.PROJECT_NAME.Contains(search) ).OrderByDescending(x => x.PROJECT_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.PROJECT_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        public ActionResult Create()
        {
            PROJECTTBL_Business b = new PROJECTTBL_Business();
            ENGINEERTBL_Business B_Eng = new ENGINEERTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Engineer = B_Eng.getall();
            ViewBag.Project_NO = b.GetMaxID();
           


            var model = b.getall();


            return View();
        }


        [HttpPost]
        public ActionResult Create(PROJECTTBL_Model model)
        {
           
            PROJECTTBL_Business b = new PROJECTTBL_Business();

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

            ENGINEERTBL_Business B_Eng = new ENGINEERTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Engineer = B_Eng.getall();
            ViewBag.Project_NO = b.GetMaxID();

            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PROJECTTBL_Business b = new PROJECTTBL_Business();
            ENGINEERTBL_Business B_Eng = new ENGINEERTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Engineer = B_Eng.getall();
            



            var model = b.GetByID(id);


            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(PROJECTTBL_Model model)
        {

            PROJECTTBL_Business b = new PROJECTTBL_Business();

            try
            {
                long reusert = b.Update(model);
                if (reusert > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }

            ENGINEERTBL_Business B_Eng = new ENGINEERTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Engineer = B_Eng.getall();
          

            return View(model);

        }



        [HttpGet]
        public ActionResult Details(int id)
        {
            PROJECTTBL_Business b = new PROJECTTBL_Business();
            ENGINEERTBL_Business B_Eng = new ENGINEERTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Engineer = B_Eng.getall();




            var model = b.GetByID(id);


            return View(model);
        }

        public bool delete(int? id)
        {
            bool return_result = false;
            PROJECTTBL_Business projectB = new PROJECTTBL_Business();

            int result = projectB.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }

    }
}