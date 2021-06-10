using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Business_Model.Helper;

namespace BuildMyUnicorn.Business_Layer
{
    public class MarketResearchManager
    {
        public OurObservation GetObservation()
        {

            var query = $@"SELECT dbo.tbl_marketresearch_obervation.*  FROM  dbo.tbl_marketresearch_obervation WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<OurObservation>(query);
        }

        public _KeyFinding GetKeyFinding()
        {

            var query = $@"SELECT dbo.tbl_marketresearch_keyfinding.*  FROM  dbo.tbl_marketresearch_keyfinding WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_KeyFinding>(query);
        }

        public _OnlineResearch GetOnlineResearch()
        {

            var query = $@"SELECT dbo.tbl_marketresearch_onlineresearch.*  FROM  dbo.tbl_marketresearch_onlineresearch WHERE ClientID = '{new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}'";
            return SharedManager.GetSingle<_OnlineResearch>(query);
        }

        public IEnumerable<MarketKeyPlayer> GetMarketKeyPlayer(Guid OnlineResearchID)
        {
            var query = $@"SELECT dbo.tbl_onlineresearch_marketkeyplayer.*  FROM  dbo.tbl_onlineresearch_marketkeyplayer WHERE OnlineResearchID = '{OnlineResearchID}'";
            return SharedManager.GetList<MarketKeyPlayer>(query);


        }

        public string AddObservation(OurObservation Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ObervationID", ParamterValue = Model.ObervationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Observation", ParamterValue = Model.Observation, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Collection", ParamterValue = Model.Collection, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@AnyPatterns", ParamterValue = Model.AnyPatterns, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Patterns", ParamterValue = Model.Patterns, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@KeyMoments", ParamterValue = Model.KeyMoments, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_marketresearch_observation", parameters);
            if (result > 0) return "OK"; else return "Error in adding observation";

        }

        public string AddKeyFinding(KeyFinding Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@KeyFindingID", ParamterValue = Model.KeyFindingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InterviewKeyFinding", ParamterValue = Model.MarketResearchResults.InterviewKeyFinding, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InterviewKeyFindingConfident", ParamterValue = Model.MarketResearchResults.InterviewKeyFindingConfident, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ObservationKeyFinding", ParamterValue = Model.MarketResearchResults.ObservationKeyFinding, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ObservationKeyFindingConfident", ParamterValue = Model.MarketResearchResults.ObservationKeyFindingConfident, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SurveyKeyFinding", ParamterValue = Model.MarketResearchResults.SurveyKeyFinding , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SurveyKeyFindingConfident", ParamterValue = Model.MarketResearchResults.SurveyKeyFindingConfident, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OnlineResearchKeyFinding", ParamterValue = Model.MarketResearchResults.OnlineResearchKeyFinding , ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OnlineResearchKeyFindingConfident", ParamterValue = Model.MarketResearchResults.OnlineResearchKeyFindingConfident, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_marketresearch_keyfinding", parameters);
            if (result > 0) return "OK"; else return "Error in adding keyfinding";

        }

        public string AddOnlineResearch(OnlineResearch Model, List<MarketKeyPlayer> MarketKeyPlayerList)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
          //  Guid OnlineResearchID = Guid.NewGuid();
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@OnlineResearchID", ParamterValue = Model.OnlineResearchID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ResearchCarriedOutBit1", ParamterValue = Model.BigPictureResearch.ResearchCarriedOutBit1, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ResearchCarriedOutBit2", ParamterValue = Model.BigPictureResearch.ResearchCarriedOutBit2, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ResearchCarriedOutBit3", ParamterValue = Model.BigPictureResearch.ResearchCarriedOutBit3, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IndustryTrends", ParamterValue = Model.BigPictureResearch.IndustryTrends, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CaptureInitially", ParamterValue = Model.BigPictureResearch.CaptureInitially, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MarketSize", ParamterValue = Model.BigPictureResearch.MarketSize, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CaptureShare", ParamterValue = Model.BigPictureResearch.CaptureShare, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MarketShare", ParamterValue = Model.BigPictureResearch.MarketShare, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MarketShareCaptureDuration", ParamterValue = Model.BigPictureResearch.MarketShareCaptureDuration, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IdeaProgressed", ParamterValue = Model.FocussedResearch.IdeaProgressed, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomerFeedback", ParamterValue = Model.FocussedResearch.CustomerFeedback, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackReceived", ParamterValue = Model.FocussedResearch.FeedbackReceived, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackRate", ParamterValue = Model.FocussedResearch.FeedbackRate, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackKeyfinding", ParamterValue = Model.FocussedResearch.FeedbackKeyfinding, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_onlineresearch", parameters);

            if (result > 0)
            {
                MarketKeyPlayerList.ForEach(x => x.MarketKeyPlayerID = Guid.NewGuid());
                MarketKeyPlayerList.ForEach(x => x.OnlineResearchID = Model.OnlineResearchID);
                DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(MarketKeyPlayerList);
                obj.ExecuteBulkInsert("sp_add_marketkeyplayer_data", dtMarketKeyPlayer, "UT_MarketKey_Player_Data", "@DataTable");
            }
            if (result > 0) return "OK"; else return "Client Online research exists";
        }

        //public string UpdateOnlineResearch(OnlineResearch Model, List<MarketKeyPlayer> MarketKeyPlayerList)
        //{
        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@KeyFindingID", ParamterValue = Model.KeyFindingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Model.ClientID), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ResearchCarriedOutBit1", ParamterValue = Model.BigPictureResearch.ResearchCarriedOutBit1, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ResearchCarriedOutBit2", ParamterValue = Model.BigPictureResearch.ResearchCarriedOutBit2, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ResearchCarriedOutBit3", ParamterValue = Model.BigPictureResearch.ResearchCarriedOutBit3, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@IndustryTrends", ParamterValue = Model.BigPictureResearch.IndustryTrends, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@CaptureInitially", ParamterValue = Model.BigPictureResearch.CaptureInitially, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@MarketSize", ParamterValue = Model.BigPictureResearch.MarketSize, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@CaptureShare", ParamterValue = Model.BigPictureResearch.CaptureShare, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@MarketShare", ParamterValue = Model.BigPictureResearch.MarketShare, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@MarketShareCaptureDuration", ParamterValue = Model.BigPictureResearch.MarketShareCaptureDuration, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@IdeaProgressed", ParamterValue = Model.FocussedResearch.IdeaProgressed, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@CustomerFeedback", ParamterValue = Model.FocussedResearch.CustomerFeedback, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@FeedbackReceived", ParamterValue = Model.FocussedResearch.FeedbackReceived, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@FeedbackRate", ParamterValue = Model.FocussedResearch.FeedbackRate, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@FeedbackKeyfinding", ParamterValue = Model.FocussedResearch.FeedbackKeyfinding, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },

        //    };
        //    int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_keyfinding", parameters);
        //    if (result > 0)
        //    {
        //        MarketKeyPlayerList.ForEach(x => x.MarketKeyPlayerID = Guid.NewGuid());
        //        MarketKeyPlayerList.ForEach(x => x.KeyFindingID = Model.KeyFindingID);
        //        DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(MarketKeyPlayerList);
        //        obj.ExecuteBulkInsert("sp_add_client_keyplayer_data", dtMarketKeyPlayer, "UT_MarketKey_Player_Data", "@DataTable");
        //    }
        //    if (result > 0) return "OK"; else return "Client keyfinding Exists";

        //}

        public int ExistObervation(Guid id)
        {
            var query = $@"select count(ObervationID) from tbl_marketresearch_obervation WHERE ObervationID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(ObervationID) from tbl_marketresearch_obervation WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }

        public int ExistKeyfinding(Guid id)
        {
            var query = $@"select count(KeyFindingID) from tbl_marketresearch_keyfinding WHERE KeyFindingID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(KeyFindingID) from tbl_marketresearch_keyfinding WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }

        public int ExistOnlineResearch(Guid id)
        {
            var query = $@"select count(OnlineResearchID) from tbl_marketresearch_onlineresearch WHERE OnlineResearchID = '{id}'";
            if (id == Guid.Empty)
            {
                id = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
                query = $@"select count(OnlineResearchID) from tbl_marketresearch_onlineresearch WHERE ClientID = '{id}'";
            }
            return SharedManager.ExecuteScalar<int>(query);
        }
    }
}