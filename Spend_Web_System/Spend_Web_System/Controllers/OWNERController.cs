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
    public class OWNERController : Controller
    {

        public ActionResult Index(int? Page, string search)
        {

            OWNERTBL_Business b = new OWNERTBL_Business();

            List<OWNERTBL_Model> model = new List<OWNERTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.OWNER_NAME.Contains(search)).OrderByDescending(x => x.OWNER_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.OWNER_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        public ActionResult Create()
        {
            OWNERTBL_Business b = new OWNERTBL_Business();
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
           
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();




            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "ACC_NO,PARENT_ACC, ACC_TYPE, ACC_NAT,GENDER,IS_ENABLE,NATIONALITY" +
                                                   "NOTE,OP_ACC,NOTE,OWNER_NAME,PARENT_ACC")] OWNERTBL_Model model)
        {
            OWNERTBL_Business b = new OWNERTBL_Business();

            if (ModelState.IsValid)
            {
               
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
            }


            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            OWNERTBL_Business b = new OWNERTBL_Business();

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();

            var model = b.GetPyID(id);


            return View(model);
        }


        [HttpPost]
        public ActionResult Edit( OWNERTBL_Model model)
        {
            OWNERTBL_Business b = new OWNERTBL_Business();

            if (ModelState.IsValid)
            {

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
            }


            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
         
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            return View(model);

        }




        [HttpGet]
        public ActionResult Details(int id)
        {
            OWNERTBL_Business b = new OWNERTBL_Business();

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();

            var model = b.GetPyID(id);


            return View(model);
        }




        public JsonResult getAcc_NO(int parent_id)
        {
            OWNERTBL_Business b = new OWNERTBL_Business();
            string Acc_NO = "001";
            try
            {
                Acc_NO = (b.getPayParent_acc(parent_id).Max(x => x.ACC_NO) + 1).ToString().Substring(parent_id.ToString().Length);
            }
            catch
            {

            }

            return Json(Acc_NO, JsonRequestBehavior.AllowGet);
        }



        public bool delete(int? id)
        {
            bool return_result = false;
            OWNERTBL_Business ownerB = new OWNERTBL_Business();

            int result = ownerB.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
    }
}