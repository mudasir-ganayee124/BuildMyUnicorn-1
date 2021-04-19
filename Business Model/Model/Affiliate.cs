using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Affiliate
    {
        public Guid AffiliateLinkID { get; set; }
        public Guid AccountNetworkID { get; set; }
        public string CompanyName { get; set; }
        public decimal AmountEarned { get; set; }
        public decimal TotalAmountEarned { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int Count { get; set; }
    }
}
