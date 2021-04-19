using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using Business_Model.Helper;

namespace BuildMyUnicorn.Controllers
{
    public class FinanceController : WebController
    {
        // GET: Business
        public ActionResult Investors(string id)
        {

            int State = (int)EntityState.New;
            _Investor Model = new FinanceManager().GetFinanceInvestor();
            if (Model != null && Model.InvestorID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Finance, (int)ModuleSection.Finance_FinancialProjections);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.Finance_FinancialProjections))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Finance",
                    ActionName = "Investors",
                    ModuleName = Module.Finance.ToString(),
                    SectionName = ModuleSection.Finance_FinancialProjections.ToString()
                });
            }
            return View(State);
        }

        public ActionResult Grants()
        {

            int CountryID =  ViewBag.Client.CountryID;
            IEnumerable<Grants> Model = new FinanceManager().GetCountryGrant(CountryID);
            if(Model == null)
            return PartialView("_BadRequest");
            return View(Model);
        }
        public ActionResult PitchDecks()
        {
           return View();
        }
        public ActionResult PitchDeckPage(string id)
        {
            switch (id)
            {
                case "1":
                    return PartialView("_PitchDeckPage_1");
                 
                case "2":
                    return PartialView("_PitchDeckPage_2");
                case "3":
                    return PartialView("_PitchDeckPage_3");
                case "4":
                    return PartialView("_PitchDeckPage_4");
                case "5":
                    return PartialView("_PitchDeckPage_5");
                case "6":
                    return PartialView("_PitchDeckPage_6");
            }
            return PartialView("_PitchDeckPage_1");
        }
        public ActionResult New(int Type)
        {
            if ((int)ModuleSection.Finance_FinancialProjections == Type)
            {
                ViewBag.TitleFriendsFamily = TypeDescriptor.GetProperties(typeof(Business_Model.Model.FriendsFamily)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleAngelInvestor = TypeDescriptor.GetProperties(typeof(Business_Model.Model.AngelInvestor)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleVC = TypeDescriptor.GetProperties(typeof(Business_Model.Model.VC)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
                Investor obj = new Investor();
                FriendsFamily objFriendsFamily = new FriendsFamily();
                AngelInvestor objAngelInvestor = new AngelInvestor();
                VC objVC = new VC();
                _Investor Model = new FinanceManager().GetFinanceInvestor();
                if (Model != null)
                {
                    obj.ClientID = Model.ClientID;
                    obj.InvestorID = Model.InvestorID;
                    obj.EntityState = EntityState.Old;
                    objFriendsFamily.BusinessMoneyPut = Model.BusinessMoneyPut;
                    objFriendsFamily.DecisionMakingAbility = Model.DecisionMakingAbility;
                    objFriendsFamily.FullTimeDoing = Model.FullTimeDoing;
                    objFriendsFamily.GetBack = Model.GetBack;
                    objFriendsFamily.GrownExcitedCompany = Model.GrownExcitedCompany;
                    objFriendsFamily.Investing = Model.Investing;
                    objFriendsFamily.InvolvementLevelRequired = Model.InvolvementLevelRequired;
                    objFriendsFamily.LoanOrInvestment = Model.LoanOrInvestment;
                    objFriendsFamily.PayingCustomer = Model.PayingCustomer;
                    objFriendsFamily.RealRisks = Model.RealRisks;
                    objFriendsFamily.Timeframe = Model.Timeframe;
                    objFriendsFamily.BusinessMoneyPut = Model.BusinessMoneyPut;
                    objAngelInvestor.BusinessComparable = Model.BusinessComparable;
                    objAngelInvestor.BusinessGrowFast = Model.BusinessGrowFast;
                    objAngelInvestor.BusinessTeamSuited = Model.BusinessTeamSuited;
                    objAngelInvestor.CompaniesTalked = Model.CompaniesTalked;
                    objAngelInvestor.CompeteWith = Model.CompeteWith;
                    objAngelInvestor.DecideTo = Model.DecideTo;
                    objAngelInvestor.EvaluationComeup = Model.EvaluationComeup;
                    objAngelInvestor.GetStarted = Model.GetStarted;
                    objAngelInvestor.GrowFaster = Model.GrowFaster;
                    objAngelInvestor.HowDifferentYou = Model.HowDifferentYou;
                    objAngelInvestor.HowMany = Model.HowMany;
                    objAngelInvestor.IdeaComeup = Model.IdeaComeup;
                    objAngelInvestor.Interested = Model.Interested;
                    objAngelInvestor.MakeMoney = Model.MakeMoney;
                    objAngelInvestor.MakeMoneyInvestors = Model.MakeMoneyInvestors;
                    objAngelInvestor.MarketingExpenseRealize = Model.MarketingExpenseRealize;
                    objAngelInvestor.MileStonesMet = Model.MileStonesMet;
                    objAngelInvestor.ObjectionAbout = Model.ObjectionAbout;
                    objAngelInvestor.PatentStrong = Model.PatentStrong;
                    objAngelInvestor.ProblemKindGroup = Model.ProblemKindGroup;
                    objAngelInvestor.ProblemWantSolving = Model.ProblemWantSolving;
                    objAngelInvestor.Proprietary = Model.Proprietary;
                    objAngelInvestor.SalesClose = Model.SalesClose;
                    objAngelInvestor.SalesMade = Model.SalesMade;
                    objAngelInvestor.ShownTo = Model.ShownTo;
                    objAngelInvestor.SpendInvestorsMoney = Model.SpendInvestorsMoney;
                    objAngelInvestor.TractionMade = Model.TractionMade;
                    objAngelInvestor.WhyNeedInvestors = Model.WhyNeedInvestors;
                    objAngelInvestor.WordoutGet = Model.WordoutGet;
                    objAngelInvestor.YourInvestors = Model.YourInvestors;
                    objVC.AchieveMilestone = Model.AchieveMilestone;
                    objVC.BigMarketOportunity = Model.BigMarketOportunity;
                    objVC.BusinessPotentialRisk = Model.BusinessPotentialRisk;
                    objVC.ClaimIntellectualProperty = Model.ClaimIntellectualProperty;
                    objVC.CompanyFinancialProjection = Model.CompanyFinancialProjection;
                    objVC.CurrentCash = Model.CurrentCash;
                    objVC.DevelopedIntellectualProperty = Model.DevelopedIntellectualProperty;
                    objVC.DifferentiatedTechnology = Model.DifferentiatedTechnology;
                    objVC.ExistingInvestors = Model.ExistingInvestors;
                    objVC.ExpectedEvaluation = Model.ExpectedEvaluation;
                    objVC.ForecastingGrowth = Model.ForecastingGrowth;
                    objVC.FoundersUnderstand = Model.FoundersUnderstand;
                    objVC.GreatManagementTeam = Model.GreatManagementTeam;
                    objVC.InitialInvestorPitchDesk = Model.InitialInvestorPitchDesk;
                    objVC.InvestmentCapital = Model.InvestmentCapital;
                    objVC.KeyFeatures = Model.KeyFeatures;
                    objVC.KeyIntellectualProperty = Model.KeyIntellectualProperty;
                    objVC.Margins = Model.Margins;
                    objVC.OwnedIntellectualProperty = Model.OwnedIntellectualProperty;
                    objVC.ProductMilestone = Model.ProductMilestone;
                    objVC.ReplicateTechnology = Model.ReplicateTechnology;
                    objVC.SalesMarketingModel = Model.SalesMarketingModel;
                    objVC.StructurePlan = Model.StructurePlan;
                    objVC.TractionPositiveAchieved = Model.TractionPositiveAchieved;
                    objVC.ViolateIntellectualProperty = Model.ViolateIntellectualProperty;
                }
                else

                    obj.EntityState = EntityState.New;
                obj.AngelInvestor = objAngelInvestor;
                obj.FriendsFamily = objFriendsFamily;
                obj.VC = objVC;

                return PartialView("_NewInvestorPartial", obj);

            }
            return PartialView("_NewFinanceInvestorPartial");

        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.Finance_FinancialProjections == Type)
            {

                ViewBag.TitleFriendsFamily = TypeDescriptor.GetProperties(typeof(Business_Model.Model.FriendsFamily)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleAngelInvestor = TypeDescriptor.GetProperties(typeof(Business_Model.Model.AngelInvestor)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleVC = TypeDescriptor.GetProperties(typeof(Business_Model.Model.VC)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
                Investor obj = new Investor();
                FriendsFamily objFriendsFamily = new FriendsFamily();
                AngelInvestor objAngelInvestor = new AngelInvestor();
                VC objVC = new VC();
                _Investor Model = new FinanceManager().GetFinanceInvestor();
                if (Model != null)
                {
                    obj.ClientID = Model.ClientID;
                    obj.InvestorID = Model.InvestorID;
                    obj.EntityState = EntityState.Old;
                    objFriendsFamily.BusinessMoneyPut = Model.BusinessMoneyPut;
                    objFriendsFamily.DecisionMakingAbility = Model.DecisionMakingAbility;
                    objFriendsFamily.FullTimeDoing = Model.FullTimeDoing;
                    objFriendsFamily.GetBack = Model.GetBack;
                    objFriendsFamily.GrownExcitedCompany = Model.GrownExcitedCompany;
                    objFriendsFamily.Investing = Model.Investing;
                    objFriendsFamily.InvolvementLevelRequired = Model.InvolvementLevelRequired;
                    objFriendsFamily.LoanOrInvestment = Model.LoanOrInvestment;
                    objFriendsFamily.PayingCustomer = Model.PayingCustomer;
                    objFriendsFamily.RealRisks = Model.RealRisks;
                    objFriendsFamily.Timeframe = Model.Timeframe;
                    objFriendsFamily.BusinessMoneyPut = Model.BusinessMoneyPut;
                    objFriendsFamily.Completed = 100;
                    int Count = IsNotAnyNullOrEmpty(objFriendsFamily);
                    obj.Answered += Count;
                    objFriendsFamily.Completed = Math.Round(PercentComplete(Count, 9.09m));
                    objAngelInvestor.BusinessComparable = Model.BusinessComparable;
                    objAngelInvestor.BusinessGrowFast = Model.BusinessGrowFast;
                    objAngelInvestor.BusinessTeamSuited = Model.BusinessTeamSuited;
                    objAngelInvestor.CompaniesTalked = Model.CompaniesTalked;
                    objAngelInvestor.CompeteWith = Model.CompeteWith;
                    objAngelInvestor.DecideTo = Model.DecideTo;
                    objAngelInvestor.EvaluationComeup = Model.EvaluationComeup;
                    objAngelInvestor.GetStarted = Model.GetStarted;
                    objAngelInvestor.GrowFaster = Model.GrowFaster;
                    objAngelInvestor.HowDifferentYou = Model.HowDifferentYou;
                    objAngelInvestor.HowMany = Model.HowMany;
                    objAngelInvestor.IdeaComeup = Model.IdeaComeup;
                    objAngelInvestor.Interested = Model.Interested;
                    objAngelInvestor.MakeMoney = Model.MakeMoney;
                    objAngelInvestor.MakeMoneyInvestors = Model.MakeMoneyInvestors;
                    objAngelInvestor.MarketingExpenseRealize = Model.MarketingExpenseRealize;
                    objAngelInvestor.MileStonesMet = Model.MileStonesMet;
                    objAngelInvestor.ObjectionAbout = Model.ObjectionAbout;
                    objAngelInvestor.PatentStrong = Model.PatentStrong;
                    objAngelInvestor.ProblemKindGroup = Model.ProblemKindGroup;
                    objAngelInvestor.ProblemWantSolving = Model.ProblemWantSolving;
                    objAngelInvestor.Proprietary = Model.Proprietary;
                    objAngelInvestor.SalesClose = Model.SalesClose;
                    objAngelInvestor.SalesMade = Model.SalesMade;
                    objAngelInvestor.ShownTo = Model.ShownTo;
                    objAngelInvestor.SpendInvestorsMoney = Model.SpendInvestorsMoney;
                    objAngelInvestor.TractionMade = Model.TractionMade;
                    objAngelInvestor.WhyNeedInvestors = Model.WhyNeedInvestors;
                    objAngelInvestor.WordoutGet = Model.WordoutGet;
                    objAngelInvestor.YourInvestors = Model.YourInvestors;
                    Count = IsNotAnyNullOrEmpty(objAngelInvestor);
                    obj.Answered += Count;
                    objAngelInvestor.Completed = Math.Round(PercentComplete(Count, 3.33m));
                    objVC.AchieveMilestone = Model.AchieveMilestone;
                    objVC.BigMarketOportunity = Model.BigMarketOportunity;
                    objVC.BusinessPotentialRisk = Model.BusinessPotentialRisk;
                    objVC.ClaimIntellectualProperty = Model.ClaimIntellectualProperty;
                    objVC.CompanyFinancialProjection = Model.CompanyFinancialProjection;
                    objVC.CurrentCash = Model.CurrentCash;
                    objVC.DevelopedIntellectualProperty = Model.DevelopedIntellectualProperty;
                    objVC.DifferentiatedTechnology = Model.DifferentiatedTechnology;
                    objVC.ExistingInvestors = Model.ExistingInvestors;
                    objVC.ExpectedEvaluation = Model.ExpectedEvaluation;
                    objVC.ForecastingGrowth = Model.ForecastingGrowth;
                    objVC.FoundersUnderstand = Model.FoundersUnderstand;
                    objVC.GreatManagementTeam = Model.GreatManagementTeam;
                    objVC.InitialInvestorPitchDesk = Model.InitialInvestorPitchDesk;
                    objVC.InvestmentCapital = Model.InvestmentCapital;
                    objVC.KeyFeatures = Model.KeyFeatures;
                    objVC.KeyIntellectualProperty = Model.KeyIntellectualProperty;
                    objVC.Margins = Model.Margins;
                    objVC.OwnedIntellectualProperty = Model.OwnedIntellectualProperty;
                    objVC.ProductMilestone = Model.ProductMilestone;
                    objVC.ReplicateTechnology = Model.ReplicateTechnology;
                    objVC.SalesMarketingModel = Model.SalesMarketingModel;
                    objVC.StructurePlan = Model.StructurePlan;
                    objVC.TractionPositiveAchieved = Model.TractionPositiveAchieved;
                    objVC.ViolateIntellectualProperty = Model.ViolateIntellectualProperty;
                    Count = IsNotAnyNullOrEmpty(objVC);
                    obj.Answered += Count;
                    objVC.Completed = Math.Round(PercentComplete(Count, 3.33m));
                }
                else

                    obj.EntityState = EntityState.New;
                obj.AngelInvestor = objAngelInvestor;
                obj.FriendsFamily = objFriendsFamily;
                obj.VC = objVC;

                return PartialView("_DetailInvestorPartial", obj);

            }
            return PartialView("_NewFinanceInvestorPartial");
        }

        public string AddInvestors(Investor Model)
        {
            if (Model.EntityState == EntityState.New)
                Model.InvestorID = Guid.NewGuid();

            return new FinanceManager().AddFinanceInvestor(Model);
        }

        public string CheckModuleCourse(int State, int SectionValue)
        {
            if (State == 0)
            {
                string getValue = "0";
                string getClientID = string.Empty;
                string LoginUserID = User.Identity.Name.ToString();
                string SectionName = Enum.GetName(typeof(ModuleSection), SectionValue);
                string CookieID = SectionName.ToString() + LoginUserID;
                if (Request.Cookies[CookieID.ToString()] != null)
                {
                    HttpCookie aCookie = Request.Cookies[CookieID.ToString()];
                    getValue = aCookie.Values["Status"];
                }
                else
                {
                    HttpCookie appCookie = new HttpCookie(CookieID.ToString());
                    appCookie.Values["Status"] = "0";
                    appCookie.Values["ClientID"] = User.Identity.Name.ToString();
                    appCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(appCookie);
                }
                ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.TheBusiness, SectionValue);

                if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                    return ResponseType.Redirect.ToString();
                else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }

        int IsNotAnyNullOrEmpty(object myObject)
        {
            int Count = 0;
            foreach (System.Reflection.PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (!string.IsNullOrEmpty(value))
                    {
                        Count++;
                    }
                }
            }
            return Count;
        }

        public decimal PercentComplete(int Count, decimal Unit)
        {
            decimal Completed = 0.00m;
            if (Count > 0)
                for (int i = 1; i <= Count; i++)
                {
                    Completed += Unit;
                }
            return Completed;
        }

    }
}
