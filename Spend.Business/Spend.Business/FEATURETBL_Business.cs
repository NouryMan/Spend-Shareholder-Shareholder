using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class FEATURETBL_Business
    {
        oracleDbContextSpend db;
        public FEATURETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<FEATURETBL_Model> getall()
        {
            List<FEATURETBL_Model> obj1 = db.FEATURETBL_Model.ToList();


            return obj1;
        }
        public FEATURETBL_Model GetPyID(int id)
        {
            FEATURETBL_Model obj = db.FEATURETBL_Model.Find(id);


            return obj;
        }
    }
}
