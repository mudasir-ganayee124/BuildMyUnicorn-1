using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Xml.Linq;
using Business_Model.Helper;

namespace Administration.Business_Layer
{
    public class QuestionVideoManager
    {
        public IEnumerable<QuestionVideo> GetAllModules()
        {
            var query = $@"SELECT distinct(tbl_question_video.ModuleID),tbl_question_video.ModuleID,tbl_module_section.ModuleSectionID, tbl_module.DisplayAs AS ModuleName, tbl_module_section.DisplayAs As SectionName
                            FROM tbl_question_video INNER JOIN tbl_module_section ON tbl_module_section.ModuleSectionID = tbl_question_video.SectionID INNER JOIN tbl_module ON tbl_module.ModuleID = tbl_question_video.ModuleID ";
            return SharedManager.GetList<QuestionVideo>(query);
        }

        public IEnumerable<QuestionVideo> GetModuleQustionList(int ModuleID, int ModuleSectionID)
        {
            var query = $"SELECT *  FROM tbl_question_video  WHERE ModuleID = '{ModuleID}' AND SectionID  = '{ModuleSectionID}'";
            return SharedManager.GetList<QuestionVideo>(query);
        }

        public string UpdateQuestionVideo(Guid QuestionVideoID, string VideoUrl)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@QuestionVideoID", ParamterValue = QuestionVideoID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@VideoUrl", ParamterValue = VideoUrl, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },

           //  new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_question_video", parameters);
            return result > 0 ? "OK" : "Failed to add";


        }

    }
}