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
  public  class ARCHIVETBL_Business
    {
        oracleDbContextSpend db;
        public ARCHIVETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ARCHIVETBL_Model> getall()
        {
            List<ARCHIVETBL_Model> obj = db.ARCHIVETBL_Model.ToList();


            return obj;
        }
        public ARCHIVETBL_Model GetPyID(int id)
        {
            ARCHIVETBL_Model obj = db.ARCHIVETBL_Model.Find(id);


            return obj;
        }

        public int Create(ARCHIVETBL_Model ARCHIVE)
        {
            ARCHIVE.ID = getMaxid();
            ARCHIVE.CR_DATE = DateTime.Now;
            db.ARCHIVETBL_Model.Add(ARCHIVE);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(ARCHIVETBL_Model ARCHIVE)
        {

            ARCHIVE.UP_DATE = DateTime.Now;

            db.Entry(ARCHIVE).State = EntityState.Modified;
            db.Entry(ARCHIVE).Property(x => x.CR_DATE).IsModified = false;
            db.Entry(ARCHIVE).Property(x => x.USER_CR).IsModified = false;



            int return_value = db.SaveChanges();
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.ARCHIVETBL_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            ARCHIVETBL_Model obj = db.ARCHIVETBL_Model.Find(id);

            try
            {
                db.ARCHIVETBL_Model.Remove(obj);
                return_value = db.SaveChanges();
                return return_value;

            }
            catch
            {

            }


            return return_value;
        }

    }
}
