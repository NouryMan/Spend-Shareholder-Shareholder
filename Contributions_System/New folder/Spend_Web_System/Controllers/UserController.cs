using Microsoft.AspNet.Identity;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        [Authorize(Roles = "User_Select")]
        public ActionResult Index()
        {
            USERSTBL_Business User_B = new USERSTBL_Business();
            var model = User_B.getall();
            return View(model);
          
        }
        [HttpGet]
        [Authorize(Roles = "User_Create")]
        public ActionResult Create()
        {
            ROLETBL_Business Roll_B = new ROLETBL_Business();
            PERSONAL_INFOTBL_Business pERSONAL_B = new PERSONAL_INFOTBL_Business();
            ViewBag.Roll = Roll_B.getall();
            ViewBag.personalinfo = pERSONAL_B.GetAll();

            return View();

        }

        [HttpPost]
        [Authorize(Roles = "User_Create")]
        public ActionResult Create(USERSTBL_Model user, string Role)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope trns = new TransactionScope())
                    {
                        PERSONAL_INFOTBL_Business PERSONAL_B = new PERSONAL_INFOTBL_Business();
                        PERSONAL_INFOTBL_Model pERSONAL = new PERSONAL_INFOTBL_Model();
                        long PERSONAL_ID;
                        if (user.PERSON_ID > 0)
                        {
                            PERSONAL_INFOTBL_Business PERSONAL_B1 = new PERSONAL_INFOTBL_Business();
                            pERSONAL.ID = user.PERSON_ID.Value;

                            pERSONAL.AR_NAME = user.PERSONAL_INFOTBL_Model.AR_NAME;
                            pERSONAL.EN_NAME = user.PERSONAL_INFOTBL_Model.EN_NAME;
                            pERSONAL.ADDRESS1 = user.PERSONAL_INFOTBL_Model.ADDRESS1;
                            pERSONAL.EMAIL_ADDRESS = user.PERSONAL_INFOTBL_Model.EMAIL_ADDRESS;
                            pERSONAL.MOBILE_NO = user.PERSONAL_INFOTBL_Model.MOBILE_NO;
                            pERSONAL.C_ID = user.PERSONAL_INFOTBL_Model.C_ID;
                            pERSONAL.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
                            PERSONAL_ID = PERSONAL_B.Update(pERSONAL);
                        }
                        else
                        {
                            pERSONAL.AR_NAME = user.PERSONAL_INFOTBL_Model.AR_NAME;
                            pERSONAL.EN_NAME = user.PERSONAL_INFOTBL_Model.EN_NAME;
                            pERSONAL.ADDRESS1 = user.PERSONAL_INFOTBL_Model.ADDRESS1;
                            pERSONAL.EMAIL_ADDRESS = user.PERSONAL_INFOTBL_Model.EMAIL_ADDRESS;
                            pERSONAL.MOBILE_NO = user.PERSONAL_INFOTBL_Model.MOBILE_NO;
                            pERSONAL.C_ID = user.PERSONAL_INFOTBL_Model.C_ID;
                            pERSONAL.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                             PERSONAL_ID = PERSONAL_B.Create(pERSONAL);

                        }




                        if (PERSONAL_ID > 0)
                            {
                                USERSTBL_Model UserM = new USERSTBL_Model();
                                UserM.PERSON_ID = PERSONAL_ID;
                                UserM.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                                UserM.LOGIN_NAME = user.LOGIN_NAME;
                                UserM.LOGIN_PASS = user.LOGIN_PASS;


                                USERSTBL_Business User_B = new USERSTBL_Business();

                                
                                    int User_ID = User_B.Create(UserM);

                                    if (User_ID > 0)
                                    {
                                        int reusrt = User_B.AsddUserRole(User_ID, Role);
                                        if (reusrt > 0)
                                        {
                                    trns.Complete();
                                    return RedirectToAction("Index");
                                        }
                                    }


                                
                                  
                            }
                        
                        

                       
                    }
                }
                catch(Exception ex) {
                    ModelState.AddModelError("", "اسم المستخدم موجود بالفعل");
                }


            }
            PERSONAL_INFOTBL_Business pERSONAL_B = new PERSONAL_INFOTBL_Business();
            ROLETBL_Business Roll_B = new ROLETBL_Business();
            ViewBag.personalinfo = pERSONAL_B.GetAll();
            ViewBag.Roll = Roll_B.getall();
            return View();

        }







        [HttpGet]
       [Authorize(Roles = "User_Update")]
        public ActionResult User_Login_date(int id)
        {
            USERSTBL_Business User_B = new USERSTBL_Business();
         
            var model = User_B.GetPyID(id);
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "User_Update")]
        public ActionResult User_Login_date(USERSTBL_Model user, string Role)
        {
            USERSTBL_Business User_B = new USERSTBL_Business();
          
            if (ModelState.IsValid)
            {
                USERSTBL_Business User_B1 = new USERSTBL_Business();
                //if (User_B1.getall().Where(x=>x.LOGIN_NAME==user.LOGIN_NAME).Count()<1 )
                //    {
                    user.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
                    int reusrt = User_B.Update(user);
                    if (reusrt > 0)
                    {
                        return RedirectToAction("Index");
                    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "يجب تغير اسم المستخدم");
                //}

            }
           
            return View();

        }




        [HttpGet]
        [Authorize(Roles = "User_Update")]
        public ActionResult User_Role(int id)
        {
            USERSTBL_Business User_B = new USERSTBL_Business();
            ROLETBL_Business Roll_B = new ROLETBL_Business();
            ViewBag.Roll = Roll_B.getall();
            var model = User_B.GetPyID(id);
            return View(model);

        }
        [HttpPost]
        [Authorize(Roles = "User_Update")]
        public ActionResult User_Role(USERSTBL_Model user,string Role)
        {
            USERSTBL_Business User_B = new USERSTBL_Business();

            if (ModelState.IsValid)
            {

                int reusrt = User_B.AsddUserRole(user.USER_ID, Role);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            ROLETBL_Business Roll_B = new ROLETBL_Business();
           ViewBag.Roll = Roll_B.getall();
          
            return View(user);

        }

        [Authorize(Roles = "User_Create")]
        public JsonResult getPersondeteiles(int id)
        {
            PERSONAL_INFOTBL_Business pERSONAL_B = new PERSONAL_INFOTBL_Business();

            var personal = pERSONAL_B.GetByID(id);
            string[] data = { personal.AR_NAME, personal.EN_NAME,personal.C_ID,personal.ADDRESS1, personal.MOBILE_NO,personal.EMAIL_ADDRESS };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}