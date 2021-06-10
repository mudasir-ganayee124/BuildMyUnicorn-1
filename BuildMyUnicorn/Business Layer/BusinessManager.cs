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
        public _Business GetBusinessOverview()
        {

            var query = $@"SELECT dbo.tbl_business_overview.*  FROM  dbo.tbl_business_overview WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_Business>(query);
        }
        //public _Business GetBusinessOverview()
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    return obj.GetSingle<_Business>(CommandType.StoredProcedure, "sp_get_business_overview_by_client", parameters);

        //}
        public _ProductService GetProductService()
        {

            var query = $@"SELECT dbo.tbl_business_productservice.*  FROM  dbo.tbl_business_productservice WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_ProductService>(query);
        }
        //public _ProductService GetProductService()
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    return obj.GetSingle<_ProductService>(CommandType.StoredProcedure, "sp_get_client_product_service", parameters);

        //}

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
            var query = $@"SELECT tbl_business_competitoranalysis.*  FROM  tbl_business_competitoranalysis WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetList<CompetitorAnalysis>(query);
        }

        public SWOT GetCompetitorSWOT()
        {
            var query = $@"SELECT tbl_business_swot.*  FROM  tbl_business_swot WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<SWOT>(query);
        }

       
        public _BusinessOperation GetBusinessOperation()
        {

            var query = $@"SELECT dbo.tbl_business_operation.*  FROM  dbo.tbl_business_operation WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_BusinessOperation>(query);
        }
        //public _BusinessOperation GetBusinessOperation()
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    return obj.GetSingle<_BusinessOperation>(CommandType.StoredProcedure, "sp_get_client_business_operation", parameters);

        //}
       

        public BuyerPersona GetbuyerPersona(Guid BuyerPersonaID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@BuyerPersonaID", ParamterValue = BuyerPersonaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<BuyerPersona>(CommandType.StoredProcedure, "sp_get_business_customer_buyerpersona_by_id", parameters);

        }

    

        public string AddBusniessOverview(Business Model)
        {
           

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@BusinessOverID", ParamterValue = Model.BusinessOverID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IdeaComeup", ParamterValue = Model.Founder.IdeaComeup, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessRun", ParamterValue = Model.Founder.BusinessRun, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PreviousWorkExperience", ParamterValue = Model.Founder.PreviousWorkExperience, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Qualification", ParamterValue = Model.Founder.Qualification, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HobbiesInterest", ParamterValue = Model.Founder.HobbiesInterest, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessRequirePremises", ParamterValue = Model.CompanyDetails.BusinessRequirePremises, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LandlordCostStatus", ParamterValue = Model.CompanyDetails.LandlordCostStatus, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhyYou", ParamterValue = Model.Founder.WhyYou, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyRegisterdName", ParamterValue = Model.CompanyDetails.CompanyRegisterdName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Founded", ParamterValue = Model.CompanyDetails.Founded, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyNumber", ParamterValue = Model.CompanyDetails.CompanyNumber, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessAddress", ParamterValue = Model.CompanyDetails.BusinessAddress, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessPhone", ParamterValue = Model.CompanyDetails.BusinessPhone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@VatNumber", ParamterValue = Model.CompanyDetails.VatNumber, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyLegalStructureID", ParamterValue = Model.CompanyDetails.CompanyLegalStructureID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@NumberofFounder", ParamterValue = Model.CompanyDetails.NumberofFounder, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }


            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_overview", parameters);
            
            if (result > 0) return "OK"; else return "Business overview exists";

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
                new ParametersCollection { ParamterName = "@PSHaveIPAddress", ParamterValue = Model.AboutProduct.PSHaveIPAddress, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSIPAddress", ParamterValue = Model.AboutProduct.PSIPAddress, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSHaveTradeMark", ParamterValue = Model.AboutProduct.PSHaveTradeMark, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSTradeMark", ParamterValue = Model.AboutProduct.PSTradeMark, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PSHaveTechnologyRoadMap", ParamterValue = Model.AboutProduct.PSHaveTechnologyRoadMap, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPDevelopmentFarID", ParamterValue = Model.MVP.MVPDevelopmentFarID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPHavePrototype", ParamterValue = Model.MVP.MVPHavePrototype, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPPrototype", ParamterValue = Model.MVP.MVPPrototype, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPCreate", ParamterValue = Model.MVP.MVPCreate, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MVPReasonID", ParamterValue = Model.MVP.MVPReasonID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
             
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_business_productservice", parameters);
            if (result > 0)
            {
                Master objMaster = new Master();
                objMaster.DeleteMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID);
               // if(Model.AboutProduct.PSHaveTechnologyRoadMap == 1)
                objMaster.AddProductServiceMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID, MultipleMaster);
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
                new ParametersCollection { ParamterName = "@StaffWorkID", ParamterValue = Model.Operations.StaffWorkID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Needsoftware", ParamterValue = Model.Operations.Needsoftware, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HaveInsurance", ParamterValue = Model.Operations.HaveInsurance, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@NeedLicense", ParamterValue = Model.Operations.NeedLicense, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@NeedQualification", ParamterValue = Model.Operations.NeedQualification, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QualityControlMethod", ParamterValue = Model.Operations.QualityControlMethod, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
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
            List<ParametersCollection> parameterswot = new List<ParametersCollection>() {
               
                new ParametersCollection { ParamterName = "@SWOTID", ParamterValue = Model.SWOT.SWOTID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
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
                if (Model.EntityState ==EntityState.Old && item.CompetitorAnalysisID != Guid.Empty)  CompetitorAnalysisID = item.CompetitorAnalysisID; 
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

       


       

        public string DeleteBuyerPersona(Guid BuyerPersonaID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@BuyerPersonaID", ParamterValue = BuyerPersonaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
           
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_business_buyerpersona", parameters);
            return result > 0 ? "OK" : "Can not delete ";


        }

        public int ExistBusinessOverview(Guid id)
        {
            var query = $@"select count(BusinessOverID) from tbl_business_overview WHERE BusinessOverID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(BusinessOverID) from tbl_business_overview WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }
        public int ExistProductService(Guid id)
        {
            var query = $@"select count(ProductServiceID) from tbl_business_productservice WHERE ProductServiceID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(ProductServiceID) from tbl_business_productservice WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }
        public int ExistBusinessOperation(Guid id)
        {
            var query = $@"select count(BusinessOperationID) from tbl_business_operation WHERE BusinessOperationID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(BusinessOperationID) from tbl_business_operation WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }
        /// <summary>
        /// id must always be client id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ExistBusinessCompetitorsAnalysis(Guid id)
        {
            if(id == Guid.Empty) id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
            int Count = 0;
         //   Guid ClientID = id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
            var query = $@"select count(CompetitorAnalysisID) from tbl_business_competitoranalysis WHERE ClientID = '{id}'";
            Count =  SharedManager.ExecuteScalar<int>(query);
            if (Count > 0) return Count;
            else query = $@"select count(SWOTID) from tbl_business_swot WHERE ClientID = '{id}'";
            return SharedManager.ExecuteScalar<int>(query);
     
        }
    }
}