using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public class PARTTBL_Business
    {
        oracleDbContextSpend db;
        public PARTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<PARTTBL_Model> getall()
        {
            List<PARTTBL_Model> obj = db.PARTTBL_Model.ToList();


            return obj;
        }
        public PARTTBL_Model GetPyID(int id)
        {
            PARTTBL_Model obj = db.PARTTBL_Model.Find(id);


            return obj;
        }
    }
}
