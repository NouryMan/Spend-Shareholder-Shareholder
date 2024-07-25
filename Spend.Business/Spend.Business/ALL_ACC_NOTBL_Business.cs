using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
 public   class ALL_ACC_NOTBL_Business
    {
        oracleDbContextSpend db;
        public ALL_ACC_NOTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ALL_ACC_NOTBL_Model> getall()
        {



            List<ALL_ACC_NOTBL_Model> obj = db.ALL_ACC_NOTBL_Model.ToList();
            //foreach (var item in obj)
            //{
            //    try
            //    {
            //        item.VAT_NO = db.PERSONAL_INFOTBL_Model.Find(item.PERSONAL_NO).VAT_NO.ToString();
            //    }
            //    catch
            //    {

            //    }
            //}

            return obj;


        }
        public ALL_ACC_NOTBL_Model GetPyID(long id)
        {
            ALL_ACC_NOTBL_Model obj = db.ALL_ACC_NOTBL_Model.Find(id);


            return obj;


        }
        public bool IsExist(long id)
        {

            try
            {
                return  db.ALL_ACC_NOTBL_Model.Any(x=>x.ACC_NO==id);
            }
            catch (Exception ex) { string s = ex.Message; }
            return false;
        }
       

        



    }
}
