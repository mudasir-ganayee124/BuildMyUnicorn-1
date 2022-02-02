using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn.Business_Layer;
using System.IO;

namespace BuildMyUnicorn.Controllers
{
    public class ProfileController : WebController
    {
        // GET: Profile
        public ActionResult Index(string ClientID)
        {
            Guid clientID = Guid.Empty;
            if (ClientID == null)
                 clientID = Guid.Parse(User.Identity.Name);
            else {  clientID = Guid.Parse(ClientID); }
            ViewBag.CountryList = new CountryManager().GetCountryList();
            Client obj = new  ClientManager().GetClient(clientID);
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            return View(obj);
        }

      
    }
}