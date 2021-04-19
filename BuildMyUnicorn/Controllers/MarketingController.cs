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


namespace BuildMyUnicorn.Controllers
{
    public class MarketingController : WebController
    {
        public ActionResult OnlinePresence(string id)
        {
            int State = (int)EntityState.New;
            _OnlinePresance Model = new MarketingManager().GetOnlinePresance();
            if (Model != null && Model.OnlinePresanceID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Marketing, (int)ModuleSection.Marketing_OnlinePresence);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.Marketing_OnlinePresence))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Marketing",
                    ActionName = "OnlinePresence",
                    ModuleName = Module.Marketing.ToString(),
                    SectionName = ModuleSection.Marketing_OnlinePresence.ToString()
                });
            }
            return View(State);
        }

        public ActionResult Marketing()
        {

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);
            IEnumerable<_Marketing> ModelList = new MarketingManager().GetMarketingPlan();
            int State = (int)EntityState.New;
            if (ModelList == null || ModelList.Count() == 0)
            {
                if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_Team))
                {
                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Marketing",
                        ActionName = "Marketing",
                        ModuleName = Module.Marketing.ToString(),
                        SectionName = ModuleSection.Marketing_MarketingPlan.ToString()
                    });
                }

            }
            return View(ModelList);
        }

        public ActionResult Brand(Guid? MarketingBrandID)
        {
            int State = (int)EntityState.New;
            _MarketingBrand Model = new MarketingManager().GetMarketingBrand();
            if (Model != null && Model.MarketingBrandID != MarketingBrandID) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Marketing, (int)ModuleSection.Marketing_Brand);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.Marketing_MarketingPlan))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Marketing",
                    ActionName = "Brand",
                    ModuleName = Module.Marketing.ToString(),
                    SectionName = ModuleSection.Marketing_MarketingPlan.ToString()
                });
            }
            return View("MarketingBrand", State);
        }
      
        public ActionResult New(int Type)
        {

            if ((int)ModuleSection.Marketing_OnlinePresence == Type)
            {


                ViewBag.TitleYourWebsite = TypeDescriptor.GetProperties(typeof(Business_Model.Model.YourWebsite))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleSocialHandles = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SocialHandles))
                                   .Cast<PropertyDescriptor>()
                                   .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.StageList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Stage);
                ViewBag.AchieveList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Achieve);
                ViewBag.ContentList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Content);
                ViewBag.TrafficList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Traffic);
                ViewBag.FunctionNecessary = new Master().GetOptionMasterList((int)OptionType.YourWebsite_FunctionNecessary);

                OnlinePresance obj = new OnlinePresance();
                YourWebsite objYourWebsite = new YourWebsite();
                SocialHandles objSocialHandles = new SocialHandles();
                _OnlinePresance Model = new MarketingManager().GetOnlinePresance();


                if (Model != null)
                {

                    obj.OnlinePresanceID = Model.OnlinePresanceID;
                    obj.ClientID = Model.ClientID;
                    objYourWebsite.CompetitorSitedislike = Model.CompetitorSitedislike;
                    objYourWebsite.CompetitorSitelike = Model.CompetitorSitelike;
                    objYourWebsite.ContentWebsiteCreatorID = Model.ContentWebsiteCreatorID;
                    objYourWebsite.CTA = Model.CTA;
                    objYourWebsite.HaveRegisteredDomain = Model.HaveRegisteredDomain;
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
                    ViewBag.FeatureStatusList = new Master().GetSingleMultipleMaster((int)ModuleName.Marketing, Model.OnlinePresanceID);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.YourWebsite = objYourWebsite;
                    obj.SocialHandles = objSocialHandles;
                }


                return PartialView("_NewOnlinePresencePartial", obj);
            }
            if ((int)ModuleSection.Marketing_MarketingPlan == Type)
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
                ViewBag.BuyerPersonaList = new BusinessManager().GetCustomerbuyerPersona(Guid.Empty);
                ViewBag.GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals);
                ViewBag.AudianceReachList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach);
                ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
                ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
                ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);

                Marketing obj = new Marketing();
                MarketingPlan objPlan = new MarketingPlan();
                MarketingBudget objBudget = new MarketingBudget();
             
      
                obj.EntityState = EntityState.New;
                obj.MarketingPlan = objPlan;
                obj.MarketingBudget = objBudget;

                return PartialView("_NewMarketingPlanPartial", obj);
            }
            if ((int)ModuleSection.Marketing_Brand == Type)
            {


                ViewBag.TitleBrandingStrategy = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BrandingStrategy))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleBrandGuidelines = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BrandGuidelines))
                                   .Cast<PropertyDescriptor>()
                                   .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.BrandTouchPointList = new Master().GetOptionMasterList((int)OptionType.Brand_BrandTouchPoint);
 

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
                   return PartialView("_NewMarketingBrandPartial", obj);
            }
            return PartialView("_NewObservationPartial");
        }

        public ActionResult Create()
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
            ViewBag.BuyerPersonaList = new BusinessManager().GetCustomerbuyerPersona(Guid.Empty);
            ViewBag.GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals);
            ViewBag.AudianceReachList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach);
            ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);

            Marketing obj = new Marketing();
            MarketingPlan objPlan = new MarketingPlan();
            MarketingBudget objBudget = new MarketingBudget();


            obj.EntityState = EntityState.New;
            obj.MarketingPlan = objPlan;
            obj.MarketingBudget = objBudget;
            return View("_NewMarketingPlan", obj);
        }

        public ActionResult Edit(string id)
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
            ViewBag.BuyerPersonaList = new BusinessManager().GetCustomerbuyerPersona(Guid.Empty);
            ViewBag.GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals);
            ViewBag.AudianceReachList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach);
            ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);
            _Marketing Model= new MarketingManager().GetSingleMarketingPlan(Guid.Parse(id));
            Marketing obj = new Marketing();
            MarketingPlan objPlan = new MarketingPlan();
            MarketingBudget objBudget = new MarketingBudget();
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
            return View("_EditMarketingPlan", obj);
        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.Marketing_OnlinePresence == Type)
            {


                ViewBag.TitleYourWebsite = TypeDescriptor.GetProperties(typeof(Business_Model.Model.YourWebsite))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.TitleSocialHandles = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SocialHandles))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.StageList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Stage);
                ViewBag.AchieveList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Achieve);
                ViewBag.ContentList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Content);
                ViewBag.TrafficList = new Master().GetOptionMasterList((int)OptionType.YourWebsite_Traffic);
                ViewBag.FunctionNecessary = new Master().GetOptionMasterList((int)OptionType.YourWebsite_FunctionNecessary);
                OnlinePresance obj = new OnlinePresance();
                YourWebsite objYourWebsite = new YourWebsite();
                SocialHandles objSocialHandles = new SocialHandles();
                _OnlinePresance Model = new MarketingManager().GetOnlinePresance();

                if (Model != null)
                {
                    obj.OnlinePresanceID = Model.OnlinePresanceID;
                    obj.ClientID = Model.ClientID;
                    objYourWebsite.CompetitorSitedislike = Model.CompetitorSitedislike;
                    objYourWebsite.CompetitorSitelike = Model.CompetitorSitelike;
                    objYourWebsite.ContentWebsiteCreatorID = Model.ContentWebsiteCreatorID;
                    objYourWebsite.CTA = Model.CTA;
                    objYourWebsite.HaveRegisteredDomain = Model.HaveRegisteredDomain;
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
                    ViewBag.FeatureStatusList = new Master().GetSingleMultipleMaster((int)ModuleName.Marketing, Model.OnlinePresanceID);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.YourWebsite = objYourWebsite;
                    obj.SocialHandles = objSocialHandles;
                }


                return PartialView("_DetailOnlinePresencePartial", obj);
            }

            if ((int)ModuleSection.Marketing_Brand == Type)
            {


                ViewBag.TitleBrandingStrategy = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BrandingStrategy))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleBrandGuidelines = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BrandGuidelines))
                                   .Cast<PropertyDescriptor>()
                                   .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.BrandTouchPointList = new Master().GetOptionMasterList((int)OptionType.Brand_BrandTouchPoint);


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
                    objBrandingStrategy.Completed += objBrandingStrategy.EmotionalSellingPoint == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.OurAmbition == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.OurPersonality == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.WhatAreWe == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.WhatDoHowDo == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.WhatMakesDifferent == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.WhatValueMost == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.WhoWeHereFor == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed += objBrandingStrategy.WhyWeHere == null ? 0.00m : 11.11m;
                    objBrandingStrategy.Completed = Math.Round(objBrandingStrategy.Completed);
                    objBrandGuidelines.PowerPresentationID = Model.PowerPresentationID;
                    objBrandGuidelines.TemplateDownloaded = Model.TemplateDownloaded;
                    objBrandGuidelines.Completed = objBrandGuidelines.TemplateDownloaded == true ? 100.00m : 0.00m;
                    objBrandTouchPoints.BrandTouchPointID = Model.BrandTouchPointID;
                    objBrandTouchPoints.Completed = objBrandTouchPoints.BrandTouchPointID == null ? 0.00m : 100.00m;
                    obj.EntityState = EntityState.Old;

                }
                else
                
                    obj.EntityState = EntityState.New;
                    obj.BrandingStrategy = objBrandingStrategy;
                    obj.BrandGuidelines = objBrandGuidelines;
                    obj.BrandTouchPoints = objBrandTouchPoints;
                


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
            return Json(new BusinessManager().GetCustomerbuyerPersona(CustomerID), JsonRequestBehavior.AllowGet);

        }
        public string AddOnlinePresence(OnlinePresance Model, List<MultipleMaster> ModelList)
        {

            if (Model.EntityState == EntityState.New)
                Model.OnlinePresanceID = Guid.NewGuid();

            return new MarketingManager().AddOnlinePresance(Model, ModelList);
        }


        public JsonResult AddBuyerPersona(BuyerPersona Model)
        {
            Model.BuyerPersonaID = Guid.NewGuid();
            if (Model.CustomerID == Guid.Empty)
                Model.CustomerID = Guid.NewGuid();
            return Json(new { Status = new BusinessManager().AddBuyerPersona(Model), CustomerID = Model.CustomerID, BuyerPersonaID = Model.BuyerPersonaID }, JsonRequestBehavior.AllowGet);


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

        public string CheckModuleCourse(int State, int SectionValue)
        {
            if (State == 0)
            {
                string getValue = "0";
                string getClientID = string.Empty;
                string LoginUserID = User.Identity.Name.ToString();
                string SectionName = Enum.GetName(typeof(ModuleSection), SectionValue);
                string CookieID = SectionName.ToString() + LoginUserID;
                if (Request.Cookies[CookieID.ToString()] != null)
                {
                    HttpCookie aCookie = Request.Cookies[CookieID.ToString()];
                    getValue = aCookie.Values["Status"];
                }
                else
                {
                    HttpCookie appCookie = new HttpCookie(CookieID.ToString());
                    appCookie.Values["Status"] = "0";
                    appCookie.Values["ClientID"] = User.Identity.Name.ToString();
                    appCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(appCookie);
                }
                ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.Marketing, SectionValue);

                if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                    return ResponseType.Redirect.ToString();
                else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }

        public JsonResult CalculateMarketBudget(MarketingBudget Model)
        {
            Model.Clicks = Model.Impressions * Model.CTR / 100;
            Model.Leads = (Model.Clicks * Model.ConversionRate) / 100;
            Model.QualifiedLeads = (Model.Leads * Model.QualifiedLeadsPercentage)/100 ;
            Model.SalesNumber = (Model.QualifiedLeads * Model.QualifiedLeadConversionPercentage) / 100;
            Model.Revenue = Model.SalesNumber * Model.AvgSale;
            Model.GoodsCostSold = (Model.GoodsCostPercentage * Model.Revenue)/100;
            Model.AdvertisingBudget = (Model.Clicks * Model.AvgCostPerClick);
            Model.ProfitAfterAdvertising = Model.Revenue - (Model.GoodsCostSold + Model.AdvertisingBudget);
            Model.ProfitPercentage  = Model.AdvertisingBudget != 0 ?  (Model.ProfitAfterAdvertising / Model.AdvertisingBudget) * 100 : Model.ProfitAfterAdvertising * 100;
            Model.ProfitPerClick = Model.Clicks != 0 ? Model.ProfitAfterAdvertising/ Model.Clicks : Model.ProfitAfterAdvertising;
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
    }
}