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
    public class SPEND_SCRIPTBLController : Controller
    {
        // GET: Spend/Default


      
        public ActionResult Index(int? Page ,string search)
        {
            SPEND_SCRIPTBL_Business b = new SPEND_SCRIPTBL_Business();
            


            List<SPEND_SCRIPTBL_Model> model = new List<SPEND_SCRIPTBL_Model>();
            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.DEBTOR_ACC.ToString().Contains(search)|| x.DEBTOR_ACC_Model.ACC_NAME.Contains(search) || x.CREDITOR_Model.ACC_NAME.Contains(search) || x.REMARK.Contains(search) || x.SCRIP_NO.ToString().Contains(search) ).OrderByDescending(x => x.CR_DATE).ToList();
            }
            else {
                model = b.getall().OrderByDescending(x => x.CR_DATE).ToList();
            }
           
       

            int pageSize = 40;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }



        public ActionResult Create(int? id ,string redirect )
        {
            SPEND_SCRIPTBL_Business spend_script = new SPEND_SCRIPTBL_Business();
            ViewBag.spend_script_number = spend_script.getMaxNO(DateTime.Now.Year.ToString(),1);

            SCRIPT_TYPE_Business script_type = new SCRIPT_TYPE_Business();
            ViewBag.script_type = script_type.getall();

            SCRIP_OPTYPETBL_Business SRIP_OPTYPE = new SCRIP_OPTYPETBL_Business();
            ViewBag.SRIP_OPTYPE = SRIP_OPTYPE.getall();

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            SCRIPT_ACTIONSTBL_Business ScACTION_b = new SCRIPT_ACTIONSTBL_Business();
            ViewBag.ScACTION= ScACTION_b.getall();


            STAGESTBL_Business STAGES_b = new STAGESTBL_Business();
            ViewBag.STAGES = STAGES_b.getall();

            SUB_PROJTBL_Business Subproj_b = new SUB_PROJTBL_Business();
            ViewBag.Subproj = Subproj_b.getall();


            PARTTBL_Business Part_b = new PARTTBL_Business();
            ViewBag.Part_b = Part_b.getall();

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x=>x.OP_ACC==true);

            MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();
            ViewBag.UNDER_NO = Under_NO_b.getall().Where(x=>x.NAM=="under_no").FirstOrDefault().MAX_NO;
            UNITTBL_Business Unit_b = new UNITTBL_Business();
            ViewBag.Unitt_b = Unit_b.getall();


            
            return View();
        }


        //[HttpPost]
        //public ActionResult Create(SPEND_SCRIPTBL_Model model)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            SPEND_SCRIPTBL_Business spend_script_B = new SPEND_SCRIPTBL_Business();
                  

        //            model.ACTION_TYPE = 3;
        //            //model.PROJECT_NO = contr.callProjectModel.callProjectModels.callProjectModels.ID;
        //            ////model.SUB_PROJ = ;
        //            //model.PART_NO = contr.callProjectModel.callProjectModels.ID;
        //            //model.UNIT_NO = contr.PROJECT_ID;
        //            //model.USER_CR = constants.getUserId();

        //            spend_script_B.create(model);

        //            return RedirectToAction("index");

        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", ex.Message );
        //        }

        //    }


        //     SPEND_SCRIPTBL_Business spend_script = new SPEND_SCRIPTBL_Business();
        //    ViewBag.spend_script_number = spend_script.getMaxNO(DateTime.Now.Year.ToString(),1);

        //    SCRIPT_TYPE_Business script_type = new SCRIPT_TYPE_Business();
        //    ViewBag.script_type = script_type.getall();

        //    SCRIP_OPTYPETBL_Business SRIP_OPTYPE = new SCRIP_OPTYPETBL_Business();
        //    ViewBag.SRIP_OPTYPE = SRIP_OPTYPE.getall();

          

        //    return View(model);
        //}
  
        public ActionResult details(int id ,int type)
        {
            SPEND_SCRIPTBL_Business spend_script = new SPEND_SCRIPTBL_Business();

            var model = spend_script.GetPyID(id, type);

            SCRIPT_TYPE_Business script_type = new SCRIPT_TYPE_Business();
            ViewBag.script_type = script_type.getall();

            SCRIP_OPTYPETBL_Business SRIP_OPTYPE = new SCRIP_OPTYPETBL_Business();
            ViewBag.SRIP_OPTYPE = SRIP_OPTYPE.getall();

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            SCRIPT_ACTIONSTBL_Business ScACTION_b = new SCRIPT_ACTIONSTBL_Business();
            ViewBag.ScACTION = ScACTION_b.getall();


            STAGESTBL_Business STAGES_b = new STAGESTBL_Business();
            ViewBag.STAGES = STAGES_b.getall();

            SUB_PROJTBL_Business Subproj_b = new SUB_PROJTBL_Business();
            ViewBag.Subproj = Subproj_b.getall();


            PARTTBL_Business Part_b = new PARTTBL_Business();
            ViewBag.Part_b = Part_b.getall();

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);

          
            UNITTBL_Business Unit_b = new UNITTBL_Business();
            ViewBag.Unitt_b = Unit_b.getall();



            return View(model);
        }




    }
}