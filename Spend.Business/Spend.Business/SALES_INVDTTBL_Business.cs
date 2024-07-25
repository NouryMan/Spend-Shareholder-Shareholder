using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class SALES_INVDTTBL_Business
    {
        oracleDbContextSpend db;
        public SALES_INVDTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SALES_INVDTTBL_Model> getall()
        {
            List<SALES_INVDTTBL_Model> obj = db.SALES_INVDTTBL_Model.ToList();


            return obj;
        }
        public SALES_INVDTTBL_Model GetPyID(int id)
        {
            SALES_INVDTTBL_Model obj = db.SALES_INVDTTBL_Model.Find(id);


            return obj;
        }


    

    }
}
