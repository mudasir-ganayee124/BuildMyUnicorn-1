using System;
using System.Web.Mvc;
using Business_Model.Model;


namespace BuildMyUnicornAccelerator.Controllers
{
    [Authorize]
    public class WebController : Controller
    {
        protected override void ExecuteCore()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                StartupAccelerator obj = new BuildMyUnicornAccelerator.Business_Layer.AccountManager().GetAccelerator(Guid.Parse(User.Identity.Name));
                ViewBag.obj = obj;
                ViewBag.Accelerator = obj;
            }
            else
            {
                RedirectToAction("Index", "Login");
            }
            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {

            get { return true; }
        }
    }
}