using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using System.Threading;
using System.Net;
using System.Text;
using Spend.Business;
using Spend.Models;

namespace Shareholder_System.Controllers
{
   // [Authorize]
    public class authController : Controller
    {
        [AllowAnonymous]
        public ActionResult login()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult login(string LOGIN_NAME, string LOGIN_PASS, string ReturnUrl)
        {
            auth_business aub = new auth_business();

            if (LOGIN_NAME != null && LOGIN_PASS != null)
            {


                ClaimsIdentity ci = aub.logIn(LOGIN_NAME, LOGIN_PASS);


                if (ci != null)
                {

                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ci);

                    //IAuthenticationManager authManager = Request.GetOwinContext().Authentication;

                    //authManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, ci);
                    PERSONAL_INFOTBL_Business PERSONAL_INFO_B = new PERSONAL_INFOTBL_Business();

                    long p_id = aub.getUserId(LOGIN_NAME, LOGIN_PASS);
                    //fillSissino(p_id);
                    fillSissinoGlobal(p_id);


                    return RedirectToLocal(ReturnUrl);
                    // return RedirectToAction("index", "main"); // auth succeed 
                }
            }

            // invalid username or password
            ModelState.AddModelError("", "اسم المستخدم او كلمة المرور خطاء.");
            return View();
        }
         public void fillSissinoGlobal(long id)
        {
            try
            {
                // int uid = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUserId());
                PERSONAL_INFOTBL_Business pinfob = new PERSONAL_INFOTBL_Business();

                PERSONAL_INFOTBL_Model p = pinfob.GetByID(id);

                userSession us = new userSession()
                {
                    PERSONAL_INFO = p

                };

                System.Web.HttpContext.Current.Session["userSession"] = us;
            }
            catch (Exception ex) { string s = ex.Message; }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult logoff()
        {

            AuthenticationManager.SignOut();

            System.Web.HttpContext.Current.Session["userSession"] = null;
            return RedirectToAction("login", "auth");
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public userSession getUserSessionGlobal()
        {
           userSession gus = (userSession)System.Web.HttpContext.Current.Session["userSession"];
            USERSTBL_Business uSERSTBL_ = new USERSTBL_Business();
            long usr_id = 0;
            try
            {
               
                
                usr_id = uSERSTBL_.GetPyID(Convert.ToInt32(User.Identity.GetUserId())).PERSON_ID.Value;
                if (gus != null)
                {

                    if (usr_id != gus.PERSONAL_INFO.ID)
                    {
                        usr_id = 0;
                        gus = null;
                        System.Web.HttpContext.Current.Session["userSession"] = null;
                    }
                }
            }
            catch { }
            if (gus == null || usr_id == 0)
            {
                long x = 0;
                if (User == null)
                {
                    try { x = uSERSTBL_.GetPyID(Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUserId())).PERSON_ID.Value; } catch { }
                }
                else
                {
                    x = uSERSTBL_.GetPyID(Convert.ToInt32(User.Identity.GetUserId())).PERSON_ID.Value;
                }

                fillSissinoGlobal(x);

                gus = (userSession)System.Web.HttpContext.Current.Session["userSession"];
            }
            return gus;
        }

        [AllowAnonymous]
        public ActionResult acessDenied()
        {
            if (User.IsInRole("HR_ACCESS"))
            {
                ViewBag.Layout = 1;

            }
            else
            {
                ViewBag.Layout = 0;
            }
            return View();
        }
    }
}