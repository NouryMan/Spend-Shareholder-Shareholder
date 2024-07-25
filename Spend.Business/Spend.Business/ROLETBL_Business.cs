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
   public class ROLETBL_Business
    {
        oracleDbContextSpend db;
        public ROLETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ROLETBL_Model> getall()
        {
            List<ROLETBL_Model> obj = db.ROLETBL_Model.ToList();


            return obj;
        }
        public ROLETBL_Model GetPyID(int id)
        {
            ROLETBL_Model obj = db.ROLETBL_Model.Find(id);


            return obj;
        }

        public int Create(ROLETBL_Model ROLE ,string Feature)
        {
            ROLE.ID = getMaxid();
            db.ROLETBL_Model.Add(ROLE);
            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                var featurelist = Feature.Split(',');
                foreach(var featu in featurelist)
                {
                    if (featu != "")
                    {
                        FEATURE_ROLETBL_Model FEATURE_ROLE = new FEATURE_ROLETBL_Model();
                        FEATURE_ROLETBL_Business FEATURE_ROLE_b = new FEATURE_ROLETBL_Business();
                        FEATURE_ROLE.ROLE_ID = ROLE.ID;
                        FEATURE_ROLE.FEATURE_ID = Convert.ToInt32(featu);
                        FEATURE_ROLE_b.Create(FEATURE_ROLE);
                    }

                }
            }
                return return_value;
        }

        public int Update(ROLETBL_Model ROLE, string Feature)
        {



            db.Entry(ROLE).State = EntityState.Modified;
            FEATURE_ROLETBL_Business FEATURE_ROLEs_b = new FEATURE_ROLETBL_Business();


            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                var featurelist = Feature.Split(',').ToList();
                var FEATURE_ROLETBL = FEATURE_ROLEs_b.getall().Where(x=>x.ROLE_ID==ROLE.ID);

                foreach (var item in FEATURE_ROLETBL)
                {
                    var isfind = featurelist.FirstOrDefault(x => x == item.ID.ToString());
                    if (isfind!=null)
                    {
                        featurelist.Remove(isfind);
                    }
                    else
                    {
                        FEATURE_ROLETBL_Business FEATURE_ROLE_b = new FEATURE_ROLETBL_Business();
                        FEATURE_ROLE_b.delete(item.ID);
                    }
                }
                if (featurelist.Count() > 0 )
                {
                    foreach (var featu in featurelist)
                    {
                        if (featu != "")
                        {
                            FEATURE_ROLETBL_Model FEATURE_ROLE = new FEATURE_ROLETBL_Model();
                            FEATURE_ROLETBL_Business FEATURE_ROLE_b = new FEATURE_ROLETBL_Business();
                            FEATURE_ROLE.ROLE_ID = ROLE.ID;
                            FEATURE_ROLE.FEATURE_ID = Convert.ToInt32(featu);
                            FEATURE_ROLE_b.Create(FEATURE_ROLE);
                        }

                    }
                }
            }

            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.ROLETBL_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            ROLETBL_Model obj = db.ROLETBL_Model.Find(id);
          
            try
            {
              //  return_value = Update(obj);

            }
            catch
            {

            }


            return return_value;
        }
    }
}
