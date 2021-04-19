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

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_PricingProductService>(CommandType.StoredProcedure, "sp_get_pricing_productservice_by_client", parameters);

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
                new ParametersCollection { ParamterName = "@PricingStrategyChosen", ParamterValue = Model.ChosePricingStrategy.PricingStrategyChosen, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_pricing_productservice", parameters);
            if (result > 0) return "OK"; else return "Error in adding product service pricing";

        }
    }
}