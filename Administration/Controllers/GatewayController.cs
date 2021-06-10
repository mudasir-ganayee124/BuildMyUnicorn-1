using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class GatewayController : Controller
    {
        // GET: Gateway
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            AdministrationManager obj = new AdministrationManager();
            IEnumerable<Gateway> list = obj.GetAllGateway();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
    }
}