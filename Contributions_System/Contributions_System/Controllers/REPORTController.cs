using Spend.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contributions_System.Controllers
{
    [Authorize]
    public class REPORTController : Controller
    {
        // GET: REPORT
        public ActionResult AccountsRevenueAndExpenses()
        {
            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();
            var model = b.GetAll().GroupBy(x => x.ACC_HOLDER_NO);

            return View(model);
        }

        public ActionResult AccountsRevenueAndExpensesByProject(int? id)
        {
            ACC_HOLDERTBL_Business AccHolderB = new ACC_HOLDERTBL_Business();
            ViewBag.AccHolder = AccHolderB.getall();

            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();
            var model = b.GetAll().Where(x => x.ACC_HOLDER_NO==id);


            return View(model);
        }

         public ActionResult Project(int? id)
        {
            ACCH_PROJECT_Business projB = new ACCH_PROJECT_Business();
            ViewBag.Project = projB.GetAllAsync(1).Result;

            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();
            var model = b.GetAll().Where(x => x.TARGET_PROJ==id);


            return View(model);
        }

        public ActionResult Capital ()
        {
           
            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();
            var model = b.GetAll().GroupBy(x=>x.ACC_HOLDER_NO);


            return View(model);
        }

        public ActionResult AccountStatement(long? AccounNo, int? Project, int? buildingId, int? unitId, DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
          
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ACC_HOLDERTBL_Business AllAcc_b = new ACC_HOLDERTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result, "ID", "PROJECT_AR_NAME", Project);
            ViewBag.building = new SelectList(proj_b.GetProjectListByParentIdAsync(Project).Result, "ID", "PROJECT_AR_NAME", buildingId);
            ViewBag.unit = new SelectList(proj_b.GetProjectListByParentIdAsync(buildingId).Result, "ID", "PROJECT_AR_NAME", unitId);




            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();


            var model = b.Search(AccounNo, Project, buildingId, unitId, FromDate, ToDate);

            //try { model.AddRange(b.SearchByParent(AccounNo, Project, Transe, FromDate, ToDate)); } catch { }
            try
            {
                var OpenBalnce = b.OpenBalnce(AccounNo.Value, Project, buildingId, unitId, FromDate.Value);
                ViewBag.SPEND_COST = Convert.ToDouble((OpenBalnce.Sum(x => x.SPEND_COST)).ToString("0.##"));
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