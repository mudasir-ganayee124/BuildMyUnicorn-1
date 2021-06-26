using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;
using Business_Model.Model;

namespace Administration.Controllers
{
    public class EmailTemplatesController : Controller
    {
        // GET: EmailTemplates
        public ActionResult Index()
        {
            Master obj = new Master();
            ViewBag.emailtemplates = obj.GetAllEmailTemplates();
            return View();
        }
        //public JsonResult GetEmailTemplates()
        //{
        //    Master obj = new Master();
        //    IEnumerable<_EmailTemplates> emailtemplates = obj.GetAllEmailTemplates();
        //    return Json(new { msg = emailtemplates, total = emailtemplates.Count() }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetEmailTemplateData(Guid EmailTemplateID)
        {
            Master obj = new Master();
            _EmailTemplates emailtemplate = obj.GetSingleEmailTemplate(EmailTemplateID);
            return Json(new { emailtemplate }, JsonRequestBehavior.AllowGet);
        }

        //[ValidateInput(false)]
        //public string UpdateEmailTemplate(_EmailTemplates Model)
        //{
        //    Master obj = new Master();
        //    return obj.UpdateEmailTemplate(Model);
        //}
    }
}