using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models.Helper
{
    public class TransModelView
    {
   
        public int ACTION_TYPE { get; set; }
        public int OP_TYPE { get; set; }
        public int SOURCE_BOX { get; set; }
        public int TARGET_PROJ { get; set; }
        public int? BUILDING_ID { get; set; }
        public int? UNIT_ID { get; set; }
        public DateTime DATE_M { get; set; }
        public string DATE_H { get; set; }
        public string NOTE { get; set; }

       public List<ACCH_OPBOXTBL_Model> ACCH_OPBOXTBLs { get; set; }


    }
}
