using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Helper;

namespace Business_Model.Model
{
    public class Survey : Common
    {
        public Guid SurveyID { get; set; }
        public Guid ClientID { get; set; }
        public string SurveyTitle { get; set; }
        [AllowHtml]
        public string SurveyForm { get; set; }
        public string ClientName { get; set; }
        public int SurveyReceived { get; set; }
    }

    public class SurveyData
    {
        public Guid SurveyDataID { get; set; }
        public Guid SurveyID { get; set; }
        public int SurveyUnitID { get; set; } 
        public string KeyField { get; set; }
        public string KeyValue { get; set; }

        public string JsonData { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
    public class Interview : Common
    {
        public Guid InterviewID { get; set; }
        public Guid ClientID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Form { get; set; }
        public string ClientName { get; set; }
        public int InterviewReceived { get; set; }
        public EntityState EntityState { get; set; }
    }

    public class InterviewData
    {
        public Guid InterviewDataID { get; set; }
        public Guid InterviewID { get; set; }
        public int InterviewUnitID { get; set; }
        public string KeyField { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}