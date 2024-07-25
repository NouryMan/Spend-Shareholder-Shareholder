using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class MARKETINGTBL_Business
    {

        oracleDbContextSpend db;
        public MARKETINGTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<MARKETINGTBL_Model> getall()
        {



            List<MARKETINGTBL_Model> obj = db.MARKETINGTBL_Model.ToList();


            return obj;


        }
        public MARKETINGTBL_Model GetPyID(int id)
        {
            MARKETINGTBL_Model obj = db.MARKETINGTBL_Model.Find(id);


            return obj;


        }

    }
}
