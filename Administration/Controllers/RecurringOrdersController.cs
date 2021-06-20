using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class RecurringOrdersController : Controller
    {
        // GET: RecurringOrders
        public ActionResult Index()
        {
            return View();
        }
    }
}