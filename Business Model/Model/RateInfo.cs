using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
   public class RateInfo
    {
        public Guid RateInfoID { get; set; }
        public Guid ClientID { get; set; }
        public int ModuleID { get; set; }
        public int SectionID { get; set; }
        public decimal Rating { get; set; }
        public string Note { get; set; }
    }
}
