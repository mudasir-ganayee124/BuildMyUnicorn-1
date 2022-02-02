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
          //  ChatHubAcc obj = new ChatHubAcc();
           // obj.Send("20F49C82-5C74-4DE2-A03C-9A73BB3FA1BD", "This message broadcasted on " + DateTime.Now, "");
           // var context = GlobalHost.ConnectionManager.GetHubContext<ChatHubAcc>();

           // context.Clients.All.addMessage("20F49C82-5C74-4DE2-A03C-9A73BB3FA1BD", "This message broadcasted on " + DateTime.Now);
            return new BuildMyUnicornAccelerator.Business_Layer.AccountManager().UpdateProfile(Model);
        }
    }
}