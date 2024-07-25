using Microsoft.AspNet.Identity;
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
    public class BANKController : Controller
    {
        public ActionResult Index()
        {

            BANKTBL_Business b = new BANKTBL_Business();

            List<BANKTBL_Model> model = new List<BANKTBL_Model>();

            
                model = b.getall().OrderByDescending(x => x.BANK_NO).ToList();
            



           
            return View(model);

        }


        [HttpGet]
        public ActionResult Create()
        {
            BANKTBL_Business b = new BANKTBL_Business();
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();




            return View();
        }


        [HttpPost]
        public ActionResult Create(BANKTBL_Model model)
        {
            BANKTBL_Business b = new BANKTBL_Business();
            if (ModelState.IsValid)
            {

               

                try
                {
                    model.USER_CR= Convert.ToInt16(User.Identity.GetUserId());
                    long reusert = b.Create(model);

                    if (reusert == 10001001)
                    {
                        ModelState.AddModelError("", "رقم الحساب موجود من قبل.");
                    }else if (reusert > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
            }

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
             OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BANKTBL_Business b = new BANKTBL_Business();
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            var model = b.GetPyID(id);


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BANKTBL_Model model)
        {
            BANKTBL_Business b = new BANKTBL_Business();
            if (ModelState.IsValid)
            {


                model.USER_UP= Convert.ToInt16(User.Identity.GetUserId());
                try
                {

                    long reusert = b.Update(model);
                    if (reusert > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
            }

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            return View(model);

        }


        public ActionResult Details(int id)
        {
            BANKTBL_Business b = new BANKTBL_Business();
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();

            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            var model = b.GetPyID(id);


            return View(model);
        }



        [HttpGet]
        public JsonResult getAcc_NO(int parent_id)
        {
             BANKTBL_Business b= new BANKTBL_Business();
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
            BANKTBL_Business BANKTBL_B = new BANKTBL_Business();

            int result = BANKTBL_B.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }

    }
}