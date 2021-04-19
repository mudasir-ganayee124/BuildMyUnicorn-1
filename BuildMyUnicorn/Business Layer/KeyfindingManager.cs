using ALMS_DAL;
using Business_Model.Model;
using Business_Model.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace BuildMyUnicorn.Business_Layer
{
    public class KeyfindingManager
    {
        public _KeyFinding GetKeyFinding()
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<_KeyFinding>(CommandType.StoredProcedure, "sp_get_client_keyfinding", parameters);

        }

        public IEnumerable<MarketKeyPlayer> GetMarketKeyPlayer(Guid KeyFindingID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "KeyFindingID", ParamterValue = KeyFindingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<MarketKeyPlayer>(CommandType.StoredProcedure, "sp_get_market_keyplayer", parameters);

        }

        public string AddNewKeyFinding(KeyFinding Model, List<MarketKeyPlayer> MarketKeyPlayerList)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            Guid KeyFindingID = Guid.NewGuid();
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@KeyFindingID", ParamterValue = KeyFindingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
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
                new ParametersCollection { ParamterName = "@CustomerFeedback", ParamterValue = Model.FocussedResearch.CustomerFeedback, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackReceived", ParamterValue = Model.FocussedResearch.FeedbackReceived, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackRate", ParamterValue = Model.FocussedResearch.FeedbackRate, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackKeyfinding", ParamterValue = Model.FocussedResearch.FeedbackKeyfinding, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
               
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_keyfinding", parameters);

            if (result > 0)
            {
                MarketKeyPlayerList.ForEach(x => x.MarketKeyPlayerID = Guid.NewGuid());
                MarketKeyPlayerList.ForEach(x => x.KeyFindingID = KeyFindingID);
                DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(MarketKeyPlayerList);
                obj.ExecuteBulkInsert("sp_add_client_keyplayer_data", dtMarketKeyPlayer, "UT_MarketKey_Player_Data", "@DataTable");
            }
            if (result > 0) return "OK"; else return "Client keyfinding Exists";
        }
        
        public string UpdateKeyFinding(KeyFinding Model, List<MarketKeyPlayer> MarketKeyPlayerList)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@KeyFindingID", ParamterValue = Model.KeyFindingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Model.ClientID), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
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
                new ParametersCollection { ParamterName = "@CustomerFeedback", ParamterValue = Model.FocussedResearch.CustomerFeedback, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackReceived", ParamterValue = Model.FocussedResearch.FeedbackReceived, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackRate", ParamterValue = Model.FocussedResearch.FeedbackRate, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedbackKeyfinding", ParamterValue = Model.FocussedResearch.FeedbackKeyfinding, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_keyfinding", parameters);
            if (result > 0)
            {
                MarketKeyPlayerList.ForEach(x => x.MarketKeyPlayerID = Guid.NewGuid());
                MarketKeyPlayerList.ForEach(x => x.KeyFindingID = Model.KeyFindingID);
                DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(MarketKeyPlayerList);
                obj.ExecuteBulkInsert("sp_add_client_keyplayer_data", dtMarketKeyPlayer, "UT_MarketKey_Player_Data", "@DataTable");
            }
            if (result > 0) return "OK"; else return "Client keyfinding Exists";
      
        }
    }
}