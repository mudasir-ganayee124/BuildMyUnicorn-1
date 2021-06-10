using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Business_Model.Helper;

namespace Business_Model.Model
{


    public class Investor
    {
        public Guid InvestorID { get; set; }
        public Guid ClientID { get; set; }
        public int Answered { get; set; }
        public EntityState EntityState { get; set; }
        public FriendsFamily FriendsFamily { get; set; }
        public AngelInvestor AngelInvestor { get; set; }
        public VC VC { get; set; }

    }

    public class FriendsFamily
    {
        [Description("Have You Grown and Exited a Company Before")]
        public string GrownExcitedCompany { get; set; }
        [Description("What Level of Involvement Is Required")]
        public string InvolvementLevelRequired { get; set; }
        [Description("What's the Timeframe")]
        public string Timeframe { get; set; }
        [Description("What will      get back")]
        public string GetBack { get; set; }
        [Description("What Are the Real Risks")]
        public string RealRisks { get; set; }
        [Description("Is this a loan or an investment ")]
        public string LoanOrInvestment { get; set; }
        [Description("Who Else Is Investing")]
        public string Investing { get; set; }
        [Description("How Much Money Have You Put in the Business")]
        public string BusinessMoneyPut { get; set; }
        [Description("Do You Have Paying Customers")]
        public string PayingCustomer { get; set; }
        [Description("Are You Doing This Full Time")]
        public string FullTimeDoing { get; set; }
        [Description("Do I have any decision making abilities in the company ")]
        public string DecisionMakingAbility { get; set; }
        public decimal Completed { get; set; }
    }
    public class AngelInvestor
    {
        [Description("What problem (or want) are you solving?")]
        public string ProblemWantSolving { get; set; }
        [Description("What kinds of people, groups, or organizations have that problem")]
        public string ProblemKindGroup { get; set; }
        [Description("How many are there")]
        public string HowMany { get; set; }
        [Description("How are you different?")]
        public string HowDifferentYou { get; set; }
        [Description("Who will you compete with? How are they different?")]
        public string CompeteWith { get; set; }
        [Description("How will you make money?")]
        public string MakeMoney { get; set; }
        [Description("How will you make money for your investors?")]
        public string MakeMoneyInvestors { get; set; }
        [Description("How fast can you grow your business? Can you scale up volume without proportional scaling up headcount?")]
        public string BusinessGrowFast { get; set; }
        [Description("What’s proprietary? What are you going to do to defend that?")]
        public string Proprietary { get; set; }
        [Description("What traction have you made?")]
        public string TractionMade { get; set; }
        [Description("What milestones have you met?")]
        public string MileStonesMet { get; set; }
        [Description("How are you going to get the word out?")]
        public string WordoutGet { get; set; }
        [Description("How are you going to close sales?")]
        public string SalesClose { get; set; }
        [Description("How are you going to get started?")]
        public string GetStarted { get; set; }
        [Description("How are you going to spend investors’ money?")]
        public string SpendInvestorsMoney { get; set; }
        [Description("What makes your team suited for this business?")]
        public string BusinessTeamSuited { get; set; }
        [Description("How did you come up with this idea?")]
        public string IdeaComeup { get; set; }
        [Description("Why did you decide to (some marketing, product, or financial decision in the pitch)?")]
        public string DecideTo { get; set; }
        [Description("What about (some objection related to market, competition, financial plans)?")]
        public string ObjectionAbout { get; set; }
        [Description("Who are your investors so far?")]
        public string YourInvestors { get; set; }
        [Description("How strong is your patent?")]
        public string PatentStrong { get; set; }
        [Description("Could you grow faster with more money?")]
        public string GrowFaster { get; set; }
        [Description("Do you realize you’re vastly underestimating your marketing expenses (or sales expense, or margins through channels, or headcount required for direct selling)?")]
        public string MarketingExpenseRealize { get; set; }
        [Description("Do you know comparable numbers for similar businesses?")]
        public string BusinessComparable { get; set; }
        [Description("Why don’t you do this yourself? (Meaning, why do you think you need investors?)")]
        public string WhyNeedInvestors { get; set; }
        [Description("What sales have you made so far?")]
        public string SalesMade { get; set; }
        [Description("Have you actually talked to those companies?")]
        public string CompaniesTalked { get; set; }
        [Description("Who else is interested?")]
        public string Interested { get; set; }
        [Description("Who else have you shown this to?")]
        public string ShownTo { get; set; }
        [Description("How did you come up with that valuation?")]
        public string EvaluationComeup { get; set; }
        public decimal Completed { get; set; }

    }
    public class VC
    {
        [Description("Is There a Great Management Team")]
        public string GreatManagementTeam { get; set; }
        [Description("Is the Market Opportunity Big")]
        public string BigMarketOportunity { get; set; }
        [Description("What Positive Early Traction Has the Company Achieved")]
        public string TractionPositiveAchieved { get; set; }
        [Description("Do the Founders Understand the Financials and Key Metrics of Their Business")]
        public string FoundersUnderstand { get; set; }
        [Description("Is the Initial Investor Pitch Deck Professional and Interesting")]
        public string InitialInvestorPitchDesk { get; set; }
        [Description("What Are the Potential Risks to the Business")]
        public string BusinessPotentialRisk { get; set; }
        [Description("What are the major product milestones")]
        public string ProductMilestone { get; set; }
        [Description("What are the two or three key features you plan to add")]
        public string KeyFeatures { get; set; }
        [Description("How Will My Investment Capital Be Used and What Progress Will Be Made With That Capital")]
        public string InvestmentCapital { get; set; }
        [Description("Is the Expected Valuation for the Company Realistic")]
        public string ExpectedEvaluation { get; set; }
        [Description("Does the Company Have Differentiated Technology")]
        public string DifferentiatedTechnology { get; set; }
        [Description("How easy will it be to replicate the technology")]
        public string ReplicateTechnology { get; set; }
        [Description("What key intellectual property does the company have (patents, patents pending, copyrights, trade secrets, trademarks, domain names)?")]
        public string KeyIntellectualProperty { get; set; }
        [Description("What comfort is there that the company’s intellectual property does not violate the rights of a third party")]
        public string ViolateIntellectualProperty { get; set; }
        [Description("Would any prior employers of a team member have a potential claim to the company’s intellectual property")]
        public string ClaimIntellectualProperty { get; set; }
        [Description("Is the intellectual property properly owned by the company, and have all employees and consultants assigned the intellectual property over to the company")]
        public string OwnedIntellectualProperty { get; set; }
        [Description("If the intellectual property was developed at a university or through government grants or with open source technology, how does the company have the right to use the technology")]
        public string DevelopedIntellectualProperty { get; set; }
        [Description("Are the Company’s Financial Projections Realistic and Interesting")]
        public string CompanyFinancialProjection { get; set; }
        [Description("What are your margins? How much does it cost to serve your customers")]
        public string Margins { get; set; }
        [Description("What is your sales and marketing model, and how much does it cost to acquire new customers and retain existing customers")]
        public string SalesMarketingModel { get; set; }
        [Description("How are you forecasting growth for the next 12–24 months? What are your key assumptions, and how did you come up with them")]
        public string ForecastingGrowth { get; set; }
        [Description("Who are the existing investors, and how much money have you raised so far")]
        public string ExistingInvestors { get; set; }
        [Description("What are you looking to raise now, and what milestones are you hoping to achieve with this financing round")]
        public string AchieveMilestone { get; set; }
        [Description("How much cash do you currently have, and what’s your burn rate")]
        public string CurrentCash { get; set; }
        [Description("How are you planning to structure this round, and who else is investing in this round")]
        public string StructurePlan { get; set; }
        public decimal Completed { get; set; }
    }
    public class _Investor
    {
        public Guid InvestorID { get; set; }
        public Guid ClientID { get; set; }
        [Description("Have You Grown and Exited a Company Before")]
        public string GrownExcitedCompany { get; set; }
        [Description("What Level of Involvement Is Required")]
        public string InvolvementLevelRequired { get; set; }
        [Description("What's the Timeframe")]
        public string Timeframe { get; set; }
        [Description("What will      get back")]
        public string GetBack { get; set; }
        [Description("What Are the Real Risks")]
        public string RealRisks { get; set; }
        [Description("Is this a loan or an investment ")]
        public string LoanOrInvestment { get; set; }
        [Description("Who Else Is Investing")]
        public string Investing { get; set; }
        [Description("How Much Money Have You Put in the Business")]
        public string BusinessMoneyPut { get; set; }
        [Description("Do You Have Paying Customers")]
        public string PayingCustomer { get; set; }
        [Description("Are You Doing This Full Time")]
        public string FullTimeDoing { get; set; }
        [Description("Do I have any decision making abilities in the company ")]
        public string DecisionMakingAbility { get; set; }
        [Description("What problem (or want) are you solving?")]
        public string ProblemWantSolving { get; set; }
        [Description("What kinds of people, groups, or organizations have that problem")]
        public string ProblemKindGroup { get; set; }
        [Description("How many are there")]
        public string HowMany { get; set; }
        [Description("How are you different?")]
        public string HowDifferentYou { get; set; }
        [Description("Who will you compete with? How are they different?")]
        public string CompeteWith { get; set; }
        [Description("How will you make money?")]
        public string MakeMoney { get; set; }
        [Description("How will you make money for your investors?")]
        public string MakeMoneyInvestors { get; set; }
        [Description("How fast can you grow your business? Can you scale up volume without proportional scaling up headcount?")]
        public string BusinessGrowFast { get; set; }
        [Description("What’s proprietary? What are you going to do to defend that?")]
        public string Proprietary { get; set; }
        [Description("What traction have you made?")]
        public string TractionMade { get; set; }
        [Description("What milestones have you met?")]
        public string MileStonesMet { get; set; }
        [Description("How are you going to get the word out?")]
        public string WordoutGet { get; set; }
        [Description("How are you going to close sales?")]
        public string SalesClose { get; set; }
        [Description("How are you going to get started?")]
        public string GetStarted { get; set; }
        [Description("How are you going to spend investors’ money?")]
        public string SpendInvestorsMoney { get; set; }
        [Description("What makes your team suited for this business?")]
        public string BusinessTeamSuited { get; set; }
        [Description("How did you come up with this idea?")]
        public string IdeaComeup { get; set; }
        [Description("Why did you decide to (some marketing, product, or financial decision in the pitch)?")]
        public string DecideTo { get; set; }
        [Description("What about (some objection related to market, competition, financial plans)?")]
        public string ObjectionAbout { get; set; }
        [Description("Who are your investors so far?")]
        public string YourInvestors { get; set; }
        [Description("How strong is your patent?")]
        public string PatentStrong { get; set; }
        [Description("Could you grow faster with more money?")]
        public string GrowFaster { get; set; }
        [Description("Do you realize you’re vastly underestimating your marketing expenses (or sales expense, or margins through channels, or headcount required for direct selling)?")]
        public string MarketingExpenseRealize { get; set; }
        [Description("Do you know comparable numbers for similar businesses?")]
        public string BusinessComparable { get; set; }
        [Description("Why don’t you do this yourself? (Meaning, why do you think you need investors?)")]
        public string WhyNeedInvestors { get; set; }
        [Description("What sales have you made so far?")]
        public string SalesMade { get; set; }
        [Description("Have you actually talked to those companies?")]
        public string CompaniesTalked { get; set; }
        [Description("Who else is interested?")]
        public string Interested { get; set; }
        [Description("Who else have you shown this to?")]
        public string ShownTo { get; set; }
        [Description("How did you come up with that valuation?")]
        public string EvaluationComeup { get; set; }
        [Description("Is There a Great Management Team")]
        public string GreatManagementTeam { get; set; }
        [Description("Is the Market Opportunity Big")]
        public string BigMarketOportunity { get; set; }
        [Description("What Positive Early Traction Has the Company Achieved")]
        public string TractionPositiveAchieved { get; set; }
        [Description("Do the Founders Understand the Financials and Key Metrics of Their Business")]
        public string FoundersUnderstand { get; set; }
        [Description("Is the Initial Investor Pitch Deck Professional and Interesting")]
        public string InitialInvestorPitchDesk { get; set; }
        [Description("What Are the Potential Risks to the Business")]
        public string BusinessPotentialRisk { get; set; }
        [Description("What are the major product milestones")]
        public string ProductMilestone { get; set; }
        [Description("What are the two or three key features you plan to add")]
        public string KeyFeatures { get; set; }
        [Description("How Will My Investment Capital Be Used and What Progress Will Be Made With That Capital")]
        public string InvestmentCapital { get; set; }
        [Description("Is the Expected Valuation for the Company Realistic")]
        public string ExpectedEvaluation { get; set; }
        [Description("Does the Company Have Differentiated Technology")]
        public string DifferentiatedTechnology { get; set; }
        [Description("How easy will it be to replicate the technology")]
        public string ReplicateTechnology { get; set; }
        [Description("What key intellectual property does the company have (patents, patents pending, copyrights, trade secrets, trademarks, domain names)?")]
        public string KeyIntellectualProperty { get; set; }
        [Description("What comfort is there that the company’s intellectual property does not violate the rights of a third party")]
        public string ViolateIntellectualProperty { get; set; }
        [Description("Would any prior employers of a team member have a potential claim to the company’s intellectual property")]
        public string ClaimIntellectualProperty { get; set; }
        [Description("Is the intellectual property properly owned by the company, and have all employees and consultants assigned the intellectual property over to the company")]
        public string OwnedIntellectualProperty { get; set; }
        [Description("If the intellectual property was developed at a university or through government grants or with open source technology, how does the company have the right to use the technology")]
        public string DevelopedIntellectualProperty { get; set; }
        [Description("Are the Company’s Financial Projections Realistic and Interesting")]
        public string CompanyFinancialProjection { get; set; }
        [Description("What are your margins? How much does it cost to serve your customers")]
        public string Margins { get; set; }
        [Description("What is your sales and marketing model, and how much does it cost to acquire new customers and retain existing customers")]
        public string SalesMarketingModel { get; set; }
        [Description("How are you forecasting growth for the next 12–24 months? What are your key assumptions, and how did you come up with them")]
        public string ForecastingGrowth { get; set; }
        [Description("Who are the existing investors, and how much money have you raised so far")]
        public string ExistingInvestors { get; set; }
        [Description("What are you looking to raise now, and what milestones are you hoping to achieve with this financing round")]
        public string AchieveMilestone { get; set; }
        [Description("How much cash do you currently have, and what’s your burn rate")]
        public string CurrentCash { get; set; }
        [Description("How are you planning to structure this round, and who else is investing in this round")]
        public string StructurePlan { get; set; }

    }
    public class FinancialProjection
    {
        public Guid ProjectionID { get; set; }
        public Guid RevenueID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ActivityID { get; set; }
        public decimal Units { get; set; }
        public decimal Increment { get; set; }
        public decimal Charged { get; set; }
        public decimal Revenue { get; set; }
        public decimal Markup { get; set; }
        public bool CarryForward { get; set; }
    }

    public class Grants : Common
    {
        public Guid GrantID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ProvidedBy { get; set; }
        public string Website { get; set; }
        public string SupplierID { get; set; }
        public string VideoUrl { get; set; }
        public DateTime DueDate { get; set; }
        public int CountryID { get; set; }
        public int GrantStatus { get; set; }
        public string CountryName { get; set; }
    }

    public class PersonalSurvivalBudget
    {
        public Guid PersonalSurvivalBudgetID { get; set; }
        public Guid GrantID { get; set; }
        public Guid ClientID { get; set; }
        public int PersonalType { get; set; }
        public int PartnerCount { get; set; }
        public Guid OptionMasterID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
        public decimal Monthly { get; set; }
        public decimal Yearly { get; set; }
        public string Notes { get; set; }
        public string UsefullNote { get; set; }
    }
    public class SaleforeCast
    {
        public Guid SaleforeCastID { get; set; }
        public Guid ClientID { get; set; }
        public Guid GrantID { get; set; }
        public string Item { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal UnitsSold { get; set; }
        public decimal UnitPrice { get; set; }
        public string UsefullNote { get; set; }
        public int ItemNumber { get; set; }
        public decimal SubTotalSales { get; set; }

    }
    public class Cashflowforecast
    {
        public Guid CashflowforecastID { get; set; }
        public Guid ClientID { get; set; }
        public Guid GrantID { get; set; }
        public Guid OptionMasterID { get; set; }
        public int ForecastType { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ItemNumber { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
        public decimal IncomeDecrease { get; set; }
        public decimal ExpenditureIncrease { get; set; }
        public string UsefullNote { get; set; }
    }

    public class LoanCalculator
    {
        public Guid LoanCalculatorID { get; set; }
        public Guid ClientID { get; set; }
        public Guid GrantID { get; set; }
        public decimal AmountToBorrow { get; set; }
        public int YearsToRepay { get; set; }
        public decimal InterestRate { get; set; }
    }

    public class GrantSurvivalBudget:Common
    {
        public Guid GrantSurvivalBudgetLogID { get; set; }
        public Guid GrantID { get; set; }
        public Guid ClientID { get; set; }
        public GrantStatus GrantStatus { get; set; }
    }
    public class GrantLog : Common
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartupName { get; set; }
        public decimal AmountToBorrow { get; set; }
        public int YearsToRepay { get; set; }
        public decimal InterestRate { get; set; }
    }


}



