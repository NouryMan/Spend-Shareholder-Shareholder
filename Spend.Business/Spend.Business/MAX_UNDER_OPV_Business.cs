using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class MAX_UNDER_OPV_Business
    {

        oracleDbContextSpend db;
        public MAX_UNDER_OPV_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<MAX_UNDER_OPV_Model> getall()
        {



            List<MAX_UNDER_OPV_Model> obj = db.MAX_UNDER_OPV_Model.ToList();


            return obj;


        }
        public MAX_UNDER_OPV_Model GetPyID(int id)
        {
            MAX_UNDER_OPV_Model obj = db.MAX_UNDER_OPV_Model.Find(id);


            return obj;


        }
    }
}