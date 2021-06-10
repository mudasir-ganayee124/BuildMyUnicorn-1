using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicornAccelerator.Business_Layer;
using Business_Model.Model;

namespace BuildMyUnicornAccelerator.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new Business_Layer.AccountManager().ConfirmForgotPassword(Request.QueryString["refid"].ToString());

                if (returnvalue[0] == "OK")
                {
                    ViewBag.StartupAcceleratorID = returnvalue[1];
                    ViewBag.ConfirmationID = returnvalue[2];
                    ViewBag.ContactName = returnvalue[3];
                    return View();
                }
                else
                {
                    return RedirectToAction("BadRequest", "Error");

                }
            }
            else
            {
                return RedirectToAction("BadRequest", "Error");

            }
        }

        public string SendPasswordResetLink(String Email)
        {

            return new Business_Layer.AccountManager().SendPasswordResetLink(Email);
        }

        public string UpdateForgotPassword(StartupAccelerator Model)
        {
            new  Business_Layer.AccountManager().UpdateForgotPassword(Model);

            return new  Business_Layer.AccountManager().UpdatePassword(Model);
        }


    }
}
