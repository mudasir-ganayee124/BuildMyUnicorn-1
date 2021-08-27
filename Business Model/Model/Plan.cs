using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;


namespace Business_Model.Model
{
    public class Plan : Common
    {
        public Guid PlanID { get; set; }
        public string PlanName { get; set; }
        public string Url { get; set; }
        public Guid CurrencyID { get; set; }
        public decimal Amount { get; set; }
        public string PlanHeading { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string PlanSubHeading { get; set; }
        public string Method { get; set; }
        public int DisplayOrder { get; set; }
        public EntityState EntityState { get; set; }
        public Guid PlanRecurringID { get; set; }

        public Frequency Duration { get; set; }
        public Frequency Frequency { get; set; }
        public string DurationString => Duration.ToString();
        public string FrequencyString => Frequency.ToString();
        public bool ProcessAutomatically { get; set; }
        public IEnumerable<PlanAttribute> PlanAttribute { get; set; }
    }
    public class PlanAttribute
    {
        public Guid PlanAttributeID { get; set; }
        public Guid PlanID { get; set; }
        public string Attribute { get; set; }
        public int DisplayOrder { get; set; }

    }

    public class PlanRecurring
    {
        public Guid PlanRecurringID { get; set; }
        public Guid PlanID { get; set; }    
        public Frequency Frequency { get; set; }
        public bool ProcessAutomatically { get; set; }
    }
}
