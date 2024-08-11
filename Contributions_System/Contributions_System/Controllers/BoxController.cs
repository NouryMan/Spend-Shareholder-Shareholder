using PagedList;
using Spend.Business;
using Spend.Models;
using Contributions_System.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Contributions_System.Controllers
{
    [Authorize]
    public class BoxController : Controller
    {
        public ActionResult Index(int? Page, string search)
        {

            BOXTBL_Business b = new BOXTBL_Business();

            List<BOXTBL_Model> model = new List<BOXTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.BOX_NAME.Contains(search)).OrderByDescending(x => x.BOX_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.BOX_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        public ActionResult Create()
        {
            BOXTBL_Business b = new BOXTBL_Business();
            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();


            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            return View();
        }


        [HttpPost]
        public ActionResult Create(BOXTBL_Model model)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            if (ModelState.IsValid)
            {



                try
                {
                    Date d = new Date();
                    model.CR_DATEM = DateTime.Now;
                   // model.CR_DATEH =Convert.ToDateTime(d.GregToHijri(DateTime.Now.ToShortDateString()));
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

            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();


            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            

            return View(model);

        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();

            var model = b.GetById(id);

           
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(BOXTBL_Model model)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            if (ModelState.IsValid)
            {



                try
                {
                     long reusert = b.Update(model);
                    if (reusert > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
            }

            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();


            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();



            return View(model);

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();

            var model = b.GetById(id);

            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            return View(model);
        }


        public bool delete(int? id)
        {
            bool return_result = false;
            BOXTBL_Business boxB = new BOXTBL_Business();

            int result = boxB.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }


        public ActionResult Report(long? AccounNo, int? Project, int? buildingId, int? unitId, DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;

            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            BOXTBL_Business bOXTBL_Business = new BOXTBL_Business();
            ViewBag.BoxAcc = bOXTBL_Business.getall();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result, "ID", "PROJECT_AR_NAME", Project);
            ViewBag.building = new SelectList(proj_b.GetProjectListByParentIdAsync(Project).Result, "ID", "PROJECT_AR_NAME", buildingId);
            ViewBag.unit = new SelectList(proj_b.GetProjectListByParentIdAsync(buildingId).Result, "ID", "PROJECT_AR_NAME", unitId);



            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();


            var model = b.BoxStatement(AccounNo, Project, buildingId, unitId, FromDate, ToDate);

            //try { model.AddRange(b.SearchByParent(AccounNo, Project, Transe, FromDate, ToDate)); } catch { }
            try
            {
                var OpenBalnce = b.BoxOpenBalnce(AccounNo.Value, Project, buildingId, unitId, FromDate.Value);
                ViewBag.SPEND_COST = Convert.ToDouble((OpenBalnce.Sum(x => x.SPEND_COST)).ToString("0.##"));//عكس لان الجدول مبني فقط على المساهمين قيد واحد للعملية 
                ViewBag.INCOME = Convert.ToDouble((OpenBalnce.Sum(x => x.INCOME)).ToString("0.##"));

            }
            catch
            {
                ViewBag.FOR_HIM = 0;
                ViewBag.FROM_HIM = 0;
            }

            return View(model);
        }




    }
}