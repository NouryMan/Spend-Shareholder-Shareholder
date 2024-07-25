using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class UTL_TYPETBL_Business
    {
        oracleDbContextSpend db;
        public UTL_TYPETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<UTL_TYPETBL_Model> getall()
        {
            List<UTL_TYPETBL_Model> obj = db.UTL_TYPETBL_Model.ToList();


            return obj;
        }
        public UTL_TYPETBL_Model GetPyID(int id)
        {
            UTL_TYPETBL_Model obj = db.UTL_TYPETBL_Model.Find(id);


            return obj;
        }
    }
}
