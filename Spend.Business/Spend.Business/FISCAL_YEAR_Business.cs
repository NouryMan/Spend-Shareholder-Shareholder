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
    public class FISCAL_YEAR_Business
    {
        oracleDbContextSpend db;
        public FISCAL_YEAR_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<FISCAL_YEAR_Model> GetAll()
        {
            List<FISCAL_YEAR_Model> obj = db.FISCAL_YEAR_Model.Where(x=>x.IS_DELETE==false).ToList();

            return obj;
        }
        public FISCAL_YEAR_Model GetPyID(long id)
        {
            FISCAL_YEAR_Model obj = db.FISCAL_YEAR_Model.Find(id);


            return obj;
        }

        public long GetMaxID()
        {
            long id = 0;
            try
            {
                id = db.FISCAL_YEAR_Model.Max(x => x.YEAR_ID) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public int Create(FISCAL_YEAR_Model fisal)
        {

            fisal.YEAR_ID = GetMaxID();
            fisal.CR_DATE = DateTime.Now;
            fisal.IS_DELETE =false;
            fisal.IS_ENABLED = true;
            fisal.STATUS = 0;// لم تفتح او تغلق بعد 
            db.FISCAL_YEAR_Model.Add(fisal);

            int return_value = db.SaveChanges();


            return return_value;
        }

        public int Update(FISCAL_YEAR_Model fisal)
        {
            int return_value = 0;
            if (fisal.STATUS == 0)
            {
                fisal.UP_DATE = DateTime.Now;

                db.Entry(fisal).State = EntityState.Modified;
                db.Entry(fisal).Property(x => x.IS_ENABLED).IsModified = false;
                db.Entry(fisal).Property(x => x.IS_DELETE).IsModified = false;
                db.Entry(fisal).Property(x => x.CR_DATE).IsModified = false;
                db.Entry(fisal).Property(x => x.USER_CR).IsModified = false;
                return_value = db.SaveChanges();

            }
            return return_value;
        }


        public int Open(long id)
        {
            int return_value = 0;
            var obj = db.FISCAL_YEAR_Model.Find(id);
            obj.STATUS = 1;
            try
            {
                db.Entry(obj).State = EntityState.Modified;
                return_value = db.SaveChanges();
            }
            catch
            {
            }

            return return_value;
        }

        public int Close(long id)
        {
            int return_value = 0;
            var obj = db.FISCAL_YEAR_Model.Find(id);
            obj.STATUS = 2;
            try
            {
                db.Entry(obj).State = EntityState.Modified;
                return_value = db.SaveChanges();
            }
            catch
            {
            }

            return return_value;
        }


        public int Delete(long id)
		{
			int return_value = 0;
			var obj = db.FISCAL_YEAR_Model.Find(id);
			obj.IS_DELETE = true;
			try
			{
				db.Entry(obj).State = EntityState.Modified;
				return_value = db.SaveChanges();
			}
			catch
			{
			}

			return return_value;
		}
    }
}
