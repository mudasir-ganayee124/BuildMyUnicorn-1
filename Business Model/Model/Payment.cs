using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class _Payment
    {
        public Guid PaymentID { get; set; }
        public Guid ClientID { get; set; }
        public Guid OrderID { get; set; }
        public PaymentMode PaymentMode {get; set;}
        public decimal Amount { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
    public class PaymentLog
    {
        public Guid PaymentLogID { get; set; }
        public Guid OrderID { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentLogDateTime { get; set; }

    }
}
