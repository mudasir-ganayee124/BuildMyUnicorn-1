using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}