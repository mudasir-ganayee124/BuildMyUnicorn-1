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
            if ((int)Module.TheBusiness == ModuleID && (int)ModuleSection.TheBusiness_Businessoverview == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Business))
                                         .Cast<PropertyDescriptor>()
                                         .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Business_BusinessOverview", list);
            }
            if ((int)Module.MyIdea == ModuleID && (int)ModuleSection.MyIdea_Ideaoutofhead == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Idea))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Business_TheTeam", list);
            }
            if ((int)Module.TheBusiness == ModuleID && (int)ModuleSection.TheBusiness_ProductorService == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._ProductService))
                                    .Cast<PropertyDescriptor>()
                                    .ToDictionary(p => p.Name, p => p.Description);
              
                return PartialView("_Business_ProductService", list);
            }
            if ((int)Module.TheBusiness == ModuleID && (int)ModuleSection.TheBusiness_BusinessOperation == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._BusinessOperation))
                                    .Cast<PropertyDescriptor>()
                                    .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Business_BusinessOperation", list);
            }
            if ((int)Module.TheBusiness == ModuleID && (int)ModuleSection.TheBusiness_Competitors == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Competitor))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Business_Competitors", list);
            }
            if ((int)Module.Selling == ModuleID && (int)ModuleSection.Selling_Customers == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Customer))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Selling_YourCustomers", list);
            }
            if ((int)Module.Selling == ModuleID && (int)ModuleSection.Selling_Pricing == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._PricingProductService))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Selling_ProductServicePricing", list);
            }
            if ((int)Module.MarketResearch == ModuleID && (int)ModuleSection.MarketResearch_KeyFindings == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._KeyFinding))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_MarketResearch_Keyfinding", list);
            }

            if ((int)Module.MarketResearch == ModuleID && (int)ModuleSection.MarketResearch_OnlineResearch == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._OnlineResearch))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_MarketResearch_OnlineResearch", list);
            }
            if ((int)Module.MarketResearch == ModuleID && (int)ModuleSection.MarketResearch_Observation == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model.OurObservation))
                                         .Cast<PropertyDescriptor>()
                                         .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_MarketResearch_Observation", list);
            }
            if ((int)Module.Marketing == ModuleID && (int)ModuleSection.Marketing_OnlinePresence == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._OnlinePresance))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Marketing_OnlinePresence", list);
            }
            if ((int)Module.Marketing == ModuleID && (int)ModuleSection.Marketing_MarketingPlan == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Marketing))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Marketing_Marketing", list);
            }
            if ((int)Module.Marketing == ModuleID && (int)ModuleSection.Marketing_Brand == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._MarketingBrand))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Marketing_Brand", list);
            }
            if ((int)Module.Finance == ModuleID && (int)ModuleSection.Finance_FindingInvestors == ModuleSectionID)
            {
                ViewBag.Title = TypeDescriptor.GetProperties(typeof(Business_Model.Model._Investor))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
                return PartialView("_Finance_Investors", list);
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