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
    public class MarketResearchController : WebController
    {
        // GET: MarketResearch
        public ActionResult Observation(string id)
        {
            int State = (int)EntityState.New;
            OurObservation Model = new MarketResearchManager().GetObservation();
            if (Model != null && Model.ObervationID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.MarketResearch_Observation))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "MarketResearch",
                    ActionName = "Observation",
                    ModuleName = Module.MarketResearch.ToString(),
                    SectionName = ModuleSection.MarketResearch_Observation.ToString()
                });
            }
            return View(State);
        }

        // GET: MarketResearch
        public ActionResult KeyFinding(string id)
        {
            int State = (int)EntityState.New;
            _KeyFinding1 Model = new MarketResearchManager().GetKeyFinding();
            if (Model != null && Model.KeyFindingID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.MarketResearch_KeyFindings))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "MarketResearch",
                    ActionName = "KeyFinding",
                    ModuleName = Module.MarketResearch.ToString(),
                    SectionName = ModuleSection.MarketResearch_KeyFindings.ToString()
                });
            }
            return View(State);
        }

        

        public ActionResult New(int Type)
        {
            if ((int)ModuleSection.MarketResearch_Observation == Type)
            {


                ViewBag.TitleObservation = TypeDescriptor.GetProperties(typeof(Business_Model.Model.OurObservation))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                OurObservation obj = new OurObservation();
                OurObservation Model = new MarketResearchManager().GetObservation();

                if (Model != null)
                {
                    obj.ObervationID = Model.ObervationID;
                    obj.Observation = Model.Observation;
                    obj.Collection = Model.Collection;
                    obj.Patterns = Model.Patterns;
                    obj.KeyMoments = Model.KeyMoments;
                    obj.EntityState = EntityState.Old;
                }
                else
                    obj.EntityState = EntityState.New;


                return PartialView("_NewObservationPartial", obj);
            }

            if ((int)ModuleSection.MarketResearch_KeyFindings == Type)
            {


                ViewBag.TitleMarketResearchResults = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketResearchResults))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                KeyFinding1 obj = new KeyFinding1();
                MarketResearchResults objMarketResearchResults = new MarketResearchResults();
                _KeyFinding1 Model = new MarketResearchManager().GetKeyFinding();

                if (Model != null)
                {
                    obj.KeyFindingID = Model.KeyFindingID;
                    obj.ClientID = Model.ClientID;
                    objMarketResearchResults.InterviewKeyFinding = Model.InterviewKeyFinding;
                    objMarketResearchResults.InterviewKeyFindingConfident = Model.InterviewKeyFindingConfident;
                    objMarketResearchResults.ObservationKeyFinding = Model.ObservationKeyFinding;
                    objMarketResearchResults.ObservationKeyFindingConfident = Model.ObservationKeyFindingConfident;
                    objMarketResearchResults.OnlineResearchKeyFinding = Model.OnlineResearchKeyFinding;
                    objMarketResearchResults.OnlineResearchKeyFindingConfident = Model.OnlineResearchKeyFindingConfident;
                    objMarketResearchResults.SurveyKeyFinding = Model.SurveyKeyFinding;
                    objMarketResearchResults.SurveyKeyFindingConfident = Model.SurveyKeyFindingConfident;
                    obj.MarketResearchResults = objMarketResearchResults;
                    obj.EntityState = EntityState.Old;
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.MarketResearchResults = objMarketResearchResults;
                }


                return PartialView("_NewKeyFindingPartial", obj);
            }
            return PartialView("_NewObservationPartial");
        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.MarketResearch_Observation == Type)
            {


                ViewBag.TitleObservation = TypeDescriptor.GetProperties(typeof(Business_Model.Model.OurObservation))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                OurObservation obj = new OurObservation();
                OurObservation Model = new MarketResearchManager().GetObservation();

                if (Model != null)
                {
                    obj.ObervationID = Model.ObervationID;
                    obj.Observation = Model.Observation;
                    obj.Collection = Model.Collection;
                    obj.Patterns = Model.Patterns;
                    obj.KeyMoments = Model.KeyMoments;
                    obj.EntityState = EntityState.Old;
                }
                else
                    obj.EntityState = EntityState.New;


                return PartialView("_DetailObservationPartial", obj);
            }
            else if ((int)ModuleSection.MarketResearch_KeyFindings == Type)
            {


                ViewBag.TitleMarketResearchResults = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketResearchResults))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                KeyFinding1 obj = new KeyFinding1();
                MarketResearchResults objMarketResearchResults = new MarketResearchResults();
                _KeyFinding1 Model = new MarketResearchManager().GetKeyFinding();

                if (Model != null)
                {
                    obj.KeyFindingID = Model.KeyFindingID;
                    obj.ClientID = Model.ClientID;
                    objMarketResearchResults.InterviewKeyFinding = Model.InterviewKeyFinding;
                    objMarketResearchResults.InterviewKeyFindingConfident = Model.InterviewKeyFindingConfident;
                    objMarketResearchResults.ObservationKeyFinding = Model.ObservationKeyFinding;
                    objMarketResearchResults.ObservationKeyFindingConfident = Model.ObservationKeyFindingConfident;
                    objMarketResearchResults.OnlineResearchKeyFinding = Model.OnlineResearchKeyFinding;
                    objMarketResearchResults.OnlineResearchKeyFindingConfident = Model.OnlineResearchKeyFindingConfident;
                    objMarketResearchResults.SurveyKeyFinding = Model.SurveyKeyFinding;
                    objMarketResearchResults.SurveyKeyFindingConfident = Model.SurveyKeyFindingConfident;
                    obj.MarketResearchResults = objMarketResearchResults;
                    obj.EntityState = EntityState.Old;
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.MarketResearchResults = objMarketResearchResults;
                }


                return PartialView("_DetailKeyFindingPartial", obj);
            }
            return PartialView("_DetailObservationPartial");


        }

        public string AddObservation(OurObservation Model)
        {

            if (Model.EntityState == EntityState.New)
               Model.ObervationID = Guid.NewGuid();
             return new MarketResearchManager().AddObservation(Model);
        }

        public string AddKeyfinding(KeyFinding1 Model)
        {

            if (Model.EntityState == EntityState.New)
                Model.KeyFindingID = Guid.NewGuid();
            return new MarketResearchManager().AddKeyFinding(Model);
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
                ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.TheBusiness, SectionValue);

                if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                    return ResponseType.Redirect.ToString();
                else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }
    }
}