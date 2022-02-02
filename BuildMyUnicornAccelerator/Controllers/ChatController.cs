using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicornAccelerator.Business_Layer;
using Business_Model.Model;

namespace BuildMyUnicornAccelerator.Controllers
{
    public class ChatController : WebController
    {
        // GET: Chat
        public ActionResult Index()
        {
          
            ViewBag.Affilates =  new ClientManager().GetStartupAcceleratorClient();
            return View();
        }
        public ActionResult Chat()
        {
            ViewBag.Affilates = new ClientManager().GetStartupAcceleratorClient();
            return PartialView("_ChatPartial");
        }

        public ActionResult ChatDialog()
        {
            ViewBag.Affilates = new ClientManager().GetStartupAcceleratorClient();
            return PartialView("_ChatDialogPartial");
        }

        public ActionResult GetUnReadChat(ChatMessage Model)
        {

            return PartialView("_UnreadChatPartial", new Master().GetUnreadChat(Model));
        }

        public int GetUnreadChatCount()
        {

            return new Master().GetUnreadChatCount();
        }

        public JsonResult GetChatMessage(Chat Model)
        {
            return Json(new Master().GetChatMessages(Model), JsonRequestBehavior.AllowGet);
          
        }

        public JsonResult GetModuleSectionChatMessage(ChatMessage Model)
        {
            return Json(new Master().GetModuleSectionChatMessages(Model), JsonRequestBehavior.AllowGet);

        }
        public void UpdateChatMessageRead(ChatMessage obj)
        {
            new Master().UpdateChatMessageRead(obj);
        }
        public void UpdateModuleMessageRead(ChatMessage obj)
        {
            obj.ChatSenderID = Guid.Parse(User.Identity.Name);
            new Master().UpdateModuleChatMessageRead(obj);
        }
        public JsonResult RefreshChatMessage(ChatMessage Model)
        {
            return Json(new Master().GetRefreshChatMessages(Model), JsonRequestBehavior.AllowGet);
        }


    }
}