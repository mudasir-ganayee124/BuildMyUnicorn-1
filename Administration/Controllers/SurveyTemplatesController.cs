using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using Administration.Business_Layer;
using Business_Model.Helper;

namespace Administration.Controllers
{
    public class SurveyTemplatesController : Controller
    {
        // GET: SurveyTemplates
        public ActionResult Index()
        {
            Master obj = new Master();
            ViewBag.SurveyTemplate = obj.GetAllSurveyTemplates();
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            ViewBag.Template = new Master().GetSingleSurveyTemplate(Guid.Parse(id));
            return View();
        }

        public JsonResult GetAll()
        {
            Master obj = new Master();
            IEnumerable<SurveyTemplates> list = obj.GetAllSurveyTemplates();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

       
        public string AddTemplate(SurveyTemplates Model)
        {
            Model.TemplateID = Guid.NewGuid();
            Model.EntityState = EntityState.New;
            return new Master().AddSurveyTemplate(Model);
        }
        public string UpdateTemplate(SurveyTemplates Model)
        {
          
            Model.EntityState = EntityState.Old;
            return new Master().AddSurveyTemplate(Model);
        }
        public string Delete(Guid ID)
        {

            return new Master().DeleteSurveyTemplate(ID);
        }
    }
}