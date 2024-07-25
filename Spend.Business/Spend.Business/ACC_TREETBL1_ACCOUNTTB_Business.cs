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
    public class ACC_TREETBL1_ACCOUNTTB_Business
    {
        oracleDbContextSpend db;
        public ACC_TREETBL1_ACCOUNTTB_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACC_TREETBL1_ACCOUNTTB_Model> getall()
        {
            List<ACC_TREETBL1_ACCOUNTTB_Model> obj = db.ACC_TREETBL1_ACCOUNTTB_Model.Where(x => x.IS_DELETE == false).ToList();


            return obj;
        }
        public ACC_TREETBL1_ACCOUNTTB_Model GetPyID(int id)
        {
            ACC_TREETBL1_ACCOUNTTB_Model obj = db.ACC_TREETBL1_ACCOUNTTB_Model.Find(id);


            return obj;
        }

        public int Create(ACC_TREETBL1_ACCOUNTTB_Model item)
        {
            item.ID = getMaxid();




            db.ACC_TREETBL1_ACCOUNTTB_Model.Add(item);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(ACC_TREETBL1_ACCOUNTTB_Model item)
        {



            db.Entry(item).State = EntityState.Modified;



            int return_value = db.SaveChanges();
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.ACC_TREETBL1_ACCOUNTTB_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            ACC_TREETBL1_ACCOUNTTB_Model obj = db.ACC_TREETBL1_ACCOUNTTB_Model.Find(id);
            obj.IS_DELETE = true;

            try
            {
                return_value = Update(obj);

            }
            catch
            {

            }


            return return_value;
        }
    }
}
