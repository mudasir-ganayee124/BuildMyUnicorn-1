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
    public class BusinessManager
    {
        public _Business GetBusiness()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_Business>(CommandType.StoredProcedure, "sp_get_client_business", parameters);

        }

        public _ProductService GetProductService()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_ProductService>(CommandType.StoredProcedure, "sp_get_client_product_service", parameters);

        }

        public RunningBusiness GetRunningBusiness()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<RunningBusiness>(CommandType.StoredProcedure, "sp_get_client_running_business", parameters);

        }

        public IEnumerable<CompetitorAnalysis> GetCompetitorAnalysis()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<CompetitorAnalysis>(CommandType.StoredProcedure, "sp_get_business_competitor_analysis_by_client", parameters);

        }

        public _Customer GetBusinessCustomer()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_Customer>(CommandType.StoredProcedure, "sp_get_business_customer_by_client", parameters);

        }

        public _BusinessOperation GetBusinessOperation()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_BusinessOperation>(CommandType.StoredProcedure, "sp_get_client_business_operation", parameters);

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

        public BuyerPersona GetbuyerPersona(Guid BuyerPersonaID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@BuyerPersonaID", ParamterValue = BuyerPersonaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<BuyerPersona>(CommandType.StoredProcedure, "sp_get_business_customer_buyerpersona_by_id", parameters);

        }

        public SWOT GetCompetitorSWOT()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<SWOT>(CommandType.StoredProcedure, "sp_get_business_swot_by_client", parameters);

        }

        public string AddBusniess(Business Model, List<Client> ClientList)
        {
           

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@BusinessID", ParamterValue = Model.BusinessID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IdeaComeup", ParamterValue = Model.Founder.IdeaComeup, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessRun", ParamterValue = Model.Founder.BusinessRun, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PreviousWorkExperience", ParamterValue = Model.Founder.PreviousWorkExperience, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Qualification", ParamterValue = Model.Founder.Qualification, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HobbiesInterest", ParamterValue = Model.Founder.HobbiesInterest, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhyYou", ParamterValue = Model.Founder.WhyYou, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyRegisterdName", ParamterValue = Model.CompanyDetails.CompanyRegisterdName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Founded", ParamterValue = Model.CompanyDetails.Founded, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyNumber", ParamterValue = Model.CompanyDetails.CompanyNumber, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessAddress", ParamterValue = Model.CompanyDetails.BusinessAddress, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessPhone", ParamterValue = Model.CompanyDetails.BusinessPhone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@VatNumber", ParamterValue = Model.CompanyDetails.VatNumber, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@NumberofFounder", ParamterValue = Model.CompanyDetails.NumberofFounder, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }


            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_business", parameters);
            if (result > 0)
            {
                

                //MarketKeyPlayerList.ForEach(x => x.MarketKeyPlayerID = Guid.NewGuid());
                //MarketKeyPlayerList.ForEach(x => x.KeyFindingID = Model.KeyFindingID);
                //DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(MarketKeyPlayerList);
                //obj.ExecuteBulkInsert("sp_add_client_keyplayer_data", dtMarketKeyPlayer, "UT_MarketKey_Player_Data", "@DataTable");
            }
            if (result > 0) return "OK"; else return "Client keyfinding Exists";

        }

        public string AddProductService(ProductService Model, List<MultipleMaster> MultipleMaster)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ProductServiceID", ParamterValue = Model.ProductServiceID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSSelling", ParamterValue = Model.AboutProduct.PSSelling, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSDescription", ParamterValue = Model.AboutProduct.PSDescription, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSProduce", ParamterValue = Model.AboutProduct.PSProduce, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSDevelopStart", ParamterValue = Model.AboutProduct.PSDevelopStart, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSReadylaunch", ParamterValue = Model.AboutProduct.PSReadylaunch, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSVariations", ParamterValue = Model.AboutProduct.PSVariations, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSHaveIPAddress", ParamterValue = Model.AboutProduct.PSHaveIPAddress, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSIPAddress", ParamterValue = Model.AboutProduct.PSIPAddress, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSHaveTradeMark", ParamterValue = Model.AboutProduct.PSHaveTradeMark, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSTradeMark", ParamterValue = Model.AboutProduct.PSTradeMark, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSHaveTechnologyRoadMap", ParamterValue = Model.AboutProduct.PSHaveTechnologyRoadMap, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPDevelopmentFarID", ParamterValue = Model.MVP.MVPDevelopmentFarID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPHavePrototype", ParamterValue = Model.MVP.MVPHavePrototype, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPPrototype", ParamterValue = Model.MVP.MVPPrototype, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPCreate", ParamterValue = Model.MVP.MVPCreate, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPReasonID", ParamterValue = Model.MVP.MVPReasonID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
             
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_product_service", parameters);
            if (result > 0)
            {
                Master objMaster = new Master();
                objMaster.DeleteMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID);
                if(Model.AboutProduct.PSHaveTechnologyRoadMap == 1)
                objMaster.AddMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID, MultipleMaster);
            }
            if (result > 0) return "OK"; else return "Error in adding product service";

        }

        public string AddRunningBusiness(RunningBusiness Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@RunningBusinessID", ParamterValue = Model.RunningBusinessID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBProducedID", ParamterValue = Model.RBProducedID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBDeliveryMethodID", ParamterValue = Model.RBDeliveryMethodID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBThirdPartyInvovedID", ParamterValue = Model.RBThirdPartyInvovedID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBPaymentMethodID", ParamterValue = Model.RBPaymentMethodID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBQualityControlMethod", ParamterValue = Model.RBQualityControlMethod, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBStaffWorkID", ParamterValue = Model.RBStaffWorkID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBHaveEquipmentSoftware", ParamterValue = Model.RBHaveEquipmentSoftware, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBEquipmentSoftware", ParamterValue = Model.RBEquipmentSoftware, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBStaffNumber", ParamterValue = Model.RBStaffNumber, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBStaffNumberNextYear", ParamterValue = Model.RBStaffNumberNextYear, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBHaveInsurance", ParamterValue = Model.RBHaveInsurance, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBNeedSpecificLicense", ParamterValue = Model.RBNeedSpecificLicense, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RBNeedSpecificeQualification", ParamterValue = Model.RBNeedSpecificeQualification, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_running_business", parameters);
            if (result > 0) return "OK"; else return "Error in adding running business";

        }

        public string AddBusinessOperation(BusinessOperation Model, List<MultipleMaster> ModelList)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@BusinessOperationID", ParamterValue = Model.BusinessOperationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FinanceOperationalResponsible", ParamterValue = Model.WhoDoesWhat.FinanceOperationalResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FinanceManagementResponsible", ParamterValue = Model.WhoDoesWhat.FinanceManagementResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OperationResponsible", ParamterValue =  Model.WhoDoesWhat.OperationResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LegalMatterResponsible", ParamterValue =  Model.WhoDoesWhat.LegalMatterResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HRresponsible", ParamterValue =  Model.WhoDoesWhat.HRresponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FacilitiesResponsible", ParamterValue =  Model.WhoDoesWhat.FacilitiesResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SalesResponsible", ParamterValue =  Model.WhoDoesWhat.SalesResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MarektingResponsible", ParamterValue = Model.WhoDoesWhat.MarektingResponsible, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductProduced", ParamterValue = Model.Operations.ProductProduced, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DeliveryMethodID", ParamterValue = Model.Operations.DeliveryMethodID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ThirdPartyInvovedID", ParamterValue = Model.Operations.ThirdPartyInvovedID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PaymentMethodID", ParamterValue = Model.Operations.PaymentMethodID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QualityControlMethod", ParamterValue = Model.Operations.QualityControlMethod, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StaffWorkID", ParamterValue = Model.Operations.StaffWorkID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StaffCount", ParamterValue = Model.Operations.StaffCount, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StaffCountNextYear", ParamterValue = Model.Operations.StaffCountNextYear, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InsuranceType", ParamterValue = Model.Operations.InsuranceType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SoftwareType", ParamterValue = Model.Operations.SoftwareType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LicenseType", ParamterValue = Model.Operations.LicenseType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QualificationType", ParamterValue = Model.Operations.QualificationType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_operation", parameters);
            if (result > 0)
            {
                new Master().DeleteMultipleMaster((int)ModuleName.BusinessOperation, Model.BusinessOperationID);
                ModelList.ForEach(x => x.ID = Guid.NewGuid());
                ModelList.ForEach(x => x.ModuleID = (int)ModuleName.BusinessOperation);
                ModelList.ForEach(x => x.EntityID = Model.BusinessOperationID);
                DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(ModelList);
                obj.ExecuteBulkInsert("sp_add_multiple_master_data", dtMarketKeyPlayer, "UT_MultipleMaster_Data", "@DataTable");
                return "OK";
            }
            else return "Error in adding business operation";

        }

        public string AddCompetitor(Competitor Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            Guid SWOTID = Guid.NewGuid();
            if (Model.EntityState != 0) { SWOTID = Model.SWOT.SWOTID; }
            List<ParametersCollection> parameterswot = new List<ParametersCollection>() {
               
                new ParametersCollection { ParamterName = "@SWOTID", ParamterValue = SWOTID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Strengths", ParamterValue = Model.SWOT.Strengths, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Weaknesses", ParamterValue = Model.SWOT.Weaknesses, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Opportunities", ParamterValue = Model.SWOT.Opportunities, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Threats", ParamterValue = Model.SWOT.Threats, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int resultSWOT = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_swot", parameterswot);
           
            foreach (var item in Model.CompetitorAnalysis)
            {
                Guid CompetitorAnalysisID = Guid.NewGuid();
                if (Model.EntityState != 0) { CompetitorAnalysisID = item.CompetitorAnalysisID; }
                List<ParametersCollection> parametercompetitoranalysis = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CompetitorAnalysisID", ParamterValue = CompetitorAnalysisID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Name", ParamterValue = item.Name, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Website", ParamterValue = item.Website, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Location", ParamterValue =item.Location, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Offering", ParamterValue = item.Offering, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Pricing", ParamterValue = item.Pricing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Differentiator", ParamterValue = item.Differentiator, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
                int resultCompetitorAnalysis = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_competitor_analysis", parametercompetitoranalysis);

            }

            if (resultSWOT > 0) return "OK"; else return "Error in adding competitors";

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

        public string DeleteBuyerPersona(Guid BuyerPersonaID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@BuyerPersonaID", ParamterValue = BuyerPersonaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
           
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_business_buyerpersona", parameters);
            return result > 0 ? "OK" : "Can not delete ";


        }


    }
}