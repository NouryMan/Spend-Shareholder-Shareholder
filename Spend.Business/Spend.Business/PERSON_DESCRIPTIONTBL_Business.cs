using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class PERSON_DESCRIPTIONTBL_Business
    {
        oracleDbContextSpend db;
        public PERSON_DESCRIPTIONTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<PERSON_DESCRIPTIONTBL_Model> getall()
        {



            List<PERSON_DESCRIPTIONTBL_Model> obj = db.PERSON_DESCRIPTIONTBL_Model.ToList();


            return obj;


        }
        public PERSON_DESCRIPTIONTBL_Model GetPyID(int id)
        {
            PERSON_DESCRIPTIONTBL_Model obj = db.PERSON_DESCRIPTIONTBL_Model.Find(id);


            return obj;


        }
    }
}
