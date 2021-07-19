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
    public class ThirdPartyManager
    {
        public void SendPackageInvoivce(Guid OrderID)
        {
            var queryInvoice = $@"select * from tbl_email_templates where EmailTemplateCode = '{TemplateType.CPAI.ToString()}' and IsActive = 1";
            string queryOrder = $@"select * from tbl_order where OrderID = '{OrderID}'";
            Order Order = SharedManager.GetSingle<Order>(queryOrder);
            string queryPackage = $@"select * from tbl_supplier_package where SupplierPackageID = '{Order.PlanID}'";
            Package Package = SharedManager.GetSingle<Package>(queryPackage);
            var Template = SharedManager.GetSingle<_EmailTemplates>(queryInvoice);
            string queryClient = $@"select * from tbl_client where ClientID ='{Order.ClientID}' ";
            Client client = SharedManager.GetSingle<Client>(queryClient);
            string supplier = $@"SELECT * FROM tbl_supplier 
                                INNER JOIN tbl_supplier_package ON tbl_supplier_package.SupplierID = tbl_supplier.SupplierID
                                WHERE tbl_supplier_package.SupplierPackageID = '{Order.PlanID}'";

            string Invoice = Template.EmailTemplateBody.ToString();          
           
            Invoice = Invoice.Replace("@DATE", Order.OrderDateTime.ToString()).Replace("@ORDERID", Order.Order_ID.ToString()).Replace("@AMOUNT", Package.PackageAmount.ToString()).Replace("@EMAIL", client.Email).Replace("@NAME", client.FirstName + " " + client.LastName).Replace("@PLANNAME", Package.PackageTitle);
            string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
            Thread Transaction = new Thread(delegate () {
                Transaction Model = new Transaction();
                Model.PaymentMode = PaymentMode.CreditCard;
                Model.Amount = Package.PackageAmount;
                Model.OrderID = Order.OrderID;
                new PaymentOrderManager().AddTransaction(Model);
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
                emailobj.SendMail(SenderEmail, client.Email.ToString(), Template.EmailTemplateSubject.ToString(), Invoice.ToString());
            });
            email_sender_thread.IsBackground = true;
            email_sender_thread.Start();
            Transaction.Start();
            TransactionLog.Start();
        }
    }
}