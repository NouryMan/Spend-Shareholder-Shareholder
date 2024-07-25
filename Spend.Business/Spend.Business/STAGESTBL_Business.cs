using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class STAGESTBL_Business
    {
        oracleDbContextSpend db;
        public STAGESTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<STAGESTBL_Model> getall()
        {
            List<STAGESTBL_Model> obj = db.STAGESTBL_Model.ToList();


            return obj;
        }
        public STAGESTBL_Model GetPyID(int id)
        {
            STAGESTBL_Model obj = db.STAGESTBL_Model.Find(id);


            return obj;
        }
    }
}
