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
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignupSuccess(string email)
        {
            ViewBag.Email = email.ToString();
            return View();
        }

        public string AddStartupAccelerator(StartupAccelerator Model)
        {

            return new BuildMyUnicornAccelerator.Business_Layer.AccountManager().AddNewStartupAccelerator(Model);

        }

        public ActionResult EmailVerification()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new BuildMyUnicornAccelerator.Business_Layer.AccountManager().ConfirmEmail(Request.QueryString["refid"].ToString());

                if (returnvalue[0] == "OK")
                {
                    ViewBag.ClientID = returnvalue[1];
                    ViewBag.ConfirmationID = returnvalue[2];
                    ViewBag.CustomerName = returnvalue[3];
                    return View();
                }
                else
                {
                    return PartialView("_BadRequest");

                }
            }
            else
            {
                return PartialView("_BadRequest");

            }
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        public string SendPasswordResetLink(String Email)
        {

            return new Business_Layer.AccountManager().SendPasswordResetLink(Email);
        }


        public string UpdatePassword(Client Model)
        {
            new BuildMyUnicornAccelerator.Business_Layer.AccountManager().UpdateEmailConfirmation(Model);
            FormsAuthentication.SetAuthCookie(Model.ClientID.ToString(), true);
            return new BuildMyUnicornAccelerator.Business_Layer.AccountManager().UpdatePassword(Model);
        }
    }
}