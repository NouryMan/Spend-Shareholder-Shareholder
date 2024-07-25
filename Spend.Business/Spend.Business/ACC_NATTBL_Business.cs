using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class ACC_NATTBL_Business
    {
        oracleDbContextSpend db;
        public ACC_NATTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACC_NATTBL_Model> getall()
        {



            List<ACC_NATTBL_Model> obj = db.ACC_NATTBL_Model.ToList();


            return obj;


        }
        public ACC_NATTBL_Model GetPyID(int id)
        {
            ACC_NATTBL_Model obj = db.ACC_NATTBL_Model.Find(id);


            return obj;


        }


    }
}
