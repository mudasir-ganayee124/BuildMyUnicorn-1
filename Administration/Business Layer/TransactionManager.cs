using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administration.Business_Layer
{
    public class TransactionManager
    {
        public IEnumerable<_Transaction> GetAllTransaction()
        {
            var query =    $@"SELECT dbo.tbl_transaction.TransactionID, dbo.tbl_transaction.PaymentMode, dbo.tbl_transaction.Amount, dbo.tbl_transaction.TransactionDateTime, dbo.tbl_client.FirstName, dbo.tbl_client.LastName, dbo.tbl_client.StartupName, dbo.tbl_client.Email, dbo.tbl_plan.PlanName, dbo.tbl_currency.Symbol FROM  dbo.tbl_transaction INNER JOIN
                           dbo.tbl_order ON dbo.tbl_transaction.OrderID = dbo.tbl_order.OrderID INNER JOIN
                           dbo.tbl_client ON dbo.tbl_order.ClientID = dbo.tbl_client.ClientID INNER JOIN
                           dbo.tbl_plan ON dbo.tbl_order.PlanID = dbo.tbl_plan.PlanID INNER JOIN
                           dbo.tbl_currency ON dbo.tbl_currency.CurrencyID = dbo.tbl_plan.CurrencyID  
                           UNION ALL
                           SELECT dbo.tbl_transaction.TransactionID, dbo.tbl_transaction.PaymentMode, dbo.tbl_transaction.Amount, dbo.tbl_transaction.TransactionDateTime, dbo.tbl_client.FirstName, dbo.tbl_client.LastName, dbo.tbl_client.StartupName, dbo.tbl_client.Email, dbo.tbl_supplier_package.PackageTitle AS PlanName, dbo.tbl_currency.Symbol FROM  dbo.tbl_transaction INNER JOIN
                           dbo.tbl_order ON dbo.tbl_transaction.OrderID = dbo.tbl_order.OrderID INNER JOIN
                           dbo.tbl_client ON dbo.tbl_order.ClientID = dbo.tbl_client.ClientID INNER JOIN
                           dbo.tbl_supplier_package ON dbo.tbl_order.PlanID = dbo.tbl_supplier_package.SupplierPackageID INNER JOIN
                           dbo.tbl_currency ON dbo.tbl_currency.CurrencyID = dbo.tbl_supplier_package.CurrencyID  ORDER BY TransactionDateTime DESC";
            return SharedManager.GetList<_Transaction>(query).ToList();

        }
        public IEnumerable<_TransactionLog> GetAllTransactionLog()
        {
            var query = $@"SELECT dbo.tbl_transaction_log.TransactionLogID, tbl_transaction_log.TransactionStatus, dbo.tbl_plan.Amount, tbl_transaction_log.MerchantTransactionStatus,  dbo.tbl_transaction_log.TransactionLogDateTime, dbo.tbl_client.FirstName, dbo.tbl_client.LastName, dbo.tbl_client.StartupName, dbo.tbl_client.Email, dbo.tbl_plan.PlanName, dbo.tbl_currency.Symbol
                         FROM  dbo.tbl_transaction_log INNER JOIN
                         dbo.tbl_order ON dbo.tbl_transaction_log.OrderID = dbo.tbl_order.OrderID INNER JOIN
                         dbo.tbl_client ON dbo.tbl_order.ClientID = dbo.tbl_client.ClientID INNER JOIN
                         dbo.tbl_plan ON dbo.tbl_order.PlanID = dbo.tbl_plan.PlanID INNER JOIN
                         dbo.tbl_currency ON dbo.tbl_currency.CurrencyID = dbo.tbl_plan.CurrencyID  ORDER BY TransactionLogDateTime DESC";
            return SharedManager.GetList<_TransactionLog>(query).ToList();

        }
    }
}