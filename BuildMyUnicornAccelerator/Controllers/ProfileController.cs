using System;
using Business_Model.Model;
using System.Web.Mvc;

namespace BuildMyUnicornAccelerator.Controllers
{
    public class ProfileController : WebController
    {
        public ActionResult Edit(string id)
        {
            return View(new BuildMyUnicornAccelerator.Business_Layer.AccountManager().GetAccelerator(Guid.Parse(id)));
        }

        public string Update(StartupAccelerator Model)
        {
            return new BuildMyUnicornAccelerator.Business_Layer.AccountManager().UpdateProfile(Model);
        }
    }
}