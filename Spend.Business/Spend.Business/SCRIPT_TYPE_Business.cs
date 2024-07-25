using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class SCRIPT_TYPE_Business
    {
        oracleDbContextSpend db;
        public SCRIPT_TYPE_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SCRIP_TYPETBL_Model> getall()
        {
            List<SCRIP_TYPETBL_Model> obj = db.SCRIP_TYPETBL_Model.ToList();


            return obj;
        }
        public SCRIP_TYPETBL_Model GetPyID(int id)
        {
            SCRIP_TYPETBL_Model obj = db.SCRIP_TYPETBL_Model.Find(id);


            return obj;
        }
    }
}
