
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public enum Network
    {

        Direct = 1,
        Affiliate = 2

    }

    public enum _EntityType
    {

        Admin = 1,
        Client = 2,
        Supplier = 3,
        Accelerator = 4
    }

    public enum AccountType
    {

        Admin = 1,
        Client = 2,
        Supplier = 3,
        Accelerator = 4
    }

    public enum AccountStatus
    {

        Approved = 1,
        Disapproved = 2,
        Pending = 3,
        Suspednded = 4,
        NotFilled = 5
    }

    public enum ModuleName
    {
        IdeaOutofMyHead = 1,
        BusinessOverview = 2,
        TheTeam = 3,
        ProductService = 4,
        RunningBusiness = 5,
        Competitors = 6,
        KeyFinding = 7,
        Customer = 8,
        BusinessOperation = 9,
        Marketing = 10

    }

    public enum BusinessSection
    {
        BusinessOverview = 1,
        ProductService = 2,
        RunningBusiness = 3,
        KeyFinding = 4,
        Competitors = 6,
        Customer = 7,
        BusinessOperation = 8

    }

    public enum Module
    {
        MyIdea = 1,
        MarketResearch = 2,
        TheBusiness = 3,
        Marketing = 4,
        RecruitingTalent = 5,
        Selling = 6,
        Finance = 7,
        LegalRegulatory = 8,
        Recruitment = 9
    }

    public enum ModuleSection
    {
        MyIdea_Ideaoutofhead = 1,
        MarketResearch_KeyFindings = 2,
        TheBusiness_Businessoverview = 3,
        TheBusiness_ProductorService = 4,
        TheBusiness_RunningtheBusiness = 5,
        TheBusiness_Competitors = 6,
       // TheBusiness_Customers = 7,
        TheBusiness_BusinessOperation = 8,
        MarketResearch_Interview = 9,
        MarketResearch_Observation = 10,
        MarketResearch_Survey = 11,
        MarketResearch_OnlineResearch = 12,
        TheBusiness_Team = 13,
        Selling_Customers =14,
        Selling_Pricing = 15,
        Marketing_OnlinePresence = 16,
        Marketing_MarketingPlan = 17,
        Marketing_Brand = 18,
       // Finance_FinancialProjections = 19,
        Finance_FinancialProjections = 19,
        Finance_CompanyValuation = 20,
        Finance_RaisingFinance = 21,
        Finance_PitchDecks = 22,
        Finance_WhatinvestorsAsk = 23,
        Finance_FindingInvestors = 30,
        // Finance_RaisingFinance = 19,
        // Finance_PitchDeckTemplate = 20,
        //Finance_CompanyCashFlow = 21,
        //Finance_BusinessBankAccount = 22,
        //LegalRegulatory_CompanySetup = 23,
        LegalRegulatory_RecommendedDocumentation = 24,
        LegalRegulatory_PrivacyDataPolicies  =25,
        LegalRegulatory_GDPR = 26,
        Recruitment_RecruitmentPlan = 27,
        Recruitment_FindIntern = 28,
        Recruitment_PostJob = 29






    }
    public enum ResponseType
    {
        Success = 1,
        Failed = 2,
        Error = 3,
        Ok = 4,
        Yes = 5,
        No = 6,
        Redirect = 7,
        NotRedirect = 8,
        Return = 9,
        Exist = 10,
        NotExist = 11,
        Updated = 12,
        NotUpdated = 14,
        Started = 15
    }

    public enum MemberType
    {
        TeamMember = 1,
        Contributor = 2

    }
    public enum AppendixType
    {
        Keyword = 1,
        Messgae = 2
    }
    public enum GrantStatus
    {
        Continue = 1,
        Locked = 2
    }

    public enum TemplateType
    {
       NPC,NSC,NACC,NAC, PFP,SFP,ACFP, ADFP, CPLI, CPAI

    }

    public enum GatewayType
    {
        Revolut = 1, Paypal  = 2, Stripe = 3
        
    }
    public enum ResponseErrorType
    {
        HttpResponseError = 1,
        PaymentDeclineResponseError = 2
       
    }

    public enum CaptureModeEnum
    {
        AUTOMATIC, MANUAL
    }
    public enum OrderStatus
    {
        Completed, Pending
    }

    public enum TransactionStatus
    { 
     Success, Failed
    }

    public enum PaymentMode
    { Cash, Checks , DebitCard , CreditCard , ACH , MoneyTransfer}

    public enum Frequency
    { 
       None, Daily, Weekly, Biweekly, Monthly, SemiMonthly, Quarterly, Yearly
    }

    public enum OrderType
    {
        Plan, Package
    }
    public enum SurveyTemplateType
    {
        Survey, Interview
    }

    public enum Priority
    { 
        High, Medium, Low
    }
    public enum Status
    {
        Active, InActive, Hold, Complete, Incomplete
    }
    
}
