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
    public class DashboardController : WebController
    {

        public ActionResult Index()
        {
           var Clientlist = new ClientManager().GetStartupAcceleratorClient();
           foreach (var item in Clientlist)
            {
                item.ProgressAnalytic = new Master().GetClientProgressAnalytic(item.ClientID);
            }
            ViewBag.Model = Clientlist;
            ViewBag.CustomerList = new ClientManager().GetStartupCountryClientList(ViewBag.Accelerator.LinkID);
            return View();
        }
    }
   
}