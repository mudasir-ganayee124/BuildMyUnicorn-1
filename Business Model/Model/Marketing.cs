using Business_Model.Helper;
using System;
using System.ComponentModel;


namespace Business_Model.Model
{

    public class OnlinePresance
    {
        public Guid OnlinePresanceID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public YourWebsite YourWebsite { get; set; }
        public SocialHandles SocialHandles { get; set; }

    }
    [Description("OnlinePresance")]
    public class YourWebsite
    {
        [Description("Have you registered a Domain")]
        public bool HaveRegisteredDomain { get; set; }
        [Description("What is Domain Name")]
        public string DomainName { get; set; }
        [Description("What stage is your webpage at")]
        public Guid WebPageStageID { get; set; }
        [Description("What do  you want to acheive with your website")]
        public Guid WantWebsiteAchieveID { get; set; }
        [Description("Who will create the content for your website")]
        public Guid ContentWebsiteCreatorID { get; set; }
        [Description("What will visitors accomplish on your website")]
        public string VisitorsAccomplish { get; set; }
        [Description("What are the most important calls-to-action (CTAs) on your site")]
        public string CTA { get; set; }
        [Description("How much traffic are you anticipating per month")]
        public Guid TrafficAnticipatePerMonthID { get; set; }
        [Description("Do you use your domain emaill address")]
        public bool UseDomainEmail { get; set; }
        [Description("How will you measure success ")]
        public string SuccessMeasure { get; set; }
        [Description("Are you planning on making money directly with your website")]
        public bool WebsitePlanMakeMoney { get; set; }
        [Description("Will you be putting adds on your webpage")]
        public bool PutWebpageAdds { get; set; }
        [Description("When analyzing your competitors’ sites, what do you like and not like about their websites")]
        public string CompetitorSitelike { get; set; }
        [Description("When analyzing your competitors’ sites, what do you like and not like about their websites")]
        public string CompetitorSitedislike { get; set; }
        public decimal Completed { get; set; }
    }

    [Description("OnlinePresance")]
    public class SocialHandles
    {
        [Description("Facebook")]
        public string Facebook { get; set; }
        [Description("Twitter")]
        public string Twitter { get; set; }
        [Description("YouTube")]
        public string Youtube { get; set; }
        [Description("Instagram")]
        public string Instagram { get; set; }
        [Description("TicTok")]
        public string TicTok { get; set; }
        [Description("Linkedin")]
        public string Linkedin { get; set; }
        public decimal Completed { get; set; }
    }

    public class _OnlinePresance
    {
        public Guid OnlinePresanceID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public YourWebsite YourWebsite { get; set; }
        [Description("Have you registered a Domain")]
        public bool HaveRegisteredDomain { get; set; }
        [Description("What stage is your webpage at")]
        public Guid WebPageStageID { get; set; }
        [Description("What do  you want to acheive with your website")]
        public Guid WantWebsiteAchieveID { get; set; }
        [Description("Who will create the content for your website")]
        public Guid ContentWebsiteCreatorID { get; set; }
        [Description("What will visitors accomplish on your website")]
        public string VisitorsAccomplish { get; set; }
        [Description("What are the most important calls-to-action (CTAs) on your site")]
        public string CTA { get; set; }
        [Description("How much traffic are you anticipating per month")]
        public Guid TrafficAnticipatePerMonthID { get; set; }
        [Description("Do you use your domain emaill address")]
        public bool UseDomainEmail { get; set; }
        [Description("How will you measure success ")]
        public string SuccessMeasure { get; set; }
        [Description("Are you planning on making money directly with your website")]
        public bool WebsitePlanMakeMoney { get; set; }
        [Description("Will you be putting adds on your webpage")]
        public bool PutWebpageAdds { get; set; }
        [Description("When analyzing your competitors’ sites, what do you like and not like about their websites")]
        public string CompetitorSitelike { get; set; }
        [Description("When analyzing your competitors’ sites, what do you like and not like about their websites")]
        public string CompetitorSitedislike { get; set; }
        [Description("Facebook")]
        public string Facebook { get; set; }
        [Description("Twitter")]
        public string Twitter { get; set; }
        [Description("YouTube")]
        public string Youtube { get; set; }
        [Description("Instagram")]
        public string Instagram { get; set; }
        [Description("TicTok")]
        public string TicTok { get; set; }
        [Description("Linkedin")]
        public string Linkedin { get; set; }
        public decimal Completed { get; set; }

    }

    public class Marketing
    {
        public Guid MarketingPlanID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public MarketingPlan MarketingPlan { get; set; }
        public MarketingBudget MarketingBudget { get; set; }

    }
    [Description("Marketing - MarketingPlan")]
    public class MarketingPlan
    {
        [Description("Marketing Plan Name")]
        public string PlanName { get; set; }
        [Description("Set your goals")]
        public Guid GoalID { get; set; }
        [Description("Set your KPIs")]
        public string KPIS { get; set; }
        [Description("Who is your target audiance")]
        public Guid BuyerPersonaID { get; set; }
        [Description("How will I find and attract my most likely buyers")]
        public string FindBuyers { get; set; }
        [Description("What is your unique Selling proposition")]
        public string SellingProposition { get; set; }
        [Description("How are you planning on reaching your audiance")]
        public Guid AudianceReachID { get; set; }
        [Description("How will you track the KPI's")]
        public string TrackKPIS { get; set; }
        public decimal Completed { get; set; }
    }

    [Description("Marketing - MarketingBudget")]
    public class MarketingBudget
     {
        [Description("Impressions")]
        public decimal Impressions { get; set; }
        [Description("Click Through Rate")]
        public decimal CTR { get; set; }
        [Description("Conversion Rate - Clicks to Leads (All)")]
        public decimal ConversionRate { get; set; }
        [Description("% Qualified Leads")]
        public decimal QualifiedLeadsPercentage { get; set; }
        [Description("% Conversion from Qualified Lead to Sale")]
        public decimal QualifiedLeadConversionPercentage { get; set; }
        [Description("Avg. Cost/Click")]
        public decimal AvgCostPerClick { get; set; }
        [Description("Avg. Sale")]
        public decimal AvgSale { get; set; }
        [Description("Cost of Goods %")]
        public decimal GoodsCostPercentage { get; set; }
        [Description("Clicks")]
        public decimal Clicks { get; set; }
        [Description("Leads (All)")]
        public decimal Leads { get; set; }
        [Description("Qualified Leads")]
        public decimal QualifiedLeads { get; set; }
        [Description("Number of Sales")]
        public decimal SalesNumber { get; set; }
        [Description("Revenue")]
        public decimal Revenue { get; set; }
        [Description("Cost Of Goods Sold")]
        public decimal GoodsCostSold { get; set; }
        [Description("Advertising Budget")]
        public decimal AdvertisingBudget { get; set; }
        [Description("Profit After Advertising")]
        public decimal ProfitAfterAdvertising { get; set; }
        [Description("% Profit")]
        public decimal ProfitPercentage { get; set; }
        [Description("Profit/Click")]
        public decimal ProfitPerClick { get; set; }
        [Description("Target (Acceptable) Cost Per Lead (All)")]
        public decimal TargetCostPerLead { get; set; }
        public decimal Completed { get; set; }
    }

    public class _Marketing
    {
        public Guid MarketingPlanID { get; set; }
        public Guid ClientID { get; set; }
        [Description("Marketing Plan Name")]
        public string PlanName { get; set; }
        [Description("Set your goals")]
        public Guid GoalID { get; set; }
        [Description("Set your KPIs")]
        public string KPIS { get; set; }
        [Description("Who is your target audiance")]
        public Guid BuyerPersonaID { get; set; }
        [Description("How will I find and attract my most likely buyers")]
        public string FindBuyers { get; set; }
        [Description("What is your unique Selling proposition")]
        public string SellingProposition { get; set; }
        [Description("How are you planning on reaching your audiance")]
        public Guid AudianceReachID { get; set; }
        [Description("How will you track the KPI's")]
        public string TrackKPIS { get; set; }
        [Description("Impressions")]
        public int Impressions { get; set; }
        [Description("Click Through Rate")]
        public decimal CTR { get; set; }
        [Description("Conversion Rate - Clicks to Leads (All)")]
        public decimal ConversionRate { get; set; }
        [Description("% Qualified Leads")]
        public decimal QualifiedLeadsPercentage { get; set; }
        [Description("% Conversion from Qualified Lead to Sale")]
        public decimal QualifiedLeadConversionPercentage { get; set; }
        [Description("Avg. Cost/Click")]
        public decimal AvgCostPerClick { get; set; }
        [Description("Avg. Sale")]
        public decimal AvgSale { get; set; }
        [Description("Cost of Goods %")]
        public decimal GoodsCostPercentage { get; set; }
        [Description("Clicks")]
        public int Clicks { get; set; }
        [Description("Leads (All)")]
        public int Leads { get; set; }
        [Description("Qualified Leads")]
        public int QualifiedLeads { get; set; }
        [Description("Number of Sales")]
        public decimal SalesNumber { get; set; }
        [Description("Revenue")]
        public decimal Revenue { get; set; }
        [Description("Cost Of Goods Sold")]
        public decimal GoodsCostSold { get; set; }
        [Description("Advertising Budget")]
        public decimal AdvertisingBudget { get; set; }
        [Description("Profit After Advertising")]
        public decimal ProfitAfterAdvertising { get; set; }
        [Description("% Profit")]
        public decimal ProfitPercentage { get; set; }
        [Description("Profit/Click")]
        public decimal ProfitPerClick { get; set; }
        [Description("Target (Acceptable) Cost Per Lead (All)")]
        public decimal TargetCostPerLead { get; set; }
        public string AudianceReachValue { get; set; }
        public string GoalValue { get; set; }
        public decimal Completed { get; set; }


    }

    [Description("YourBrand")]
    public class MarketingBrand
    {
        public Guid MarketingBrandID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public BrandingStrategy BrandingStrategy { get; set; }
        public BrandGuidelines BrandGuidelines { get; set; }
        public BrandTouchPoints BrandTouchPoints  { get; set; }
    }
    public class BrandingStrategy
    {
        [Description("What are we")]
        public string WhatAreWe { get; set; }
        [Description("Why are we here")]
        public string WhyWeHere { get; set; }
        [Description("What do we do and how do we do it")]
        public string WhatDoHowDo { get; set; }
        [Description("What makes us different")]
        public string WhatMakesDifferent { get; set; }
        [Description("Who are we here for")]
        public string WhoWeHereFor { get; set; }
        [Description("What do we value the most")]
        public string WhatValueMost { get; set; }
        [Description("What’s our personality")]
        public string OurPersonality { get; set; }
        [Description("Our ambition")]
        public string OurAmbition { get; set; }
        [Description("Do you have an emotional selling point")]
        public string EmotionalSellingPoint { get; set; }
        public decimal Completed { get; set; }



    }

    public class BrandGuidelines
    {
        [Description("Show a Powerpoint ")]
        public Guid PowerPresentationID { get; set; } = Guid.Parse("B39E91B2-DA11-41D0-A20A-F4A097C495B0");
        [Description("Download the template ")]
        public bool TemplateDownloaded { get; set; }
        public decimal Completed { get; set; }
    }

    public class BrandTouchPoints
    {
        public string BrandTouchPointID { get; set; }
        public decimal Completed { get; set; }
    }

    public class _MarketingBrand
    {
        public Guid MarketingBrandID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        [Description("What are we")]
        public string WhatAreWe { get; set; }
        [Description("Why are we here")]
        public string WhyWeHere { get; set; }
        [Description("What do we do and how do we do it")]
        public string WhatDoHowDo { get; set; }
        [Description("What makes us different")]
        public string WhatMakesDifferent { get; set; }
        [Description("Who are we here for")]
        public string WhoWeHereFor { get; set; }
        [Description("What do we value the most")]
        public string WhatValueMost { get; set; }
        [Description("What’s our personality")]
        public string OurPersonality { get; set; }
        [Description("Our ambition")]
        public string OurAmbition { get; set; }
        [Description("Do you have an emotional selling point")]
        public string EmotionalSellingPoint { get; set; }
        [Description("Show a Powerpoint ")]
        public Guid PowerPresentationID { get; set; } = Guid.Parse("B39E91B2-DA11-41D0-A20A-F4A097C495B0");
        [Description("Download the template ")]
        public bool TemplateDownloaded { get; set; }
        public string BrandTouchPointID { get; set; }
    }

}
