using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicornAccelerator.Business_Layer;
using System.Web.Security;


namespace BuildMyUnicornAccelerator.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                StartupAccelerator  obj = new BuildMyUnicornAccelerator.Business_Layer.AccountManager().GetAccelerator(Guid.Parse(User.Identity.Name));
                ViewBag.obj = obj;



            }
            return View();
        }
    }
}