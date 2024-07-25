using Microsoft.AspNet.Identity;
using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class CREDENCE_DTController : Controller
    {
        // GET: CREDENCE_DT
        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Select")]
        public ActionResult Index(int id, string redirct,string isall )
        {
            
            USERSTBL_Business user = new USERSTBL_Business();
            int userid = Convert.ToInt32(User.Identity.GetUserId());
            var utlsproj = user.GetPyID(userid).USER_UTLIST_PROJECTTBL_Collection.Where(x => x.IS_DELETED == false);
            PERSON_ACC_PROJTBL_Business per_acc_proj_b = new PERSON_ACC_PROJTBL_Business();
            var per_acc_proj =  per_acc_proj_b.GetPyID(id);
           
            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            ViewBag.project_name =per_acc_proj.PROJECTTBL_Model.PROJECT_NAME;
            ViewBag.project_id = per_acc_proj.PROJECT_NO.Value;
            ViewBag.utlist_id = per_acc_proj.UTL_NO;
            ViewBag.Utl_Name = per_acc_proj.UTLISTTBL_Model.UTL_NAME;
            ViewBag.per_acc_proj_id = id;
            ViewBag.redirct = redirct;

          

            CREDENCE_DTTBL_Business CREDENCE_B = new CREDENCE_DTTBL_Business();
            if (User.IsInRole("CREDENCETBL_Manager"))
            {
                
                if(isall=="true")
                {
                    List<CREDENCE_DTTBL_Model> model = new List<CREDENCE_DTTBL_Model>();
                    var per_acc_prolist = per_acc_proj_b.getall().Where(x => x.ACC_NO == per_acc_proj.ACC_NO && x.PROJECT_NO == per_acc_proj.PROJECT_NO && x.UTL_NO == per_acc_proj.UTL_NO);
                    foreach (var item in per_acc_prolist)
                    {
                        model.AddRange(item.CREDENCE_DTTBL_Collection.Where(x=>x.IS_DELETED==false));

                    }

                    return View(model);
                }
                else
                {
                   
                    var model = CREDENCE_B.getall().Where(x => x.PERSON_ACC_PROJTBL_ID == id && x.IS_DELETED==false);

                    return View(model);
                }
            }
            else if (utlsproj.Where(x => x.PROJECT_ID == per_acc_proj.PROJECT_NO && x.UTLIST_ID == per_acc_proj.UTL_NO).Count() > 0)
            {
                if (isall == "true")
                {
                    List<CREDENCE_DTTBL_Model> model = new List<CREDENCE_DTTBL_Model>();
                    var per_acc_prolist = per_acc_proj_b.getall().Where(x => x.ACC_NO == per_acc_proj.ACC_NO && x.PROJECT_NO == per_acc_proj.PROJECT_NO && x.UTL_NO == per_acc_proj.UTL_NO);
                    foreach (var item in per_acc_prolist)
                    {
                        model.AddRange(item.CREDENCE_DTTBL_Collection.Where(x => x.IS_DELETED == false));

                    }

                    return View(model);
                }
                else
                {

                    var model = CREDENCE_B.getall().Where(x => x.PERSON_ACC_PROJTBL_ID == id && x.IS_DELETED == false);

                    return View(model);
                }
            }
            return View();
        }
     
        
        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Create")]
        public ActionResult Create(int id, string redirct)
        {
           
            PERSON_ACC_PROJTBL_Business per_acc_proj_b = new PERSON_ACC_PROJTBL_Business();
            var per_acc_proj = per_acc_proj_b.GetPyID(id);


            
            ViewBag.per_acc_proj = id;
            ViewBag.project_name = per_acc_proj.PROJECTTBL_Model.PROJECT_NAME;
            ViewBag.project_id = per_acc_proj.PROJECT_NO.Value;
            ViewBag.utlist_id = per_acc_proj.UTL_NO;
            ViewBag.Utl_Name = per_acc_proj.UTLISTTBL_Model.UTL_NAME;
            ViewBag.PERSON_ACC_Name = per_acc_proj.PERSON_ACCTBL_Model.PERSONAL_INFOTBL_Model.AR_NAME;
            ViewBag.redirct = redirct;


            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CREDENCE_DTTBL_Create")]
        public ActionResult Create(CREDENCE_DTTBL_Model model, string redirct, HttpPostedFileBase[] files)
        {
            CREDENCE_DTTBL_Business CREDENCE_B = new CREDENCE_DTTBL_Business();
            PERSON_ACC_PROJTBL_Business per_acc_proj_b = new PERSON_ACC_PROJTBL_Business();
            var per_acc_proj = per_acc_proj_b.GetPyID(model.PERSON_ACC_PROJTBL_ID.Value);


            if (ModelState.IsValid)
            {



                CREDENCETBL_Business cred = new CREDENCETBL_Business();
                var credt = cred.getall().Where(x =>  x.DATE_M <= DateTime.Now && x.TO_DATE_M >= DateTime.Now && x.CLOSED==false &&x.IS_DELETED==false);

                if (credt.Count() > 0 && credt !=null)
                {
                    var credtHD = credt.FirstOrDefault();
                    decimal totalCredtHd = credtHD.CREDENCE_DTTBL_Collection.Sum(x => x.AMMOUNT).Value;

                    if (totalCredtHd + model.AMMOUNT <= credtHD.BALANCE_LIMIT)
                    {

                        model.HD_ID = credt.FirstOrDefault().ID_NO;

                        decimal totalCred = per_acc_proj.CREDENCE_DTTBL_Collection.Sum(x => x.AMMOUNT).Value;

                        if (totalCred + model.AMMOUNT <= (per_acc_proj.QNTY * per_acc_proj.SING_PRICE))
                        {

                            model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                            model.EMP_NO = Convert.ToInt16(User.Identity.GetUserId());
                            model.CREDITOR_ACC = per_acc_proj.PERSON_ACCTBL_Model.ACC_NO;
                          

                            int reusrt = CREDENCE_B.Create(model);
                            if (reusrt > 0)
                            {

                                if (files.Length >0 )
                                {

                                    foreach(var file  in files)
                                    {
                                        try
                                        {
                                            ARCHIVETBL_Business b = new ARCHIVETBL_Business();
                                            ARCHIVETBL_Model archive = new ARCHIVETBL_Model();
                                            archive.CREDENCE_DT_ID = reusrt;
                                            archive.CREDENCE_DT_HD_ID = model.HD_ID;
                                            archive.USER_CR = Convert.ToInt16(User.Identity.GetUserId());

                                            var formattedFileName = string.Format("{0}-{1}{2}"
                                             , Path.GetFileNameWithoutExtension(file.FileName)
                                             , Guid.NewGuid().ToString("N")
                                             , Path.GetExtension(file.FileName));
                                            string path = Path.Combine(Server.MapPath("~/CREDENCE_DT_File"), formattedFileName);
                                            file.SaveAs(path);


                                            archive.NAME = formattedFileName;
                                            archive.PATH = formattedFileName;

                                            b.Create(archive);
                                        }
                                        catch { }

                                    }


                                  
                                }

                                return RedirectToAction("Index", new { id = model.PERSON_ACC_PROJTBL_ID, redirct = redirct });
                            }


                        }
                        else
                        {
                            ModelState.AddModelError("", "الملبغ تجاوز المستحق للمقاول");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "الملبغ تجاوز الحد الاعلى لمزانية الاستحقاق يرجى مرجعة الادارة ");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "لايوجد استحقاق لهذا اليوم يرجاء مرجعة الادارة");
                }


            }

                ViewBag.per_acc_proj = per_acc_proj.ID;
                ViewBag.project_name = per_acc_proj.PROJECTTBL_Model.PROJECT_NAME;
                ViewBag.project_id = per_acc_proj.PROJECT_NO.Value;
                ViewBag.utlist_id = per_acc_proj.UTL_NO;
                ViewBag.Utl_Name = per_acc_proj.UTLISTTBL_Model.UTL_NAME;
                ViewBag.PERSON_ACC_Name = per_acc_proj.PERSON_ACCTBL_Model.PERSONAL_INFOTBL_Model.AR_NAME;

                return View();
        }

        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Update")]
        public ActionResult Edit(int id,int Hd_Id )
        {
            CREDENCE_DTTBL_Business cred_B = new CREDENCE_DTTBL_Business();
            var CREDENCE_DTTBL=cred_B.GetPyID(id,Hd_Id);
            
                return View(CREDENCE_DTTBL);
            
           
        
        }

        [HttpPost]
        [Authorize(Roles = "CREDENCE_DTTBL_Update")]
        public ActionResult Edit(CREDENCE_DTTBL_Model model, HttpPostedFileBase[] files)
        {
            CREDENCE_DTTBL_Business CREDENCE_B = new CREDENCE_DTTBL_Business();
            CREDENCE_DTTBL_Business CREDENCE_B1 = new CREDENCE_DTTBL_Business();
            PERSON_ACC_PROJTBL_Business per_acc_proj_b = new PERSON_ACC_PROJTBL_Business();
            var per_acc_proj = per_acc_proj_b.GetPyID(model.PERSON_ACC_PROJTBL_ID.Value);
            var CREDENCE_DTTBLOld = CREDENCE_B1.GetPyID(model.DT_ID, model.HD_ID);

            if (ModelState.IsValid)
            {
                CREDENCETBL_Business cred = new CREDENCETBL_Business();



                decimal totalCred = per_acc_proj.CREDENCE_DTTBL_Collection.Sum(x => x.AMMOUNT).Value-CREDENCE_DTTBLOld.AMMOUNT.Value;

                if (CREDENCE_DTTBLOld.DONE != true)
                {
                    var credtHD = CREDENCE_DTTBLOld.CREDENCETBL_Model;

                    decimal totalCredtHd = credtHD.CREDENCE_DTTBL_Collection.Sum(x => x.AMMOUNT).Value - CREDENCE_DTTBLOld.AMMOUNT.Value;

                    if (totalCredtHd + model.AMMOUNT <= credtHD.BALANCE_LIMIT)
                    {

                        if (totalCred + model.AMMOUNT <= (per_acc_proj.QNTY * per_acc_proj.SING_PRICE))
                        {

                            model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
                            model.IS_DELETED = false;

                            int reusrt = CREDENCE_B.Update(model);
                            if (reusrt > 0)
                            {

                                if (files.Length > 0)
                                {

                                    foreach (var file in files)
                                    {
                                        try
                                        {
                                            ARCHIVETBL_Business b = new ARCHIVETBL_Business();
                                            ARCHIVETBL_Model archive = new ARCHIVETBL_Model();
                                            archive.CREDENCE_DT_ID = model.DT_ID;
                                            archive.CREDENCE_DT_HD_ID = model.HD_ID;
                                            archive.USER_CR = Convert.ToInt16(User.Identity.GetUserId());

                                            var formattedFileName = string.Format("{0}-{1}{2}"
                                             , Path.GetFileNameWithoutExtension(file.FileName)
                                             , Guid.NewGuid().ToString("N")
                                             , Path.GetExtension(file.FileName));
                                            string path = Path.Combine(Server.MapPath("~/CREDENCE_DT_File"), formattedFileName);
                                            file.SaveAs(path);


                                            archive.NAME = formattedFileName;
                                            archive.PATH = formattedFileName;

                                            b.Create(archive);
                                        }
                                        catch { }

                                    }



                                }

                                return RedirectToAction("Index", new { id = model.PERSON_ACC_PROJTBL_ID });
                            }


                        }
                        else
                        {
                            ModelState.AddModelError("", "الملبغ تجاوز المستحق للمقاول");
                        }
                    }
                    {
                        ModelState.AddModelError("", "الملبغ تجاوز الحد الاعلى لمزانية الاستحقاق يرجى مرجعة الادارة ");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "لايمكنك التعديل لقد تم تسليم الاستحقاق");
                }
            }
            

           


            return View(CREDENCE_DTTBLOld);

        }

        [Authorize(Roles = "CREDENCE_DTTBL_Select")]
        public ActionResult details(int id, int Hd_Id)
        {
            CREDENCE_DTTBL_Business cred_B = new CREDENCE_DTTBL_Business();
            var CREDENCE_DTTBL = cred_B.GetPyID(id, Hd_Id);

            return View(CREDENCE_DTTBL);



        }


        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Select")]
        public ActionResult Indexproject()
        {
            USERSTBL_Business userb = new USERSTBL_Business();
            USER_UTLIST_PROJECTTBL_Business uSER_UTLIST_B = new USER_UTLIST_PROJECTTBL_Business();

            var user = userb.GetPyID( Convert.ToInt32(User.Identity.GetUserId()));
            List<USER_UTLIST_PROJECTTBL_Model> model;

            if (User.IsInRole("CREDENCETBL_Manager"))
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
        [Authorize(Roles = "CREDENCE_DTTBL_Select")]
        public ActionResult Index_person_acc_project(int id)
        {
            UTLISTTBL_Business utl_b = new UTLISTTBL_Business();
            USERSTBL_Business user = new USERSTBL_Business();
            int userid = Convert.ToInt32(User.Identity.GetUserId());
            var utlsproj = user.GetPyID(userid).USER_UTLIST_PROJECTTBL_Collection.Where(x => x.IS_DELETED == false);

            PROJECTTBL_Business projb = new PROJECTTBL_Business();
            ViewBag.project_name = projb.GetByID(id).PROJECT_NAME;
            ViewBag.project_id = id;


            var utls = utlsproj.Where(x => x.PROJECT_ID == id);

          






            PERSON_ACC_PROJTBL_Business b = new PERSON_ACC_PROJTBL_Business();
            List<PERSON_ACC_PROJTBL_Model> model = new List<PERSON_ACC_PROJTBL_Model>();

            if (User.IsInRole("CREDENCETBL_Manager"))
            {



                model = b.getall().Where(x => x.PROJECT_NO == id && x.IS_DELETED==false).ToList();

               

              
            }
            //التحقق انة يملك هذة الشغلة او لا

            else if (utls.Count() > 0)
            {


                foreach (var item in utls)
                {
                    model.AddRange(b.getall().Where(x => (x.UTL_NO == item.UTLIST_ID && x.PROJECT_NO == id && x.IS_DELETED == false)).ToList());
                }


              
            }
            else
            {
                return View();
            }
            ACCOUNTTBL_Business acb = new ACCOUNTTBL_Business();
            //get pay amount and REST_TOTAL
            foreach (var item in model)
            {
                item.PAY_AMOUNT = acb.GetPyProjectNoAndAccountNO(item.PROJECT_NO.Value, item.ACC_NO.Value).Sum(x => x.FROM_HIM).Value;
                item.REST_TOTAL = item.TOTAL_PRICE-(item.PAY_AMOUNT+item.NO_PAY_AMOUNT);

            }
            ///////

            return View(model);
        }

        [Authorize(Roles = "CREDENCE_DTTBL_Deleted")]
        public bool delete(int dt_id,int hd_id)
        {
            bool return_result = false;
            CREDENCE_DTTBL_Business b = new CREDENCE_DTTBL_Business();

            int result = b.delete(dt_id, hd_id);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }





        [Authorize(Roles = "CREDENCE_DTTBL_Update")]
        public bool deleteImage(int? id)
        {
            bool return_result = false;
            ARCHIVETBL_Business b = new ARCHIVETBL_Business();

            int result = b.delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }



        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Create")]
        public ActionResult CreateForAll(int id, string redirct)
        {

            PERSON_ACC_PROJTBL_Business per_acc_proj_b = new PERSON_ACC_PROJTBL_Business();
            var per_acc_proj = per_acc_proj_b.GetPyID(id);



            ViewBag.per_acc_proj = id;
            ViewBag.project_name = per_acc_proj.PROJECTTBL_Model.PROJECT_NAME;
            ViewBag.project_id = per_acc_proj.PROJECT_NO.Value;
            ViewBag.utlist_id = per_acc_proj.UTL_NO;
            ViewBag.Utl_Name = per_acc_proj.UTLISTTBL_Model.UTL_NAME;
            ViewBag.PERSON_ACC_Name = per_acc_proj.PERSON_ACCTBL_Model.PERSONAL_INFOTBL_Model.AR_NAME;
            ViewBag.redirct = redirct;


            return View();
        }

        [HttpPost]
        [Authorize(Roles = "CREDENCE_DTTBL_Create")]
        public ActionResult CreateForAll(CREDENCE_DTTBL_Model model, string redirct, HttpPostedFileBase[] files)
        {
            CREDENCE_DTTBL_Business CREDENCE_B = new CREDENCE_DTTBL_Business();
            PERSON_ACC_PROJTBL_Business per_acc_proj_b = new PERSON_ACC_PROJTBL_Business();
            var per_acc_proj = per_acc_proj_b.GetPyID(model.PERSON_ACC_PROJTBL_ID.Value);
            try
            {

                if (ModelState.IsValid)
                {



                    CREDENCETBL_Business credhdb = new CREDENCETBL_Business();
                    var credthd = credhdb.getall().Where(x => x.DATE_M <= DateTime.Now && x.TO_DATE_M >= DateTime.Now && x.CLOSED == false && x.IS_DELETED == false);

                    if (credthd.Count() > 0 && credthd != null) //is ther are credence hd for this day
                    {
                        var credtHD = credthd.FirstOrDefault();
                        decimal totalCredtHd = credtHD.CREDENCE_DTTBL_Collection.Sum(x => x.AMMOUNT).Value;

                        if (totalCredtHd + model.AMMOUNT <= credtHD.BALANCE_LIMIT)//is the ammount of credence hd up then limted
                        {

                            model.HD_ID = credthd.FirstOrDefault().ID_NO;


                            var per_acc_projList = per_acc_proj_b.getall().Where(x => x.ACC_NO == per_acc_proj.ACC_NO && x.PROJECT_NO == per_acc_proj.PROJECT_NO && x.UTL_NO == per_acc_proj.UTL_NO && x.IS_DELETED == false).ToList();


                            //List<CREDENCE_DTTBL_Model> CREDENCEList = new List<CREDENCE_DTTBL_Model>();

                            //foreach (var item in per_acc_projList)
                            //{
                            //    CREDENCEList.AddRange(item.CREDENCE_DTTBL_Collection.Where(x => x.IS_DELETED == false));

                            //}


                            decimal AMMOUNT = model.AMMOUNT.Value / per_acc_projList.Count();

                            using (TransactionScope trns = new TransactionScope())
                            {
                                foreach (var per_acc_pro in per_acc_projList)
                                {




                                    decimal totalCred = per_acc_proj.CREDENCE_DTTBL_Collection.Where(x => x.IS_DELETED == false).Sum(x => x.AMMOUNT).Value;


                                    if (totalCred + AMMOUNT <= (per_acc_proj.QNTY * per_acc_proj.SING_PRICE))
                                    {
                                        CREDENCE_DTTBL_Model newcred = new CREDENCE_DTTBL_Model();
                                        newcred.ACT_EXEC_DATE_H = model.ACT_EXEC_DATE_H;
                                        newcred.ACT_EXEC_DATE_M = model.ACT_EXEC_DATE_M;

                                        newcred.CREDENCE_TEXt = model.CREDENCE_TEXt;
                                        // newcred.CREDITOR_ACC = model.CREDITOR_ACC;
                                        newcred.CRED_TYPE = model.CRED_TYPE;
                                        newcred.CR_DATE = model.CR_DATE;
                                        newcred.DEBTOR_ACC = model.DEBTOR_ACC;
                                        // newcred.CREDITOR_ACC = model.CREDITOR_ACC;
                                        // newcred.DT_ID = model.DT_ID;
                                        newcred.EMP_SUSPEND = model.EMP_SUSPEND;
                                        newcred.EXEC_DATE_H = model.EXEC_DATE_H;
                                        newcred.EXEC_DATE_M = model.EXEC_DATE_M;
                                        newcred.HD_ID = model.HD_ID;
                                        newcred.NOTE = model.NOTE;
                                        newcred.PROJECT_NO = model.PROJECT_NO;
                                        newcred.REASON = model.REASON;
                                        newcred.SUSPENDED = model.SUSPENDED;
                                        newcred.UTL_NO = model.UTL_NO;
                                        newcred.CONTRACT_NO = model.CONTRACT_NO;
                                     




                                        newcred.PERSON_ACC_PROJTBL_ID = per_acc_pro.ID;
                                        newcred.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                                        newcred.EMP_NO = Convert.ToInt16(User.Identity.GetUserId());
                                        newcred.CREDITOR_ACC = per_acc_proj.PERSON_ACCTBL_Model.ACC_NO;
                                        newcred.DONE = false;
                                        newcred.IS_DELETED = false;
                                        newcred.AMMOUNT = AMMOUNT;
                                        int reusrt = CREDENCE_B.Create(newcred);
                                        if (reusrt > 0)
                                        {

                                            if (files.Length > 0)
                                            {

                                                foreach (var file in files)
                                                {
                                                    try
                                                    {
                                                        ARCHIVETBL_Business b = new ARCHIVETBL_Business();
                                                        ARCHIVETBL_Model archive = new ARCHIVETBL_Model();
                                                        archive.CREDENCE_DT_ID = reusrt;
                                                        archive.CREDENCE_DT_HD_ID = model.HD_ID;
                                                        archive.USER_CR = Convert.ToInt16(User.Identity.GetUserId());

                                                        var formattedFileName = string.Format("{0}-{1}{2}"
                                                         , Path.GetFileNameWithoutExtension(file.FileName)
                                                         , Guid.NewGuid().ToString("N")
                                                         , Path.GetExtension(file.FileName));
                                                        string path = Path.Combine(Server.MapPath("~/CREDENCE_DT_File"), formattedFileName);
                                                        file.SaveAs(path);


                                                        archive.NAME = formattedFileName;
                                                        archive.PATH = formattedFileName;

                                                        b.Create(archive);
                                                    }
                                                    catch { }

                                                }



                                            }
                                        }
                                    }
                                }
                                trns.Complete();
                                return RedirectToAction("Index", new { id = model.PERSON_ACC_PROJTBL_ID, redirct = redirct, isall = "true" });
                            }


                        }




                        else
                        {
                            ModelState.AddModelError("", "الملبغ تجاوز المستحق للمقاول");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "الملبغ تجاوز الحد الاعلى لمزانية الاستحقاق يرجى مرجعة الادارة ");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "لايوجد استحقاق لهذا اليوم يرجاء مرجعة الادارة");
                }


            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ex.InnerException.Message   +ex.InnerException.InnerException.Message);
            }

            ViewBag.per_acc_proj = per_acc_proj.ID;
            ViewBag.project_name = per_acc_proj.PROJECTTBL_Model.PROJECT_NAME;
            ViewBag.project_id = per_acc_proj.PROJECT_NO.Value;
            ViewBag.utlist_id = per_acc_proj.UTL_NO;
            ViewBag.Utl_Name = per_acc_proj.UTLISTTBL_Model.UTL_NAME;
            ViewBag.PERSON_ACC_Name = per_acc_proj.PERSON_ACCTBL_Model.PERSONAL_INFOTBL_Model.AR_NAME;

            return View();
        }



        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Select")]
        public  ActionResult Index_All(int? Page)
        {
            CREDENCE_DTTBL_Business CREDENCE_B = new CREDENCE_DTTBL_Business();
            var model = CREDENCE_B.getall().OrderByDescending(x => x.CR_DATE);

            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
          //  return  View(model);
        }


        [HttpGet]
        [Authorize(Roles = "CREDENCE_DTTBL_Select")]
        public ActionResult APPROVED(int id, int Hd_Id)
        {
            CREDENCE_DTTBL_Business cred_B = new CREDENCE_DTTBL_Business();
            var CREDENCE_DTTBL = cred_B.GetPyID(id, Hd_Id);

            return View(CREDENCE_DTTBL);



        }


        [HttpPost]
        [Authorize(Roles = "CREDENCE_DTTBL_Update")]
        public ActionResult APPROVED(CREDENCE_DTTBL_Model model)
        {
            CREDENCE_DTTBL_Business cred_B = new CREDENCE_DTTBL_Business();
            var CREDENCE_DTTBL = cred_B.GetPyID(model.DT_ID, model.HD_ID);
            if (CREDENCE_DTTBL.APPROVED == false)
            {
                CREDENCE_DTTBL.APPROVED = true;
            }
            else
            {
                CREDENCE_DTTBL.APPROVED = false;
            }
           
            cred_B.Update(CREDENCE_DTTBL);
            return RedirectToAction("Index_All");



        }




    }
}