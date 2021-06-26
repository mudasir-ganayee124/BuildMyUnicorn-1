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
    public class PlanManager
    {
        public IEnumerable<Plan> GetAllPlan()
        {
            var query = $@"select tbl_plan.* ,tbl_plan_recurring.PlanRecurringID, tbl_plan_recurring.Frequency, tbl_plan_recurring.ProcessAutomatically FROM tbl_plan
                        LEFT JOIN tbl_plan_recurring ON tbl_plan.PlanID = tbl_plan_recurring.PlanID where IsActive = 1 and IsDeleted = 0";
            return SharedManager.GetList<Plan>(query).ToList();

        }

        public Plan GetSinglePlan(Guid PlandID)
        {
            var queryplan = $@"select * from tbl_plan where PlanID = '{PlandID}'";
            var queryplanattribute = $@"select * from tbl_plan_attribute where PlanID = '{PlandID}'";
            var Plan =  SharedManager.GetSingle<Plan>(queryplan);
            var PlanAttribute = SharedManager.GetList<PlanAttribute>(queryplanattribute);
            Plan.PlanAttribute = PlanAttribute;
            return Plan;
        }
        public PlanRecurring GetPlanRecurring(Guid PlanRecurringID)
        {
            var query= $@"select * from tbl_plan_recurring where PlanRecurringID = '{PlanRecurringID}'";
             return SharedManager.GetSingle<PlanRecurring>(query);
   
        }
        public string AddPlan(Plan Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@PlanID", ParamterValue = Model.PlanID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@PlanName", ParamterValue = Model.PlanName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CurrencyID", ParamterValue = Model.CurrencyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Amount", ParamterValue = Model.Amount, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DisplayOrder", ParamterValue = Model.DisplayOrder, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Duration", ParamterValue = Model.Duration, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@PlanHeading", ParamterValue = Model.PlanHeading, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@PlanSubHeading", ParamterValue = Model.PlanSubHeading, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Url", ParamterValue = Model.Url, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_plan", parameters);
            if (result > 0)
            {

                List<PlanAttribute> PlanAttributeList = new List<PlanAttribute>();
                if (Model.PlanAttribute != null)
                {
                    foreach (var item in Model.PlanAttribute)
                    {
                        PlanAttribute objPlanAttribute = new PlanAttribute();
                        objPlanAttribute.PlanID = Model.PlanID;
                        objPlanAttribute.PlanAttributeID = Guid.NewGuid();
                        objPlanAttribute.Attribute = item.Attribute;
                        objPlanAttribute.DisplayOrder = item.DisplayOrder;
                        PlanAttributeList.Add(objPlanAttribute);

                    }
                    DataTable dtCourseOption = Business_Model.Helper.Extensions.ListToDataTable(PlanAttributeList);
                    obj.ExecuteBulkInsert("sp_add_plan_attribute_data", dtCourseOption, "UT_PlanAttribute_Data", "@DataTable");
                }
                else
                {
                    var query = $@"Delete from tbl_plan_attribute where PlanID = '{Model.PlanID}'";
                    SharedManager.ExecuteScalar<int>(query);
                }

            }
            return result > 0 ? "OK" : Model.PlanName + " already exists";
        }

        public string UpdateRecurringPlan(PlanRecurring Model)
        {
            var query = $@"Update tbl_plan_recurring SET Frequency = '{(Int16)Model.Frequency}',  ProcessAutomatically = '{Model.ProcessAutomatically}' WHERE PlanRecurringID = '{Model.PlanRecurringID}'";
            var result  = SharedManager.ExecuteScalar<int>(query);
            return result == 0 ? "OK" : "Recurring Update failed";
        }
    }
}