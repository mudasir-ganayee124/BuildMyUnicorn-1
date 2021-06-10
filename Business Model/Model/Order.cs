using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
   public class Order
    {
        public Guid OrderID { get; set; }
        public Guid ClientID { get; set; }
        public Guid PlanID { get; set; }
        public Guid OrderPublicID { get; set; }
        public Guid GatewayClientID { get; set; }
        public Guid GatewayOrderID { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDateTime { get; set; }
    }

    public class _Order
    {
        public Guid OrderID { get; set; }
        public Guid ClientID { get; set; }
        public Guid PlanID { get; set; }
        public Guid OrderPublicID { get; set; }
        public Guid GatewayClientID { get; set; }
        public Guid GatewayOrderID { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string StartupName { get; set; }
        public string PlanName { get; set; }
        public decimal Amount { get; set; }
        public string Code { get; set; }
    }
}
