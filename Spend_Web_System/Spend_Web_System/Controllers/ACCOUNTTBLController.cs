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
    public class ACCOUNTTBLController : Controller
    {
        // GET: ACCOUNTTBL
        public ActionResult Report(long? AccounNo,int? Project,int ? Transe, DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
            ViewBag.Transe = Transe;
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

            ACCOUNTTBL_Business b = new ACCOUNTTBL_Business();
           

            var model = b.Search(AccounNo,Project, Transe, FromDate, ToDate);

            //try { model.AddRange(b.SearchByParent(AccounNo, Project, Transe, FromDate, ToDate)); } catch { }
            try {
                var OpenBalnce = b.OpenBalnce(AccounNo.Value,Project,Transe, FromDate.Value);
                var OpenBalnceByParent = b.OpenBalnceByParent(AccounNo.Value,Project,Transe, FromDate.Value);

                //ViewBag.FOR_HIM =Convert.ToDecimal((OpenBalnce.Sum(x => x.FOR_HIM).Value  + OpenBalnceByParent.Sum(x => x.FOR_HIM).Value).ToString("0.##"));
                ViewBag.FOR_HIM =Convert.ToDecimal((OpenBalnce.Sum(x => x.FOR_HIM).Value).ToString("0.##"));
                //ViewBag.FROM_HIM =Convert.ToDecimal((OpenBalnce.Sum(x => x.FROM_HIM).Value  + OpenBalnceByParent.Sum(x => x.FROM_HIM).Value).ToString("0.##"));
                ViewBag.FROM_HIM =Convert.ToDecimal((OpenBalnce.Sum(x => x.FROM_HIM).Value).ToString("0.##"));

            } catch { 
                ViewBag.FOR_HIM = 0;
                ViewBag.FROM_HIM = 0;
            }
            if (model.Count>0)
            {
                if (model.FirstOrDefault().ACC_NO == 110101006)
                {
                    model = model.OrderByDescending(x => x.ACC_DATE).ToList();


                }
            }
            return View(model);
        }


       
        public ActionResult TrialBalance(long? AccounNo, int? Project, int? Transe, DateTime? FromDate, DateTime? ToDate,int level=1)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
            ViewBag.Transe = Transe;
            ViewBag.level = level;
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

            ACCOUNTTBL_Business b = new ACCOUNTTBL_Business();
            var model = b.TrialBalance(AccounNo, Project, Transe, FromDate, ToDate).Where(x=>x.ACC_LEVEL<=level);


            return View(model);
        }

        public ActionResult AccountStatementPyVatNo(long? AccounNo, int? Project, int? Transe, DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
            ViewBag.Transe = Transe;
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

            ACCOUNTTBL_Business b = new ACCOUNTTBL_Business();


            var model = b.SearchWhitParent(AccounNo, Project, Transe, FromDate, ToDate);

            //try { model.AddRange(b.SearchByParent(AccounNo, Project, Transe, FromDate, ToDate)); } catch { }
            try
            {
                var OpenBalnce = b.OpenBalnceWhitParent(AccounNo.Value, Project, Transe, FromDate.Value);
                var OpenBalnceByParent = b.OpenBalnceByParent(AccounNo.Value, Project, Transe, FromDate.Value);

                //ViewBag.FOR_HIM =Convert.ToDecimal((OpenBalnce.Sum(x => x.FOR_HIM).Value  + OpenBalnceByParent.Sum(x => x.FOR_HIM).Value).ToString("0.##"));
                ViewBag.FOR_HIM = Convert.ToDecimal((OpenBalnce.Sum(x => x.FOR_HIM).Value).ToString("0.##"));
                //ViewBag.FROM_HIM =Convert.ToDecimal((OpenBalnce.Sum(x => x.FROM_HIM).Value  + OpenBalnceByParent.Sum(x => x.FROM_HIM).Value).ToString("0.##"));
                ViewBag.FROM_HIM = Convert.ToDecimal((OpenBalnce.Sum(x => x.FROM_HIM).Value).ToString("0.##"));

            }
            catch
            {
                ViewBag.FOR_HIM = 0;
                ViewBag.FROM_HIM = 0;
            }
            if (model.Count > 0)
            {
                if (model.FirstOrDefault().ACC_NO == 110101006)
                {
                    model = model.OrderByDescending(x => x.ACC_DATE).ToList();


                }
            }
            return View(model);
        }


        public ActionResult Balance(int? Project, int? Transe,int ? Balnce, DateTime? FromDate, DateTime? ToDate)
        {


            ViewBag.PROJECT_NO = Project;
            ViewBag.Transe = Transe;
            ViewBag.Balnce = Balnce;
            //try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            FromDate = DateTime.Now.AddYears(-20);
            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            var allAcc= AllAcc_b.getall();
            ViewBag.AllAcc = allAcc;

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

            ACCOUNTTBL_Business b = new ACCOUNTTBL_Business();

            List<ACCOUNTTBL_Model> modelList = new List<ACCOUNTTBL_Model>();

            foreach (var account in allAcc.Where(x=>x.OP_ACC==true))
            {

                var model = b.Search(account.ACC_NO, Project, Transe, FromDate, ToDate);



                if (model.Count > 0)
                {

                    var acc = new ACCOUNTTBL_Model();
                    acc.ACC_NO = model.FirstOrDefault().ACC_NO;
                    acc.NOTE = account.ACC_NAME;
                    acc.FROM_HIM = model.Sum(x => x.FROM_HIM);
                    acc.FOR_HIM = model.Sum(x => x.FOR_HIM);

                    if (Balnce == 0) { 
                    
                        if(acc.FROM_HIM < acc.FOR_HIM)
                        {
                            modelList.Add(acc);
                        }
                    }
                    else if (Balnce == 1)
                    {
                        if (acc.FROM_HIM > acc.FOR_HIM)
                        {
                            modelList.Add(acc);
                        }
                    }
                    else
                    {
                        modelList.Add(acc);
                    }
                    
                }
           
           }

            return View(modelList);
        }
     
        public ActionResult AdjustingEntry(long? AccounNo, int? Project, DateTime? FromDate, DateTime? ToDate)
        {
            ACCOUNTTBL_Business b = new ACCOUNTTBL_Business();

           
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
           
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x=>x.OP_ACC==true);

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();




            List<ACCOUNTTBL_Model> model=new List<ACCOUNTTBL_Model>();  
            try {
                model = b.Search(AccounNo,Project,1,FromDate,ToDate).OrderByDescending(x=>x.ACC_DATE).ToList();
             

            } catch {  }
            return View(model); 
           
        }




      }
    }
