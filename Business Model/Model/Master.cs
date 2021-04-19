using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business_Model.Model
{
    public static class GetMasterType
    {
        public static Dictionary<OptionType, MasterType>  TypeRetur()
        {
           return  new Dictionary<OptionType, MasterType>
          {
           {OptionType.WorkLocation, new MasterType() { OptionType = OptionType.WorkLocation, Title = "Work location", DisplayAs = "Manage Work ocation"}},
           { OptionType.Operation_ThirdPartiesInvolved , new MasterType() { OptionType = OptionType.Operation_ThirdPartiesInvolved , Title = "Third Party", DisplayAs = "Manage Third Party"}},
          };

        }
    }

    public enum OptionType
    {
       
        WorkLocation = 1,
        BusinessPlacement = 2,
        Charge = 3,
        MoneyRaise = 4,
        Selling = 5,
        Startup = 6,
        Technology = 7,
        CompanyType = 8,
        AffiliateRules = 9,
        IdeaProgress = 10,
        FeedBack = 11,
        RoleInCompany = 12,
        CompanyLegalStructure = 13,
        SellingType  = 14, // Ignore
        ProductServiceSelling = 15,
        ProductServiceProduce = 16,
        MVPReason = 17,
        Operation_ProductServicebeproduced = 18,
        Operation_DeliveryMethod = 19,
        Operation_ThirdPartiesInvolved =  20,
        Operation_PaymentMethod = 21,
        Operation_StaffWork = 22,
        BuyerPersona_Age = 23,
        BuyerPersona_Income = 24,
        Gender = 25,
        YourWebsite_Stage = 26,
        YourWebsite_Achieve = 27,
        YourWebsite_Content = 28,
        YourWebsite_Traffic = 29,
        YourWebsite_FunctionNecessary = 30,
        MarketingPlan_Goals = 31,
        MarketingPlan_AudianceReach = 32,
        Brand_BrandTouchPoint = 33,
        ProductService_MVPDevelopmentFar = 34





    }



    public  class MasterType
    {
        public OptionType OptionType;
        public string Title;
        public string DisplayAs;

    }
    public class MasterCommon : Common
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class Option : Common
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
        public OptionType Type { get; set; }
    }

    public class Choice
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }
  

    public class Startup : MasterCommon
    {
        public string TableName { get; } = "tbl_master_startup";
    }
    public class Technology : MasterCommon
    {
        public string TableName { get; } = "tbl_master_technology";
    }
    public class Selling : MasterCommon
    {
        public string TableName { get; } = "tbl_master_selling";
    }
    public class Charge : MasterCommon
    {
        public string TableName { get; } = "tbl_master_charge";
    }
    public class MoneyRasie : MasterCommon
    {
        public string TableName { get; } = "tbl_master_moneyraise  ";
    }

    public class MultipleMaster
    {
        public Guid ID { get; set; }
        public int ModuleID { get; set; }
        public Guid EntityID { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
    }
}