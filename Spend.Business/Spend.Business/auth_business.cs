using Spend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Spend.Models;

namespace Spend.Business
{
    public class userSession
    {
        //public HR_EMPLOYEE EMPLOYEE { get; set; }
        //public ERP_POSITION POSITION { get; set; }
        public PERSONAL_INFOTBL_Model PERSONAL_INFO { get; set; }
        //public HR_SCHEME_HD SCHEME_HD { get; set; }
    }




    public class auth_business
    {


        class UserManager
        {



            public bool IsValid(string LOGIN_NAME, string LOGIN_PASS)
            {
                try
                {
                    // int user_id = Convert.ToInt32(LOGIN_NAME);
                    using (var db = new oracleDbContextSpend()) // use your DbConext
                    {
                        //string ps = passHash(LOGIN_PASS);
                        return db.USERSTBL_Model.Any(u => u.LOGIN_NAME == LOGIN_NAME && u.LOGIN_PASS == LOGIN_PASS && u.ENABLE_LOGIN == true);

                    }
                }
                catch (Exception ex) { string s = ex.Message; }
                return false;
            }
        }


        public bool chackPass(string LOGIN_NAME, string LOGIN_PASS)
        {

            string ps = passHash(LOGIN_PASS);//not used
            UserManager us = new UserManager();
            bool us1 = us.IsValid(LOGIN_NAME, LOGIN_PASS);
            return us1;


        }

        public string passHash(string pass)
        {
            byte[] data = Encoding.ASCII.GetBytes(pass);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
            string r = Encoding.ASCII.GetString(result);
            return r;
        }

        public long getUserId(string LOGIN_NAME, string LOGIN_PASS)
        {
            try
            {
                // int user_id = Convert.ToInt32(LOGIN_NAME);
                using (var db = new oracleDbContextSpend()) // use your DbConext
                {
                    //string ps = passHash(LOGIN_PASS);
                    return db.USERSTBL_Model.Where(u => u.LOGIN_NAME == LOGIN_NAME && u.LOGIN_PASS == LOGIN_PASS).FirstOrDefault().PERSON_ID.Value;

                }
            }
            catch (Exception ex) { string s = ex.Message; }
            return 0;
        }

        public ClaimsIdentity logIn(string LOGIN_NAME, string LOGIN_PASS)
        {
           



            ClaimsIdentity ci = null;
            if (chackPass(LOGIN_NAME, LOGIN_PASS))
            {

                USERSTBL_Model login = db.USERSTBL_Model.Where(u => u.LOGIN_NAME == LOGIN_NAME && u.LOGIN_PASS == LOGIN_PASS && u.ENABLE_LOGIN==true).FirstOrDefault();

                ci = getUserRols(login);
                var fullName = ClaimsPrincipal.Current.Identities;
               
                //.GetFullName();


                //try
                //{
                //   //leter//noury
                //   // PERSONAL_INFOTBL_business pb = new PERSONAL_INFOTBL_business();

                //   // PERSONAL_INFOTBL_Model p = pb.GetByID(login.PERSONAL_INFO_ID);

                //    IList<Claim> claimCollection = new List<Claim>
                // {
                //  new Claim(ClaimTypes.Name,"p.FULL_NAME_AR")
                //     , new Claim(ClaimTypes.NameIdentifier, "p.ID.ToString()")
                //     , new Claim(ClaimTypes.Country, "yemen")
                //     , new Claim(ClaimTypes.Gender, "M")
                //     , new Claim(ClaimTypes.Surname, "p.CODE")
                //     , new Claim(ClaimTypes.Email, "p.EMAIL")
                //     , new Claim(ClaimTypes.Role, "admin")
                // };

                //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection);

                //    claimsIdentity.Label = "acc";

                //    System.Security.Claims.ClaimsPrincipal.Current.AddIdentity(claimsIdentity);

                //    //System.Security.Principal.SecurityIdentifier ii = System.Security.Principal.WindowsIdentity.GetCurrent().User;
                //    //System.Security.Principal.WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();



                //}
                //catch
                //{
                //}
        
            
            }




            return ci;
        }

        private List<int> getRoleList(string roles)
        {
            List<int> sl = null;
            try
            {
                List<int> jsonObject = JsonConvert.DeserializeObject<List<int>>(roles);
                sl = (List<int>)jsonObject;

            }
            catch { }

            return sl;
        }

        public List<FEATURETBL_Model> user_getRoles(List<USER_ROLETBLE_Model> roles)
        {

          //  List<int> sl = getRoleList(roles);


            List<FEATURETBL_Model> Featurelist = new List<FEATURETBL_Model>();

            if (roles != null && roles.Count() > 0)
            {
                foreach (var role  in roles)
                {
                    if (role.ROLETBL_Model.FEATURE_ROLETBL_Collection != null && role.ROLETBL_Model.FEATURE_ROLETBL_Collection.Count() > 0)
                    {
                        foreach (var featrole in role.ROLETBL_Model.FEATURE_ROLETBL_Collection)
                        {

                            var tr = Featurelist.Where(x => x.ID == featrole.ID).FirstOrDefault();
                            if (tr == null)
                            {

                                Featurelist.Add(featrole.FEATURETBL_Model);
                            }
                        }
                    }



                   
                }

            }

            return Featurelist;
        }

        private ClaimsIdentity getUserRols(USERSTBL_Model logIn)
        {

            // ERP_PERSONAL_INFO p_info = db.ERP_PERSONAL_INFOS.Find(logIn.PERSONAL_INFO_ID);
           // List<FEATURETBL_Model> FEATURETBLlist = logIn.USER_ROLETBLE_Collection.FirstOrDefault().ROLETBL_Model.FEATURE_ROLETBL_Collection.FirstOrDefault().FEATURETBL_Model.CODE;



            List<FEATURETBL_Model> FEATURETBLlist = user_getRoles(logIn.USER_ROLETBLE_Collection.ToList());

           

            List<Claim> claimList = new List<Claim>();

            claimList.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));


            claimList.Add(new Claim(ClaimTypes.Name, logIn.PERSONAL_INFOTBL_Model.AR_NAME.ToString()));
            claimList.Add(new Claim(ClaimTypes.NameIdentifier, logIn.USER_ID.ToString()));
            claimList.Add(new Claim(ClaimTypes.Surname, logIn.LOGIN_NAME));
            //claimList.Add(new Claim(ClaimTypes.UserData, u.ICON));
            try { claimList.Add(new Claim(ClaimTypes.Email, logIn.PERSONAL_INFOTBL_Model.EMAIL_ADDRESS)); } catch { }
            try
            {
                claimList.Add(new Claim(ClaimTypes.PrimarySid, logIn.PERSONAL_INFOTBL_Model.DESCRIPTION.ToString()));
            }
            catch { }

            if (FEATURETBLlist != null && FEATURETBLlist.Count() > 0)
            {
                foreach (var r in FEATURETBLlist)
                {
                    claimList.Add(new Claim(ClaimTypes.Role, r.CODE));
                }
            }

            //
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimList);

            //claimsIdentity.Label = constants.IdentitiesLabel_acc;

            System.Security.Claims.ClaimsPrincipal.Current.AddIdentity(claimsIdentity);
            //
            var ident = new ClaimsIdentity(claimList.ToArray(), DefaultAuthenticationTypes.ApplicationCookie);
            return ident;
        }






        private oracleDbContextSpend db;



        public auth_business()
        {
            db = new oracleDbContextSpend();
        }

        public int Create(USERSTBL_Model item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<USERSTBL_Model> GetAll()
        {
            return db.USERSTBL_Model.ToList();
        }

        public USERSTBL_Model GetByID(int id)
        {
            return db.USERSTBL_Model.Find(id);
        }

        public IEnumerable<USERSTBL_Model> SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(USERSTBL_Model item)
        {
            throw new NotImplementedException();
        }
    }

}
