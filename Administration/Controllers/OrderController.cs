using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            OrderManager obj = new OrderManager();
            IEnumerable<_Order> list = obj.GetAllOrder();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllRecurringOrder()
        {
            OrderManager obj = new OrderManager();
            IEnumerable<RecurringOrder> list = obj.GetAllRecurringOrder();
            foreach (var item in list)
            {
                if (item.Frequency == Frequency.Monthly)
                {
                    item.NextOrderDateTime = item.OrderDateTime.AddDays(30);
                }
                else if (item.Frequency == Frequency.Yearly)
                {
                    item.NextOrderDateTime = item.OrderDateTime.AddYears(1);
                }
            }

            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ProcessOrderAutomatically(Guid OrderID)
        {
            string CustomerID = await new OrderManager().AddOrderinGateway(OrderID);
            return Json(new { status = "FAILED", msg = "The " + CustomerID + " already exist in the system" }, JsonRequestBehavior.AllowGet);
        }
          
    }
}