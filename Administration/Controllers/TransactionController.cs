using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;

namespace Administration.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllTransaction()
        {
            TransactionManager obj = new TransactionManager();
            IEnumerable<_Transaction> list = obj.GetAllTransaction();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllTransactionLog()
        {
            TransactionManager obj = new TransactionManager();
            IEnumerable<_TransactionLog> list = obj.GetAllTransactionLog();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
    }
}