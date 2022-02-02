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
           // ChatHubAcc.SendChatMessage("20F49C82-5C74-4DE2-A03C-9A73BB3FA1BD", null,"welcome", null);

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