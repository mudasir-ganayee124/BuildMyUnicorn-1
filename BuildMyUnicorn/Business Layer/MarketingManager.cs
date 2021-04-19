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
    public class MarketingManager
    {
        public _OnlinePresance GetOnlinePresance()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_OnlinePresance>(CommandType.StoredProcedure, "sp_get_marketing_presance_by_client", parameters);

        }

        public IEnumerable<_Marketing> GetMarketingPlan()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<_Marketing>(CommandType.StoredProcedure, "sp_get_marketing_plan_by_client", parameters);

        }

        public _MarketingBrand GetMarketingBrand()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings
                ["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_MarketingBrand>(CommandType.StoredProcedure, "sp_get_marketing_brand_by_client", parameters);

        }

        public _Marketing GetSingleMarketingPlan(Guid MarketingPlanID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@MarketingPlanID", ParamterValue = MarketingPlanID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_Marketing>(CommandType.StoredProcedure, "sp_get_marketing_plan_by_id", parameters);

        }
        public string AddOnlinePresance(OnlinePresance Model, List<MultipleMaster> ModelList)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@OnlinePresanceID", ParamterValue = Model.OnlinePresanceID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DomainName", ParamterValue = Model.YourWebsite.DomainName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WebPageStageID", ParamterValue = Model.YourWebsite.WebPageStageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WantWebsiteAchieveID", ParamterValue = Model.YourWebsite.WantWebsiteAchieveID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ContentWebsiteCreatorID", ParamterValue = Model.YourWebsite.ContentWebsiteCreatorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@VisitorsAccomplish", ParamterValue = Model.YourWebsite.VisitorsAccomplish, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SuccessMeasure", ParamterValue = Model.YourWebsite.SuccessMeasure, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CTA", ParamterValue = Model.YourWebsite.CTA , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TrafficAnticipatePerMonthID", ParamterValue = Model.YourWebsite.TrafficAnticipatePerMonthID , ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@UseDomainEmail", ParamterValue = Model.YourWebsite.UseDomainEmail, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WebsitePlanMakeMoney", ParamterValue = Model.YourWebsite.WebsitePlanMakeMoney, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PutWebpageAdds", ParamterValue = Model.YourWebsite.PutWebpageAdds, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompetitorSitelike", ParamterValue = Model.YourWebsite.CompetitorSitelike, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompetitorSitedislike", ParamterValue = Model.YourWebsite.CompetitorSitedislike, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Facebook", ParamterValue = Model.SocialHandles.Facebook, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Twitter", ParamterValue = Model.SocialHandles.Twitter, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Youtube", ParamterValue = Model.SocialHandles.Youtube, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Linkedin", ParamterValue = Model.SocialHandles.Linkedin, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TicTok", ParamterValue = Model.SocialHandles.TicTok, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Instagram", ParamterValue = Model.SocialHandles.Instagram, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_marketing_presance", parameters);
            if (result > 0)
            {
                Guid ClientID = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                new Master().DeleteMultipleMaster((int)ModuleName.Marketing, Model.OnlinePresanceID);
                ModelList.ForEach(x => x.ID = Guid.NewGuid());
                ModelList.ForEach(x => x.ModuleID = (int)ModuleName.Marketing);
                ModelList.ForEach(x => x.EntityID = Model.OnlinePresanceID);
                ModelList.ForEach(x => x.Value1 = ClientID.ToString());
                DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(ModelList);
                obj.ExecuteBulkInsert("sp_add_multiple_master_data", dtMarketKeyPlayer, "UT_MultipleMaster_Data", "@DataTable");
                return "OK";
            }
            else return "Error in adding marketing presance";
         

        }

        public string AddMarketingPlan(Marketing Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@MarketingPlanID", ParamterValue = Model.MarketingPlanID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PlanName", ParamterValue = Model.MarketingPlan.PlanName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GoalID", ParamterValue = Model.MarketingPlan.GoalID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@KPIS", ParamterValue = Model.MarketingPlan.KPIS, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BuyerPersonaID", ParamterValue = Model.MarketingPlan.BuyerPersonaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FindBuyers", ParamterValue = Model.MarketingPlan.FindBuyers, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SellingProposition", ParamterValue = Model.MarketingPlan.SellingProposition, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AudianceReachID", ParamterValue = Model.MarketingPlan.AudianceReachID , ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TrackKPIS", ParamterValue = Model.MarketingPlan.TrackKPIS , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Impressions", ParamterValue = Model.MarketingBudget.Impressions, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CTR", ParamterValue = Model.MarketingBudget.CTR, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ConversionRate", ParamterValue = Model.MarketingBudget.ConversionRate, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QualifiedLeadsPercentage", ParamterValue = Model.MarketingBudget.QualifiedLeadsPercentage, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QualifiedLeadConversionPercentage", ParamterValue = Model.MarketingBudget.QualifiedLeadConversionPercentage, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AvgCostPerClick", ParamterValue = Model.MarketingBudget.AvgCostPerClick, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AvgSale", ParamterValue = Model.MarketingBudget.AvgSale, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GoodsCostPercentage", ParamterValue = Model.MarketingBudget.GoodsCostPercentage, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Clicks", ParamterValue = Model.MarketingBudget.Clicks, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Leads", ParamterValue = Model.MarketingBudget.Leads, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QualifiedLeads", ParamterValue = Model.MarketingBudget.QualifiedLeads, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SalesNumber", ParamterValue = Model.MarketingBudget.SalesNumber, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Revenue", ParamterValue = Model.MarketingBudget.Revenue, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GoodsCostSold", ParamterValue = Model.MarketingBudget.GoodsCostSold, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AdvertisingBudget", ParamterValue = Model.MarketingBudget.AdvertisingBudget, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitAfterAdvertising", ParamterValue = Model.MarketingBudget.ProfitAfterAdvertising, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitPercentage", ParamterValue = Model.MarketingBudget.ProfitPercentage, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitPerClick", ParamterValue = Model.MarketingBudget.ProfitPerClick, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TargetCostPerLead", ParamterValue = Model.MarketingBudget.TargetCostPerLead, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_marketing_plan", parameters);
            
             return "Error in adding marketing plan";


        }

        public string AddMarketingBrand(MarketingBrand Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@MarketingBrandID", ParamterValue = Model.MarketingBrandID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhatAreWe", ParamterValue = Model.BrandingStrategy.WhatAreWe, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhyWeHere", ParamterValue = Model.BrandingStrategy.WhyWeHere, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhatDoHowDo", ParamterValue = Model.BrandingStrategy.WhatDoHowDo, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhatMakesDifferent", ParamterValue = Model.BrandingStrategy.WhatMakesDifferent, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhoWeHereFor", ParamterValue = Model.BrandingStrategy.WhoWeHereFor, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhatValueMost", ParamterValue = Model.BrandingStrategy.WhatValueMost, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OurPersonality", ParamterValue = Model.BrandingStrategy.OurPersonality , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OurAmbition", ParamterValue = Model.BrandingStrategy.OurAmbition , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EmotionalSellingPoint", ParamterValue = Model.BrandingStrategy.EmotionalSellingPoint, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PowerPresentationID", ParamterValue = Model.BrandGuidelines.PowerPresentationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TemplateDownloaded", ParamterValue = Model.BrandGuidelines.TemplateDownloaded, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BrandTouchPointID", ParamterValue = Model.BrandTouchPoints.BrandTouchPointID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_marketing_brand", parameters);

            return "Error in adding marketing brand";


        }
        public string DeleteMarketingPlan(Guid MarketingPlanID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@MarketingPlanID", ParamterValue = MarketingPlanID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_marketing_plan", parameters);
            return result > 0 ? "OK" : "Can not Delete ";


        }
    }
}