using ALMS_DAL;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;


namespace BuildMyUnicorn_Supplier.Business_Layer
{
    public class PackageManager
    {

        public string AddNewPackage(Package Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Duration", ParamterValue = Model.Duration, ParamterType = DbType.Int64, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PackageTitle", ParamterValue = Model.PackageTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PackageAmount", ParamterValue = Model.PackageAmount, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CurrencyID", ParamterValue = Model.CurrencyID, ParamterType =DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PackageAttribute", ParamterValue = Model.PackageAttribute, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue =  Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_supplie_package", parameters);
            return result > 0 ? "OK" : "Package Added Failed";

        }

        public string UpdatePackage(Package Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@SupplierPackageID", ParamterValue = Model.SupplierPackageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Duration", ParamterValue = Model.Duration, ParamterType = DbType.Int64, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PackageTitle", ParamterValue = Model.PackageTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PackageAmount", ParamterValue = Model.PackageAmount, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CurrencyID", ParamterValue = Model.CurrencyID, ParamterType =DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@PackageAttribute", ParamterValue = Model.PackageAttribute, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ModifiedBy", ParamterValue =  Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_supplie_package", parameters);
            return result > 0 ? "OK" : "Package Update Failed";

        }

        public IEnumerable<Package> GetSupplierPackageList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<Package>(CommandType.StoredProcedure, "sp_get_supplier_package", parameters);
        }

        public Package GetSinglePackage(Guid SupplierPackageID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@SupplierPackageID", ParamterValue = SupplierPackageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
              

            };
          return obj.GetSingle<Package>(CommandType.StoredProcedure, "sp_get_supplier_package_by_id", parameters);
           
        }

        public IEnumerable<SubscribedPackages> GetAllSubscribedPackages()
        {
            var query = $@"select tbl_order.*,tbl_supplier_package.*,tbl_client.FirstName, tbl_client.LastName, tbl_client.Email, tbl_client.StartupName  from tbl_order 
                        INNER JOIN tbl_supplier_package ON tbl_supplier_package.SupplierPackageID = tbl_order.PlanID
                        INNER JOIN tbl_client ON tbl_client.ClientID = tbl_order.ClientID
                        WHERE tbl_order.OrderType = 1 AND tbl_supplier_package.SupplierID = '{Guid.Parse(HttpContext.Current.User.Identity.Name)}' 
                        ORDER BY tbl_supplier_package.CreatedDateTime DESC";

            return SharedManager.GetList<SubscribedPackages>(query);
        }

        public string DeletePackage(Guid SupplierPackageID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@SupplierPackageID", ParamterValue = SupplierPackageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
               
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_supplier_package", parameters);
            return result > 0 ? "OK" : "Package Deleted Failed";

        }
    }
}