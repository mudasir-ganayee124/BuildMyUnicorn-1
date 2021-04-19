using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Administration.Controllers
{
    public class AppnedixController : Controller
    {
        // GET: Appnedix
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            Master obj = new Master();
            IEnumerable<Appendix> list = obj.GetAppendexList();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllLog()
        {
            Master obj = new Master();
            IEnumerable<_AppendixSearchLog> list = obj.GetAppendexLogList();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(Guid ID)
        {
            return Json(new Master().GetSingleOptionMaster((int)OptionType.MoneyRaise, ID), JsonRequestBehavior.AllowGet);
        }

        public string Add(Option Model)
        {
            Model.Type = OptionType.MoneyRaise;
            return new Master().AddNewOptionMaster(Model);
        }

        public string Update(Option Model)
        {
            Model.Type = OptionType.MoneyRaise;
            return new Master().UpdateOptionMaster(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteAppendixLog(ID);
        }

        public string ImportFile()
        {
            HttpFileCollectionBase file = Request.Files;
            return Master.ImportAppendix(file);
        }
    }
}