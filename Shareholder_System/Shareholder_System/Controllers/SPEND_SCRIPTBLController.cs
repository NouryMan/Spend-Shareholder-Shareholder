﻿using Microsoft.AspNet.Identity;
using PagedList;
using Spend.Business;
using Spend.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shareholder_System.Controllers
{
    [Authorize]
    public class SPEND_SCRIPTBLController : Controller
    {
        // GET: Spend/Default


      
      



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


            ACC_HOLDERTBL_Business ACCH_B = new ACC_HOLDERTBL_Business();
            ViewBag.ACCH = ACCH_B.getall();

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll();



            return View();
        }


      
        public ActionResult details(long id )
        {
            SPEND_SCRIPTBL_Business spend_script = new SPEND_SCRIPTBL_Business();

            var model = spend_script.GetPyID(id);

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