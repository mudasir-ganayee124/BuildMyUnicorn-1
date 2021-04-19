using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;

namespace Business_Model.Model
{
    class MarketResearch
    {
    }

    public class OurObservation
    {
        public Guid ObervationID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        [Description("What have you observed?")]
        public string Observation { get; set; }
        [Description("What data have you collected from your observations?")]
        public string Collection { get; set; }
        [Description("Are there any Patterns?")]
        public bool AnyPatterns { get; set; }
        [Description("What are the Patterns?")]
        public string Patterns { get; set; }
        [Description("Key characteristics in their Behaviour?")]
        public string KeyMoments { get; set; }
        public decimal Completed { get; set; }
    }

    public class KeyFinding1
    {
        public Guid KeyFindingID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public MarketResearchResults MarketResearchResults { get; set; }

    }

    [Description("KeyFinding1")]
    public class MarketResearchResults
    {
        [Description("Your key findings from the Interview?")]
        public string InterviewKeyFinding { get; set; }
        [Description("After the interviews, how confident  are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public int InterviewKeyFindingConfident { get; set; }
        [Description("Your key findings from the observation?")]
        public string ObservationKeyFinding { get; set; }
        [Description("After the observation, how confident  are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public int ObservationKeyFindingConfident { get; set; }
        [Description("Your key findings from the survey?")]
        public string SurveyKeyFinding { get; set; }
        [Description("After the Surveys, how confident  are you in your start-up 1 been pack it in and 10 been 'never been as sure as anything ever?")]
        public int SurveyKeyFindingConfident { get; set; }
        [Description("Your key findings from the online research?")]
        public string OnlineResearchKeyFinding { get; set; }
        [Description("After the online research, how confident are you in your product 1 been pack it in and 10 been never been as sure as anything ever?")]
        public int OnlineResearchKeyFindingConfident { get; set; }
        public decimal Completed { get; set; }
    }
    public class _KeyFinding1
    {
        public Guid KeyFindingID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public MarketResearchResults MarketResearchResults { get; set; }
        [Description("Your key findings for Interview?")]
        public string InterviewKeyFinding { get; set; }
        [Description("After the interviews, how confident  are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public int InterviewKeyFindingConfident { get; set; }
        [Description("Your key findings from the observation?")]
        public string ObservationKeyFinding { get; set; }
        [Description("After the observation, how confident are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public int ObservationKeyFindingConfident { get; set; }
        [Description("Your key findings from the survey?")]
        public string SurveyKeyFinding { get; set; }
        [Description("After the Surveys, how confident are you in your start-up 1 been pack it in and 10 been 'never been as sure as anything ever?")]
        public int SurveyKeyFindingConfident { get; set; }
        [Description("Your key findings from the online research?")]
        public string OnlineResearchKeyFinding { get; set; }
        [Description("After the online research, how confident are you in your product 1 been pack it in and 10 been never been as sure as anything ever?")]
        public int OnlineResearchKeyFindingConfident { get; set; }
        public decimal Completed { get; set; }

    }
}
