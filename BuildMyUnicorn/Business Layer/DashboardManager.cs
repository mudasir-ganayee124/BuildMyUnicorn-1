﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using ALMS_DAL;
using Business_Model.Model;

namespace BuildMyUnicorn.Business_Layer
{
    public class DashboardManager
    {
        public IdeaProgress GetIdeaProgressData()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            _Idea  Model =  obj.GetSingle<_Idea>(CommandType.StoredProcedure, "sp_get_idea_by_client", parameters);
            IdeaProgress IdeaObj = new IdeaProgress();
            if (Model != null)
            {
                IdeaObj.TotalProgressData = Model.ProgressValue;
                IdeaObj.YourIdeaProgressData = Model.IdeaExplain == null ? 0.00m : 100.00m;
                IdeaObj.YourIdeaProgressData = Model.IdeaExplain == null ? 0.00m : 100.00m;
                IdeaObj.YourIdeaProgressData = Math.Round(IdeaObj.YourIdeaProgressData);
                //-- Let us break down idea
                IdeaObj.IdeaBreakDownProgressData = Model.ProblemSolve == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.ProblemSolver == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.FeedBackReceived == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.Niche == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.SpaceExist == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.StartupType ==null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.StartupTechnology == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.ProductDemand == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.InMarketAlready == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.Scalable == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData = Math.Round(IdeaObj.IdeaBreakDownProgressData);
                //-- About you
                IdeaObj.AboutYouProgressData += Model.Entrepreneur == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.YearsDoing == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.Experience == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.Priorities == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.EndGoal == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData = Math.Round(IdeaObj.AboutYouProgressData);
                //-- The Company
                IdeaObj.CompanyProgressData += Model.CompanyName == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.DomainName == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.BrandThought == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.CompanyMission == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.CompanyLookFeel == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.CompanySetup == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.HaveGotDomain == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.Cofounder == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.SupportTechnically == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.BuildFrom == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData = Math.Round(IdeaObj.CompanyProgressData);
                //Selling The Idea
                IdeaObj.IdeaSellingProgressData += Model.ProductBuy == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.ChargeGoing == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.CustomerFindPlan == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.SellType == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.ProductCharge == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.SellTo == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.SaleStaffPlan == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData = Math.Round(IdeaObj.IdeaSellingProgressData);
                //-- The Money
                IdeaObj.MoneyProgressData += Model.Affort == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.ProfitableMake == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.ProfitableThinkTime == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.BusinessCost == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.MoneyRaisePlan == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData = Math.Round(IdeaObj.MoneyProgressData);
            }
            return IdeaObj;

        }

        public IEnumerable<ProgressAnalytic> GetClientProgressAnalytic(decimal ProgressData)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProgressData", ParamterValue = ProgressData, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<ProgressAnalytic>(CommandType.StoredProcedure, "sp_add_progress_analytic", parameters);

        }

        public int GetClientTeamCount()
        {
            var query = $@"SELECT count(dbo.tbl_client.ClientID) FROM dbo.tbl_client WHERE TeamClientID = '{ new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}' ";
            return SharedManager.ExecuteScalar<int>(query);

        }

        
        public int GetClientMarketingPlanCount()
        {
            var query = $@"SELECT Count(MarketingPlanID) FROM  dbo.tbl_marketing_marketingplan  WHERE ClientID = '{ new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}' AND tbl_marketing_marketingplan.IsDeleted = 0 ";
            return SharedManager.ExecuteScalar<int>(query);
        }

        public int GetClientEligibleGrantCount(int CountryID)
        {
          
              var  query = $@"SELECT Count(GrantID) FROM tbl_grants WHERE CountryID = '{CountryID}' AND tbl_grants.IsDeleted = 0 ";
              return SharedManager.ExecuteScalar<int>(query);
        }

        public decimal GetClientProfileProgress(Client Model)
        {
            decimal ClientProfileProgress = 0.0m;
            decimal ClientProfileProgressUnit = 12.50m;
            ClientProfileProgress += string.IsNullOrEmpty(Model.StartupName) ? 0.0m: ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.FirstName) ? 0.0m : ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.LastName) ? 0.0m : ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.Email) ? 0.0m : ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.Phone) ? 0.0m : ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.RoleInCompany) ? 0.0m : ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.LinkedProfile) ? 0.0m : ClientProfileProgressUnit;
            ClientProfileProgress += string.IsNullOrEmpty(Model.ShortBio) ? 0.0m : ClientProfileProgressUnit;
            return Math.Round(ClientProfileProgress);


        }

        public IEnumerable<SubscribedPackages> GetAllSubscribedPackages()
        {
            var query = $@"select *, tbl_supplier_package.CreatedDateTime AS SubscribedDate from tbl_order INNER JOIN tbl_supplier_package ON tbl_supplier_package.SupplierPackageID = tbl_order.PlanID
                        INNER JOIN tbl_supplier ON tbl_supplier.SupplierID = tbl_supplier_package.SupplierID
                        WHERE tbl_order.OrderType = 1 AND ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}' 
                        ORDER BY tbl_supplier_package.CreatedDateTime DESC";
            return SharedManager.GetList<SubscribedPackages>(query);
        }


    }
}