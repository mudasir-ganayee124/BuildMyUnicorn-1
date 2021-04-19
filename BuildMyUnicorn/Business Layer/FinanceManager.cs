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
    public class FinanceManager
    {
        public _Investor GetFinanceInvestor()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_Investor>(CommandType.StoredProcedure, "sp_get_finance_investors_by_client", parameters);

        }
        public IEnumerable<FinancialProjection> GetFinanceProjection()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<FinancialProjection>(CommandType.StoredProcedure, "sp_get_finance_projection", parameters);

        }

        public IEnumerable<Grants> GetCountryGrant(int CountryID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<Grants>(CommandType.StoredProcedure, "sp_get_single_grant_by_country", parameters);

        }
        public string AddFinanceInvestor(Investor Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@InvestorID", ParamterValue = Model.InvestorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessComparable", ParamterValue = Model.AngelInvestor.BusinessComparable, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessGrowFast", ParamterValue = Model.AngelInvestor.BusinessGrowFast, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessTeamSuited", ParamterValue = Model.AngelInvestor.BusinessTeamSuited, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompaniesTalked", ParamterValue = Model.AngelInvestor.CompaniesTalked, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompeteWith", ParamterValue = Model.AngelInvestor.CompeteWith, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DecideTo", ParamterValue = Model.AngelInvestor.DecideTo , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EvaluationComeup", ParamterValue = Model.AngelInvestor.EvaluationComeup , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GetStarted", ParamterValue = Model.AngelInvestor.GetStarted, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GrowFaster", ParamterValue = Model.AngelInvestor.GrowFaster, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HowDifferentYou", ParamterValue = Model.AngelInvestor.HowDifferentYou, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HowMany", ParamterValue = Model.AngelInvestor.HowMany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IdeaComeup", ParamterValue = Model.AngelInvestor.IdeaComeup, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Interested", ParamterValue = Model.AngelInvestor.Interested, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MakeMoney", ParamterValue = Model.AngelInvestor.MakeMoney, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MakeMoneyInvestors", ParamterValue = Model.AngelInvestor.MakeMoneyInvestors, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MarketingExpenseRealize", ParamterValue = Model.AngelInvestor.MarketingExpenseRealize, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MileStonesMet", ParamterValue = Model.AngelInvestor.MileStonesMet, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ObjectionAbout", ParamterValue = Model.AngelInvestor.ObjectionAbout, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PatentStrong", ParamterValue = Model.AngelInvestor.PatentStrong, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProblemKindGroup", ParamterValue = Model.AngelInvestor.ProblemKindGroup, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProblemWantSolving", ParamterValue = Model.AngelInvestor.ProblemWantSolving, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Proprietary", ParamterValue = Model.AngelInvestor.Proprietary, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SalesClose", ParamterValue = Model.AngelInvestor.SalesClose, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SalesMade", ParamterValue = Model.AngelInvestor.SalesMade, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShownTo", ParamterValue = Model.AngelInvestor.ShownTo, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SpendInvestorsMoney", ParamterValue = Model.AngelInvestor.SpendInvestorsMoney, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TractionMade", ParamterValue = Model.AngelInvestor.TractionMade, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WhyNeedInvestors", ParamterValue = Model.AngelInvestor.WhyNeedInvestors, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@WordoutGet", ParamterValue = Model.AngelInvestor.WordoutGet, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@YourInvestors", ParamterValue = Model.AngelInvestor.YourInvestors, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessMoneyPut", ParamterValue = Model.FriendsFamily.BusinessMoneyPut, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DecisionMakingAbility", ParamterValue = Model.FriendsFamily.DecisionMakingAbility, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FullTimeDoing", ParamterValue = Model.FriendsFamily.FullTimeDoing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GetBack", ParamterValue = Model.FriendsFamily.GetBack, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GrownExcitedCompany", ParamterValue = Model.FriendsFamily.GrownExcitedCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Investing", ParamterValue = Model.FriendsFamily.Investing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InvolvementLevelRequired", ParamterValue = Model.FriendsFamily.InvolvementLevelRequired, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LoanOrInvestment", ParamterValue = Model.FriendsFamily.LoanOrInvestment, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PayingCustomer", ParamterValue = Model.FriendsFamily.PayingCustomer, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RealRisks", ParamterValue = Model.FriendsFamily.RealRisks, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Timeframe", ParamterValue = Model.FriendsFamily.Timeframe, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AchieveMilestone", ParamterValue = Model.VC.AchieveMilestone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BigMarketOportunity", ParamterValue = Model.VC.BigMarketOportunity, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessPotentialRisk", ParamterValue = Model.VC.BusinessPotentialRisk, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClaimIntellectualProperty", ParamterValue = Model.VC.ClaimIntellectualProperty, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyFinancialProjection", ParamterValue = Model.VC.CompanyFinancialProjection, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CurrentCash", ParamterValue = Model.VC.CurrentCash, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DevelopedIntellectualProperty", ParamterValue = Model.VC.DevelopedIntellectualProperty, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DifferentiatedTechnology", ParamterValue = Model.VC.DifferentiatedTechnology, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ExistingInvestors", ParamterValue = Model.VC.ExistingInvestors, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ForecastingGrowth", ParamterValue = Model.VC.ForecastingGrowth, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FoundersUnderstand", ParamterValue = Model.VC.FoundersUnderstand, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@GreatManagementTeam", ParamterValue = Model.VC.GreatManagementTeam, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InitialInvestorPitchDesk", ParamterValue = Model.VC.InitialInvestorPitchDesk, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InvestmentCapital", ParamterValue = Model.VC.InvestmentCapital, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@KeyFeatures", ParamterValue = Model.VC.KeyFeatures, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ExpectedEvaluation", ParamterValue = Model.VC.ExpectedEvaluation, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@KeyIntellectualProperty", ParamterValue = Model.VC.KeyIntellectualProperty, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Margins", ParamterValue = Model.VC.Margins, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OwnedIntellectualProperty", ParamterValue = Model.VC.OwnedIntellectualProperty, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductMilestone", ParamterValue = Model.VC.ProductMilestone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ReplicateTechnology", ParamterValue = Model.VC.ReplicateTechnology, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SalesMarketingModel", ParamterValue = Model.VC.SalesMarketingModel, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StructurePlan", ParamterValue = Model.VC.StructurePlan, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TractionPositiveAchieved", ParamterValue = Model.VC.TractionPositiveAchieved, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ViolateIntellectualProperty", ParamterValue = Model.VC.ViolateIntellectualProperty, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_finance_investors", parameters);
            if (result > 0) return "OK"; else return "Error in adding finance investors";
        


        }

        public string AddFinanceProjection(FinancialProjection Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ProjectionID", ParamterValue = Model.ProjectionID , ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Year", ParamterValue = Model.Year, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Month", ParamterValue = Model.Month, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ActivityID", ParamterValue = Model.ActivityID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Units", ParamterValue = Model.Units, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Increment", ParamterValue = Model.Increment, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Charged", ParamterValue = Model.Charged , ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Revenue", ParamterValue = Model.Revenue , ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CarryForward", ParamterValue = Model.CarryForward, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_finance_projection", parameters);
            if (result > 0) return "OK"; else return "Error in adding finance projection";


        }
    }
}