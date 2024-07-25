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
    public class SALES_CUSTController : Controller
    {

        public ActionResult Index(int? Page, string search)
        {

            SALES_CUSTTBL_Business b = new SALES_CUSTTBL_Business();

            List<SALES_CUSTTBL_Model> model = new List<SALES_CUSTTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.CUST_NAME_AR.Contains(search) || x.CUST_ACC.ToString().Contains(search)).OrderByDescending(x => x.CUST_ID).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.CUST_ID).ToList();
            }



            int pageSize = 40;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
          
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
           

            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
       
            return View();
        }


        [HttpPost]
        public ActionResult Create( SALES_CUSTTBL_Model model)
        {
            SALES_CUSTTBL_Business b = new SALES_CUSTTBL_Business();

            if (ModelState.IsValid)
            {
                
                try
                {
                    if (b.getall().Any(x => x.CUST_NAME_AR == model.CUST_NAME_AR ||(x.MOBILE_NO!=null && x.MOBILE_NO == model.MOBILE_NO)))
                    {
                        ModelState.AddModelError("", "العميل موجود من قبل.");
                    }
                    else
                    {
                        model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                        long reusert = b.Create(model);

                        if (reusert == 10001001)
                        {
                            ModelState.AddModelError("", "رقم الحساب موجود من قبل.");
                        }
                        else if (reusert > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch
                {

                }
            }


            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
           
            return View(model);

        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            SALES_CUSTTBL_Business b=new SALES_CUSTTBL_Business();
            var model = b.GetPyID(id);

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            model.Acc_NOString=model.CUST_ACC.ToString().Substring(model.CUST_ACC_PARENT.ToString().Length);
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(SALES_CUSTTBL_Model model)
        {
            SALES_CUSTTBL_Business b = new SALES_CUSTTBL_Business();

            if (ModelState.IsValid)
            {

                try
                {
                    model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
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
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);

            return View(model);

        }




        [HttpGet]
        public ActionResult Details(long id)
        {
            SALES_CUSTTBL_Business b = new SALES_CUSTTBL_Business();
            var model = b.GetPyID(id);

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            model.Acc_NOString = model.CUST_ACC.ToString().Substring(model.CUST_ACC_PARENT.ToString().Length);
            return View(model);
        }
        [HttpGet]
      



        public JsonResult getAcc_NO(long parent_id)
        {
             SALES_CUSTTBL_Business b= new SALES_CUSTTBL_Business();
            string Acc_NO = "0001";
            try
            {
                Acc_NO = (b.GetbyParentAcc(parent_id).Max(x => x.CUST_ACC) + 1).ToString().Substring(parent_id.ToString().Length);
            }
            catch
            {

            }

            return Json(Acc_NO, JsonRequestBehavior.AllowGet);
        }

        public bool delete(long? id)
        {
            bool return_result = false;
            SALES_CUSTTBL_Business b = new SALES_CUSTTBL_Business();

            int result = b.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
        public ActionResult Synchronization()
        {
            SALES_CUSTTBL_Business b = new SALES_CUSTTBL_Business();
            var reusert = b.Synchronization();

            ViewBag.msg = " تم مزامنة " + reusert + " بنجاح";
            return RedirectToAction("Index");
        }
    }
}