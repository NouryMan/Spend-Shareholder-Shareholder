﻿using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contributions_System.Controllers
{
    [Authorize]
    public class ACC_HOLDERController : Controller
    {
        // GET: ACC_HOLDERTBL
        public ActionResult Index()
        {
            //ACCH_BALANCEV_Business n = new ACCH_BALANCEV_Business();
            //var d =n.GetAll();
            ACC_HOLDERTBL_Business  b=new ACC_HOLDERTBL_Business();
            var model = b.getall().Where(x => x.ACCH_TYPE == 1).ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ACC_HOLDERTBL_Business PARENT_ACCH_B = new ACC_HOLDERTBL_Business();
            ACCH_TYPETBL_Business aCCH_TYPETBL_Business = new ACCH_TYPETBL_Business();

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall() ;
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = PARENT_ACCH_B.getall().Where(x => x.ACCH_TYPE == 1).ToList(); 
            ViewBag.ACCH_TYPE = aCCH_TYPETBL_Business.GetAll().Where(x => x.ID == 1).ToList();




            return View();
        }


        [HttpPost]
        public ActionResult Create(ACC_HOLDERTBL_Model model)
        {
            ACC_HOLDERTBL_Business b = new ACC_HOLDERTBL_Business();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

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
            ACCH_TYPETBL_Business aCCH_TYPETBL_Business = new ACCH_TYPETBL_Business();

            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = b.getall().Where(x => x.ACCH_TYPE == 1).ToList();
            ViewBag.ACCH_TYPE = aCCH_TYPETBL_Business.GetAll().Where(x => x.ID == 1).ToList();


            return View(model);

        }

        [HttpGet]
        public ActionResult Update(long id)
        {

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ACC_HOLDERTBL_Business PARENT_ACCH_B = new ACC_HOLDERTBL_Business();
            ACCH_TYPETBL_Business aCCH_TYPETBL_Business = new ACCH_TYPETBL_Business();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = PARENT_ACCH_B.getall().Where(x => x.ACCH_TYPE == 1).ToList();
            var model = PARENT_ACCH_B.GetById(id);
            ViewBag.ACCH_TYPE = aCCH_TYPETBL_Business.GetAll().Where(x => x.ID == 1).ToList();



            return View(model);
        }


        [HttpPost]
        public ActionResult Update(ACC_HOLDERTBL_Model model)
        {
            ACC_HOLDERTBL_Business b = new ACC_HOLDERTBL_Business();
            
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
            ACCH_TYPETBL_Business aCCH_TYPETBL_Business = new ACCH_TYPETBL_Business();
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = b.getall().Where(x => x.ACCH_TYPE == 1).ToList();
            ViewBag.ACCH_TYPE = aCCH_TYPETBL_Business.GetAll().Where(x => x.ID == 1).ToList();



            return View(model);

        }


        [HttpGet]
        public ActionResult Details(long id)
        {

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ACC_HOLDERTBL_Business PARENT_ACCH_B = new ACC_HOLDERTBL_Business();
            ACCH_TYPETBL_Business aCCH_TYPETBL_Business = new ACCH_TYPETBL_Business();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = PARENT_ACCH_B.getall();
            var model = PARENT_ACCH_B.GetById(id);
            ViewBag.ACCH_TYPE = aCCH_TYPETBL_Business.GetAll();



            return View(model);
        }









        public JsonResult getAcc_NO(int parent_id)
        {
            ACC_HOLDERTBL_Business b = new ACC_HOLDERTBL_Business();
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


        [Authorize]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            bool result = false;
            ACC_HOLDERTBL_Business b = new ACC_HOLDERTBL_Business();

            try
            {
                b.Delete(id);
                result = true;
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = result });
        }

        }
}