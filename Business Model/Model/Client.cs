using System;
using System.Collections.Generic;
using System.ComponentModel;
using Business_Model.Helper;
using Business_Model.Model;

namespace Business_Model.Model
{
    public class Client : Common
    {
        public Guid ClientID { get; set; }
        public Guid TeamID { get; set; }
        public Guid BusinessID { get; set; }
        public Guid TeamClientID { get; set; }
        public Guid PlanID { get; set; }
        public string TeamInfo { get; set; }
        public EntityState EntityState { get; set; }
        public MemberType MemberType { get; set; }
        public string StartupName { get; set; }

        public string StartupLogo { get; set; }
        public string StartupAddress { get; set; }
        public string Domain { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string RoleInCompany { get; set; }
        public string LinkedProfile { get; set; }
        public string CountryName { get; set; }
        public string ImageID { get; set; }
        public string ShortBio { get; set; }
        public int CountryID { get; set; }
        public bool IsProfileUpdated { get; set; }
        public bool IsCountryGrant { get; set; }

        public bool RememberMe { get; set; }
        public Guid ConfirmationID { get; set; }
        public Guid AffiliateLinkID { get; set; }
        public DateTime LastLoginDateTime { get; set; }

        public bool IsPasswordChanged { get; set; }
    }
    public class ClientTeam : Client
    {
        public Guid TeamClientID { get; set; }
        public MemberType MemberType { get; set; }
    }
    public class ExtendedClientTeam : ClientTeam
    {

       public IEnumerable<ProgressAnalytic> ProgressAnalytic { get; set; }
    }
    //public class KeyFinding
    //{
    //    public Guid KeyFindingID { get; set; }
    //    public Guid ClientID { get; set; }
    //    public decimal ProgressValue { get; set; }
    //    public BigPictureResearch BigPictureResearch { get; set; }
    //    public FocussedResearch FocussedResearch { get; set; }
    //}

    //public class BigPictureResearch
    //{
    //    [Description("")]
    //    public string ResearchCarriedOutBit1 { get; set; }
    //    [Description("")]
    //    public string ResearchCarriedOutBit2 { get; set; }
    //    [Description("")]
    //    public string ResearchCarriedOutBit3 { get; set; }
    //    [Description("")]
    //    public string IndustryTrends { get; set; }
    //    [Description("")]
    //    public string CaptureInitially { get; set; }
    //    [Description("")]
    //    public decimal MarketSize { get; set; }
    //    [Description("")]
    //    public decimal CaptureShare { get; set; }
    //    [Description("")]
    //    public decimal MarketShare { get; set; }
    //    [Description("")]
    //    public int MarketShareCaptureDuration { get; set; }
    //    [Description("")]
    //    public Guid MarketKeyPlayerID { get; set; }
    //    public decimal Completed { get; set; }

    //}

    //public class FocussedResearch
    //{
    //    [Description("")]
    //    public string IdeaProgressed { get; set; }
    //    [Description("")]
    //    public int CustomerFeedback { get; set; }
    //    [Description("")]
    //    public string FeedbackReceived { get; set; }
    //    [Description("")]
    //    public string FeedbackRate { get; set; }
    //    [Description("")]
    //    public string FeedbackKeyfinding { get; set; }
    //    public decimal Completed { get; set; }

    //}



    public class MarketKeyPlayer
    {
        public Guid MarketKeyPlayerID { get; set; }
        public Guid OnlineResearchID { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
    
    }

   


}