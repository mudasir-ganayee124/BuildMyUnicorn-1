using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ALMS_DAL;
using Business_Model.Model;
using System.Linq;
using Business_Model.Helper;
using System.Web;

namespace BuildMyUnicorn.Business_Layer
{
    public class Master
    {

        public ModuleCourselog AddModuleCourselog(ModuleCourselog Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
              new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = Model.ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@SectionID", ParamterValue = Model.ModuleSectionID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@Status", ParamterValue = Model.Status, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<ModuleCourselog>(CommandType.StoredProcedure, "add_module_course_log", parameters);

        }

        public void AddMultipleMaster(int ModuleID, Guid EntityID, List<MultipleMaster> Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            Model.ForEach(x => x.ID = Guid.NewGuid());
            Model.ForEach(x => x.ModuleID = ModuleID);
            Model.ForEach(x => x.EntityID = new ClientManager().GetMainClientID(EntityID));
            DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(Model);
            obj.ExecuteBulkInsert("sp_add_multiple_master_data", dtMarketKeyPlayer, "UT_MultipleMaster_Data", "@DataTable");
        }
        public void AddProductServiceMultipleMaster(int ModuleID, Guid EntityID, List<MultipleMaster> Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            Model.ForEach(x => x.ID = Guid.NewGuid());
            Model.ForEach(x => x.ModuleID = ModuleID);
            Model.ForEach(x => x.EntityID = EntityID);
            DataTable dtMarketKeyPlayer = Extensions.ListToDataTable(Model);
            obj.ExecuteBulkInsert("sp_add_multiple_master_data", dtMarketKeyPlayer, "UT_MultipleMaster_Data", "@DataTable");
        }

        public IEnumerable<LanguageModule> GetDefaultModuleLanguage(int ModuleID, int SectionID)
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
            return obj.GetList<MasterCommon>(CommandType.StoredProcedure, "sp_get_option_master_by_type", parameters).OrderBy(x => x.DisplayOrder);

        }
        public ModuleVideo GetSingleModuleVideo(int ModuleVideoID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleVideoID", ParamterValue = ModuleVideoID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }};
            return obj.GetSingle<ModuleVideo>(CommandType.StoredProcedure, "sp_get_single_module_video", parameters);

        }

        public RateInfo GetRateInfo(int ModuleID, int SectionID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
              new ParametersCollection { ParamterName = "@ClientID", ParamterValue = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name)), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
              new ParametersCollection { ParamterName = "@SectionID", ParamterValue = SectionID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<RateInfo>(CommandType.StoredProcedure, "sp_get_rate_info", parameters);

        }
        public ModuleVideo GetSectionModuleVideo(int ModuleID, int ModuleSectionID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ModuleID", ParamterValue = ModuleID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleSectionID", ParamterValue = ModuleSectionID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }};

            return obj.GetSingle<ModuleVideo>(CommandType.StoredProcedure, "sp_get_single_section_module_video", parameters);

        }

        public Template GetTemplate(int Type)
        {
            var query = $@"SELECT tbl_template.*FROM tbl_template WHERE TemplateType = {Type} AND IsActive = 1 AND IsDeleted = 0";
            return SharedManager.GetSingle<Template>(query);
        }

        public int ExistModuleCourse(int ModuleID, int ModuleSectionID)
        {
            var query = $@"SELECT count(ModuleCourseID) from tbl_module_course where ModuleID  = '{ModuleID}'  and ModuleSectionID = '{ModuleSectionID}' ";
            return SharedManager.ExecuteScalar<int>(query);
        }

        public ModuleCourse GetSingleModuleCourse(int ModuleID, int ModuleSectionID)
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

        public IEnumerable<MultipleMaster> GetSingleMultipleMaster(int ModuleID, Guid EntityID)
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

        public decimal GetModuleTotalProgress(Module module)
        {
            switch (module)
            {
                case Module.MyIdea:
                    return TotalProgressIdea();
                case Module.MarketResearch:
                    return TotalProgressMarketResearch();
                case Module.TheBusiness:
                    return TotalProgressBusiness();
                case Module.Selling:
                    return TotalProgressSelling();
                case Module.Finance:
                    return TotalProgressFinance();
                case Module.Marketing:
                    return TotalProgressMarketing();
                default:
                    return 0.0m;
            }

        }

        public decimal TotalProgressIdea()
        {
            decimal ProgressValue = 0.0m;
            int ModuleTotalQuestions = 37;
            int ModuleUnansweredQuestions = 37;
            int TextTypeQuestions = 0;
            int _TextTypeQuestions = 25;
            int CheckboxTypeQuestions = 0;
            int _GuidSelectTypeQuestions = 12;
            int GuidSelectTypeQuestions = 0;
            int MultipleSelectTypeQuestions = 0;
            int ModuleModelCount = 1;
            decimal ProgressUnit = 2.70m;
            var ModelIdea = new IdeaManager().GetIdea();
            string[] ignoretypeofstring = { "DomainName", "CompanyName" };
            string[] ignoretypeofguid = { "IdeaID", "CompanyName" };
            if (ModelIdea != null)
            {

                foreach (System.Reflection.PropertyInfo pi in ModelIdea.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelIdea);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelIdea);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    // return Math.Round(_progress);
                }


            }
            return Math.Round(ProgressValue);
        }

        public decimal TotalProgressMarketResearch()
        {
            decimal ProgressValue = 0.0m;
            int ModuleTotalQuestions = 26;
            int ModuleUnansweredQuestions = 26;
            int TextTypeQuestions = 0;
            int _TextTypeQuestions = 13;
            int CheckboxTypeQuestions = 0;
            int _GuidSelectTypeQuestions = 8;
            int GuidSelectTypeQuestions = 0;
            int NumberTypeQuestions = 0;
            int _NumberTypeQuestions = 4;
            int MultipleRowTypeQuestions = 0;
            int _MultipleRowTypeQuestions = 1;
            int MultipleSelectTypeQuestions = 0;
            int ModuleModelCount = 3;
            decimal ProgressUnit = 3.70m;


            var ModelObservation = new MarketResearchManager().GetObservation();
            var ModelKeyfinding = new MarketResearchManager().GetKeyFinding();
            var ModelOnlineResearch = new MarketResearchManager().GetOnlineResearch();
            string[] ignoretypeofstring = { "Patterns" };
            string[] ignoretypeofguid = { "ObervationID", "ClientID", "KeyFindingID", "OnlineResearchID" };
            if (ModelObservation != null)
                foreach (System.Reflection.PropertyInfo pi in ModelObservation.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelObservation);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelObservation);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    // return Math.Round(_progress);
                }

            if (ModelKeyfinding != null)
                foreach (System.Reflection.PropertyInfo pi in ModelKeyfinding.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelKeyfinding);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelKeyfinding);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    // return Math.Round(_progress);
                }

            if (ModelOnlineResearch != null)
            {
                foreach (System.Reflection.PropertyInfo pi in ModelOnlineResearch.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelOnlineResearch);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelOnlineResearch);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                NumberTypeQuestions += NumberTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(int))
                    {
                        int value = (int)pi.GetValue(ModelOnlineResearch);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != 0)
                            {
                                _NumberTypeQuestions += _NumberTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(decimal))
                    {
                        decimal value = (decimal)pi.GetValue(ModelOnlineResearch);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != 0.0m)
                            {
                                _NumberTypeQuestions += _NumberTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    // retu
                    // return Math.Round(_progress);
                }
                var MarketKeyPlayer = new MarketResearchManager().GetMarketKeyPlayer(ModelOnlineResearch.OnlineResearchID).ToList();
                if (MarketKeyPlayer.Count() > 0)
                {
                    MultipleRowTypeQuestions += MultipleRowTypeQuestions;
                    ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                    ProgressValue += ProgressUnit;
                }
            }
            return Math.Round(ProgressValue);
        }

        public decimal TotalProgressBusiness()
        {
            decimal ProgressValue = 0.0m;
            int ModuleTotalQuestions = 47;
            int ModuleUnansweredQuestions = 47;
            int TextTypeQuestions = 0;
            int _TextTypeQuestions = 25;
            int CheckboxTypeQuestions = 0;
            int _GuidSelectTypeQuestions = 12;
            int MultipleRowTypeQuestions = 0;
            int _MultipleRowTypeQuestions = 1;
            int GuidSelectTypeQuestions = 0;
            int MultipleSelectTypeQuestions = 0;
            int ModuleModelCount = 5;
            decimal ProgressUnit = 2.12m;
            var ModelBusinessOverview = new BusinessManager().GetBusinessOverview();
            var ModelProductService = new BusinessManager().GetProductService();
            var ModelCompetitorAnalysis = new BusinessManager().GetCompetitorAnalysis();
            var ModelSWOT = new BusinessManager().GetCompetitorSWOT();
            var ModelBusinessOperation = new BusinessManager().GetBusinessOperation();

            string[] ignoretypeofstring = { "LandlordCostStatus", "LicenseType", "QualificationType", "InsuranceType", "Needsoftware", "OperationResponsible", "QualityControlMethod" };
            string[] ignoretypeofguid = { "BusinessOverID", "ProductServiceID", "BusinessOperationID", "ClientID", "StaffWorkID", "ThirdPartyInvovedID", "SWOTID" };
            if (ModelBusinessOverview != null)
                foreach (System.Reflection.PropertyInfo pi in ModelBusinessOverview.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelBusinessOverview);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelBusinessOverview);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    // return Math.Round(_progress);
                }

            if (ModelProductService != null)
                foreach (System.Reflection.PropertyInfo pi in ModelProductService.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelProductService);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelProductService);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    // return Math.Round(_progress);
                }

            if (ModelBusinessOperation != null)
                foreach (System.Reflection.PropertyInfo pi in ModelBusinessOperation.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelBusinessOperation);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelBusinessOperation);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(int?))
                    {
                        int? value = (int?)pi.GetValue(ModelBusinessOperation);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != null)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }

                    // return Math.Round(_progress);
                }
            if (ModelBusinessOperation != null)
            {
                var OperationPartyList = new Master().GetSingleMultipleMaster((int)ModuleName.BusinessOperation, ModelBusinessOperation.BusinessOperationID);
                if (OperationPartyList.Count() > 0)
                {
                    MultipleRowTypeQuestions += MultipleRowTypeQuestions;
                    ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                    ProgressValue += ProgressUnit;
                }
            }

            if (ModelCompetitorAnalysis.Count() > 0)
            {
                MultipleRowTypeQuestions += MultipleRowTypeQuestions;
                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                ProgressValue += ProgressUnit;
            }
            if (ModelSWOT != null)
                foreach (System.Reflection.PropertyInfo pi in ModelSWOT.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelSWOT);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                }

                    return Math.Round(ProgressValue);
        }

        public decimal TotalProgressSelling()
        {

            decimal ProgressValue = 0.0m;
            int ModuleTotalQuestions = 24;
            int ModuleUnansweredQuestions = 24;
            int TextTypeQuestions = 0;
            int _TextTypeQuestions = 0;
            int CheckboxTypeQuestions = 0;
            int _GuidSelectTypeQuestions = 12;
            int GuidSelectTypeQuestions = 0;
            int MultipleSelectTypeQuestions = 0;
            int MultipleRowTypeQuestions = 0;
            int _MultipleRowTypeQuestions = 1;
            int ModuleModelCount = 2;
            decimal ProgressUnit = 4.16m;
            var ModelCustomer = new SellingManager().GetCustomers();
            var ModelProductService = new SellingManager().GetPricingProductService();
            string[] ignoretypeofstring = { "DomainName", "CompanyName" };
            string[] ignoretypeofguid = { "CustomerID", "ClientID", " ProductServicePricingID" };
            if (ModelCustomer != null)
                foreach (System.Reflection.PropertyInfo pi in ModelCustomer.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelCustomer);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }


                }

            if (ModelCustomer != null)
            {
                var CustomerBuyerPersona = new SellingManager().GetCustomerbuyerPersona(ModelCustomer.CustomerID);
                if (CustomerBuyerPersona.Count() > 0)
                {
                    MultipleRowTypeQuestions += MultipleRowTypeQuestions;
                    ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                    ProgressValue += ProgressUnit;
                }
            }

            if (ModelProductService != null)
                foreach (System.Reflection.PropertyInfo pi in ModelProductService.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelProductService);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }


                }

            return Math.Round(ProgressValue);


        }

        public decimal TotalProgressFinance()
        {

            decimal ProgressValue = 0.0m;
            int ModuleTotalQuestions = 66;
            int ModuleUnansweredQuestions = 66;
            int TextTypeQuestions = 0;
            int _TextTypeQuestions = 0;
            int CheckboxTypeQuestions = 0;
            int _GuidSelectTypeQuestions = 12;
            int GuidSelectTypeQuestions = 0;
            int MultipleSelectTypeQuestions = 0;
            int MultipleRowTypeQuestions = 0;
            int _MultipleRowTypeQuestions = 0;
            int ModuleModelCount = 1;
            decimal ProgressUnit = 1.51m;
            var  ModelInvestor = new FinanceManager().GetFinanceInvestor();
        
            string[] ignoretypeofstring = {};
            string[] ignoretypeofguid = { "InvestorID", "ClientID"};
            if (ModelInvestor != null)
                foreach (System.Reflection.PropertyInfo pi in ModelInvestor.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelInvestor);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }


                }

            return Math.Round(ProgressValue);


        }

        public Decimal TotalProgressMarketing()
        {
            decimal ProgressValue = 0.0m;
            int ModuleTotalQuestions = 31;
            int ModuleUnansweredQuestions = 31;
            int TextTypeQuestions = 0;
            int _TextTypeQuestions = 20;
            int CheckboxTypeQuestions = 0;
            int _GuidSelectTypeQuestions = 0;
            int GuidSelectTypeQuestions = 0;
            int MultipleSelectTypeQuestions = 0;
            int ModuleModelCount = 3;
            decimal ProgressUnit = 3.22m;
            var ModelOnlinePresence = new MarketingManager().GetOnlinePresance();
            IEnumerable<_Marketing> ModelMarketingPlanList = new MarketingManager().GetMarketingPlan();
            var ModelMarketingBrand = new MarketingManager().GetMarketingBrand();
            string[] ignoretypeofstring = { "DomainName" };
            string[] ignoretypeofguid = { "OnlinePresenceID", "ClientID", "MarketingBrandID", "PowerPresentationID" };
            if (ModelOnlinePresence != null)
              foreach (System.Reflection.PropertyInfo pi in ModelOnlinePresence.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelOnlinePresence);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelOnlinePresence);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                   
                }
            if (ModelOnlinePresence != null)
            {
                var FunctionNecessary = new Master().GetOptionMasterList((int)OptionType.YourWebsite_FunctionNecessary);
                var FeatureStatus = new Master().GetSingleMultipleMaster((int)ModuleName.Marketing, ModelOnlinePresence.OnlinePresenceID);
                foreach(var Item in FunctionNecessary)
                if (FeatureStatus != null && FeatureStatus.Any(x => x.Value2 == Convert.ToString(Item.ID)))
                {
                        TextTypeQuestions += TextTypeQuestions;
                        ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                        ProgressValue += ProgressUnit;
                        break;
                    }
            }
            if (ModelMarketingPlanList != null)
            {
                if (ModelMarketingPlanList.Count() > 0) ProgressValue += ProgressUnit;
            }
            if (ModelMarketingBrand != null)
                foreach (System.Reflection.PropertyInfo pi in ModelMarketingBrand.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(ModelMarketingBrand);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofstring, element => element == propertyname))
                            if (!string.IsNullOrEmpty(value))
                            {
                                TextTypeQuestions += TextTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }
                    if (pi.PropertyType == typeof(Guid))
                    {
                        Guid value = (Guid)pi.GetValue(ModelMarketingBrand);
                        string propertyname = (string)pi.Name;
                        if (!Array.Exists(ignoretypeofguid, element => element == propertyname))
                            if (value != Guid.Empty)
                            {
                                GuidSelectTypeQuestions += GuidSelectTypeQuestions;
                                ModuleUnansweredQuestions -= ModuleUnansweredQuestions;
                                ProgressValue += ProgressUnit;


                            }
                    }

                }
            return Math.Round(ProgressValue);
        }
    }
}