using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class VATTBL_Business
    {
        oracleDbContextSpend db;
        public VATTBL_Business()
        {

           
                db = new oracleDbContextSpend();

        }
        public List<VATTBL_Model> getall()
        {



            List<VATTBL_Model> obj = db.VATTBL_Model.ToList();


            return obj;


        }
        public VATTBL_Model GetPyID(int id)
        {
            VATTBL_Model obj = db.VATTBL_Model.Find(id);


            return obj;


        }
    }
}
