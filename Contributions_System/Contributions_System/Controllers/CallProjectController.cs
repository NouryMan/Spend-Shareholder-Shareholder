
using allcenter.business.call.Helper;
using callcenter.business.call;
using callcenter.business.global;
using callcenter.model.call;
using callcenter.model.global;
using Spend.Business;
//using Spend.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contributions_System.Controllers
{
    [Authorize]
    public class CallProjectController : Controller
    {
        // GET: Real_estate_rentals/Project

        public ActionResult Index()
        {
            callProjectBusiness projB = new callProjectBusiness(true);

            var model = projB.GetProjectListByParentId(null).ToList();
            ViewBag.buldingNo = projB.GetProjectListByProjectType(call_constants.project_type.building).Count();
            ViewBag.suiteNo = projB.GetProjectListByProjectType(call_constants.project_type.suite).Count();
            ViewBag.suiteNoclosed = projB.GetProjectListByProjectType(call_constants.project_type.suite).Where(x=>x.STATUS_ID==call_constants.projectStatus.closed).Count();

            return View(model);
        }

        public ActionResult Suite(int? projectId,int? status )
        {
            callProjectBusiness projB = new callProjectBusiness(true);

            var model = projB.SuiteByStuteAndProject(projectId,status);

            
            return View(model);
        }


        public ActionResult Create(int? id)
        {
            callProjectModel pro = new callProjectModel();
            call_lovDt_business lovB = new call_lovDt_business();
            callProjectBusiness dprb = new callProjectBusiness(true);
            callProjectModel aread = new callProjectModel();
            callCustomerBusiness agents = new callCustomerBusiness();
          

            //PROJECTTBL_Business spendB = new PROJECTTBL_Business();
            ViewBag.AGENT_ID = new SelectList(agents.getallpytype(call_constants.callCustumerType.agent), "ID", "CUST_AR_NAME");
            CallOwnerBusiness ownerB = new CallOwnerBusiness();
            ViewBag.OWNER_ID = new SelectList(ownerB.GetAll(), "ID", "AR_NAME");

       
            ViewBag.UNIT_TYPE = new SelectList(lovB.GetAllBYParentId(call_constants.unit_type.hd), "ID", "NAME_AR",call_constants.unit_type.workshop);
            //ViewBag.spendId = spendB.GetMaxID();
            if (id != null && id > 0)
            {
                aread = dprb.getbyid(id.Value);

                ViewBag.PARENT_ID = aread.ID;
                ViewBag.PARENT_NAME = aread.PROJECT_AR_NAME;
                if (aread.PROJECT_TYPE_ID == call_constants.project_type.project)
                {
                    ViewBag.cmbPROJECT_TYPE_ID = call_constants.project_type.building;
                    ViewBag.PROJECT_TYPE_Name = "عقار";
                    ViewBag.PROJECT_NO = dprb.getMaxByProjectParent(aread.ID, call_constants.project_type.building);
                }
                else if (aread.PROJECT_TYPE_ID == call_constants.project_type.building)
                {
                    ViewBag.cmbPROJECT_TYPE_ID = call_constants.project_type.suite;
                    ViewBag.PROJECT_TYPE_Name = "وحدة";
                    ViewBag.PROJECT_NO = dprb.getMaxByProjectParent(aread.ID, call_constants.project_type.suite);
                    pro.PIECE_NO = aread.PIECE_NO;
                    pro.PLANNED_NO = aread.PLANNED_NO;
                    pro.ISSUER = aread.ISSUER;
                    ViewBag.MainProjId =aread.PROJECT_PARENT_ID;

                }
            }
            else
            {

                ViewBag.PARENT_ID = null;
                ViewBag.PARENT_NAME = null;

                ViewBag.cmbPROJECT_TYPE_ID = call_constants.project_type.project; //new SelectList(lb.GetAllBYParentId(call_constants.project_type.hd), "ID", "NAME_AR");
                ViewBag.PROJECT_TYPE_Name = "مشروع";
                ViewBag.PROJECT_NO = dprb.getMaxByProjectParent(aread.ID, call_constants.project_type.project);


            }


            return View(pro);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "ADMIN")]
        
        public ActionResult Create(callProjectModel eRP_AREA, HttpPostedFileBase file, HttpPostedFileBase file2)
        {
            try {
                PROJECTTBL_Business spendB = new PROJECTTBL_Business();

            

                eRP_AREA.STATUS_ID = call_constants.projectStatus.open;
                try
                {
                    eRP_AREA.PROJECT_CODE = eRP_AREA.PROJECT_NO.ToString();
                    eRP_AREA.PROJECT_CODE = eRP_AREA.PROJECT_NO.ToString();
                }
                catch { }

                if (ModelState.IsValid)
                {
                    if (eRP_AREA.PROJECT_TYPE_ID == call_constants.project_type.suite)
                    {
                        eRP_AREA.SPEND_ID = eRP_AREA.SUITE_NO;
                    }
                    else if (eRP_AREA.PROJECT_TYPE_ID == call_constants.project_type.project)
                    {
                        eRP_AREA.SPEND_ID = spendB.GetMaxID();
                    }

                    eRP_AREA.IS_FOR_RENTAL = true;
                    if (file != null)
                    {

                        string path = Path.Combine(Server.MapPath("~/image"), file.FileName);
                        file.SaveAs(path);

                        eRP_AREA.IMAGE_PATH = file.FileName;
                    }

                    callProjectBusiness d = new callProjectBusiness(true);

                    int reusert = d.create(eRP_AREA);
                  
                    
                    if (file2 != null && reusert >= 1)
                    {
                        callImageModel image = new callImageModel();
                        callImagebusiness im = new callImagebusiness();

                        string path = Path.Combine(Server.MapPath("~/image"), file2.FileName);
                        file2.SaveAs(path);

                        image.PROJECT_ID = eRP_AREA.ID;
                        image.IMAGE_PATH = file2.FileName;
                        image.IMAGE_NO = im.getMaxImage_NoByProjectid(eRP_AREA.ID);
                        image.IMAGE_CODE = image.IMAGE_NO.ToString();
                        image.IMAGE_TYPE_ID = call_constants.callImageType.roleDesignsImages;
                        im.create(image);



                    }
                    
                    
                    
                    if (eRP_AREA.PROJECT_TYPE_ID == call_constants.project_type.building)
                    {

                        return RedirectToAction("buildReview", new { id = eRP_AREA.PROJECT_PARENT_ID });
                    }
                    else if (eRP_AREA.PROJECT_TYPE_ID == call_constants.project_type.suite)
                    {
                        var projId = d.getbyid(eRP_AREA.PROJECT_PARENT_ID).PROJECT_PARENT_ID;
                        return RedirectToAction("buildReview", new { id = projId });
                    }
                    else
                    {
                        return RedirectToAction("Index", new { id = eRP_AREA.PROJECT_PARENT_ID });
                    }

                }

              
            }
            catch
            {
                
            }

            call_lovDt_business lovB = new call_lovDt_business();
            callProjectBusiness dprb = new callProjectBusiness(true);
            callProjectModel aread = null;
            ViewBag.DEPT_TYPE_ID = new SelectList(lovB.GetAllBYParentId(constants.area.hd).ToList(), "ID", "NAME_AR", eRP_AREA.PROJECT_TYPE_ID);

            if (eRP_AREA.PROJECT_PARENT_ID != null)
            {
                aread = dprb.getbyid(eRP_AREA.PROJECT_PARENT_ID.Value);
                ViewBag.PARENT_ID = aread.ID;
                ViewBag.PARENT_NAME = aread.PROJECT_AR_NAME;

            }
            else
            {
                callCustomerBusiness agents = new callCustomerBusiness();
                ViewBag.AGENT_ID = new SelectList(agents.getallpytype(call_constants.callCustumerType.agent).ToList(), "ID", "CUST_AR_NAME", eRP_AREA.callCustomerModels);
                CallOwnerBusiness ownerB = new CallOwnerBusiness();
                ViewBag.OWNER_ID = new SelectList(ownerB.GetAll(), "ID", "AR_NAME", eRP_AREA.OWNER_ID);
                ViewBag.UNIT_TYPE = new SelectList(lovB.GetAllBYParentId(call_constants.unit_type.hd), "ID", "NAME_AR", call_constants.unit_type.workshop);
                ViewBag.PARENT_ID = null;
                ViewBag.PARENT_NAME = null;

            }

            return View(eRP_AREA);

        }



        [Authorize(Roles = "Real_estate_select")]
        public ActionResult Edit(int id)
        {
            callProjectModel pro = new callProjectModel();
            call_lovDt_business lovB = new call_lovDt_business();
            callProjectBusiness dprb = new callProjectBusiness(true);
            callCustomerBusiness agents = new callCustomerBusiness();

            pro = dprb.getbyid(id);
           
            ViewBag.AGENT_ID = new SelectList(agents.getallpytype(call_constants.callCustumerType.agent), "ID", "CUST_AR_NAME", pro.AGENT_ID);
            ViewBag.UNIT_TYPE = new SelectList(lovB.GetAllBYParentId(call_constants.unit_type.hd), "ID", "NAME_AR", pro.UNIT_TYPE);
            CallOwnerBusiness ownerB = new CallOwnerBusiness();
            ViewBag.OWNER_ID = new SelectList(ownerB.GetAll(), "ID", "AR_NAME");
            return View(pro);
        }



      
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "ADMIN")]
        [Authorize(Roles = "Real_estate_select")]
        public ActionResult Edit(callProjectModel eRP_AREA, HttpPostedFileBase file, HttpPostedFileBase file2)
        {

           
            callProjectBusiness d = new callProjectBusiness(false);
            try
            {
                eRP_AREA.PROJECT_CODE = eRP_AREA.PROJECT_NO.ToString();
            }
            catch { }
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/image"), file.FileName);
                    file.SaveAs(path);

                    eRP_AREA.IMAGE_PATH = file.FileName;
                }
                else
                {
                    callProjectBusiness b = new callProjectBusiness(false);

                    //for save old image if it no edit
                    eRP_AREA.IMAGE_PATH =eRP_AREA.IMAGE_PATH;

                }

                int reusert = d.update(eRP_AREA);
                if (file2 != null && reusert >= 1)
                {

                    callImageModel image = new callImageModel();
                    callImagebusiness im = new callImagebusiness();
                    callImagebusiness oldim = new callImagebusiness();
                    string path = Path.Combine(Server.MapPath("~/image"), file2.FileName);
                    file2.SaveAs(path);

                    image.PROJECT_ID = eRP_AREA.ID;
                    image.IMAGE_PATH = file2.FileName;
                    image.IMAGE_NO = im.getMaxImage_NoByProjectid(eRP_AREA.ID);
                    image.IMAGE_CODE = image.IMAGE_NO.ToString();
                    image.IMAGE_TYPE_ID = call_constants.callImageType.roleDesignsImages;


                    var oldimage = oldim.GetImadetListByProjectId(eRP_AREA.ID);
                    if (oldimage.Count() != 0)
                    {
                        image.ID = oldimage.FirstOrDefault().ID;
                        im.update(image);
                    }
                    else
                    {
                        im.create(image);
                    }


                }


                if (eRP_AREA.PROJECT_TYPE_ID == call_constants.project_type.building)
                {

                    return RedirectToAction("buildReview", new { id = eRP_AREA.PROJECT_PARENT_ID });
                }
                else if (eRP_AREA.PROJECT_TYPE_ID == call_constants.project_type.suite)
                {
                    callProjectBusiness b = new callProjectBusiness(true);

                    var projId = b.getbyid(eRP_AREA.PROJECT_PARENT_ID).PROJECT_PARENT_ID;
                    return RedirectToAction("buildReview", new { id = projId });
                }
                else
                {
                    return RedirectToAction("Index", new { id = eRP_AREA.PROJECT_PARENT_ID });
                }
            }


            call_lovDt_business lovB = new call_lovDt_business();
            callCustomerBusiness agents = new callCustomerBusiness();
            ViewBag.AGENT_ID = new SelectList(agents.getallpytype(call_constants.callCustumerType.agent), "ID", "CUST_AR_NAME", eRP_AREA.AGENT_ID);
            ViewBag.UNIT_TYPE = new SelectList(lovB.GetAllBYParentId(call_constants.unit_type.hd), "ID", "NAME_AR", eRP_AREA.UNIT_TYPE);
            CallOwnerBusiness ownerB = new CallOwnerBusiness();
            ViewBag.OWNER_ID = new SelectList(ownerB.GetAll(), "ID", "AR_NAME",eRP_AREA.OWNER_ID);

            return View(eRP_AREA);



        }



        [Authorize(Roles = "Real_estate_select")]
        public ActionResult buildReview(int? id)
        {
            ViewBag.id = id;

            if (id != null)
            {

                callProjectBusiness rb = new callProjectBusiness(true);


                List<callProjectModel> rld = rb.GetProjectListByParentId(id.Value).ToList();
                
                //foreach (var rec in rld)
                //{
                //    try
                //    {
                //        callcenter.model.archive.ARC_FOLDER fol = new callcenter.business.archive.folder_business().GetByPatentGuid(rec.GUID_ID.Value).FirstOrDefault();
                //        rec.ARC_FOLDER = fol;
                //        rec.ARC_FOLDER.FILE_Collection = fol.FILE_Collection;
                //    }
                //    catch { }

                //}

                return View(rld);
            }
            return View();
        }
        [Authorize(Roles = "Real_estate")]
        public bool delete(int? id)
        {
            bool return_result = false;
            callProjectBusiness bu = new callProjectBusiness(true);

            int result = bu.delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }

        public ActionResult Report(RentalReport model)
        {

           
            callRentalPaybusiness b = new callRentalPaybusiness();
            callProjectBusiness projB = new callProjectBusiness(true);
           // model.FromDate = Date.FromStampToDateTime(model.FromDateTimeStamp);
           // model.ToDate = Date.FromStampToDateTime(model.ToDateTimeStamp);
            model = b.RentalReport(model);
         
            List<callProjectModel> projectList = projB.GetProjectListByParentId(null).ToList();
            ViewBag.projectList = projectList;
            ViewBag.projectId = model.ProjectID;
            ViewBag.TypeId = model.TypeId;

            return View(model);
        }




    }
}