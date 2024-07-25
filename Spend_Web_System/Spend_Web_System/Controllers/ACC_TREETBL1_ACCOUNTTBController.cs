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
    public class ACC_TREETBL1_ACCOUNTTBController : Controller
    {
        [HttpGet]
       

        public ActionResult Index(int id)
        {
            ViewBag.Tree_No = id;

            ACC_TREETBL1_ACCOUNTTB_Business b = new ACC_TREETBL1_ACCOUNTTB_Business();

            List<ACC_TREETBL1_ACCOUNTTB_Model> model = new List<ACC_TREETBL1_ACCOUNTTB_Model>();

            model = b.getall().Where(x =>x.ACC_TREE1_NO==id).OrderByDescending(x => x.ID).ToList();
            
            return View(model);

        }


        [HttpGet]
       
        public ActionResult Create(long id)
        {
            ACC_TREETBL1_Business tREETBL1_Business= new ACC_TREETBL1_Business();
            ALL_ACC_NOTBL_Business AllAcc = new ALL_ACC_NOTBL_Business();
           
            ViewBag.AllAcc = AllAcc.getall().Where(x=>x.OP_ACC==true).OrderBy(x=>x.ACC_NO.ToString());
            ViewBag.TreeName = tREETBL1_Business.GetPyID(id).ACC_NAME;
            ViewBag.TreeNo = tREETBL1_Business.GetPyID(id).ACC_NO;
          
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(ACC_TREETBL1_ACCOUNTTB_Model model, string ACC_NO_LIST)
        {
            if (ModelState.IsValid)
            {
                int reusrt = 0;
                if (ACC_NO_LIST.Length > 0)
                {
                    var listUltist = ACC_NO_LIST.Split(',');

                    foreach (var item in listUltist)
                    {
                        ACC_TREETBL1_ACCOUNTTB_Business aCC_TREETBL1_ACCOUNTTB_Business = new ACC_TREETBL1_ACCOUNTTB_Business();

                        model.ACC_NO = Convert.ToInt64(item);
                        reusrt = aCC_TREETBL1_ACCOUNTTB_Business.Create(model);
                    }

                }

                if (reusrt > 0)
                {
                    return RedirectToAction("Index", new { id = model.ACC_TREE1_NO });
                }


            }

            ACC_TREETBL1_Business tREETBL1_Business = new ACC_TREETBL1_Business();
            ALL_ACC_NOTBL_Business AllAcc = new ALL_ACC_NOTBL_Business();

            ViewBag.AllAcc = AllAcc.getall().Where(x => x.OP_ACC == true).OrderBy(x => x.ACC_NO.ToString());
            ViewBag.TreeName = tREETBL1_Business.GetPyID(model.ACC_NO).ACC_NAME;
            ViewBag.TreeNo = tREETBL1_Business.GetPyID(model.ACC_NO).ACC_NO;

            return View(model);
        }

        [HttpGet]
       
        public ActionResult Edit(int id)
        {
           
            ALL_ACC_NOTBL_Business AllAcc = new ALL_ACC_NOTBL_Business();

            


            ACC_TREETBL1_ACCOUNTTB_Business acC_TREETBL1_ACCOUNTTB_Business = new ACC_TREETBL1_ACCOUNTTB_Business();

            var model = acC_TREETBL1_ACCOUNTTB_Business.GetPyID(id);
          

            ViewBag.AllAcc = AllAcc.getall().Where(x => x.OP_ACC == true).OrderBy(x => x.ACC_NO.ToString());

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ACC_TREETBL1_ACCOUNTTB_Model model)
        {
            if (ModelState.IsValid)
            {
                ACC_TREETBL1_ACCOUNTTB_Business acC_TREETBL1_ACCOUNTTB_Business = new ACC_TREETBL1_ACCOUNTTB_Business();

                int reusrt = acC_TREETBL1_ACCOUNTTB_Business.Update(model);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index",new { id = model.ACC_TREE1_NO });
                }


            }
            ACC_TREETBL1_Business tREETBL1_Business = new ACC_TREETBL1_Business();
            ALL_ACC_NOTBL_Business AllAcc = new ALL_ACC_NOTBL_Business();

            ViewBag.TreeName = tREETBL1_Business.GetPyID(model.ACC_NO).ACC_NAME;
            ViewBag.TreeNo = tREETBL1_Business.GetPyID(model.ACC_NO).ACC_NO;

            ViewBag.AllAcc = AllAcc.getall();

            return View(model);
        }


        
        public bool delete(int? id)
        {
            bool return_result = false;
            ACC_TREETBL1_ACCOUNTTB_Business acC_TREETBL1_ACCOUNTTB_Business = new ACC_TREETBL1_ACCOUNTTB_Business();

            int result = acC_TREETBL1_ACCOUNTTB_Business.delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
    }
}