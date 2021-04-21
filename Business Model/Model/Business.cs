using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;

namespace Business_Model.Model
{
    public class Business
    {

        public Guid BusinessID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public Founder Founder { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        public int TotalQuestions { get; set; }
        public int Answered { get; set; }
        public int Unanswered { get; set; }

    }

    public class Founder
    {
        [Description("How did you come up with the idea?")]
        public string IdeaComeup { get; set; }
        [Description("Why do you want to run your own business?")]
        public string BusinessRun { get; set; }
        [Description("What is your previous work experience that is relevant to your start-up?")]
        public string PreviousWorkExperience { get; set; }
        [Description("What are your qualifications?")]
        public string Qualification { get; set; }
        [Description("What are your hobbies and interests?")]
        public string HobbiesInterest { get; set; }
        [Description("What makes you the right person to create this business?")]
        public string WhyYou { get; set; }
        public decimal Completed { get; set; }
    }

    public class CompanyDetails
    {
        [Description("What is the registered name of the company?")]
        public string CompanyRegisterdName { get; set; }
        [Description("What is the Legal structure of company?")]
        public Guid CompanyLegalStructureID { get; set; }
        [Description("What is the company number?")]
        public string CompanyNumber { get; set; }
        [Description("What year was the company founded?")]
        public string Founded { get; set; }
        [Description("What is the registered business address of the company?")]
        public string BusinessAddress { get; set; }
        [Description("Does your business require premises?")]
        public bool PremisesRequire { get; set; }
        public string Landlord { get; set; }
        [Description("Business phone number")]
        public string BusinessPhone { get; set; }
        [Description("Have you registered for VAT, if so what is you VAT number?")]
        public string VatNumber { get; set; }
        [Description("Number of founders")]
        public int? NumberofFounder { get; set; }
        [Description("Who are they")]
        public Guid CompanyFounderID { get; set; }
        public decimal Completed { get; set; }
    }

    public class _Business
    {
        public Guid BusinessID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public Founder Founder { get; set; }
        public CompanyDetails CompanyDetails { get; set; }
        [Description("How did you come up with the Idea?")]
        public string IdeaComeup { get; set; }
        [Description("Why do you want to run your own business?")]
        public string BusinessRun { get; set; }
        [Description("What is your previous work experience that is relevant to your start-up?")]
        public string PreviousWorkExperience { get; set; }
        [Description("What are your qualifications?")]
        public string Qualification { get; set; }
        [Description("What are your hobbies and interests?")]
        public string HobbiesInterest { get; set; }
        [Description("What makes you the right person to create this business?")]
        public string WhyYou { get; set; }
        [Description("What is the registered name of the company?")]
        public string CompanyRegisterdName { get; set; }
        [Description("What is the Legal structure of company?")]
        public Guid CompanyLegalStructureID { get; set; }
        [Description("What is the company number?")]
        public string CompanyNumber { get; set; }
        [Description("What year was the company founded?")]
        public string Founded { get; set; }
        [Description("What is the registered business address of the company?")]
        public string BusinessAddress { get; set; }
        [Description("Business phone number")]
        public string BusinessPhone { get; set; }
        [Description("Have you registered for VAT, if so what is you VAT number?")]
        public string VatNumber { get; set; }
        [Description("Number of founders")]
        public int? NumberofFounder { get; set; }
        [Description("Who are they")]
        public Guid CompanyFounderID { get; set; }

    }

    public class ProductService
    {

        public Guid ProductServiceID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public AboutProduct AboutProduct { get; set; }
        public MVP MVP { get; set; }

    }

    [Description("ProductService")]
    public class AboutProduct
    {
        [Description("What are you selling?")]
        public string PSSelling { get; set; }
        [Description("Give a brief description of your product or service?")]
        public string PSDescription { get; set; }
        [Description("Who will develop your product or service?")]
        public string PSProduce { get; set; }
        [Description("When would you like to start developing your Product/Service?")]
        public string PSDevelopStart { get; set; }
        [Description("When would you like it to be ready for launch?")]
        public string PSReadylaunch { get; set; }
        [Description("Are there different variations of your Product / Service?")]
        public string PSVariations { get; set; }
        [Description("Do you think you have anything that can be patentable?")]
        public int PSHaveIPAddress { get; set; }
        [Description("why do you think it is patentable?")]
        public string PSIPAddress { get; set; }
        [Description("Have you registered a trademark?")]
        public int PSHaveTradeMark { get; set; }
        [Description("What is your Trademark?")]
        public string PSTradeMark { get; set; }
        [Description("Have you created a technology road map?")]
        public int PSHaveTechnologyRoadMap { get; set; }
        public decimal Completed { get; set; }


    }

    [Description("ProductService")]
    public class MVP
    {
        [Description("Have you developed an MVP (Minimum Viable Product)?")]
        public int MVPHavePrototype { get; set; }
        [Description("How far have you got in your development?")]
        public string MVPDevelopmentFarID { get; set; }
        [Description("What does it look like?")]
        public string MVPPrototype { get; set; }
        [Description("What did it take to create or what will it take to create?")]
        public string MVPCreate { get; set; }
        [Description("What was the reason for your MVP?")]
        public Guid MVPReasonID { get; set; }
        public decimal Completed { get; set; }

    }

    public class _ProductService
    {
        public Guid ProductServiceID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        [Description("What are you selling?")]
        public string PSSelling { get; set; }
        [Description("Give a brief description of your product or service?")]
        public string PSDescription { get; set; }
        [Description("Who will develop your product or service?")]
        public string PSProduce { get; set; }
        [Description("When would you like to start developing your Product/Service?")]
        public string PSDevelopStart { get; set; }
        [Description("When would you like it to be ready for launch?")]
        public string PSReadylaunch { get; set; }
        [Description("Are there different variations of your Product / Service?")]
        public string PSVariations { get; set; }
        [Description("Do you think you have anything that can be patentable?")]
        public int PSHaveIPAddress { get; set; }
        [Description("why do you think it is patentable?")]
        public string PSIPAddress { get; set; }
        [Description("Have you registered a trademark?")]
        public int PSHaveTradeMark { get; set; }
        [Description("What is your Trademark?")]
        public string PSTradeMark { get; set; }
        [Description("Have you created a technology road map?")]
        public int PSHaveTechnologyRoadMap { get; set; }
        [Description("Have you developed an MVP (Minimum Viable Product)?")]
        public int MVPHavePrototype { get; set; }
        [Description("How far have you got in your development?")]
        public string MVPDevelopmentFarID { get; set; }
        [Description("What does it look like?")]
        public string MVPPrototype { get; set; }
        [Description("What did it take to create or what will it take to create?")]
        public string MVPCreate { get; set; }
        [Description("What was the reason for your MVP?")]
        public Guid MVPReasonID { get; set; }
    }

    [Description("Running the Business")]
    public class RunningBusiness
    {
        public EntityState EntityState { get; set; }
        [Description("Legal Structure of company")]
        public Guid RunningBusinessID { get; set; }
        [Description("Registered Name of the company")]
        public Guid ClientID { get; set; }
        [Description("How will your Product / Service be produced")]
        public Guid RBProducedID { get; set; }
        [Description("Delivery method")]
        public Guid RBDeliveryMethodID { get; set; }
        [Description("What 3rd parties will be involved")]
        public Guid RBThirdPartyInvovedID { get; set; }
        [Description("Payment method")]
        public Guid RBPaymentMethodID { get; set; }
        [Description("What is your Quality Control Method")]
        public string RBQualityControlMethod { get; set; }
        [Description("Where will your staff work")]
        public Guid RBStaffWorkID { get; set; }
        [Description("Do you need any equipment / Software")]
        public int RBHaveEquipmentSoftware { get; set; }
        [Description("What is needed")]
        public string RBEquipmentSoftware { get; set; }
        [Description("How many staff do you have")]
        public int RBStaffNumber { get; set; }
        [Description("How many staff will you have this time next year")]
        public int RBStaffNumberNextYear { get; set; }
        [Description("Have you got insurance")]
        public int RBHaveInsurance { get; set; }
        [Description("Do you need any specific license to work")]
        public int RBNeedSpecificLicense { get; set; }
        [Description("Do you need any specific Qualifications")]
        public int RBNeedSpecificeQualification { get; set; }
        public decimal Completed { get; set; }
    }

    public class BusinessOperation
    {
        public Guid BusinessOperationID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public WhoDoesWhat WhoDoesWhat  { get; set; }
        public Operations Operations { get; set; }

    }

    public class WhoDoesWhat
    {
        [Description("Who is responsible for finance (Operational)?")]
        public string FinanceOperationalResponsible { get; set; }
        [Description("Who is responsible for finance (mangement)?")]
        public string FinanceManagementResponsible { get; set; }
        [Description("Who is responsible for Operations?")]
        public string OperationResponsible { get; set; }
        [Description("Who is responsible for Legal matters?")]
        public string LegalMatterResponsible { get; set; }
        [Description("Who is responsible for HR?")]
        public string HRresponsible { get; set; }
        [Description("Who is responsible for facilities?")]
        public string FacilitiesResponsible { get; set; }
        [Description("Who is responsible for sales?")]
        public string SalesResponsible { get; set; }
        [Description("Who is responsible for marketing?")]
        public string MarektingResponsible { get; set; }
        public decimal Completed { get; set; }
    }

    public class Operations
    {
        [Description("How will your Product / Service be produced?")]
        public string ProductProduced { get; set; } // RBProducedID dropdown
        [Description("Delivery method?")]
        public Guid DeliveryMethodID { get; set; }
        [Description("What 3rd parties will be involved?")]
        public Guid ThirdPartyInvovedID { get; set; }
        [Description("Payment method - How will you charge?")]
        public Guid PaymentMethodID { get; set; }
        [Description("What is your Quality Control Method?")]
        public string QualityControlMethod { get; set; }
        [Description("Where will your staff work?")]
        public Guid StaffWorkID { get; set; }
        [Description("Do you need any equipment / Software?")]
        public bool Needsoftware{ get; set; }
        [Description("What software is needed?")]
        public string SoftwareType { get; set; }
        //[Description("What is needed")]
        //public string RBEquipmentSoftware { get; set; }
        [Description("How many staff do you have?")]
        public int? StaffCount { get; set; }
        [Description("How many staff do you hope to have this time next year?")]
        public int? StaffCountNextYear { get; set; }
        [Description("Have you got insurance?")]
        public bool HaveInsurance { get; set; }
        [Description("What type of insurance?")]
        public string InsuranceType { get; set; }
        [Description("Do you need any specific license to work?")]
        public bool NeedLicense { get; set; }
        [Description("What type of license?")]
        public string LicenseType { get; set; }
        [Description("Do you need any specific Qualifications?")]
        public bool NeedQualification { get; set; }
        [Description("What type of qualification?")]
        public string QualificationType { get; set; }
        public decimal Completed { get; set; }

    }

    public class _BusinessOperation
    {
        public Guid BusinessOperationID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        [Description("Who is responsible for finance (Operational)?")]
        public string FinanceOperationalResponsible { get; set; }
        [Description("Who is responsible for finance (mangement)?")]
        public string FinanceManagementResponsible { get; set; }
        [Description("Who is responsible for Operations?")]
        public string OperationResponsible { get; set; }
        [Description("Who is responsible for Legal matters?")]
        public string LegalMatterResponsible { get; set; }
        [Description("Who is responsible for HR?")]
        public string HRresponsible { get; set; }
        [Description("Who is responsible for facilities?")]
        public string FacilitiesResponsible { get; set; }
        [Description("Who is responsible for sales?")]
        public string SalesResponsible { get; set; }
        [Description("Who is responsible for marketing?")]
        public string MarektingResponsible { get; set; }
        [Description("How will your Product / Service be produced?")]
        public string ProductProduced { get; set; } // RBProducedID dropdown
        [Description("Delivery method?")]
        public Guid DeliveryMethodID { get; set; }
        [Description("What 3rd parties will be involved?")]
        public Guid ThirdPartyInvovedID { get; set; }
        [Description("Payment method - How will you charge?")]
        public Guid PaymentMethodID { get; set; }
        [Description("What is your Quality Control Method?")]
        public string QualityControlMethod { get; set; }
        [Description("Where will your staff work?")]
        public Guid StaffWorkID { get; set; }
        [Description("Do you need any equipment / Software?")]
        public bool Needsoftware { get; set; }
        [Description("What software is needed?")]
        public string SoftwareType { get; set; }
        //[Description("What is needed")]
        //public string RBEquipmentSoftware { get; set; }
        [Description("How many staff do you have?")]
        public int? StaffCount { get; set; }
        [Description("How many staff do you hope to have this time next year?")]
        public int? StaffCountNextYear { get; set; }
        [Description("Have you got insurance?")]
        public bool HaveInsurance { get; set; }
        [Description("What type of insurance?")]
        public string InsuranceType { get; set; }
        [Description("Do you need any specific license to work?")]
        public bool NeedLicense { get; set; }
        [Description("What type of license?")]
        public string LicenseType { get; set; }
        [Description("Do you need any specific Qualifications?")]
        public bool NeedQualification { get; set; }
        [Description("What type of qualification?")]
        public string QualificationType { get; set; }
        public decimal Completed { get; set; }

    }

    public class Competitor
    {
        public Guid CompetitorID { get; set; }
        public EntityState EntityState { get; set; }
        public Guid ClientID { get; set; }
        public IEnumerable<CompetitorAnalysis> CompetitorAnalysis { get; set; }
        public SWOT SWOT { get; set; }
    }

    [Description("Competitor")]
    public class CompetitorAnalysis
    {
        public Guid CompetitorAnalysisID { get; set; }
        [Description("Name")]
        public string Name { get; set; }
        [Description("Location")]
        public string Location { get; set; }
        [Description("Website")]
        public string Website { get; set; }
        [Description("Offering")]
        public string Offering { get; set; }
        [Description("Pricing")]
        public string Pricing { get; set; }
        [Description("Your Differentiator")]
        public string Differentiator { get; set; }
        public decimal Completed { get; set; }

    }

    [Description("Competitor")]
    public class SWOT
    {
   
        public Guid SWOTID { get; set; }
        [Description("What are your Strengths?")]
        public string Strengths { get; set; }
        [Description("What are your Weaknesses?")]
        public string Weaknesses { get; set; }
        [Description("What are your Opportunities?")]
        public string Opportunities { get; set; }
        [Description("What are your Threats?")]
        public string Threats { get; set; }
        public decimal Completed { get; set; }

    }
   
    public class _Competitor
    {
        public Guid CompetitorID { get; set; }
        public Guid SWOTID { get; set; }
        public EntityState EntityState { get; set; }
        public Guid ClientID { get; set; }
        public Guid CompetitorAnalysisID { get; set; }
        [Description("Name")]
        public string Name { get; set; }
        [Description("Location")]
        public string Location { get; set; }
        [Description("Website")]
        public string Website { get; set; }
        [Description("Offering")]
        public string Offering { get; set; }
        [Description("Pricing")]
        public string Pricing { get; set; }
        [Description("Your Differentiator")]
        public string Differentiator { get; set; }
        [Description("What are your Strengths?")]
        public string Strengths { get; set; }
        [Description("What are your Weaknesses?")]
        public string Weaknesses { get; set; }
        [Description("What are your Opportunities?")]
        public string Opportunities { get; set; }
        [Description("What are your Threats?")]
        public string Threats { get; set; }

    }

    public class Customer:Common
    {

        public Guid CustomerID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public IEnumerable<BuyerPersona> BuyerPersona { get; set; }
        public BuyerPurchaseCycle BuyerPurchaseCycle { get; set; }
        [Description("Describe your typical customer")]
        public string About { get; set; }
        [Description("Where are they based")]
        public string Based { get; set; }
        [Description("Why are they going to buy from you")]
        public string Buy { get; set; }
        [Description("What factors help the customer choose who to buy from")]
        public string Factors { get; set; }
    }

    [Description("Customer")]
    public class BuyerPersona
    {
        public Guid BuyerPersonaID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ClientID { get; set; }
        [Description("Name")]
        public string Name { get; set; }
        [Description("Photo Select")]
        public string AvtarID { get; set; }
        [Description("Job Title")]
        public string JobTitle { get; set; }
        [Description("Responsibilities")]
        public string Responsibility { get; set; }
        [Description("Age")]
        public Guid AgeID { get; set; }
        [Description("Gender")]
        public Guid GenderID { get; set; }
        [Description("Income")]
        public Guid IncomeID { get; set; }
        [Description("Location")]
        public string Location { get; set; }
        [Description("Pain Points / Challenges")]
        public string PainPoints { get; set; }
        [Description("Their Goals")]
        public string Goals { get; set; }
        [Description("What he wants to know")]
        public string WantsKnow { get; set; }
        [Description("What he doesn't want to know")]
        public string NotWantsKnow { get; set; }
        [Description("What will drive them to buy")]
        public string DriveBuy { get; set; }
        [Description("Why they buys from us")]
        public string BuyFrom { get; set; }
        [Description("How they find us")]
        public string Findus { get; set; }
        public string GenderValue { get; set; }
        public string IncomeValue { get; set; }
        public string AgeValue { get; set; }
    }

    [Description("Customer")]
    public class BuyerPurchaseCycle
    {

        [Description("Recognition of need")]
        public string RecognitionNeed { get; set; }
        [Description("Information search")]
        public string InformationSearch { get; set; }
        [Description("Evaluation of Alternatives")]
        public string AlternativeEvaluation { get; set; }
        [Description("Purchase decision")]
        public string PurchaseDecision { get; set; }
        [Description("Post Purchase Evaluation")]
        public string PurchaseEvaluation { get; set; }
    }

    public class _Customer : Common
    {

        public Guid CustomerID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        [Description("Describe your typical customer")]
        public string About { get; set; }
        [Description("Where are they based")]
        public string Based { get; set; }
        [Description("Why are they going to buy from you")]
        public string Buy { get; set; }
        [Description("What factors help the customer choose who to buy from")]
        public string Factors { get; set; }
        [Description("Name")]
        public string Name { get; set; }
        [Description("Photo Select")]
        public string ImageID { get; set; }
        [Description("Job Title")]
        public string JobTitle { get; set; }
        [Description("Responsibilities")]
        public string Responsibility { get; set; }
        [Description("Age")]
        public Guid AgeID { get; set; }
        [Description("Gender")]
        public Guid GenderID { get; set; }
        [Description("Income")]
        public Guid IncomeID { get; set; }
        [Description("Location")]
        public string Location { get; set; }
        [Description("Pain Points / Challenges")]
        public string PainPoints { get; set; }
        [Description("Their Goals")]
        public string Goals { get; set; }
        [Description("What he wants to know")]
        public string WantsKnow { get; set; }
        [Description("What he doesn't want to know")]
        public string NotWantsKnow { get; set; }
        [Description("What will drive them to buy")]
        public string DriveBuy { get; set; }
        [Description("Why they buys from us")]
        public string BuyFrom { get; set; }
        [Description("How they find us")]
        public string Findus { get; set; }
        [Description("Recognition of need")]
        public string RecognitionNeed { get; set; }
        [Description("Information search")]
        public string InformationSearch { get; set; }
        [Description("Evaluation of Alternatives")]
        public string AlternativeEvaluation { get; set; }
        [Description("Purchase decision")]
        public string PurchaseDecision { get; set; }
        [Description("Post Purchase Evaluation")]
        public string PurchaseEvaluation { get; set; }
    }
}
