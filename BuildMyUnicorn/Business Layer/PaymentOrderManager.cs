using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ALMS_DAL;
using Business_Model.Model;
using System.Linq;
using Business_Model.Helper;
using System.Web;
using System.Threading;

namespace BuildMyUnicorn.Business_Layer
{
    public class PaymentOrderManager
    {
        public string AddTransaction(Transaction Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {              
                new ParametersCollection { ParamterName = "@PaymentMode", ParamterValue = Model.PaymentMode, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Amount", ParamterValue = Model.Amount, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@OrderID", ParamterValue = Model.OrderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             
            };

            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_transaction", parameters);
            if (result > 0) return "OK"; else return "Error in adding order";

        }
        public string AddTransactionLog(TransactionLog Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@OrderID", ParamterValue = Model.OrderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TransactionStatus", ParamterValue = Model.TransactionStatus, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MerchantTransactionStatus", ParamterValue = Model.MerchantTransactionStatus, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
             
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_transaction_log", parameters);
            if (result > 0) return "OK"; else return "Error in adding order log";

        }

        public Plan GetSinglePlan(Guid PlanID)
        {
            var queryPlan = $@"SELECT *, tbl_currency.Code , tbl_currency.Symbol FROM tbl_plan INNER JOIN  dbo.tbl_currency ON tbl_currency.CurrencyID = tbl_plan.CurrencyID where PlanID = '{PlanID}'";
             return SharedManager.GetSingle<Plan>(queryPlan);
        }
        public void SendClientPaymentInvoice(Guid ClientID)
        {
            string queryEmailTemplate = $@"select * from tbl_email_templates where EmailTemplateCode = 'CI'";
            string queryOrder = $@"select * from tbl_order where ClientID = '{ClientID}'";
            string queryClient = $@"select * from tbl_client where ClientID ='{ClientID}' ";
            _EmailTemplates InvoiceTemplate = SharedManager.GetSingle<_EmailTemplates>(queryEmailTemplate);
            string Invoice = InvoiceTemplate.EmailTemplateBody.ToString();
            Client client = SharedManager.GetSingle<Client>(queryClient);
            Order Order = SharedManager.GetSingle<Order>(queryOrder);
            string queryPlan = $@"select * from tbl_plan where PlanID = '{Order.PlanID}'";
            Plan Plan = SharedManager.GetSingle<Plan>(queryPlan);
            Invoice = Invoice.Replace("@Date", Order.OrderDateTime.ToString()).Replace("@OrderID", Order.OrderID.ToString()).Replace("@Amount", Plan.Amount.ToString()).Replace("@Phone", client.Phone).Replace("@Email",  client.Email).Replace("@Name", client.FirstName + " " + client.LastName).Replace("@PlanName", Plan.PlanName);
            string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
            Thread Transaction = new Thread(delegate () {
                Transaction Model = new Transaction();
                Model.PaymentMode = PaymentMode.CreditCard;
                Model.Amount = Plan.Amount;
                Model.OrderID = Order.OrderID;
                new PaymentOrderManager().AddTransaction( Model);
            });

            Thread TransactionLog = new Thread(delegate () {
                TransactionLog log = new TransactionLog();
                log.MerchantTransactionStatus = "Completed";
                log.OrderID = Order.OrderID;
                log.TransactionStatus = TransactionStatus.Success;
                new PaymentOrderManager().AddTransactionLog(log);
            });

            //Finally Send Mail and save data Async
            Thread email_sender_thread = new Thread(delegate ()
            {
                EmailSender emailobj = new EmailSender();
                emailobj.SendMail(SenderEmail, client.Email.ToString(), InvoiceTemplate.EmailTemplateSubject.ToString(), Invoice.ToString());
            });
            email_sender_thread.IsBackground = true;
            email_sender_thread.Start();
            Transaction.Start();
            TransactionLog.Start();
        }
    }
}