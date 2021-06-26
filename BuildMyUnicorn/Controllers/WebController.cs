using System;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    [Authorize]
    public class WebController : Controller
    {
        protected override void ExecuteCore()
        {
            if (User.Identity.IsAuthenticated == true)
            {
           
                Client Clientobj = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
                ViewBag.Client = Clientobj;
              


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