using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;
using Business_Model.Model;
using Business_Model.Helper;
using System.ComponentModel;

namespace Administration.Controllers
{
    public class ManageLanguageController : Controller
    {
        // GET: ManageLanguage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Languages()
        {
            return PartialView("_LanguagesPartial");
        }

        public ActionResult LanguageModules(Guid LanguageID)
        {
            ViewBag.LanguageID = LanguageID;
            return PartialView("_LanguagesModulesPartial");
        }


        public ActionResult ModuleQuestions(Guid LanguageID, int ModuleID, int ModuleSectionID)
        {
            ViewBag.LanguageID = LanguageID;
            ViewBag.ModuleID = ModuleID;
            ViewBag.ModuleSectionID = ModuleSectionID;
            return View();
        }
        public JsonResult GetAllLanguage()
        {
            LanguageManager obj = new LanguageManager();
            IEnumerable<Language> list = obj.GetLanguageList();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllLanguageModule(Guid LanguageID)
        {
            LanguageManager obj = new LanguageManager();
            IEnumerable<LanguageModule> list = obj.GetLanguageModuleList(LanguageID);
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLanguage(Guid ID)
        {
            return Json(new LanguageManager().GetSingleLanguage(ID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleQuestion(Guid LanguageID, int ModuleID, int ModuleSectionID)
        {
            LanguageManager obj = new LanguageManager();
            IEnumerable<LanguageModule> list = obj.GetLanguageModuleQustionList(LanguageID, ModuleID, ModuleSectionID);
            if ((int)Module.MyIdea == ModuleID && (int)ModuleSection.MyIdea_Ideaoutofhead == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Idea))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_MyIdea_IdeaoutofheadPartial", list);
            }

            return PartialView("");
        }

        public string AddLanguage(Language Model)
        {
            Model.EntityState = EntityState.New;
            Model.LanguageID = Guid.NewGuid();
            return new LanguageManager().AddLanguage(Model);
        }

        public string UpdateLanguage(Language Model)
        {
            Model.EntityState = EntityState.Old;
            return new LanguageManager().AddLanguage(Model);
        }

        public string UpdateQuestionText(Guid LanguageModuleID, string QuestionText)
        {
            return new LanguageManager().UpdateModuleQuestion(LanguageModuleID, QuestionText, 1);
        }
        public string UpdatePlaceHolderText(Guid LanguageModuleID, string PlaceHolderText)
        {
            return new LanguageManager().UpdateModuleQuestion(LanguageModuleID, PlaceHolderText, 2);
        }
    }
}