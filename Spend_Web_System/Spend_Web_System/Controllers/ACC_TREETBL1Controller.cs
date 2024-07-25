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
    public class ACC_TREETBL1Controller : Controller
    {
      

        public ActionResult Index(int? Page, string search)
        {

            ACC_TREETBL1_Business b = new ACC_TREETBL1_Business();

            List<ACC_TREETBL1_Model> model = new List<ACC_TREETBL1_Model>();


            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.ACC_NO.ToString().Contains(search) || (x.ACC_NAME!=null && x.ACC_NAME.Contains(search))).OrderBy(x => x.ACC_NO.ToString()).ToList();
            }
            else
            {
                model = b.getall().OrderBy(x => x.ACC_NO.ToString()).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }

        [HttpGet]
        public ActionResult Create()
        {
            ACC_TREETBL1_Business b = new ACC_TREETBL1_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();

            ViewBag.Parent_ACC = b.getall().Where(x=>x.OP_ACC==false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


       //    var model = b.getall();


            return View();
        }


        [HttpPost]
        public ActionResult Create(ACC_TREETBL1_Model model)
        {
           
            model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
            ACC_TREETBL1_Business b = new ACC_TREETBL1_Business();

            try
            {
               long reusert= b.Create(model);
                if (reusert > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }
            
               
                ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
                ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
                ViewBag.Parent_ACC = b.getall().Where(x => x.OP_ACC == false);
                ViewBag.ACC_TYPE = bT.getall();
                ViewBag.ACC_NAT = bN.getall();
                return View();
            
        }








                [HttpGet]
       
        public JsonResult getAcc_NO(int parent_id)
        {
            ACC_TREETBL1_Business b = new ACC_TREETBL1_Business();
            string Acc_NO="001";
            try
            {
                Acc_NO = (b.getPayParent_acc(parent_id).Max(x => x.ACC_NO) + 1).ToString().Substring(b.GetPyID(parent_id).ACC_NO.ToString().Length);
            }
            catch
            {

            }
           
            return Json(Acc_NO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TrialBalance(long? AccounNo, int? Project, int? Transe, DateTime? FromDate, DateTime? ToDate, int level = 1)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
            ViewBag.Transe = Transe;
            ViewBag.level = level;
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ACC_TREETBL1_Business AllAcc_b = new ACC_TREETBL1_Business();
            ViewBag.AllAcc = AllAcc_b.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

          
            var model = AllAcc_b.TrialBalance(AccounNo, Project, Transe, FromDate, ToDate).Where(x => x.ACC_LEVEL <= level);


            return View(model);
        }


    }
}
