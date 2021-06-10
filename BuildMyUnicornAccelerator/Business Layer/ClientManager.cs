using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace BuildMyUnicornAccelerator.Business_Layer
{
    public class ClientManager
    {

        public string AddRateInfo(RateInfo Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = Model.ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@SectionID", ParamterValue = Model.SectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@Rating", ParamterValue = Model.Rating, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@Note", ParamterValue = Model.Note, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
        };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_rate_info", parameters);
            if (result > 0) return "OK";
            else return "Rate Info Failed, Plesase check log";
               

        }

        public List<Country> GetStartupCountryClientList(Guid AffiliateLinkID)
        {
            var query = $@"SELECT COUNT(*) as Total, tbl_countries.CountryName, latitude, longitude FROM tbl_client INNER JOIN tbl_account_network 
                           ON tbl_account_network.EntityID = tbl_client.ClientID
                           INNER JOIN tbl_countries ON tbl_client.CountryID = tbl_countries.CountryID
                           WHERE tbl_account_network.AffiliateLinkID = '{AffiliateLinkID}'
                           GROUP BY tbl_countries.CountryName, latitude, longitude";
            var _objList = SharedManager.GetList<Country>(query).ToList();
            return _objList;
        }

        public Client GetClient(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_id", parameters);
        }

        public IEnumerable<MarketKeyPlayer> GetMarketKeyPlayer(Guid OnlineResearchID)
        {
            var query = $@"SELECT dbo.tbl_onlineresearch_marketkeyplayer.*  FROM  dbo.tbl_onlineresearch_marketkeyplayer WHERE OnlineResearchID = '{OnlineResearchID}'";
            return SharedManager.GetList<MarketKeyPlayer>(query);


        }

        public IEnumerable<BuyerPersona> GetCustomerbuyerPersona(Guid CustomerID, Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CustomerID ", ParamterValue = CustomerID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<BuyerPersona>(CommandType.StoredProcedure, "sp_get_business_customer_buyerpersona", parameters);

        }

        public _Idea GetIdea(Guid ClientID)
        {

            var query = $@"SELECT tbl_idea.* FROM tbl_idea WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetSingle<_Idea>(query);
            if (Model == null) return new _Idea();
            return Model;
        }
        

        public IEnumerable<ExtendedClientTeam> GetStartupAcceleratorClient()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@StartupAcceleratorID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return  obj.GetList<ExtendedClientTeam>(CommandType.StoredProcedure, "sp_get_startup_accelerator_clients", parameters);
           
        }

        public IEnumerable<LanguageModule> GetDefaultModuleLanguage(int ModuleID, int SectionID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
              new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@SectionID", ParamterValue = SectionID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<LanguageModule>(CommandType.StoredProcedure, "sp_get_default_language_by_module", parameters);

        }

        public RateInfo GetRateInfo(RateInfo Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
              new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = Model.ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@SectionID", ParamterValue = Model.SectionID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<RateInfo>(CommandType.StoredProcedure, "sp_get_rate_info", parameters);

        }


        public OurObservation GetObservation(Guid ClientID)
        {

            var query = $@"SELECT tbl_marketresearch_obervation.* FROM tbl_marketresearch_obervation WHERE ClientID = '{ClientID}'";
            var Model = SharedManager.GetSingle<OurObservation>(query);
            if (Model == null) return new OurObservation();
            return Model;
        }

        public _KeyFinding GetKeyFinding(Guid ClientID)
        {

            var query = $@"SELECT tbl_marketresearch_keyfinding.* FROM tbl_marketresearch_keyfinding WHERE ClientID = '{ClientID}'";
            var Model = SharedManager.GetSingle<_KeyFinding>(query);
            if (Model == null) return new _KeyFinding();
            return Model;
        }


        public _OnlineResearch GetOnlineResearch(Guid ClientID)
        {

            var query = $@"SELECT dbo.tbl_marketresearch_onlineresearch.*  FROM  dbo.tbl_marketresearch_onlineresearch WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetSingle<_OnlineResearch>(query);
            if (Model == null) return new _OnlineResearch();
            return Model;
        }

        public _Business GetBusinessOverview(Guid ClientID)
        {

            var query = $@"SELECT dbo.tbl_business_overview.*  FROM  dbo.tbl_business_overview WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetSingle<_Business>(query);
            if (Model == null) return new _Business();
            return Model;
        }

        public _ProductService GetProductService(Guid ClientID)
        {

            var query = $@"SELECT dbo.tbl_business_productservice.*  FROM  dbo.tbl_business_productservice WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetSingle<_ProductService>(query);
            if (Model == null) return new _ProductService();
            return Model;
        }

        public _BusinessOperation GetBusinessOperation(Guid ClientID)
        {

            var query = $@"SELECT dbo.tbl_business_operation.*  FROM  dbo.tbl_business_operation WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetSingle<_BusinessOperation>(query);
            if (Model == null) return new _BusinessOperation();
            return Model;
        }

        public SWOT GetCompetitorSWOT(Guid ClientID)
        {
            var query = $@"SELECT tbl_business_swot.*  FROM  tbl_business_swot WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetSingle<SWOT>(query);
            if (Model == null) return new SWOT();
            return Model;
        }


        public IEnumerable<CompetitorAnalysis> GetCompetitorAnalysis(Guid ClientID)
        {
            var query = $@"SELECT tbl_business_competitoranalysis.*  FROM  tbl_business_competitoranalysis WHERE ClientID = '{ClientID}'";
            var Model =  SharedManager.GetList<CompetitorAnalysis>(query);
            if (Model == null) return new List<CompetitorAnalysis>();
            return Model;
        }

        public _Customer GetCustomers(Guid ClientID)
        {
            var query = $@"SELECT dbo.tbl_selling_customer.*  FROM  dbo.tbl_selling_customer WHERE ClientID = '{ClientID}'";
            return SharedManager.GetSingle<_Customer>(query);

        }

        public _PricingProductService GetPricingProductService(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            var Model =  obj.GetSingle<_PricingProductService>(CommandType.StoredProcedure, "sp_get_pricing_productservice_by_client", parameters);
            if (Model == null) return new _PricingProductService();
            return Model;
        }
        public _Customer GetBusinessCustomer(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            var Model =  obj.GetSingle<_Customer>(CommandType.StoredProcedure, "sp_get_business_customer_by_client", parameters);
            if (Model == null) return new _Customer();
            return Model;
        }
        public _OnlinePresance GetOnlinePresance(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            var Model = obj.GetSingle<_OnlinePresance>(CommandType.StoredProcedure, "sp_get_marketing_presance_by_client", parameters);
            if (Model == null) return new _OnlinePresance();
            return Model;
        }

        public IEnumerable<_Marketing> GetMarketingPlan(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            var Model =  obj.GetList<_Marketing>(CommandType.StoredProcedure, "sp_get_marketing_plan_by_client", parameters);
            if (Model == null) return new List<_Marketing>();
            return Model;
        }

        public _MarketingBrand GetMarketingBrand(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings
                ["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            var Model =  obj.GetSingle<_MarketingBrand>(CommandType.StoredProcedure, "sp_get_marketing_brand_by_client", parameters);
            if (Model == null) return new _MarketingBrand();
            return Model;
        }
        //public _Business GetBusinessOverview(Guid ClientID)
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    var Model =  obj.GetSingle<_Business>(CommandType.StoredProcedure, "sp_get_business_overview_by_client", parameters);
        //    if (Model == null) return new _Business();
        //    return Model;

        //}

        //public _ProductService GetProductService(Guid ClientID)
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //   var Model=  obj.GetSingle<_ProductService>(CommandType.StoredProcedure, "sp_get_client_product_service", parameters);
        //    if (Model == null) return new _ProductService();
        //    return Model;


        //}

        public RunningBusiness GetRunningBusiness(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
           var Model=  obj.GetSingle<RunningBusiness>(CommandType.StoredProcedure, "sp_get_client_running_business", parameters);
            if (Model == null) return new RunningBusiness();
            return Model;

        }

        //public IEnumerable<CompetitorAnalysis> GetCompetitorAnalysis(Guid ClientID)
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue =ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    var Model =obj.GetList<CompetitorAnalysis>(CommandType.StoredProcedure, "sp_get_business_competitor_analysis_by_client", parameters);
        //    if (Model == null) return new List<CompetitorAnalysis>();
        //    return Model;
        //}

  
        //public _BusinessOperation GetBusinessOperation(Guid ClientID)
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    var Model =  obj.GetSingle<_BusinessOperation>(CommandType.StoredProcedure, "sp_get_client_business_operation", parameters);
        //    if (Model == null) return new _BusinessOperation();
        //    return Model;
        //}
        public _Investor GetFinanceInvestor(Guid ClientID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            var Model =  obj.GetSingle<_Investor>(CommandType.StoredProcedure, "sp_get_finance_investors_by_client", parameters);
            if (Model == null) return new _Investor();
            return Model;

        }
        
    }
}