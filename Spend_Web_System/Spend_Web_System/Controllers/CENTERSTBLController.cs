using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class CENTERSTBLController : Controller
    {
        public ActionResult Index(int? Page, string search)
        {

            CENTERSTBL_Business b = new CENTERSTBL_Business();

            List<CENTERSTBL_Model> model = new List<CENTERSTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.PROJECTTBL_Model.PROJECT_NAME.Contains(search)).OrderByDescending(x => x.ID).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.ID).ToList();
            }



            int pageSize = 10;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        public ActionResult Create()
        {
            CENTERSTBL_Business b = new CENTERSTBL_Business();

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            PROJECTTBL_Business Proj_b = new PROJECTTBL_Business();
            BOXTBL_Business Box_b = new BOXTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();
            MARKETINGTBL_Business Mark_b = new MARKETINGTBL_Business();
            BANKTBL_Business Bank_b = new BANKTBL_Business();
            VATTBL_Business Vat_b = new VATTBL_Business();

            ViewBag.project = Proj_b.getall();
            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Bank = Bank_b.getall();
            ViewBag.ACC = Acc_b.getall();
            ViewBag.Markting = Mark_b.getall();
            ViewBag.Box = Box_b.getall();
            ViewBag.Vat = Vat_b.getall();




            return View();
        }


        [HttpPost]
        public ActionResult Create(CENTERSTBL_Model model)
        {
            CENTERSTBL_Business b = new CENTERSTBL_Business();
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
                catch (Exception ex)
                {

                }
            }

            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            PROJECTTBL_Business Proj_b = new PROJECTTBL_Business();
            BOXTBL_Business Box_b = new BOXTBL_Business();
            OWNERTBL_Business B_Owner = new OWNERTBL_Business();
            MARKETINGTBL_Business Mark_b = new MARKETINGTBL_Business();
            BANKTBL_Business Bank_b = new BANKTBL_Business();
            VATTBL_Business Vat_b = new VATTBL_Business();

            ViewBag.project = Proj_b.getall();
            ViewBag.OWNER = B_Owner.getall();
            ViewBag.Bank = Bank_b.getall();
            ViewBag.ACC = Acc_b.getall();
            ViewBag.Markting = Mark_b.getall();
            ViewBag.Box = Box_b.getall();
            ViewBag.Vat = Vat_b.getall();


            return View(model);

        }

        [HttpPost]
        public string GetAccCenterForProj(long id)
        { 
          CENTERSTBL_Business cENTERSTBL=new CENTERSTBL_Business();
            var model= cENTERSTBL.GeCenterAccForProj(id);

           var json = new JavaScriptSerializer().Serialize(model);
            return json;
        }
    
    }
}