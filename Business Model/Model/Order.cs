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

    public class RecurringOrder
    {
        public Guid OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlanName { get; set; }
        public string StartupName { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime NextOrderDateTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Frequency Frequency { get; set; }
        public string FrequencyString => Frequency.ToString();
        public string OrderStatusString => OrderStatus.ToString();
        public bool ProcessAutomatically { get; set; }
    }

}


