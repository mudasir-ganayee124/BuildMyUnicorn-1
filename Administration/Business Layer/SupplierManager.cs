using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using ALMS_DAL;
using Business_Model.Model;
using Business_Model.Helper;

namespace Administration.Business_Layer
{
    public class SupplierManager
    {
        public IEnumerable<Supplier> GetAllSupplier()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<Supplier>(CommandType.StoredProcedure, "sp_get_all_supplier", null);
        }

        public Supplier GetSingleSupplier(Guid SupplierID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<Supplier>(CommandType.StoredProcedure, "sp_get_supplier_by_id", parameters);

        }

        public IEnumerable<AccountManager> GetAccountManagerList(Guid SupplierID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<AccountManager>(CommandType.StoredProcedure, "sp_get_account_manager_by_supplier", parameters);
        }

        public void UpdateAccountManager(List<_AccountManager> ModelList)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
   
            ModelList.ForEach(x => x.ManagedBy = Guid.Parse(HttpContext.Current.User.Identity.Name));
            DataTable dtAccountManagerData = Extensions.ListToDataTable(ModelList);
            obj.ExecuteBulkInsert("sp_update_account_manager_data", dtAccountManagerData, "UT_AccountManager_Data", "@DataTable");
        }

        public string Delete(Guid EntityID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = EntityID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityType", ParamterValue = _EntityType.Supplier, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_account", parameters);
            return result > 0 ? "OK" : "Can not delete ";


        }

        public string  UpdateStatus(Guid SupplierID, bool Status)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@Status", ParamterValue = Status, ParamterType = DbType.Boolean, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = 1, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_account_status", parameters);
            return result > 0 ? "OK" : "Can not update status ";


        }

    }
}