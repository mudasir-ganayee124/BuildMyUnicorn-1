using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
   public class ProgressAnalytic
    {
        public Guid ProgressAnalyticID { get; set; }
        public Guid ClientID { get; set; }
        public decimal ProgressData { get; set; }
        public DateTime ProgressDate { get; set; }
        public int DisplayOrder { get; set; }
    }
}
