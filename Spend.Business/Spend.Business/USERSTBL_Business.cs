using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class USERSTBL_Business
    {
        oracleDbContextSpend db;
        public USERSTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<USERSTBL_Model> getall()
        {
            List<USERSTBL_Model> obj1 = db.USERSTBL_Model.ToList();


            return obj1;
        }
        public USERSTBL_Model GetPyID(int id)
        {
            USERSTBL_Model obj = db.USERSTBL_Model.Find(id);


            return obj;
        }


        public int Create(USERSTBL_Model User)
        {
            int return_value = 0;
            try
            {
                User.USER_ID = getMaxid();
                User.CR_DATE = DateTime.Now;
                User.ENABLE_LOGIN = true;
                db.USERSTBL_Model.Add(User);
                 return_value = db.SaveChanges();
                if (return_value > 0)
                {
                    return_value = User.USER_ID;
                }
            }
            catch
            {

            }
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.USERSTBL_Model.Max(x => x.USER_ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }

        public int Update(USERSTBL_Model User)
        {
            User.UP_DATE = DateTime.Now;


            db.Entry(User).State = EntityState.Modified;
            db.Entry(User).Property(x => x.CHANGE_PASS).IsModified = false;
            db.Entry(User).Property(x => x.CR_DATE).IsModified = false;
            db.Entry(User).Property(x => x.FULL_GRANTS).IsModified = false;
            db.Entry(User).Property(x => x.IS_MAIN).IsModified = false;
            db.Entry(User).Property(x => x.LOGIN_PERIOD).IsModified = false;
            db.Entry(User).Property(x => x.NOTE).IsModified = false;
            db.Entry(User).Property(x => x.PERSON_ID).IsModified = false;
            db.Entry(User).Property(x => x.REG_DATE).IsModified = false;
            db.Entry(User).Property(x => x.USER_CR).IsModified = false;
           


            int return_value = db.SaveChanges();
            return return_value;
        }






        public int AsddUserRole(int id,string Roles )
        {
            try
            {
                var Rolelist = Roles.Split(',').ToList();
                USER_RoleTBLE_Business user_role_b = new USER_RoleTBLE_Business();
                var user_role = user_role_b.getall().Where(x => x.USER_ID == id);

                foreach (var item in user_role)
                {
                    var isfind = Rolelist.FirstOrDefault(x => x == item.ID.ToString());
                    if (isfind != null)
                    {
                        Rolelist.Remove(isfind);
                    }
                    else
                    {
                        USER_RoleTBLE_Business user_ROLE_b = new USER_RoleTBLE_Business();
                        user_ROLE_b.delete(item.ID);
                    }
                }
                if (Rolelist.Count() > 0 )
                {
                    foreach (var role in Rolelist)
                    {
                        if (role != "")
                        {
                            USER_ROLETBLE_Model User_ROLE = new USER_ROLETBLE_Model();
                            USER_RoleTBLE_Business User_ROLE_b = new USER_RoleTBLE_Business();
                            User_ROLE.USER_ID = id;
                            User_ROLE.ROLE_ID = Convert.ToInt32(role);
                            User_ROLE_b.Create(User_ROLE);
                        }
                    }
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }



    }
}
