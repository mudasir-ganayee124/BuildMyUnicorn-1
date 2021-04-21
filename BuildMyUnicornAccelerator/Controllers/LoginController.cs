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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public string ValidateUser(StartupAccelerator Model)
        {

            return new BuildMyUnicornAccelerator.Business_Layer.AccountManager().ValidateCustomerLogin(Model);

        }

        public string ChangePassword(ChangePassword Model)
        {

            return new BuildMyUnicornAccelerator.Business_Layer.AccountManager().ChangePassword(Model);

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}