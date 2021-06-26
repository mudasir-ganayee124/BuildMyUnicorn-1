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
    public class Master
    {
        public IEnumerable<MasterCommon> GetOptionMasterList(int Type)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@Type", ParamterValue = Type, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<MasterCommon>(CommandType.StoredProcedure, "sp_get_option_master_by_type", parameters);

        }

        public IEnumerable<Appendix> GetAppendexList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));

            return obj.GetList<Appendix>(CommandType.StoredProcedure, "sp_get_all_appendex", null);

        }



        public IEnumerable<_AppendixSearchLog> GetAppendexLogList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));

            return obj.GetList<_AppendixSearchLog>(CommandType.StoredProcedure, "sp_get_all_appendix_log", null);

        }
        public IEnumerable<Grants> GetGrantsList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));

            return obj.GetList<Grants>(CommandType.StoredProcedure, "sp_get_all_grants", null);

        }

        public IEnumerable<GrantLog> GetGrantLog()
        {
            var query = $@"SELECT tbl_grants.Name, tbl_grant_survival_budget.ModifiedDateTime, tbl_countries.CountryName,tbl_client.FirstName, tbl_client.LastName, tbl_client.StartupName,tbl_loan_calculator.AmountToBorrow, tbl_loan_calculator.YearsToRepay,tbl_loan_calculator.InterestRate  from tbl_grant_survival_budget INNER JOIN tbl_grants ON tbl_grants.GrantID = tbl_grant_survival_budget.GrantID INNER JOIN tbl_client ON tbl_client.ClientID = tbl_grant_survival_budget.ClientID LEFT JOIN tbl_countries ON tbl_grants.CountryID = tbl_countries.CountryID INNER JOIN tbl_loan_calculator ON tbl_loan_calculator.GrantID = tbl_grant_survival_budget.GrantID WHERE tbl_grant_survival_budget.GrantStatus = 2";
            return SharedManager.GetList<GrantLog>(query);
        }

        public IEnumerable<_EmailTemplates> GetAllEmailTemplates()
        {
            var query = $@"select * from tbl_email_templates";
            return SharedManager.GetList<_EmailTemplates>(query);
        }

        public _EmailTemplates GetSingleEmailTemplate(Guid EmailTemplateID)
        {
            var query = $@"select * from tbl_email_templates where EmailTemplateID = '{EmailTemplateID}'";
            return SharedManager.GetSingle<_EmailTemplates>(query);
        }
        public IEnumerable<ModuleCourse> GetModuleCourseList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<ModuleCourse>(CommandType.StoredProcedure, "sp_get_all_module_course", null);

        }
        public IEnumerable<Modules> GetModuleList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<Modules>(CommandType.StoredProcedure, "sp_get_all_module", null);

        }
        public IEnumerable<ModuleVideo> GetModuleVideoList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<ModuleVideo>(CommandType.StoredProcedure, "sp_get_all_module_video", null);

        }

        public IEnumerable<Country> GetCountryList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<Country>(CommandType.StoredProcedure, "sp_get_country_list", null);

        }

        public Option GetSingleOptionMaster(int Type, Guid ID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ID", ParamterValue = ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@Type", ParamterValue = Type, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<Option>(CommandType.StoredProcedure, "sp_get_single_option_master", parameters);

        }

        public Grants GetSingleGrant(Guid GrantID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@GrantID", ParamterValue = GrantID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }

            };
            return obj.GetSingle<Grants>(CommandType.StoredProcedure, "sp_get_single_grant", parameters);

        }
        public IEnumerable<Currency> GetCurrencyList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<Currency>(CommandType.StoredProcedure, "sp_get_all_currency", null);
        }

        public ModuleVideo GetSingleModuleVideo(Guid ModuleVideoID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleVideoID", ParamterValue = ModuleVideoID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            return obj.GetSingle<ModuleVideo>(CommandType.StoredProcedure, "sp_get_single_module_video", parameters);

        }

        public ModuleCourse GetSingleModuleCourse(string ModuleCourseID)
        {
            Guid objGuid = Guid.Empty;
            objGuid = Guid.Parse(ModuleCourseID);
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleCourseID", ParamterValue = objGuid, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = null, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = null, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }};
            IEnumerable<_ModuleCourse> list = obj.GetList<_ModuleCourse>(CommandType.StoredProcedure, "sp_get_single_module_course", parameters);
            ModuleCourse Model = new ModuleCourse();
            if (list.Count() > 0 && list != null)
            {
                Model.ModuleCourseID = list.FirstOrDefault().ModuleCourseID;
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

        public string AddNewOptionMaster(Option Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@Value", ParamterValue = Model.Value, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DisplayOrder", ParamterValue = Model.DisplayOrder, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = Model.Type, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_option_master", parameters);
            return result > 0 ? "OK" : Model.Type + " already exists";


        }

        public string AddNewGrant(Grants Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@Name", ParamterValue = Model.Name, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = Model.Type, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Description", ParamterValue = Model.Description, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Website", ParamterValue = Model.Website, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ProvidedBy", ParamterValue = Model.ProvidedBy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DueDate", ParamterValue = Model.DueDate, ParamterType = DbType.Date, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Model.SupplierID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@VideoUrl", ParamterValue = Model.VideoUrl, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_grant", parameters);
            return result > 0 ? "OK" : "Country Grant already exists";


        }
        public string AddModuleVideo(ModuleVideo Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@ModuleVideoID", ParamterValue = Model.ModuleVideoID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = Model.ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = Model.ModuleSectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Title", ParamterValue = Model.Title, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@VideoUrl", ParamterValue = Model.VideoUrl, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Duration", ParamterValue = Model.Duration, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_module_video", parameters);
            return result > 0 ? "OK" : "Error in Adding Module Video";


        }
        public string AddModuleCourse(ModuleCourse Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@ModuleCourseID", ParamterValue = Model.ModuleCourseID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = Model.ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = Model.ModuleSectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Title", ParamterValue = Model.Title, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@VideoUrl", ParamterValue = Model.VideoUrl, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Duration", ParamterValue = Model.Duration, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Description", ParamterValue = Model.Description, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_module_course", parameters);
            if (result > 0)
            {

                List<ModuleCourseOption> CourseOptionList = new List<ModuleCourseOption>();
                if (Model.ModuleCourseOption != null)
                {
                    foreach (var item in Model.ModuleCourseOption)
                    {
                        ModuleCourseOption objCourseOption = new ModuleCourseOption();
                        objCourseOption.MCOptionID = Guid.NewGuid();
                        objCourseOption.ModuleCourseID = Model.ModuleCourseID;
                        objCourseOption.Value = item.Value;
                        objCourseOption.DisplayOrder = item.DisplayOrder;
                        CourseOptionList.Add(objCourseOption);

                    }
                    DataTable dtCourseOption = Business_Model.Helper.Extensions.ListToDataTable(CourseOptionList);
                    obj.ExecuteBulkInsert("sp_add_module_course_option", dtCourseOption, "UT_ModuleCourse_Data", "@DataTable");
                }
                else
                {
                    var query = $@"Delete from tbl_module_course_option where ModuleCourseID = '{Model.ModuleCourseID}'";
                    SharedManager.ExecuteScalar<int>(query);
                }

            }
            return result > 0 ? "OK" : Model.ModuleName + " already exists";
        }

       
        public string UpdateOptionMaster(Option Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@ID", ParamterValue = Model.ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Value", ParamterValue = Model.Value, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DisplayOrder", ParamterValue = Model.DisplayOrder, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = Model.Type, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModifiedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_option_master", parameters);
            return result > 0 ? "OK" : Model.Type + " already exists";


        }
        public string UpdateGrant(Grants Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@GrantID", ParamterValue = Model.GrantID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Name", ParamterValue = Model.Name, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = Model.Type, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Description", ParamterValue = Model.Description, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Website", ParamterValue = Model.Website, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ProvidedBy", ParamterValue = Model.ProvidedBy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DueDate", ParamterValue = Model.DueDate, ParamterType = DbType.Date, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Model.SupplierID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@VideoUrl", ParamterValue = Model.VideoUrl, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModifiedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_grant", parameters);
            return result > 0 ? "OK" : "Country Grant already exists";


        }
        public string DeleteOptionMaster(Guid ID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@ID", ParamterValue = ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_option_master", parameters);
            return result > 0 ? "OK" : "Can not Delete ";


        }
        public string DeleteGrant(Guid GrantID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@GrantID", ParamterValue = GrantID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_grant", parameters);
            return result > 0 ? "OK" : "Can not Delete ";


        }
        public string DeleteModuleCourse(Guid ID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@ModuleCourseID", ParamterValue = ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_module_course", parameters);
            return result > 0 ? "OK" : "Can not Delete ";


        }

        public string DeleteModuleVideo(Guid ID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@ModuleVideoID", ParamterValue = ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_module_video", parameters);
            return result > 0 ? "OK" : "Can not Delete ";


        }
        public string DeleteAppendixLog(Guid AppdendixLogID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@AppdendixLogID", ParamterValue = AppdendixLogID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_appendix_log", parameters);
            return result > 0 ? "OK" : "Can not Delete ";


        }

        public static string ImportAppendix(HttpFileCollectionBase file)
        {


            try
            {
                int TotalExcelItems = 0;

                if (HttpContext.Current.Request.Files["file"].ContentLength > 0)

                {
                    string ExcelExtension = System.IO.Path.GetExtension(HttpContext.Current.Request.Files["file"].FileName);
                    string ExcelFilePath = HttpContext.Current.Server.MapPath("~/Content/Appendix/");

                    string[] RealFileName = HttpContext.Current.Request.Files["file"].FileName.Split('.');
                    Guid FileName = Guid.NewGuid();
                    ExcelFilePath = ExcelFilePath + FileName + "." + RealFileName[1];
                    HttpContext.Current.Request.Files["file"].SaveAs(ExcelFilePath);
                    DataTable ExcelData = new DataTable();
                    ExcelData = ProcessExcelFile(ExcelFilePath, ExcelExtension, "Yes");
                    TotalExcelItems = ExcelData.Rows.Count;
                    List<_Appendix> AppendixList = new List<_Appendix>();
                    foreach (DataRow row in ExcelData.Rows)
                    {
                        string Keyword = row["Word"].ToString().Trim();
                        string Category = row["Category"].ToString().Trim();
                        string Definition = row["Definition"].ToString().Trim();
                        AppendixList.Add(new _Appendix { AppendixID = Guid.NewGuid(), Keyword = Keyword, Category = Category, Definition = Definition });

                    }
                    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));

                    AppendixList.ForEach(x => x.CreatedBy = Guid.Parse(HttpContext.Current.User.Identity.Name));
                    DataTable dtMarketKeyPlayer = Business_Model.Helper.Extensions.ListToDataTable(AppendixList);
                    obj.ExecuteBulkInsert("sp_add_appendex_data", dtMarketKeyPlayer, "UT_Appendex_Data", "@DataTable");
                    return "OK";

                }
                return "Excel File Empty";
            }
            catch (Exception e)
            {
                //  Logger.WriteLog("Inventory Item Manager", "Error in Excel Import of Items" + e.Message.ToString(), "Information", Convert.ToInt16(HttpContext.Current.User.Identity.Name));
                return e.Message.ToString();
            }

        }

        public static DataTable ProcessExcelFile(string ExcelFilePath, string ExcelExtension, string isHDR)
        {
            try
            {
                string ConnectionString = "";

                switch (ExcelExtension)
                {
                    case ".xls":
                        ConnectionString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        ConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        ConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                ConnectionString = string.Format(ConnectionString, ExcelFilePath, isHDR);

                OleDbConnection ConnectionExcel = new OleDbConnection(ConnectionString);
                OleDbCommand CommandExcel = new OleDbCommand();
                OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();
                DataTable DataTableExcel = new DataTable();

                CommandExcel.Connection = ConnectionExcel;
                ConnectionExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = ConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                ConnectionExcel.Close();

                ConnectionExcel.Open();
                CommandExcel.CommandText = "SELECT * From [" + SheetName + "]";
                OleDbAdapter.SelectCommand = CommandExcel;
                OleDbAdapter.Fill(DataTableExcel);
                ConnectionExcel.Close();
                DataTableExcel = DataTableExcel.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                return DataTableExcel;
            }
            catch (Exception ex)
            {
                // Logger.WriteLog("Error in Proceesing Excel File", "Error Reason : " + ex.ToString(), "Critical", Convert.ToInt16(HttpContext.Current.User.Identity.Name));
                Console.Write(ex.Message);
                DataTable DataTableExcel = new DataTable();
                return DataTableExcel;
            }
        }

    }
}