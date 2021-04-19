using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Administration.Business_Layer
{
    public class ClientManager
    {
        public IEnumerable<Client> GetAllClient()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<Client>(CommandType.StoredProcedure, "sp_get_all_client", null);
        }

        public Client GetSingleClient(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_id", parameters);

        }


        public string Delete(Guid EntityID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = EntityID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityType", ParamterValue = _EntityType.Client, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_account", parameters);
            return result > 0 ? "OK" : "Can not delete ";


        }

        public string UpdateStatus(Guid ClientID, bool Status)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@Status", ParamterValue = Status, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = 2, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_account_status", parameters);
            return result > 0 ? "OK" : "Can not update status ";


        }
    }
}