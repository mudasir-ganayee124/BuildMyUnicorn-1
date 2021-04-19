using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class AppendixController : WebController
    {
        private readonly AppendixManager _appendixManager;

        public AppendixController()
        {
            _appendixManager = new AppendixManager();
        }

        [HttpGet]
        public ActionResult SearchText()
        {
            return View();
        }
        // GET: Chat
        
        public ActionResult SearchTextdata(Appendix Model)
        {

            return Json(_appendixManager.SearchText(Model.Keyword).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAppendixSearchLog()
        {
            return Json(_appendixManager.getAppendixSearchLog().Where(x => x.IsFound == false).ToList(), JsonRequestBehavior.AllowGet);

        }
        public string SaveMessage(AppendixSearchLog Model)
        {
            
            _appendixManager.SaveAppendixSearchLog(Model);
            return "OK";

        }
        public ActionResult getAppendixSearchWord()
        {
            return Json(_appendixManager.getAppendixSearchWord().ToList(), JsonRequestBehavior.AllowGet);

        }
        public ActionResult getAppendixSearchMessage()
        {
            return Json(_appendixManager.getAppendixSearchMessage().ToList(), JsonRequestBehavior.AllowGet);

        }
    }
}