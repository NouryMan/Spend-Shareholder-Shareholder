using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class ACC_TYPETBL_Business
    {

        oracleDbContextSpend db;
        public ACC_TYPETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACC_TYPETBL_Model> getall()
        {

            List<ACC_TYPETBL_Model> obj = db.ACC_TYPETBL_Model.ToList();

            return obj;
        }
        public ACC_TYPETBL_Model GetPyID(int id)
        {
            ACC_TYPETBL_Model obj = db.ACC_TYPETBL_Model.Find(id);


            return obj;


        }

       

    }
}
