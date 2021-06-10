using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Business_Model.Helper;
namespace BuildMyUnicorn.Business_Layer
{
    public class SellingManager
    {
        public _PricingProductService GetPricingProductService()
        {
            var query = $@"SELECT dbo.tbl_selling_productservicepricing.*  FROM  dbo.tbl_selling_productservicepricing WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_PricingProductService>(query);

        }

        public _Customer GetCustomers()
        {
            var query = $@"SELECT dbo.tbl_selling_customer.*  FROM  dbo.tbl_selling_customer WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_Customer>(query);

        }

        public string AddCustomer(Customer Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CustomerID", ParamterValue = Model.CustomerID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@About", ParamterValue = Model.About, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Based", ParamterValue = Model.Based, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Buy", ParamterValue = Model.Buy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Factors", ParamterValue = Model.Factors, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RecognitionNeed", ParamterValue = Model.BuyerPurchaseCycle.RecognitionNeed, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InformationSearch", ParamterValue = Model.BuyerPurchaseCycle.InformationSearch, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AlternativeEvaluation", ParamterValue = Model.BuyerPurchaseCycle.AlternativeEvaluation, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PurchaseDecision", ParamterValue = Model.BuyerPurchaseCycle.PurchaseDecision, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PurchaseEvaluation", ParamterValue = Model.BuyerPurchaseCycle.PurchaseEvaluation, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_customer", parameters);
            if (result > 0) return "OK"; else return "Error in adding business customer";

        }

        public string AddBuyerPersona(BuyerPersona Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));

            List<ParametersCollection> parameterswot = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@BuyerPersonaID", ParamterValue = Model.BuyerPersonaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomerID", ParamterValue = Model.CustomerID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Name", ParamterValue = Model.Name, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AvtarID", ParamterValue = Model.AvtarID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@JobTitle", ParamterValue = Model.JobTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Responsibility", ParamterValue = Model.Responsibility, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AgeID", ParamterValue = Model.AgeID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GenderID", ParamterValue = Model.GenderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IncomeID", ParamterValue = Model.IncomeID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Location", ParamterValue = Model.Location, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PainPoints", ParamterValue = Model.PainPoints, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Goals", ParamterValue = Model.Goals, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WantsKnow", ParamterValue = Model.WantsKnow, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@NotWantsKnow", ParamterValue = Model.NotWantsKnow, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DriveBuy", ParamterValue = Model.DriveBuy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BuyFrom", ParamterValue = Model.BuyFrom, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Findus", ParamterValue = Model.Findus, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }

            };
            int returnValue = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_customer_buyerpersona", parameterswot);
            if (returnValue > 0) return "OK"; else return "Error in adding buyer persona";

        }

        public string AddPricingProductService(PricingProductService Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ProductServicePricingID", ParamterValue = Model.ProductServicePricingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PricingStrategy", ParamterValue = Model.ChosePricingStrategy.PricingStrategy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductValue", ParamterValue = Model.ChosePricingStrategy.ProductValue, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductUnique", ParamterValue = Model.ChosePricingStrategy.ProductUnique, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomersValue", ParamterValue = Model.ChosePricingStrategy.CustomersValue, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@UsersBringValue", ParamterValue = Model.ChosePricingStrategy.UsersBringValue, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomersOftenToPay", ParamterValue = Model.ChosePricingStrategy.CustomersOftenToPay, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomersWillingToPay", ParamterValue = Model.ChosePricingStrategy.CustomersWillingToPay, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CostDeliverPicture", ParamterValue = Model.ChosePricingStrategy.CostDeliverPicture, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OfferCustomers", ParamterValue = Model.ChosePricingStrategy.OfferCustomers, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OfferOpportunity", ParamterValue =Model.ChosePricingStrategy.OfferOpportunity, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OfferLevels", ParamterValue = Model.ChosePricingStrategy.OfferLevels, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomerBuy", ParamterValue = Model.ChosePricingStrategy.CustomerBuy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SalesCertainPeriod", ParamterValue = Model.ChosePricingStrategy.SalesCertainPeriod, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PricingStrategyChosen", ParamterValue = Model.ChosePricingStrategy.PricingStrategyChosen, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_pricing_productservice", parameters);
            if (result > 0) return "OK"; else return "Error in adding product service pricing";

        }

        public IEnumerable<BuyerPersona> GetCustomerbuyerPersona(Guid CustomerID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CustomerID ", ParamterValue = CustomerID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<BuyerPersona>(CommandType.StoredProcedure, "sp_get_business_customer_buyerpersona", parameters);

        }


        public int ExistCustomer(Guid id)
        {
            var query = $@"select count(CustomerID) from tbl_selling_customer WHERE CustomerID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(CustomerID) from tbl_selling_customer WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }
        public int ExistPricingProductService(Guid id)
        {
            var query = $@"select count(ProductServicePricingID) from tbl_selling_productservicepricing WHERE ProductServicePricingID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(ProductServicePricingID) from tbl_selling_productservicepricing WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }

    }
}