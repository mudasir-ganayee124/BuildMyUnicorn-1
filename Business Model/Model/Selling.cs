using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;
namespace Business_Model.Model
{
  
    public class PricingProductService
    {
        public Guid ProductServicePricingID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public ChosePricingStrategy ChosePricingStrategy { get; set; }
    }
    public class ChosePricingStrategy
    {
        [Description("What is the pricing strategy of your competitors")]
        public string PricingStrategy { get; set; }
        [Description("How is the value of your product delivered")]
        public string ProductValue { get; set; }
        [Description("Can free users bring you value")]
        public string UsersBringValue { get; set; }
        [Description("Is your product unique")]
        public string ProductUnique { get; set; }
        [Description("What do your customers value most ")]
        public string CustomersValue { get; set; }
        [Description("How often are they willing to pay")]
        public string CustomersOftenToPay { get; set; }
        [Description("How much are your customers willing to pay")]
        public string CustomersWillingToPay { get; set; }
        [Description("Do you have a clear picture of what it costs to create and deliver your product/service")]
        public string CostDeliverPicture { get; set; }
        [Description("Do you offer your customers something your competitors can not")]
        public string OfferCustomers { get; set; }
        [Description("Is there an opportunity to offer different pricing levels or to offer bundle pricing")]
        public string OfferOpportunity { get; set; }
        [Description("Can you offer different levels at different prices")]
        public string OfferLevels { get; set; }
        [Description("How does the customer want to buy my product or service")]
        public string CustomerBuy { get; set; }
        [Description("What pricing strategy have you chosen and why?")]
        public string PricingStrategyChosen { get; set; }
        [Description("Are there certain periods of the year when your sales will vary? If so, when will you have high sales, average sales and low sales, and why? - (text Area)")]
        public string SalesCertainPeriod { get; set; }
        public decimal Completed { get; set; }
    }
    public class _PricingProductService
    {
        public Guid ProductServicePricingID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public ChosePricingStrategy ChosePricingStrategy { get; set; }
        [Description("What is the pricing strategy of your competitors")]
        public string PricingStrategy { get; set; }
        [Description("How is the value of your product delivered")]
        public string ProductValue { get; set; }
        [Description("Can free users bring you value")]
        public string UsersBringValue { get; set; }
        [Description("Is your product unique")]
        public string ProductUnique { get; set; }
        [Description("What do your customers value most ")]
        public string CustomersValue { get; set; }
        [Description("How often are they willing to pay")]
        public string CustomersOftenToPay { get; set; }
        [Description("How much are your customers willing to pay")]
        public string CustomersWillingToPay { get; set; }
        [Description("Do you have a clear picture of what it costs to create and deliver your product/service")]
        public string CostDeliverPicture { get; set; }
        [Description("Do you offer your customers something your competitors can not")]
        public string OfferCustomers { get; set; }
        [Description("Is there an opportunity to offer different pricing levels or to offer bundle pricing")]
        public string OfferOpportunity { get; set; }
        [Description("Can you offer different levels at different prices")]
        public string OfferLevels { get; set; }
        [Description("How does the customer want to buy my product or service")]
        public string CustomerBuy { get; set; }
        [Description("What pricing strategy have you chosen and why?")]
        public string PricingStrategyChosen { get; set; }
        [Description("Are there certain periods of the year when your sales will vary? If so, when will you have high sales, average sales and low sales, and why? - (text Area)")]
        public string SalesCertainPeriod { get; set; }
        public decimal Completed { get; set; }
    }
}
