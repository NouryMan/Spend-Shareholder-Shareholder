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
    public class PERSON_ACCTBLController : Controller
    {

        public ActionResult Index(int ? Type)
        {

            PERSON_ACCTBL_Business b = new PERSON_ACCTBL_Business();

            List<PERSON_ACCTBL_Model> model = new List<PERSON_ACCTBL_Model>();

           
                if (Type > 0) 
                {
                    if (Type == 4)
                    {
                        model = b.getall().Where(x => (x.PERSON_DESC_ID == 2 || x.PERSON_DESC_ID == 3 || x.PERSON_DESC_ID == 4 || x.PERSON_DESC_ID == 9 || x.PERSON_DESC_ID == 10  ) ).OrderByDescending(x => x.ID).ToList();

                    }
                    else
                    {
                        model = b.getall().Where(x => x.PERSON_DESC_ID == Type ).OrderByDescending(x => x.ID).ToList();
                    }
                }
                else
                {
                    model = b.getall().OrderByDescending(x => x.ID).ToList();
                }
            
           
            return View(model);
        }



        [HttpGet]
        [Authorize(Roles = "User_Create")]
        public ActionResult Create(int?id)
        {

            PERSONAL_INFOTBL_Business pERSONAL_B = new PERSONAL_INFOTBL_Business();
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            PERSON_DESCRIPTIONTBL_Business bd = new PERSON_DESCRIPTIONTBL_Business();

            ViewBag.personalinfo = pERSONAL_B.GetAll();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();

            if (id > 0)
            {
                if (id == 2 ||id==3 ||id==4||id==9||id==10)
                {
                    ViewBag.PERSON_DESCRIPTIONTBL = bd.getall().Where(x => x.ID == 2 || x.ID == 3 || x.ID == 4 || x.ID == 9 || x.ID == 10);
                }
                else
                {
                    ViewBag.PERSON_DESCRIPTIONTBL = bd.getall().Where(x => x.ID == id);
                }
            }
            else
            {
                ViewBag.PERSON_DESCRIPTIONTBL = bd.getall();
            }




            return View();

        }

        [HttpPost]

        public ActionResult Create(PERSON_ACCTBL_Model Acc)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                try
                {
                    PERSON_ACCTBL_Business b = new PERSON_ACCTBL_Business();
                    Acc.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                   int   reusert= b.CreateWithPesonal(Acc);

                    if (reusert == 10001001)
                    {
                        ModelState.AddModelError("", "رقم الحساب موجود من قبل.");
                    }
                    else if (reusert > 0)
                    {

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "حدث خطاء");
                }


            }
            PERSONAL_INFOTBL_Business pERSONAL_B = new PERSONAL_INFOTBL_Business();
            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            PERSON_DESCRIPTIONTBL_Business bd = new PERSON_DESCRIPTIONTBL_Business();

            ViewBag.personalinfo = pERSONAL_B.GetAll();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PERSON_DESCRIPTIONTBL = bd.getall();

            return View();

        }

        public JsonResult getPersondeteiles(int id)
        {
            PERSONAL_INFOTBL_Business pERSONAL_B = new PERSONAL_INFOTBL_Business();

            var personal = pERSONAL_B.GetByID(id);
            string[] data = { personal.AR_NAME, personal.EN_NAME, personal.C_ID, personal.ADDRESS1, personal.MOBILE_NO, personal.EMAIL_ADDRESS };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAcc_NO(int parent_id)
        {
            PERSON_ACCTBL_Business b = new PERSON_ACCTBL_Business();
            string Acc_NO = "000001";
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