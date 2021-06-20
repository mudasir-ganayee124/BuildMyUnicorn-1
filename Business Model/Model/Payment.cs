using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Transaction
    {
        public Guid TransactionID { get; set; }
        public Guid ClientID { get; set; }
        public Guid OrderID { get; set; }
        public PaymentMode PaymentMode {get; set;}
        public decimal Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }

    public class _Transaction
    {
        public Guid TransactionID { get; set; }
        public Guid ClientID { get; set; }
        public Guid OrderID { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }

        public string StartupName { get; set; }
        public string PlanName { get; set; }
        public string Email { get; set; }

        public string Symbol { get; set; }
    }
    public class TransactionLog
    {
        public Guid TransactionLogID { get; set; }
        public Guid OrderID { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string MerchantTransactionStatus { get; set; }
        public DateTime TransactionLogDateTime { get; set; }

    }

    public class _TransactionLog
    {
        public Guid TransactionLogID { get; set; }
        public Guid OrderID { get; set; }
        public string MerchantTransactionStatus { get; set; }
        public DateTime TransactionLogDateTime { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
        public string StartupName { get; set; }
        public string PlanName { get; set; }
        public string Email { get; set; }

        public string Symbol { get; set; }

    }
}
