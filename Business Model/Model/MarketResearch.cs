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
        public Guid AnyPatterns { get; set; }
        [Description("What are the Patterns?")]
        public string Patterns { get; set; }
        [Description("Key characteristics in their Behaviour?")]
        public string KeyMoments { get; set; }
        public decimal Completed { get; set; }
    }

    public class KeyFinding
    {
        public Guid KeyFindingID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public MarketResearchResults MarketResearchResults { get; set; }

    }

    [Description("KeyFinding")]
    public class MarketResearchResults
    {
        [Description("Your key findings from the Interview?")]
        public string InterviewKeyFinding { get; set; }
        [Description("After the interviews, how confident  are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public Guid InterviewKeyFindingConfident { get; set; }
        [Description("Your key findings from the observation?")]
        public string ObservationKeyFinding { get; set; }
        [Description("After the observation, how confident  are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public Guid ObservationKeyFindingConfident { get; set; }
        [Description("Your key findings from the survey?")]
        public string SurveyKeyFinding { get; set; }
        [Description("After the Surveys, how confident  are you in your start-up 1 been pack it in and 10 been 'never been as sure as anything ever?")]
        public Guid SurveyKeyFindingConfident { get; set; }
        [Description("Your key findings from the online research?")]
        public string OnlineResearchKeyFinding { get; set; }
        [Description("After the online research, how confident are you in your product 1 been pack it in and 10 been never been as sure as anything ever?")]
        public Guid OnlineResearchKeyFindingConfident { get; set; }
        public decimal Completed { get; set; }
    }
    public class _KeyFinding
    {
        public Guid KeyFindingID { get; set; }
        public Guid ClientID { get; set; }
        public EntityState EntityState { get; set; }
        public MarketResearchResults MarketResearchResults { get; set; }
        [Description("Your key findings for Interview?")]
        public string InterviewKeyFinding { get; set; }
        [Description("After the interviews, how confident  are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public Guid InterviewKeyFindingConfident { get; set; }
        [Description("Your key findings from the observation?")]
        public string ObservationKeyFinding { get; set; }
        [Description("After the observation, how confident are you in your start-up 1 been 'pack it in' and 10 been never been as sure as anything ever?")]
        public Guid ObservationKeyFindingConfident { get; set; }
        [Description("Your key findings from the survey?")]
        public string SurveyKeyFinding { get; set; }
        [Description("After the Surveys, how confident are you in your start-up 1 been pack it in and 10 been 'never been as sure as anything ever?")]
        public Guid SurveyKeyFindingConfident { get; set; }
        [Description("Your key findings from the online research?")]
        public string OnlineResearchKeyFinding { get; set; }
        [Description("After the online research, how confident are you in your product 1 been pack it in and 10 been never been as sure as anything ever?")]
        public Guid OnlineResearchKeyFindingConfident { get; set; }
        public decimal Completed { get; set; }

    }
    public class OnlineResearch
    {
        public Guid OnlineResearchID { get; set; }
        public Guid ClientID { get; set; }
        public decimal ProgressValue { get; set; }
        public EntityState EntityState { get; set; }
        public BigPictureResearch BigPictureResearch { get; set; }
        public FocussedResearch FocussedResearch { get; set; }
    }

    public class BigPictureResearch
    {
        [Description("Bit-1 :")]
        public string ResearchCarriedOutBit1 { get; set; }
        [Description("Bit-2 :")]
        public string ResearchCarriedOutBit2 { get; set; }
        [Description("Bit-3 :")]
        public string ResearchCarriedOutBit3 { get; set; }
        [Description("Are there any trends in the industry?")]
        public string IndustryTrends { get; set; }
        [Description("What is the market you will initially try to capture?")]
        public string CaptureInitially { get; set; }
        [Description("What is the size/value of the market?")]
        public decimal MarketSize { get; set; }
        [Description("What market share will you try to capture?")]
        public decimal CaptureShare { get; set; }
        [Description("Your Market Share?")]
        public decimal MarketShare { get; set; }
        [Description("Roughly how long before you reach your market share?")]
        public int MarketShareCaptureDuration { get; set; }
        [Description("Who are the Key players in the market?")]
        public Guid MarketKeyPlayerID { get; set; }
        public decimal Completed { get; set; }

    }

    public class FocussedResearch
    {
        [Description("At what stage is your idea?")]
        public string IdeaProgressed { get; set; }
        [Description("Have you talked to potential customers for feedback?")]
        public Guid CustomerFeedback { get; set; }
        [Description("How did you receive the feedback?")]
        public string FeedbackReceived { get; set; }
        [Description("Rate the over all feed back?")]
        public Guid FeedbackRate { get; set; }
        [Description("Key findings from Feedback?")]
        public string FeedbackKeyfinding { get; set; }
        public decimal Completed { get; set; }

    }

    public class _OnlineResearch
    {
        public Guid OnlineResearchID { get; set; }
        public Guid ClientID { get; set; }
        public decimal ProgressValue { get; set; }
        [Description("Bit-1 :")]
        public string ResearchCarriedOutBit1 { get; set; }
        [Description("Bit-2 :")]
        public string ResearchCarriedOutBit2 { get; set; }
        [Description("Bit-3 :")]
        public string ResearchCarriedOutBit3 { get; set; }
        [Description("Are there any trends in the industry?")]
        public string IndustryTrends { get; set; }
        [Description("What is the market you will initially try to capture?")]
        public string CaptureInitially { get; set; }
        [Description("What is the size/value of the market?")]
        public decimal MarketSize { get; set; }
        [Description("What market share will you try to capture?")]
        public decimal CaptureShare { get; set; }
        [Description("Your Market Share?")]
        public decimal MarketShare { get; set; }
        [Description("Roughly how long before you reach your market share?")]
        public int MarketShareCaptureDuration { get; set; }
        [Description("Who are the Key players in the market?")]
        public Guid MarketKeyPlayerID { get; set; }
        [Description("At what stage is your idea?")]
        public string IdeaProgressed { get; set; }
        [Description("Have you talked to potential customers for feedback?")]
        public Guid CustomerFeedback { get; set; }
        [Description("How did you receive the feedback?")]
        public string FeedbackReceived { get; set; }
        [Description("Rate the over all feed back?")]
        public Guid FeedbackRate { get; set; }
        [Description("Key findings from Feedback?")]
        public string FeedbackKeyfinding { get; set; }
    }
}
