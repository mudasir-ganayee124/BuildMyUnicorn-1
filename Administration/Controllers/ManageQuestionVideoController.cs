using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class ManageQuestionVideoController : Controller
    {
        // GET: ManageQuestionVideo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Modules()
        {
            return PartialView("_ModulesPartial");
        }

        public JsonResult GetAllModules()
        {
            QuestionVideoManager obj = new QuestionVideoManager();
            IEnumerable<QuestionVideo> list = obj.GetAllModules();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModuleQuestions(int ModuleID, int ModuleSectionID)
        {
         
            ViewBag.ModuleID = ModuleID;
            ViewBag.ModuleSectionID = ModuleSectionID;
            return View();
        }

        public ActionResult GetModuleQuestion(int ModuleID, int ModuleSectionID)
        {
            QuestionVideoManager obj = new QuestionVideoManager();
            IEnumerable<QuestionVideo> list = obj.GetModuleQustionList(ModuleID, ModuleSectionID);
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

        public string UpdateQuestionVideo(Guid QuestionVideoID, string VideoUrl)
        {
            return new QuestionVideoManager().UpdateQuestionVideo(QuestionVideoID, VideoUrl);
        }
    }
}