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
        public List<MAX_UNDER_OPV_Model> GetAll()
        {



            List<MAX_UNDER_OPV_Model> obj = db.MAX_UNDER_OPV_Model.ToList();
            List<MAX_UNDER_OPV_Model> c = db.MAX_UNDER_OPV_Model.Where(x=>x.NAM== "under_no").ToList();


            return obj;


        }
        public MAX_UNDER_OPV_Model GetById(int id)
        {
            MAX_UNDER_OPV_Model obj = db.MAX_UNDER_OPV_Model.Find(id);

            return obj;


        }
    }
}