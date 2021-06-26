using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Administration.Business_Layer
{
    public class OrderManager
    {
        public IEnumerable<_Order> GetAllOrder()
        {
            var query = $@"select tbl_order.*, tbl_client.StartupName,tbl_plan.PlanName, tbl_plan.Amount, 
                            tbl_currency.Code from tbl_order
                            INNER JOIN tbl_client
                            ON tbl_client.ClientID = tbl_order.ClientID
                            INNER JOIN tbl_plan ON tbl_plan.PlanID = tbl_order.PlanID
                            INNER JOIN tbl_currency ON tbl_currency.CurrencyID = tbl_plan.CurrencyID ORDER BY OrderDateTime desc";
           return  SharedManager.GetList<_Order>(query).ToList();
           
        }

        public IEnumerable<RecurringOrder> GetAllRecurringOrder()
        {
            var query = $@"SELECT LT.*, RT.OrderDateTime FROM (SELECT tbl_order.OrderID, FirstName, GatewayClientID,  LastName,PlanName,StartupName,Amount,OrderStatus,  tbl_plan_recurring.*
                         FROM  dbo.tbl_order 
						 INNER JOIN
                         dbo.tbl_client ON dbo.tbl_order.ClientID = dbo.tbl_client.ClientID 
						 INNER JOIN
                         dbo.tbl_plan ON dbo.tbl_order.PlanID = dbo.tbl_plan.PlanID 
						 INNER JOIN
						 dbo.tbl_plan_recurring ON dbo.tbl_plan.PlanID = dbo.tbl_plan_recurring.PlanID
                         WHERE OrderStatus = 0 and Frequency <> 0 ) AS LT
						 INNER JOIN
						 (select  MAX(orderID) as orderID, max(OrderDateTime) OrderDateTime from tbl_order group by GatewayClientID) AS  RT
						 ON LT.OrderID = RT.orderID";
            return SharedManager.GetList<RecurringOrder>(query).ToList();

        }

        public void AddNewOrder(Order Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@OrderID", ParamterValue = Model.OrderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@PlanID", ParamterValue = Model.PlanID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@OrderStatus", ParamterValue = Model.OrderStatus, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@OrderPublicID", ParamterValue = Model.OrderPublicID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@GatewayClientID", ParamterValue = Model.GatewayClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@GatewayOrderID", ParamterValue = Model.GatewayOrderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },


            };
            obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_order", parameters);
            //if (result > 0) return "OK"; else return "Order failed";

        }

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

        public async Task<string> AddOrderinGateway(Guid OrderID)
        {
            var queryOrder = $@"SELECT dbo.tbl_order.*, dbo.tbl_plan.Amount, dbo.tbl_currency.Code, dbo.tbl_currency.Symbol
                             FROM  dbo.tbl_order INNER JOIN dbo.tbl_plan ON dbo.tbl_order.PlanID = dbo.tbl_plan.PlanID 
							 INNER JOIN dbo.tbl_currency ON tbl_currency.CurrencyID = tbl_plan.CurrencyID where OrderID = '{OrderID}'";
            var queryGateway = $@"select * from tbl_gateway WHERE GatewayType = '{(int)GatewayType.Revolut}'";
            Gateway gateway = SharedManager.GetSingle<Gateway>(queryGateway);
            _Order Order = SharedManager.GetSingle<_Order>(queryOrder);

            CreateOrderRecrrring reqOrder = new CreateOrderRecrrring();
            reqOrder.Amount = Order.Amount * 100;
            reqOrder.Currency = Order.Code;
            reqOrder.CustomerID = Order.GatewayClientID.ToString();
            Result<CreateOrderRecrrring> order = await new RevolutManager().Post<CreateOrderRecrrring>("orders", gateway, reqOrder);
            var PublicId =  order.Value.Id;
            Result<CreateOrderRecrrring> orderConfirm = await new RevolutManager().Post<CreateOrderRecrrring>("orders/" + PublicId + "/confirm", gateway, reqOrder);
            Order OrderObj = new Order();
            OrderObj.OrderID = Guid.NewGuid();
            OrderObj.ClientID = Order.ClientID;
            OrderObj.OrderStatus = OrderStatus.Completed;
            OrderObj.PlanID = Order.PlanID;
            OrderObj.GatewayClientID = Guid.Parse(Order.GatewayClientID.ToString());
            OrderObj.GatewayOrderID = Guid.Parse(PublicId);
            OrderObj.OrderPublicID = Guid.Parse(PublicId);
            new OrderManager().AddNewOrder(OrderObj);


            Thread Transaction = new Thread(delegate () {
                Transaction Model = new Transaction();
                Model.PaymentMode = PaymentMode.CreditCard;
                Model.Amount = Order.Amount;
                Model.OrderID = OrderObj.OrderID;
                new OrderManager().AddTransaction(Model);
            });

            Thread TransactionLog = new Thread(delegate () {
                TransactionLog log = new TransactionLog();
                log.MerchantTransactionStatus = "Completed";
                log.OrderID = OrderObj.OrderID;
                log.TransactionStatus = TransactionStatus.Success;
                new OrderManager().AddTransactionLog(log);
            });
            Transaction.Start();
            TransactionLog.Start();
            return order.Value.PublicId;


        }



    }
}