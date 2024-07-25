using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class PERSONAL_INFOController : Controller
    {
        // GET: PERSONAL_INFO
        public ActionResult Index(int? Page, string search)
        {

            PERSONAL_INFOTBL_Business b = new PERSONAL_INFOTBL_Business();

            List<PERSONAL_INFOTBL_Model> model = new List<PERSONAL_INFOTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.GetAll().Where(x => x.AR_NAME.Contains(search)).OrderByDescending(x => x.ID).ToList();
            }
            else
            {
                model = b.GetAll().OrderByDescending(x => x.ID).ToList();

            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }


    }
}