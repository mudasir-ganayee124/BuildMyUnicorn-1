using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using Business_Model.Helper;
using System.Threading.Tasks;

namespace BuildMyUnicorn.Controllers
{
    public class MarketingController : WebController
    {
        public ActionResult OnlinePresence(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new MarketingManager().ExistOnlinePresence(Guid.Empty);
            else
            {
                Guid OnlinePresenceID;
                bool isValid = Guid.TryParse(id, out OnlinePresenceID);
                if (isValid) Count = new MarketingManager().ExistOnlinePresence(OnlinePresenceID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.Marketing;
                objlog.ModuleSectionID = ModuleSection.Marketing_OnlinePresence;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.Marketing, (int)ModuleSection.Marketing_OnlinePresence) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Marketing",
                        ActionName = "OnlinePresence",
                        ModuleID = (int)Module.Marketing,
                        SectionID = (int)ModuleSection.Marketing_OnlinePresence
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Marketing, (int)ModuleSection.Marketing_OnlinePresence);
            return View(State);
        }

        public ActionResult Plan()
        {

        
            IEnumerable<_Marketing> ModelList = new MarketingManager().GetMarketingPlan();
            if (ModelList == null || ModelList.Count() == 0)
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.Marketing;
                objlog.ModuleSectionID = ModuleSection.Marketing_MarketingPlan;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan) > 0)
                {
                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Marketing",
                        ActionName = "Plan",
                        ModuleID = (int)Module.Marketing,
                        SectionID = (int)ModuleSection.Marketing_MarketingPlan
                    });
                }

            }
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);
            return View(ModelList);
        }

        public ActionResult Brand(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new MarketingManager().ExistMarketingBrand(Guid.Empty);
            else
            {
                Guid MarketingBrandID;
                bool isValid = Guid.TryParse(id, out MarketingBrandID);
                if (isValid) Count = new MarketingManager().ExistMarketingBrand(MarketingBrandID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.Marketing;
                objlog.ModuleSectionID = ModuleSection.Marketing_Brand;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.Marketing, (int)ModuleSection.Marketing_Brand) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Marketing",
                        ActionName = "Brand",
                        ModuleID = (int)Module.Marketing,
                        SectionID = (int)ModuleSection.Marketing_Brand
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Marketing, (int)ModuleSection.Marketing_Brand);
            return View(State);
        }

        public ActionResult New(int Type)
        {

            if ((int)ModuleSection.Marketing_OnlinePresence == Type)
            {
                _OnlinePresance Model = new MarketingManager().GetOnlinePresance();
                OnlinePresance obj = new OnlinePresance();
                YourWebsite objYourWebsite = new YourWebsite();
                SocialHandles objSocialHandles = new SocialHandles();

                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetOnlinePresenceDataDependency();

                });               
                if (Model != null)
                {

                    obj.OnlinePresenceID = Model.OnlinePresenceID;
                    obj.ClientID = Model.ClientID;
                    objYourWebsite.CompetitorSitedislike = Model.CompetitorSitedislike;
                    objYourWebsite.CompetitorSitelike = Model.CompetitorSitelike;
                    objYourWebsite.ContentWebsiteCreatorID = Model.ContentWebsiteCreatorID;
                    objYourWebsite.CTA = Model.CTA;
                    objYourWebsite.HaveRegisteredDomain = Model.HaveRegisteredDomain;
                    objYourWebsite.DomainName = Model.DomainName;
                    objYourWebsite.PutWebpageAdds = Model.PutWebpageAdds;
                    objYourWebsite.SuccessMeasure = Model.SuccessMeasure;
                    objYourWebsite.TrafficAnticipatePerMonthID = Model.TrafficAnticipatePerMonthID;
                    objYourWebsite.UseDomainEmail = Model.UseDomainEmail;
                    objYourWebsite.VisitorsAccomplish = Model.VisitorsAccomplish;
                    objYourWebsite.WantWebsiteAchieveID = Model.WantWebsiteAchieveID;
                    objYourWebsite.WebPageStageID = Model.WebPageStageID;
                    objYourWebsite.WebsitePlanMakeMoney = Model.WebsitePlanMakeMoney;
                    obj.YourWebsite = objYourWebsite;
                    objSocialHandles.Facebook = Model.Facebook;
                    objSocialHandles.Twitter = Model.Twitter;
                    objSocialHandles.Youtube = Model.Youtube;
                    objSocialHandles.TicTok = Model.TicTok;
                    objSocialHandles.Instagram = Model.Instagram;
                    objSocialHandles.Linkedin = Model.Linkedin;
                    obj.SocialHandles = objSocialHandles;
                    obj.EntityState = EntityState.Old;
                    ViewBag.FeatureStatusList = new Master().GetSingleMultipleMaster((int)ModuleName.Marketing, Model.OnlinePresenceID);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.YourWebsite = objYourWebsite;
                    obj.SocialHandles = objSocialHandles;
                }

                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_NewOnlinePresencePartial", obj);
            }

            if ((int)ModuleSection.Marketing_MarketingPlan == Type)
            {
                Marketing obj = new Marketing();
                MarketingPlan objPlan = new MarketingPlan();
                MarketingBudget objBudget = new MarketingBudget();

                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetMarketingPlanDataDependency();

                });
               
        
                ViewBag.BuyerPersonaList = new SellingManager().GetCustomerbuyerPersona(Guid.Empty);
                obj.EntityState = EntityState.New;
                obj.MarketingPlan = objPlan;
                obj.MarketingBudget = objBudget;
                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_NewMarketingPlanPartial", obj);
            }

            if ((int)ModuleSection.Marketing_Brand == Type)
            {
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetMarketingBrandDataDependency();

                });

                MarketingBrand obj = new MarketingBrand();
                BrandingStrategy objBrandingStrategy = new BrandingStrategy();
                BrandGuidelines objBrandGuidelines = new BrandGuidelines();
                BrandTouchPoints objBrandTouchPoints = new BrandTouchPoints();
                _MarketingBrand Model = new MarketingManager().GetMarketingBrand();


                if (Model != null)
                {

                    obj.MarketingBrandID = Model.MarketingBrandID;
                    obj.ClientID = Model.ClientID;
                    objBrandingStrategy.EmotionalSellingPoint = Model.EmotionalSellingPoint;
                    objBrandingStrategy.OurAmbition = Model.OurAmbition;
                    objBrandingStrategy.OurPersonality = Model.OurPersonality;
                    objBrandingStrategy.WhatAreWe = Model.WhatAreWe;
                    objBrandingStrategy.WhatDoHowDo = Model.WhatDoHowDo;
                    objBrandingStrategy.WhatMakesDifferent = Model.WhatMakesDifferent;
                    objBrandingStrategy.WhatValueMost = Model.WhatValueMost;
                    objBrandingStrategy.WhoWeHereFor = Model.WhoWeHereFor;
                    objBrandingStrategy.WhyWeHere = Model.WhyWeHere;
                    objBrandGuidelines.PowerPresentationID = Model.PowerPresentationID;
                    objBrandGuidelines.TemplateDownloaded = Model.TemplateDownloaded;
                    objBrandTouchPoints.BrandTouchPointID = Model.BrandTouchPointID;
                    obj.EntityState = EntityState.Old;

                }
                else

                    obj.EntityState = EntityState.New;
                    obj.BrandingStrategy = objBrandingStrategy;
                    obj.BrandGuidelines = objBrandGuidelines;
                    obj.BrandTouchPoints = objBrandTouchPoints;
                 Task.WaitAll(new Task[] { GetModelDependency });
                 return PartialView("_NewMarketingBrandPartial", obj);
            }
            return PartialView("_NewObservationPartial");
        }

        public ActionResult Create()
        {
            Marketing obj = new Marketing();
            MarketingPlan objPlan = new MarketingPlan();
            MarketingBudget objBudget = new MarketingBudget();
            var GetModelDependency = Task.Factory.StartNew(() =>
            {
                GetMarketingPlanDataDependency();

            });
            ViewBag.BuyerPersonaList = new SellingManager().GetCustomerbuyerPersona(Guid.Empty);
            //ViewBag.TitleBuyerPersona = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BuyerPersona))
            //                  .Cast<PropertyDescriptor>()
            //                  .ToDictionary(p => p.Name, p => p.Description);
            //ViewBag.TitleMarketPlan = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketingPlan))
            //           .Cast<PropertyDescriptor>()
            //           .ToDictionary(p => p.Name, p => p.Description);
            //ViewBag.TitleMarketBudget = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketingBudget))
            //           .Cast<PropertyDescriptor>()
            //           .ToDictionary(p => p.Name, p => p.Description);
            //ViewBag.BuyerPersonaList = new SellingManager().GetCustomerbuyerPersona(Guid.Empty);
            //ViewBag.GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals);
            //ViewBag.AudianceReachList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach);
            //ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            //ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            //ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);
            //ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);



            obj.EntityState = EntityState.New;
            obj.MarketingPlan = objPlan;
            obj.MarketingBudget = objBudget;
            Task.WaitAll(new Task[] { GetModelDependency });
            return View("_NewMarketingPlan", obj);
        }

        public ActionResult Edit(string id)
        {
            _Marketing Model = new MarketingManager().GetSingleMarketingPlan(Guid.Parse(id));
            Marketing obj = new Marketing();
            MarketingPlan objPlan = new MarketingPlan();
            MarketingBudget objBudget = new MarketingBudget();
            var GetModelDependency = Task.Factory.StartNew(() =>
            {
                GetMarketingPlanDataDependency();

            });
            ViewBag.BuyerPersonaList = new SellingManager().GetCustomerbuyerPersona(Guid.Empty);
            //ViewBag.TitleBuyerPersona = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BuyerPersona))
            //                  .Cast<PropertyDescriptor>()
            //                  .ToDictionary(p => p.Name, p => p.Description);
            //ViewBag.TitleMarketPlan = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketingPlan))
            //           .Cast<PropertyDescriptor>()
            //           .ToDictionary(p => p.Name, p => p.Description);
            //ViewBag.TitleMarketBudget = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketingBudget))
            //           .Cast<PropertyDescriptor>()
            //           .ToDictionary(p => p.Name, p => p.Description);
            //ViewBag.BuyerPersonaList = new SellingManager().GetCustomerbuyerPersona(Guid.Empty);
            //ViewBag.GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals);
            //ViewBag.AudianceReachList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach);
            //ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            //ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            //ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);
            //ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);

            if (Model != null)
            {
                obj.MarketingPlanID = Model.MarketingPlanID;
                obj.EntityState = EntityState.Old;
                obj.ClientID = Model.ClientID;
                objPlan.AudianceReachID = Model.AudianceReachID;
                objPlan.BuyerPersonaID = Model.BuyerPersonaID;
                objPlan.FindBuyers = Model.FindBuyers;
                objPlan.GoalID = Model.GoalID;
                objPlan.KPIS = Model.KPIS;
                objPlan.PlanName = Model.PlanName;
                objPlan.SellingProposition = Model.SellingProposition;
                objPlan.TrackKPIS = Model.TrackKPIS;
                objBudget.AdvertisingBudget = Model.AdvertisingBudget;
                objBudget.AvgCostPerClick = Model.AvgCostPerClick;
                objBudget.AvgSale = Model.AvgSale;
                objBudget.Clicks = Model.Clicks;
                objBudget.ConversionRate = Model.ConversionRate;
                objBudget.CTR = Model.CTR;
                objBudget.GoodsCostPercentage = Model.GoodsCostPercentage;
                objBudget.GoodsCostSold = Model.GoodsCostSold;
                objBudget.Impressions = Model.Impressions;
                objBudget.Leads = Model.Leads;
                objBudget.ProfitAfterAdvertising = Model.ProfitAfterAdvertising;
                objBudget.ProfitPercentage = Model.ProfitPercentage;
                objBudget.ProfitPerClick = Model.ProfitPerClick;
                objBudget.QualifiedLeadConversionPercentage = Model.QualifiedLeadConversionPercentage;
                objBudget.QualifiedLeads = Model.QualifiedLeads;
                objBudget.QualifiedLeadsPercentage = Model.QualifiedLeadsPercentage;
                objBudget.Revenue = Model.Revenue;
                objBudget.SalesNumber = Model.SalesNumber;
                objBudget.TargetCostPerLead = Model.SalesNumber;


            }
            obj.MarketingPlan = objPlan;
            obj.MarketingBudget = objBudget;
            Task.WaitAll(new Task[] { GetModelDependency });
            return View("_EditMarketingPlan", obj);
        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.Marketing_OnlinePresence == Type)
            {

                _OnlinePresance Model = new MarketingManager().GetOnlinePresance();
                OnlinePresance obj = new OnlinePresance();
                YourWebsite objYourWebsite = new YourWebsite();
                SocialHandles objSocialHandles = new SocialHandles();

                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetOnlinePresenceDataDependency();

                });
                if (Model != null)
                {

                    obj.OnlinePresenceID = Model.OnlinePresenceID;
                    obj.ClientID = Model.ClientID;
                    objYourWebsite.CompetitorSitedislike = Model.CompetitorSitedislike;
                    objYourWebsite.CompetitorSitelike = Model.CompetitorSitelike;
                    objYourWebsite.ContentWebsiteCreatorID = Model.ContentWebsiteCreatorID;
                    objYourWebsite.CTA = Model.CTA;
                    objYourWebsite.HaveRegisteredDomain = Model.HaveRegisteredDomain;
                    objYourWebsite.DomainName = Model.DomainName;
                    objYourWebsite.PutWebpageAdds = Model.PutWebpageAdds;
                    objYourWebsite.SuccessMeasure = Model.SuccessMeasure;
                    objYourWebsite.TrafficAnticipatePerMonthID = Model.TrafficAnticipatePerMonthID;
                    objYourWebsite.UseDomainEmail = Model.UseDomainEmail;
                    objYourWebsite.VisitorsAccomplish = Model.VisitorsAccomplish;
                    objYourWebsite.WantWebsiteAchieveID = Model.WantWebsiteAchieveID;
                    objYourWebsite.WebPageStageID = Model.WebPageStageID;
                    objYourWebsite.WebsitePlanMakeMoney = Model.WebsitePlanMakeMoney;
                    obj.YourWebsite = objYourWebsite;
                    objSocialHandles.Facebook = Model.Facebook;
                    objSocialHandles.Twitter = Model.Twitter;
                    objSocialHandles.Youtube = Model.Youtube;
                    objSocialHandles.TicTok = Model.TicTok;
                    objSocialHandles.Instagram = Model.Instagram;
                    objSocialHandles.Linkedin = Model.Linkedin;
                    obj.SocialHandles = objSocialHandles;
                    obj.EntityState = EntityState.Old;
                    ViewBag.FeatureStatusList = new Master().GetSingleMultipleMaster((int)ModuleName.Marketing, Model.OnlinePresenceID);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.YourWebsite = objYourWebsite;
                    obj.SocialHandles = objSocialHandles;
                }

                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_DetailOnlinePresencePartial", obj);
            }

            if ((int)ModuleSection.Marketing_Brand == Type)
            {


                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetMarketingBrandDataDependency();

                });

                MarketingBrand obj = new MarketingBrand();
                BrandingStrategy objBrandingStrategy = new BrandingStrategy();
                BrandGuidelines objBrandGuidelines = new BrandGuidelines();
                BrandTouchPoints objBrandTouchPoints = new BrandTouchPoints();
                _MarketingBrand Model = new MarketingManager().GetMarketingBrand();


                if (Model != null)
                {

                    obj.MarketingBrandID = Model.MarketingBrandID;
                    obj.ClientID = Model.ClientID;
                    objBrandingStrategy.EmotionalSellingPoint = Model.EmotionalSellingPoint;
                    objBrandingStrategy.OurAmbition = Model.OurAmbition;
                    objBrandingStrategy.OurPersonality = Model.OurPersonality;
                    objBrandingStrategy.WhatAreWe = Model.WhatAreWe;
                    objBrandingStrategy.WhatDoHowDo = Model.WhatDoHowDo;
                    objBrandingStrategy.WhatMakesDifferent = Model.WhatMakesDifferent;
                    objBrandingStrategy.WhatValueMost = Model.WhatValueMost;
                    objBrandingStrategy.WhoWeHereFor = Model.WhoWeHereFor;
                    objBrandingStrategy.WhyWeHere = Model.WhyWeHere;
                    objBrandGuidelines.PowerPresentationID = Model.PowerPresentationID;
                    objBrandGuidelines.TemplateDownloaded = Model.TemplateDownloaded;
                    objBrandTouchPoints.BrandTouchPointID = Model.BrandTouchPointID;
                    obj.EntityState = EntityState.Old;

                }
                else

                    obj.EntityState = EntityState.New;
                obj.BrandingStrategy = objBrandingStrategy;
                obj.BrandGuidelines = objBrandGuidelines;
                obj.BrandTouchPoints = objBrandTouchPoints;
                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_DetailMarketingBrandPartial", obj);
            }

            return PartialView("_DetailObservationPartial");


        }

        public JsonResult GetbuyerPersona(Guid BuyerPersonaID)
        {
            return Json(new BusinessManager().GetbuyerPersona(BuyerPersonaID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBuyerPersonaList(Guid CustomerID)
        {
            return Json(new SellingManager().GetCustomerbuyerPersona(CustomerID), JsonRequestBehavior.AllowGet);

        }

        public string AddOnlinePresence(OnlinePresance Model, List<MultipleMaster> ModelList)
        {

            if (Model.EntityState == EntityState.New)
                Model.OnlinePresenceID = Guid.NewGuid();
            //if (Model.YourWebsite.HaveRegisteredDomain == Guid.Empty || Model.YourWebsite.HaveRegisteredDomain == Guid.Parse("73AC5C0F-52D1-4D93-844B-4852D676BF27"))
                //Model.YourWebsite.DomainName = null;

            return new MarketingManager().AddOnlinePresance(Model, ModelList);
        }

        public JsonResult AddBuyerPersona(BuyerPersona Model)
        {
            Model.BuyerPersonaID = Guid.NewGuid();
            if (Model.CustomerID == Guid.Empty)
                Model.CustomerID = Guid.NewGuid();
            return Json(new { Status = new SellingManager().AddBuyerPersona(Model), CustomerID = Model.CustomerID, BuyerPersonaID = Model.BuyerPersonaID }, JsonRequestBehavior.AllowGet);


        }

        public string AddMarketPlan(Marketing Model)
        {

            if (Model.EntityState == EntityState.New)
                Model.MarketingPlanID = Guid.NewGuid();

            return new MarketingManager().AddMarketingPlan(Model);
        }


        public string AddMarketingBrand(MarketingBrand Model)
        {
            if (Model.EntityState == EntityState.New)
                Model.MarketingBrandID = Guid.NewGuid();
            return new MarketingManager().AddMarketingBrand(Model);
        }

        public JsonResult CalculateMarketBudget(MarketingBudget Model)
        {
            Model.Clicks = Model.Impressions * Model.CTR / 100;
            Model.Leads = (Model.Clicks * Model.ConversionRate) / 100;
            Model.QualifiedLeads = (Model.Leads * Model.QualifiedLeadsPercentage) / 100;
            Model.SalesNumber = (Model.QualifiedLeads * Model.QualifiedLeadConversionPercentage) / 100;
            Model.Revenue = Model.SalesNumber * Model.AvgSale;
            Model.GoodsCostSold = (Model.GoodsCostPercentage * Model.Revenue) / 100;
            Model.AdvertisingBudget = (Model.Clicks * Model.AvgCostPerClick);
            Model.ProfitAfterAdvertising = Model.Revenue - (Model.GoodsCostSold + Model.AdvertisingBudget);
            Model.ProfitPercentage = Model.AdvertisingBudget != 0 ? (Model.ProfitAfterAdvertising / Model.AdvertisingBudget) * 100 : Model.ProfitAfterAdvertising * 100;
            Model.ProfitPerClick = Model.Clicks != 0 ? Model.ProfitAfterAdvertising / Model.Clicks : Model.ProfitAfterAdvertising;
            Decimal BudgetA = Model.Leads != 0 ? Model.AdvertisingBudget / Model.Leads : Model.AdvertisingBudget;
            BudgetA = Model.Leads - BudgetA;
            Model.TargetCostPerLead = BudgetA != 0 ? Model.ProfitAfterAdvertising / BudgetA : Model.ProfitAfterAdvertising;

            // Decimal TargetCostPerLeadA = Model.Leads != 0 ? Model.ProfitAfterAdvertising / Model.Leads : Model.ProfitAfterAdvertising;
            //Decimal TargetCostPerLeadB = Model.Leads != 0 ? Model.AdvertisingBudget / Model.Leads : Model.AdvertisingBudget;
            //Model.TargetCostPerLead = TargetCostPerLeadA - TargetCostPerLeadB;
            return Json(Model, JsonRequestBehavior.AllowGet);
        }

        public string Delete(Guid ID)
        {

            return new MarketingManager().DeleteMarketingPlan(ID);
        }

        public void GetOnlinePresenceDataDependency()
        {
            ViewBag.TitleYourWebsite = TypeDescriptor.GetProperties(typeof(Business_Model.Model.YourWebsite))
                                      .Cast<PropertyDescriptor>()
                                      .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleSocialHandles = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SocialHandles))
                               .Cast<PropertyDescriptor>()
                               .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.StageList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Stage);
            ViewBag.AchieveList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Achieve);
            ViewBag.ContentList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Content);
            ViewBag.TrafficList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Traffic);
            ViewBag.FunctionNecessary = new Master().GetOptionMasterList((int)OptionType.YourWebsite_FunctionNecessary);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.Marketing, (int)ModuleSection.Marketing_OnlinePresence);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_OnlinePresence);
        }

        public void GetMarketingPlanDataDependency()
        {
            ViewBag.TitleBuyerPersona = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BuyerPersona))
                               .Cast<PropertyDescriptor>()
                               .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleMarketPlan = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketingPlan))
                       .Cast<PropertyDescriptor>()
                       .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleMarketBudget = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketingBudget))
                       .Cast<PropertyDescriptor>()
                       .ToDictionary(p => p.Name, p => p.Description);
     
            ViewBag.GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals);
            ViewBag.AudianceReachList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach);
            ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);
        }

        public void GetMarketingBrandDataDependency()
        {

            ViewBag.TitleBrandingStrategy = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BrandingStrategy))
                                   .Cast<PropertyDescriptor>()
                                   .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleBrandGuidelines = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BrandGuidelines))
                               .Cast<PropertyDescriptor>()
                               .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.BrandTouchPointList = new Master().GetOptionMasterList((int)OptionType.Brand_BrandTouchPoint);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.Marketing, (int)ModuleSection.Marketing_Brand);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_Brand);
        }
    }
}