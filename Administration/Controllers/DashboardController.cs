using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;
using Business_Model.Model;

namespace Administration.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardManager _DashboardManager;
        public DashboardController()
        {
            _DashboardManager = new DashboardManager();
        }
        public ActionResult Index()
        {
            ViewBag.CustomerList = _DashboardManager.GetCountryCustomerList().ToList();
            ViewBag.SupplierList = _DashboardManager.GetCountrySupplierList().ToList();
            return View();
        } 

        public ActionResult GetCustomerCountryData(Appendix Model)
        {

            return Json(_DashboardManager.GetCountryCustomerList().ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}



