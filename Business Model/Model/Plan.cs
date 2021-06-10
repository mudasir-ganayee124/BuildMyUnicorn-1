using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Plan :Common
    {
        public  Guid PlanID { get; set; }
        public string PlanName { get; set; }
        public string Url { get; set; }
        public Guid CurrencyID { get; set; }
        public decimal Amount { get; set; }
        public string PlanHeading { get; set; }
        public string Code { get; set; }
        public string PlanSubHeading { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class PlanAttribute
    {
        public Guid PlanAttributeID { get; set; }
        public Guid PlanID { get; set; }
        public int DisplayOrder { get; set; }
    }
}
