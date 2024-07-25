using Microsoft.AspNet.Identity;
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
    public class AgreementController : Controller
    {
        // GET: AGREEMENT
        [HttpGet]
        [Authorize(Roles = "AGREEMENT_Select")]
        public ActionResult Indexproject()
        {
            USERSTBL_Business userb = new USERSTBL_Business();
            USER_UTLIST_PROJECTTBL_Business uSER_UTLIST_B = new USER_UTLIST_PROJECTTBL_Business();
            
            var user = userb.GetPyID(Convert.ToInt32(User.Identity.GetUserId()));
            List<USER_UTLIST_PROJECTTBL_Model> model;
            if (User.IsInRole("AGREEMENT_Manager"))
            {
                model = uSER_UTLIST_B.getall().Where(x => x.IS_DELETED == false).ToList();

            }
            else
            {
                 model = user.USER_UTLIST_PROJECTTBL_Collection.Where(x => x.IS_DELETED == false).ToList();
            }



          
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "AGREEMENT_Select")]
        public ActionResult IndexUtlst(int id)
        {
            USERSTBL_Business userb = new USERSTBL_Business();
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            USER_UTLIST_PROJECTTBL_Business uSER_UTLIST_B = new USER_UTLIST_PROJECTTBL_Business();

            var user = userb.GetPyID(Convert.ToInt32(User.Identity.GetUserId()));

            ViewBag.project_name = projb.GetByID(id).PROJECT_NAME;
            List<USER_UTLIST_PROJECTTBL_Model> model = new List<USER_UTLIST_PROJECTTBL_Model>();
            if (User.IsInRole("AGREEMENT_Manager"))
            {
                try{
                    foreach (var item in uSER_UTLIST_B.getall().Where(x => x.IS_DELETED == false && x.PROJECT_ID == id).GroupBy(x => x.UTLIST_ID))
                    {

                        model.Add(item.FirstOrDefault());

                    } 
                }
                catch
                {

                }

            }
            else
            {
                model = user.USER_UTLIST_PROJECTTBL_Collection.Where(x => x.PROJECT_ID == id && x.IS_DELETED == false).ToList();
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "AGREEMENT_Select")]
        public ActionResult Index(int id, int project_id)
        {
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            USERSTBL_Business user = new USERSTBL_Business();
            int userid = Convert.ToInt32(User.Identity.GetUserId());
            var utlsproj = user.GetPyID(userid).USER_UTLIST_PROJECTTBL_Collection.Where(x => x.IS_DELETED == false);

            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            ViewBag.project_name = projb.GetByID(project_id).PROJECT_NAME;
            ViewBag.project_id = project_id;
            ViewBag.utlist_id = id;
            ViewBag.Utl_Name = utl_b.GetPyID(id).UTL_NAME;
            PERSON_ACC_PROJTBL_Business b = new PERSON_ACC_PROJTBL_Business();
            List<PERSON_ACC_PROJTBL_Model> model = new List<PERSON_ACC_PROJTBL_Model>();
            if (User.IsInRole("AGREEMENT_Manager"))
            {
                 model = b.getall().Where(x => x.UTL_NO == id && x.PROJECT_NO == project_id && x.IS_DELETED == false ).ToList();
              //  return View(model);



            }
                //التحقق انة يملك هذة الشغلة او لا
              else  if (utlsproj.Where(x => x.PROJECT_ID == project_id && x.UTLIST_ID == id).Count() > 0)
             {

             
             
                 model = b.getall().Where(x => x.UTL_NO == id && x.PROJECT_NO == project_id &&x.IS_DELETED==false).ToList();
               // return View(model);
              }
            else { return View(); }
            ACCOUNTTBL_Business acb = new ACCOUNTTBL_Business();
            foreach (var item in model)
            {
                item.PAY_AMOUNT = acb.GetPyProjectNoAndAccountNO(item.PROJECT_NO.Value, item.ACC_NO.Value).Sum(x => x.FROM_HIM).Value;
                item.REST_TOTAL = item.TOTAL_PRICE - (item.PAY_AMOUNT + item.NO_PAY_AMOUNT);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "AGREEMENT_Create")]
        public ActionResult Create(int id, int project_id)
        {
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            SUB_PROJTBL_Business subproj_b = new SUB_PROJTBL_Business();
            PERSON_ACCTBL_Business Provider_b = new PERSON_ACCTBL_Business();
            PARTTBL_Business Part_b = new PARTTBL_Business();
            UNITTBL_Business Unit_b = new UNITTBL_Business();
            UTL_TYPETBL_Business Utl_type_b = new UTL_TYPETBL_Business();
            ViewBag.project_name = projb.GetByID(project_id).PROJECT_NAME;
            ViewBag.project_id = project_id;
            ViewBag.utlist_id = id;
            ViewBag.Utl_Name = utl_b.GetPyID(id).UTL_NAME;
            ViewBag.Subproject = subproj_b.getall().ToList();
            ViewBag.Provider = Provider_b.getall().Where(x=>x.IS_ENABLED==true).ToList();
            ViewBag.Part_b = Part_b.getall().ToList();
            ViewBag.Unit_b = Unit_b.getall().ToList();
            ViewBag.Utl_type_b = Utl_type_b.getall().ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "AGREEMENT_Create")]
        public ActionResult Create(PERSON_ACC_PROJTBL_Model model)
        {
            if (ModelState.IsValid)
            {
                if (model.SUB_PROJ_NO == 0)
                {
                    model.SUB_PROJ_NO = null;
                }
                if (model.PART_NO == 0)
                {
                    model.PART_NO = null;
                }
                if (model.UNIT_NO == 0)
                {
                    model.UNIT_NO = null;
                }
                if (model.UTL_TYPE == 0)
                {
                    model.UTL_TYPE = null;
                }
               
                PERSON_ACC_PROJTBL_Business pERSON_ACC_PROJTBL_b = new PERSON_ACC_PROJTBL_Business();
                PERSON_ACCTBL_Business PERSON_ACCL_b = new PERSON_ACCTBL_Business();
                model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                var personal= PERSON_ACCL_b.GetByID(model.PERSON_ACC_ID.Value);
                model.ACC_NO = personal.ACC_NO;
                model.PERSON_ID = personal.PERSON_ID;
                model.IS_DELETED = false;

               int reusrt = pERSON_ACC_PROJTBL_b.Create(model);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index", new { id = model.UTL_NO, project_id = model.PROJECT_NO });
                }
                else
                {
                    PROJECTTBL_Business projb = new PROJECTTBL_Business();
                    UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
                    SUB_PROJTBL_Business subproj_b = new SUB_PROJTBL_Business();
                    PERSON_ACCTBL_Business Provider_b = new PERSON_ACCTBL_Business();
                    PARTTBL_Business Part_b = new PARTTBL_Business();
                    UNITTBL_Business Unit_b = new UNITTBL_Business();
                    UTL_TYPETBL_Business Utl_type_b = new UTL_TYPETBL_Business();
                    ViewBag.project_name = projb.GetByID(model.PROJECT_NO.Value).PROJECT_NAME;
                    ViewBag.project_id = model.PROJECT_NO;
                    ViewBag.utlist_id = model.UTL_NO;
                    ViewBag.Utl_Name = utl_b.GetPyID(model.UTL_NO.Value).UTL_NAME;
                    ViewBag.Subproject = subproj_b.getall().ToList();
                    ViewBag.Provider = Provider_b.getall().Where(x=>x.IS_ENABLED==true).ToList();
                    ViewBag.Part_b = Part_b.getall().ToList();
                    ViewBag.Unit_b = Unit_b.getall().ToList();
                    ViewBag.Utl_type_b = Utl_type_b.getall().ToList();
                }
            }
           
               
                return View(model);
            

        }

        [HttpGet]
        [Authorize(Roles = "AGREEMENT_Update")]
        public ActionResult Edit(int id)
        {

            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            SUB_PROJTBL_Business subproj_b = new SUB_PROJTBL_Business();
            PERSON_ACCTBL_Business Provider_b = new PERSON_ACCTBL_Business();
            PARTTBL_Business Part_b = new PARTTBL_Business();
            UNITTBL_Business Unit_b = new UNITTBL_Business();
            UTL_TYPETBL_Business Utl_type_b = new UTL_TYPETBL_Business();
            PERSON_ACC_PROJTBL_Business PERSON_ACC_PROJTBL_b = new PERSON_ACC_PROJTBL_Business();
            var model = PERSON_ACC_PROJTBL_b.GetPyID(id);

            ViewBag.project_name = projb.GetByID(model.PROJECT_NO.Value).PROJECT_NAME;
            ViewBag.project_id = model.PROJECT_NO;
            ViewBag.utlist_id = model.UTL_NO;
            ViewBag.Utl_Name = utl_b.GetPyID(model.UTL_NO.Value).UTL_NAME;
            ViewBag.Subproject = subproj_b.getall().ToList();
            ViewBag.Provider = Provider_b.getall().Where(x=>x.IS_ENABLED==true).ToList();
            ViewBag.Part_b = Part_b.getall().ToList();
            ViewBag.Unit_b = Unit_b.getall().ToList();
            ViewBag.Utl_type_b = Utl_type_b.getall().ToList();
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "AGREEMENT_Update")]
        public ActionResult Edit(PERSON_ACC_PROJTBL_Model model)
        {
            if (ModelState.IsValid)
            {
                if (model.SUB_PROJ_NO == 0)
                {
                    model.SUB_PROJ_NO = null;
                }
                if (model.PART_NO == 0)
                {
                    model.PART_NO = null;
                }
                if (model.UNIT_NO == 0)
                {
                    model.UNIT_NO = null;
                }
                if (model.UTL_TYPE == 0)
                {
                    model.UTL_TYPE = null;
                }

                PERSON_ACC_PROJTBL_Business pERSON_ACC_PROJTBL_b = new PERSON_ACC_PROJTBL_Business();
                PERSON_ACC_PROJTBL_Business pERSON_ACC_PROJTBLOld_b = new PERSON_ACC_PROJTBL_Business();
                PERSON_ACCTBL_Business PERSON_ACCL_b = new PERSON_ACCTBL_Business();
                model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
                var personal = PERSON_ACCL_b.GetByID(model.PERSON_ACC_ID.Value);
                model.ACC_NO = personal.ACC_NO;
                model.ACC_NO = PERSON_ACCL_b.GetByID(model.PERSON_ACC_ID.Value).ACC_NO;
                model.PERSON_ID = personal.PERSON_ID;
                model.IS_DELETED = false;
                if (model.CREDENCE_DTTBL_Collection.Count() > 0)
                {
                    model.PERSON_ACC_ID = pERSON_ACC_PROJTBLOld_b.GetPyID(model.ID).PERSON_ACC_ID;
                }

                int reusrt = pERSON_ACC_PROJTBL_b.Update(model);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index", new { id = model.UTL_NO, project_id = model.PROJECT_NO });
                }

            }
          
                PROJECTTBL_Business projb = new PROJECTTBL_Business();
                UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
                SUB_PROJTBL_Business subproj_b = new SUB_PROJTBL_Business();
                PERSON_ACCTBL_Business Provider_b = new PERSON_ACCTBL_Business();
                PARTTBL_Business Part_b = new PARTTBL_Business();
                UNITTBL_Business Unit_b = new UNITTBL_Business();
                UTL_TYPETBL_Business Utl_type_b = new UTL_TYPETBL_Business();
                ViewBag.project_name = projb.GetByID(model.PROJECT_NO.Value).PROJECT_NAME;
                ViewBag.project_id = model.PROJECT_NO;
                ViewBag.utlist_id = model.UTL_NO;
                ViewBag.Utl_Name = utl_b.GetPyID(model.UTL_NO.Value).UTL_NAME;
                ViewBag.Subproject = subproj_b.getall().ToList();
                ViewBag.Provider = Provider_b.getall().Where(x=>x.IS_ENABLED==true).ToList();
                ViewBag.Part_b = Part_b.getall().ToList();
                ViewBag.Unit_b = Unit_b.getall().ToList();
                ViewBag.Utl_type_b = Utl_type_b.getall().ToList();
                return View(model);
            

        }

        [Authorize(Roles = "AGREEMENT_Deleted")]
        public bool delete(int? id)
        {
            bool return_result = false;
            PERSON_ACC_PROJTBL_Business b = new PERSON_ACC_PROJTBL_Business();

            int result = b.delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }



    }
}