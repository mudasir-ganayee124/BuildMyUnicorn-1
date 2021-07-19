using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuildMyUnicorn.Controllers
{
    [Authorize]
    public class ThirdPartyController : Controller
    {
        public async Task<JsonResult> AddPackageOrder(Guid PackageID)
        {

            Client Model = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
          
            if (Model != null)
            {
                Model.PlanID = PackageID;
                string CustomerID = string.Empty;
                Order ExistOrder = new ClientManager().GetClientSingleOrder(Model.ClientID);
                if (ExistOrder == null)
                    CustomerID = await new ClientManager().AddCustomerinGateway(Model);
                else CustomerID = ExistOrder.GatewayClientID.ToString();
                string PublicId = await new ClientManager().AddPackageinGateway(Model, CustomerID);

                Order OrderObj = new Order();
                OrderObj.OrderID = Guid.NewGuid();
                OrderObj.ClientID = Model.ClientID;
                OrderObj.OrderStatus = OrderStatus.Pending;
                OrderObj.PlanID = Model.PlanID;
                OrderObj.Order_ID = Keygen.Random();
                OrderObj.OrderType = OrderType.Package;
                OrderObj.GatewayClientID = Guid.Parse(CustomerID);
                OrderObj.GatewayOrderID = Guid.Parse(PublicId);
                OrderObj.OrderPublicID = Guid.Parse(PublicId);
                new ClientManager().AddNewOrder(OrderObj);
                
                // Order order = new ClientManager().GetClientOrder(Model.ClientID);
                return Json(new { status = "SUCCESS", data = OrderObj }, JsonRequestBehavior.AllowGet);
                
            }
            else
                return Json(new { status = "FAILED", msg = "Check the log" }, JsonRequestBehavior.AllowGet);

            //}
            //else
            //    return Json(new { status = "FAILED", msg = "The " + Model.Email.ToString() + " already exist in the system" }, JsonRequestBehavior.AllowGet);

        }

        public void SendPackageInvoice(Guid OrderID)
        {
            new ThirdPartyManager().SendPackageInvoivce(OrderID);
        }
    }
}