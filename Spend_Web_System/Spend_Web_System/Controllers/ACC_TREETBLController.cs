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
    public class ACC_TREETBLController : Controller
    {
      

        public ActionResult Indexreport()
        {

            ACC_TREETBL_Business b = new ACC_TREETBL_Business();

            List<ACC_TREETBL_Model> model = new List<ACC_TREETBL_Model>();

     
            model = b.getall().OrderBy(x => x.ACC_NO.ToString()).ToList();
            return View(model); 

            //if (!String.IsNullOrEmpty(search))
            //{
            //    model = b.getall().Where(x => x.ACC_NO.ToString().Contains(search) || (x.ACC_NAME!=null && x.ACC_NAME.Contains(search))).OrderBy(x => x.ACC_NO.ToString()).ToList();
            //}
            //else
            //{
            //    model = b.getall().OrderBy(x => x.ACC_NO.ToString()).ToList();
            //}
            //int pageSize = 30;
            //int pageNumber = (Page ?? 1);
            //return View(model.ToPagedList(pageNumber, pageSize));



        }


        public ActionResult Index()
        {

            string s = "";
            ACC_TREETBL_Business lv = new ACC_TREETBL_Business();

            List<ACC_TREETBL_Model> blist = lv.getall().ToList();
            if (blist != null && blist.Count() > 0)
            {
                s = getTree(blist);
            }
            ViewBag.tree = s;
            return View();

        }


        private string getTree(List<ACC_TREETBL_Model> fulllst)
        {
            List<ACC_TREETBL_Model> rootList = fulllst.Where(x => x.ACC_PARENT == null).ToList();
            string s = "";
            foreach (ACC_TREETBL_Model b in rootList)
            {
                s = s + "<li id = " + b.ACC_NO + " ><span > " + b.ACC_NAME + " </span>";

                List<ACC_TREETBL_Model> nodList = fulllst.Where(x => x.ACC_PARENT == b.ACC_NO).ToList();
                if (nodList != null && nodList.Count() > 0)
                {
                    s = s + getNode(fulllst, nodList);
                }
                s = s + "</li>";
            }
            return s;
        }
        private string getNode(List<ACC_TREETBL_Model> fulllst, List<ACC_TREETBL_Model> blst)
        {
            string s = "<ul>";

            foreach (ACC_TREETBL_Model b in blst)
            {
                s = s + "<li id = " + b.ACC_NO + " ><span > " + b.ACC_NAME + " </span>";

                List<ACC_TREETBL_Model> nodList = fulllst.Where(x => x.ACC_PARENT == b.ACC_NO).ToList();
                if (nodList != null && nodList.Count() > 0)
                {
                    s = s + getNode(fulllst, nodList);
                }
                s = s + "</li>";

            }
            s = s + "</ul>";
            return s;
        }


        [HttpGet]
        public ActionResult Create()
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();

            ViewBag.Parent_ACC = b.getall().Where(x=>x.OP_ACC==false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


       //    var model = b.getall();


            return View();
        }


        [HttpPost]
        public ActionResult Create(ACC_TREETBL_Model model)
        {
          
           


            model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();

            try
            {
               long reusert= b.Create(model);
                if(reusert==10001001)
                {
                    ModelState.AddModelError("", "رقم الحساب موجود من قبل.");
                }
                else if (reusert > 0)
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

        public ActionResult Update(long id)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();

            ViewBag.Parent_ACC = b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            var model = b.GetPyID(id);


            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ACC_TREETBL_Model model)
        {

            model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();

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


            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ViewBag.Parent_ACC = b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            return View(model);

        }

        public ActionResult Details(long id)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();

            ViewBag.Parent_ACC = b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();


            var model = b.GetPyID(id);


            return View(model);
        }

        public bool delete(int? id)
        {
            bool return_result = false;
            ACC_TREETBL_Business ACC_TYPETBL_B = new ACC_TREETBL_Business();

            int result = ACC_TYPETBL_B.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }



        [HttpGet]
      
        public JsonResult getAcc_NO(int parent_id)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
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

    }
}
