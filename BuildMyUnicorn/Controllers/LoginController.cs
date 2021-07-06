using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Syncfusion.Drawing;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BuildMyUnicorn.Controllers
{
    public class LoginController : Controller
    {
      
        public ActionResult  Index()
        {
            
            return View();
        }


        public string ValidateUser(Client Model)
        {
           // Session["HeartBeat"] = true;
            return new ClientManager().ValidateCustomerLogin(Model);

        }

        public string ChangePassword(ChangePassword Model)
        {

            return new ClientManager().ChangePassword(Model);

        }


        public ActionResult Logout()
        {

         
            if (Request.Cookies["HeartBeat"] != null)
            {
                Response.Cookies["HeartBeat"].Expires = DateTime.Now.AddDays(-1);
            }
            //if (Session["HeartBeat"] != null)
            //{
            //    Session.Remove("HeartBeat");
            //}
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public void HeartBeat()
        {
            if (Request.Cookies["HeartBeat"] != null)
            {
                Response.SetCookie(new HttpCookie("HeartBeat", "true"));
            }
            // HttpContext.Current.Response.SetCookie(new HttpCookie("HeartBeat", "true"));

            //if (Request.Cookies.Get("HeartBeat") != null)
            //{ 
            //   Response.SetCookie(new HttpCookie("HeartBeat", "true"));
            //}

            //if (Session["HeartBeat"] != null)
            //{
            //    Session["HeartBeat"] = true;
            //}

        }
    }
}