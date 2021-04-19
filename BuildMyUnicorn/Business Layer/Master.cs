using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ALMS_DAL;
using Business_Model.Model;
using System.Linq;

using Business_Model.Helper;
namespace BuildMyUnicorn.Business_Layer
{
    public class Master
    {
        public void AddMultipleMaster(int ModuleID, Guid EntityID, List<MultipleMaster> Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            Model.ForEach(x => x.ID = Guid.NewGuid());
            Model.ForEach(x => x.ModuleID = ModuleID);
            Model.ForEach(x => x.EntityID = new ClientManager().GetMainClientID(EntityID));
            DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(Model);
            obj.ExecuteBulkInsert("sp_add_multiple_master_data", dtMarketKeyPlayer, "UT_MultipleMaster_Data", "@DataTable");
        }

        public IEnumerable<LanguageModule> GetDefaultModuleLanguage(int ModuleID , int SectionID )
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
              new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@SectionID", ParamterValue = SectionID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<LanguageModule>(CommandType.StoredProcedure, "sp_get_default_language_by_module", parameters);

        }

        public IEnumerable<MasterCommon> GetOptionMasterList(int Type)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@Type", ParamterValue = Type, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<MasterCommon>(CommandType.StoredProcedure, "sp_get_option_master_by_type", parameters);

        }
        public ModuleVideo GetSingleModuleVideo(int ModuleVideoID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleVideoID", ParamterValue = ModuleVideoID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }};
            return obj.GetSingle<ModuleVideo>(CommandType.StoredProcedure, "sp_get_single_module_video", parameters);

        }
        public ModuleVideo GetSectionModuleVideo(int ModuleID, int ModuleSectionID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = ModuleSectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }};
        
            return obj.GetSingle<ModuleVideo>(CommandType.StoredProcedure, "sp_get_single_section_module_video", parameters);

        }
        public ModuleCourse GetSingleModuleCourse(int ModuleID , int ModuleSectionID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleCourseID", ParamterValue = Guid.Empty, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = ModuleSectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }};
            IEnumerable<_ModuleCourse> list = obj.GetList<_ModuleCourse>(CommandType.StoredProcedure, "sp_get_single_module_course", parameters);
            ModuleCourse Model = new ModuleCourse();
            if (!(list == null ? true : (!list.Any())))
            {
                Model.ModuleCourseID = list.FirstOrDefault().ModuleCourseID;
                //Model.ModuleName = list.FirstOrDefault().ModuleName;
                Model.ModuleID = list.FirstOrDefault().ModuleID;
                Model.ModuleSectionID = list.FirstOrDefault().ModuleSectionID;
                Model.Title = list.FirstOrDefault().Title;
                Model.Duration = list.FirstOrDefault().Duration;
                Model.Description = list.FirstOrDefault().Description;
                Model.VideoUrl = list.FirstOrDefault().VideoUrl;
                List<ModuleCourseOption> _list = new List<ModuleCourseOption>();
                foreach (var item in list)
                {
                    ModuleCourseOption _obj = new ModuleCourseOption();
                    _obj.MCOptionID = item.MCOptionID;
                    _obj.ModuleCourseID = item.ModuleCourseID;
                    _obj.DisplayOrder = item.DisplayOrder;
                    _obj.Value = item.Value;
                    _list.Add(_obj);
                }
                Model.ModuleCourseOption = _list;
            }
            return Model;

        }

        public IEnumerable<Supplier> GetBusinessSupplierList(int ModuleID, int ModuleSectionID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = ModuleSectionID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<Supplier>(CommandType.StoredProcedure, "sp_get_module_section_business_supplier", parameters);

        }

        public IEnumerable<MultipleMaster> GetSingleMultipleMaster(int ModuleID , Guid EntityID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@EntityID", ParamterValue = EntityID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            return obj.GetList<MultipleMaster>(CommandType.StoredProcedure, "sp_get_single_multiple_master", parameters);

        }
        public void DeleteMultipleMaster(int ModuleID, Guid EntityID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@EntityID", ParamterValue =  EntityID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            };
            obj.Execute(CommandType.StoredProcedure, "sp_delete_multiple_master", parameters);

        }

    }
}