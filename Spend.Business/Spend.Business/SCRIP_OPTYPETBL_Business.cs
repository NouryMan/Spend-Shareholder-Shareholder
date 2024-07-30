using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class SCRIP_OPTYPETBL_Business
    {
        oracleDbContextSpend db;
        public SCRIP_OPTYPETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SCRIP_OPTYPETBL_Model> getall()
        {
            List<SCRIP_OPTYPETBL_Model> obj = db.SCRIP_OPTYPETBL_Model.Where(x => x.IS_DELETED == false).ToList();


            return obj;
        }
        public SCRIP_OPTYPETBL_Model GetPyID(int id)
        {
            SCRIP_OPTYPETBL_Model obj = db.SCRIP_OPTYPETBL_Model.Find(id);


            return obj;
        }
    }
}
