using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}