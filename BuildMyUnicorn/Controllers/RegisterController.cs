using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuildMyUnicorn.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Signup
        public ActionResult Index(string id)
        {
            try
            {
                if (Request.QueryString["refid"] == null)
                {
                    if (!string.IsNullOrEmpty(id))
                    {

                        Session.Add("PlanID", id);
                    }
                    var PlanID = Session["PlanID"].ToString();
                    ViewBag.Plan = new PaymentOrderManager().GetSinglePlan(Guid.Parse(PlanID));
                    ViewBag.PlanID = PlanID;

                }
                else
                {
                    Guid AffiliateLinkID = Guid.Parse(Encryption.DecryptGuid(Request.QueryString["refid"]));
                    ViewBag.AffiliateLinkID = AffiliateLinkID;
                    return View("AffilateView");
                }
                return View();
            }
            catch (Exception e)
            {
                return PartialView("_BadRequest");
            }
        }
        public ActionResult SignupSuccess(string email)
        {
            ViewBag.Email = email.ToString();
            return View();
        }

        public ActionResult ResetPasswordEmailSuccess()
        {
            return View();
        }

        public ActionResult EmailVerification()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new ClientManager().ConfirmEmail(Request.QueryString["refid"].ToString());

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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new ClientManager().ConfirmResetPassword(Request.QueryString["refid"].ToString());

                if (returnvalue[0] == "OK")
                {
                    ViewBag.ClientID = returnvalue[1];
                    ViewBag.ConfirmationID = returnvalue[2];
                    ViewBag.CustomerName = returnvalue[3];
                    return View();
                }
                else
                {
                    return RedirectToAction("BadRequest", "ErrorHandler");

                }
            }
            else
            {
                return RedirectToAction("BadRequest", "ErrorHandler");

            }
        }

        public string UpdatePassword(Client Model)
        {
            new ClientManager().UpdateCustomerEmailConfirmation(Model);
            FormsAuthentication.SetAuthCookie(Model.ClientID.ToString(), true);
            return new ClientManager().UpdateCustomerPassword(Model);
        }

        public string UpdateForgotPassword(Client Model)
        {
            new ClientManager().UpdateCustomerCustomerForgotPassword(Model);

            return new ClientManager().UpdateCustomerPassword(Model);
        }

        public async  Task<JsonResult> AddCustomer(Client Model)
        {
            string CustomerID = await new ClientManager().AddCustomerinGateway(Model);
            var Client = new ClientManager().GetSingleClientByEmail(Model.Email);
            if (Client == null)
            {
          
                Model.ClientID = Guid.NewGuid();
                string returnValue = new ClientManager().AddNewClient(Model);
                if (returnValue == "OK")
                {
                   
                    
                    string PublicId = await new ClientManager().AddOrderinGateway(Model, CustomerID);
               
                    Order OrderObj = new Order();
                    OrderObj.OrderID = Guid.NewGuid();
                    OrderObj.ClientID = Model.ClientID;
                    OrderObj.OrderStatus = OrderStatus.PENDING;
                    OrderObj.PlanID = Model.PlanID;
                    OrderObj.GatewayClientID = Guid.Parse(CustomerID);
                    OrderObj.GatewayOrderID = Guid.Parse(PublicId);
                    OrderObj.OrderPublicID = Guid.Parse(PublicId);
                    Session.Add("OrderID", OrderObj.OrderID);
                    new ClientManager().AddNewOrder(OrderObj);
                    Order order = new ClientManager().GetClientOrder(Model.ClientID);
                    return Json(new { status = "SUCCESS", data = order }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { status = "FAILED", msg = "Check the log" }, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(new { status = "FAILED", msg = "The " + Model.Email.ToString() + " already exist in the system" }, JsonRequestBehavior.AllowGet);


        }

        public string SendPasswordResetLink(String Email)
        {

            return new ClientManager().SendPasswordRestLink(Email);
        }

        public void SendInvoice(Guid ClientID)
        {
            new  PaymentOrderManager().SendClientPaymentInvoice(ClientID);
        }

        public JsonResult GetCountryList()
        {

            IEnumerable<Country> countryList = new CountryManager().GetCountryList();
            return Json(new { country = countryList }, JsonRequestBehavior.AllowGet);
        }

      
    }
}