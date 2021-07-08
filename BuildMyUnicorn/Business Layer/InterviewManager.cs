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
    public class InterviewManager
    {
        public IEnumerable<Interview> GetAllInterview()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<Interview>(CommandType.StoredProcedure, "sp_get_all_interview", parameters);
        }

        public Interview GetInterview(Guid InterviewID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@InterviewID", ParamterValue = InterviewID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Interview>(CommandType.StoredProcedure, "sp_get_interview_by_id", parameters);
        }

        public IEnumerable<InterviewData> GetInterviewData(Guid InterviewID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@InterviewID", ParamterValue = InterviewID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<InterviewData>(CommandType.StoredProcedure, "sp_get_interview_data", parameters);
        }

        public string AddInterview(Interview Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@InterviewID", ParamterValue =  Model.InterviewID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue =  new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Title", ParamterValue = Model.Title, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Form", ParamterValue = Model.Form, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_interview", parameters);
            return result > 0 ? "OK" : "Error in Adding Interview";


        }

        public void AddInterviewData(List<InterviewData> ModelList, Guid InterviewID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            ModelList.ForEach(x => x.InterviewDataID = new Guid());
            ModelList.ForEach(x => x.InterviewID = InterviewID);
            ModelList.ForEach(x => x.CreatedDateTime = DateTime.Now);
            DataTable dtInterviewData = Extensions.ListToDataTable(ModelList);
            obj.ExecuteBulkInsert("sp_add_interview_data", dtInterviewData, "UT_Interview_Data", "@DataTable");
        }

        public string DeleteInterview(Guid InterviewID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@InterviewID", ParamterValue = InterviewID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_interview", parameters);
            return result > 0 ? "OK" : "Error in Delete";
        }

    }
}