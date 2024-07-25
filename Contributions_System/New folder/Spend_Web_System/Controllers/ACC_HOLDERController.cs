using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    public class ACC_HOLDERController : Controller
    {
        // GET: ACC_HOLDERTBL
        public ActionResult Index()
        {
            ACC_HOLDERTBL_Business  b=new ACC_HOLDERTBL_Business();
            var model = b.getall();
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ACC_HOLDERTBL_Business PARENT_ACCH_B = new ACC_HOLDERTBL_Business();

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = PARENT_ACCH_B.getall();




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
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = b.getall();



            return View(model);

        }

        [HttpGet]
        public ActionResult Update(long id)
        {

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ACC_HOLDERTBL_Business PARENT_ACCH_B = new ACC_HOLDERTBL_Business();

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = PARENT_ACCH_B.getall();
            var model = PARENT_ACCH_B.GetPyID(id);



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
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = b.getall();



            return View(model);

        }


        [HttpGet]
        public ActionResult Details(long id)
        {

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ACC_HOLDERTBL_Business PARENT_ACCH_B = new ACC_HOLDERTBL_Business();

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = PARENT_ACCH_B.getall();
            var model = PARENT_ACCH_B.GetPyID(id);



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



      



        }
}