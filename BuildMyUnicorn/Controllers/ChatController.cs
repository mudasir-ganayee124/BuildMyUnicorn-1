using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class ChatController : WebController
    {
        // GET: Chat
        public ActionResult Index()
        {
            ViewBag.CustomerTeamList = new ClientManager().GetClientTeam();
            ViewBag.CustomerAccelerator = new ClientManager().GetCustomerStartupAccelerator();
            return View();
        }

        public ActionResult Chat()
        {
            ViewBag.CustomerTeamList = new ClientManager().GetClientTeam();
            ViewBag.CustomerAccelerator = new ClientManager().GetCustomerStartupAccelerator();
            return PartialView("_ChatPartial");
        }

        public ActionResult ChatDialog()
        {
            ViewBag.CustomerTeamList = new ClientManager().GetClientTeam();
            ViewBag.CustomerAccelerator = new ClientManager().GetCustomerStartupAccelerator();
            return PartialView("_ChatDialogPartial");
        }
        //public ActionResult GetUnReadChat()
        //{

        //    return PartialView("_UnreadChatPartial");
        //}
        public ActionResult GetUnReadChat(ChatMessage Model)
        {

            return PartialView("_UnreadChatPartial", new ChatManager().GetUnreadChat(Model));
        }

        public int GetUnreadChatCount()
        {

          return  new ChatManager().GetUnreadChatCount();
        }
        
        public void AddChatMessage(ChatMessage Model)
        {

            new ChatManager().AddChatMessage(Model);
        }

        public void UpdateChatMessageRead(ChatMessage obj)
        {
            new ChatManager().UpdateChatMessageRead(obj);
        }

        public void UpdateModuleMessageRead(ChatMessage obj)
        {
            obj.ChatSenderID = Guid.Parse(User.Identity.Name);
            new ChatManager().UpdateModuleChatMessageRead(obj);
        }
        
        
        public JsonResult GetChatMessage(Chat Model)
        {
            return Json(new ChatManager().GetChatMessages(Model), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetModuleSectionChatMessage(ChatMessage Model)
        {
            return Json(new ChatManager().GetModuleSectionChatMessages(Model), JsonRequestBehavior.AllowGet);

        }
        public JsonResult RefreshChatMessage(ChatMessage Model)
        {
            return Json(new ChatManager().GetRefreshChatMessages(Model), JsonRequestBehavior.AllowGet);
        }
    }
}