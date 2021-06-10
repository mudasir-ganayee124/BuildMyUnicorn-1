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
        //public _KeyFinding GetKeyFinding()
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    return obj.GetSingle<_KeyFinding>(CommandType.StoredProcedure, "sp_get_client_keyfinding", parameters);

        //}

        //public IEnumerable<MarketKeyPlayer> GetMarketKeyPlayer(Guid KeyFindingID)
        //{

        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "KeyFindingID", ParamterValue = KeyFindingID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
        //    };
        //    return obj.GetList<MarketKeyPlayer>(CommandType.StoredProcedure, "sp_get_market_keyplayer", parameters);

        //}

       
        
       
    }
}