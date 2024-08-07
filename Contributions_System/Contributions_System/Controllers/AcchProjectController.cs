using PagedList;
using Spend.Business;
using Spend.Models;
using Spend.Models.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Contributions_System.Controllers
{
    public class AcchProjectController : Controller
    {

        [Authorize]
        public async Task<ActionResult> Index(AcchFilter filter)
        {
            ACCH_PROJECT_Business projB = new ACCH_PROJECT_Business();

            
            filter.Type = 1;
            filter.PageSize = 10000;
            
            
            var pagination =await projB.GetAllAsync(filter);
            ViewBag.buldingNo =await projB.GetCountByTypeAsync(1);
            ViewBag.suiteNo =await projB.GetCountByTypeAsync(2);
            ViewBag.project =await projB.GetCountByTypeAsync(3);
            //ViewBag.suiteNoclosed = projB.GetProjectListByProjectType(call_constants.project_type.suite).Where(x => x.STATUS_ID == call_constants.projectStatus.closed).Count();

            return View(pagination.Items.ToPagedList(filter.PageNumber, filter.PageSize));
        }

        [Authorize]
        public async Task<ActionResult> Suite(int? projectId, int? status)
        {
            ACCH_PROJECT_Business projB = new ACCH_PROJECT_Business();

            var model =await projB.SuiteByStuteAndProjectAsync(projectId, status);


            return View(model);
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Create(int? id)
        {
            ACCH_PROJECT_Model project = new ACCH_PROJECT_Model();
            ACCH_PROJECT_Business projectBusiness = new ACCH_PROJECT_Business();



            ACCH_PROJECT_Model aread = new ACCH_PROJECT_Model();


            if (id != null && id > 0)
            {
                 aread =await projectBusiness.GetByIdAsync(id.Value);

                ViewBag.PARENT_ID = aread.ID;
                ViewBag.PARENT_NAME = aread.PROJECT_AR_NAME;
                if (aread.PROJECT_TYPE_ID == 1)
                {
                    ViewBag.cmbPROJECT_TYPE_ID =2;
                    ViewBag.PROJECT_TYPE_Name = "عقار";
                    ViewBag.PROJECT_NO =await projectBusiness.GetMaxByProjectParent(aread.ID, 2);
                }
                else if (aread.PROJECT_TYPE_ID == 2)
                {
                    ViewBag.cmbPROJECT_TYPE_ID = 3;
                    ViewBag.PROJECT_TYPE_Name = "وحدة";
                    ViewBag.PROJECT_NO =await projectBusiness.GetMaxByProjectParent(aread.ID, 3);
                    project.PIECE_NO = aread.PIECE_NO;
                    project.PLANNED_NO = aread.PLANNED_NO;
                    project.ISSUER = aread.ISSUER;
                    ViewBag.MainProjId = aread.PROJECT_PARENT_ID;

                }
            }
            else
            {

                ViewBag.PARENT_ID = null;
                ViewBag.PARENT_NAME = null;

                ViewBag.cmbPROJECT_TYPE_ID = 1; //new SelectList(lb.GetAllBYParentId(call_constants.project_type.hd), "ID", "NAME_AR");
                ViewBag.PROJECT_TYPE_Name = "مشروع";
                ViewBag.PROJECT_NO = await projectBusiness.GetMaxByProjectParent(aread.ID, 2);


            }


            return View(project);
        }




        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(ACCH_PROJECT_Model eRP_AREA, HttpPostedFileBase file)
        {
            try
            {
                PROJECTTBL_Business spendProjectB = new PROJECTTBL_Business();
                ACCH_PROJECT_Business projectb = new ACCH_PROJECT_Business();

              

                eRP_AREA.STATUS_ID = 0;
                try
                {
                    eRP_AREA.PROJECT_CODE = eRP_AREA.PROJECT_NO.ToString();
                    eRP_AREA.PROJECT_CODE = eRP_AREA.PROJECT_NO.ToString();
                }
                catch { }

                if (ModelState.IsValid)
                {
                    if (eRP_AREA.PROJECT_TYPE_ID == 3)
                    {
                        eRP_AREA.SPEND_ID = eRP_AREA.SUITE_NO;
                    }
                    else if (eRP_AREA.PROJECT_TYPE_ID == 1)
                    {
                        eRP_AREA.SPEND_ID = spendProjectB.GetMaxID();
                    }

                    eRP_AREA.IS_FOR_RENTAL = true;
                    if (file != null)
                    {

                        string path = Path.Combine(Server.MapPath("~/image"), file.FileName);
                        file.SaveAs(path);

                        eRP_AREA.IMAGE_PATH = file.FileName;
                    }

                  

                    await projectb.CreateAsync(eRP_AREA);


                  



                    if (eRP_AREA.PROJECT_TYPE_ID == 2)
                    {

                        return RedirectToAction("buildReview", new { id = eRP_AREA.PROJECT_PARENT_ID });
                    }
                    else if (eRP_AREA.PROJECT_TYPE_ID == 3)
                    {
                        var projId =(await projectb.GetByIdAsync(eRP_AREA.PROJECT_PARENT_ID.Value)).PROJECT_PARENT_ID;
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

            ACCH_PROJECT_Model project = new ACCH_PROJECT_Model();
            ACCH_PROJECT_Business projectBusiness = new ACCH_PROJECT_Business();



            if (eRP_AREA.PROJECT_PARENT_ID != null)
            {
                project =await projectBusiness.GetByIdAsync(eRP_AREA.PROJECT_PARENT_ID.Value);
                ViewBag.PARENT_ID = project.ID;
                ViewBag.PARENT_NAME = project.PROJECT_AR_NAME;

            }
            else
            {
              
                ViewBag.PARENT_ID = null;
                ViewBag.PARENT_NAME = null;

            }

            return View(eRP_AREA);

        }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ACCH_PROJECT_Model pro = new ACCH_PROJECT_Model();

            ACCH_PROJECT_Business projectb = new ACCH_PROJECT_Business();


            pro = await projectb.GetByIdAsync(id);

         
            return View(pro);
        }




        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(ACCH_PROJECT_Model eRP_AREA, HttpPostedFileBase file)
        {


            ACCH_PROJECT_Business projectb = new ACCH_PROJECT_Business(); try
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
                    

                    //for save old image if it no edit
                    eRP_AREA.IMAGE_PATH = eRP_AREA.IMAGE_PATH;

                }

                 await projectb.UpdateAsync(eRP_AREA);
                
                if (eRP_AREA.PROJECT_TYPE_ID == 2)
                {

                    return RedirectToAction("buildReview", new { id = eRP_AREA.PROJECT_PARENT_ID });
                }
                else if (eRP_AREA.PROJECT_TYPE_ID ==3)
                {

                    var projId =(await projectb.GetByIdAsync(eRP_AREA.PROJECT_PARENT_ID.Value)).PROJECT_PARENT_ID;
                    return RedirectToAction("buildReview", new { id = projId });
                }
                else
                {
                    return RedirectToAction("Index", new { id = eRP_AREA.PROJECT_PARENT_ID });
                }
            }


            
            return View(eRP_AREA);



        }



        [Authorize]
        public async Task<ActionResult> buildReview(int? id)
        {
            ViewBag.id = id;

            if (id != null)
            {

                ACCH_PROJECT_Business projectb = new ACCH_PROJECT_Business();

                List<ACCH_PROJECT_Model> rld =await projectb.GetProjectListByParentIdAsync(id.Value);


                return View(rld);
            }
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            bool result = false;
            ACCH_PROJECT_Business projectb = new ACCH_PROJECT_Business();

            try
            {
                await projectb.Delete(id);
                result = true;
            }
            catch (Exception ex)
            {
                // يمكنك إضافة معالجة الخطأ هنا
            }

            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<JsonResult> GetByParintId(int id)
        {
            ACCH_PROJECT_Business projectb = new ACCH_PROJECT_Business();

            var project =await projectb.GetProjectListByParentIdAsync(id);

            return Json(new SelectList(project, "ID", "PROJECT_AR_NAME"), JsonRequestBehavior.AllowGet);
        }

       


    }
}