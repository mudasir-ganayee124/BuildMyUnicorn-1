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
    public class LanguageManager
    {
        public IEnumerable<Language> GetLanguageList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));         
            return obj.GetList<Language>(CommandType.StoredProcedure, "sp_get_all_language", null);

        }

        public IEnumerable<LanguageModule> GetLanguageModuleList(Guid LanguageID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@LanguageID", ParamterValue = LanguageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<LanguageModule>(CommandType.StoredProcedure, "sp_get_all_language_module", parameters);

        }

        public IEnumerable<LanguageModule> GetLanguageModuleQustionList(Guid LanguageID, int ModuleID, int ModuleSectionID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@LanguageID", ParamterValue = LanguageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = ModuleSectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<LanguageModule>(CommandType.StoredProcedure, "sp_get_all_language_module_questions", parameters);

        }

        public Language GetSingleLanguage(Guid LanguageID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@LanguageID", ParamterValue = LanguageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<Language>(CommandType.StoredProcedure, "sp_get_language_by_id", parameters);

        }


        public string AddLanguage(Language Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@LanguageID", ParamterValue = Model.LanguageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Name", ParamterValue = Model.Name, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@IsDefault", ParamterValue = Model.IsDefault, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_language", parameters);
            return result > 0 ? "OK" : "Language already exists";


        }
        public string UpdateModuleQuestion(Guid LanguageModuleID, string Text, int Type)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@LanguageModuleID", ParamterValue = LanguageModuleID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Text", ParamterValue = Text, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = Type, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
           //  new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_language_module", parameters);
            return result > 0 ? "OK" : "Language already exists";


        }

    }
}