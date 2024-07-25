using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class UTLISTTBL_Business
    {
        oracleDbContextSpend db;
        public UTLISTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<UTLISTTBL_Model> getall()
        {
            List<UTLISTTBL_Model> obj = db.UTLISTTBL_Model.ToList();


            return obj;
        }
        public UTLISTTBL_Model GetPyID(int id)
        {
            UTLISTTBL_Model obj = db.UTLISTTBL_Model.Find(id);


            return obj;
        }
    }
}
