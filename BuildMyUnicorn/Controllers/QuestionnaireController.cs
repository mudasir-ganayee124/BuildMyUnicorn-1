using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using System.IO;
using Newtonsoft.Json;
using Business_Model.Helper;

namespace BuildMyUnicorn.Controllers
{
    public class QuestionnaireController : WebController
    {
        // GET: Questionnaire
        public ActionResult Index()
        {
            // string surveyID = "2022fc15-8e34-4216-9271-995e27cd2fc0";
            //Survey obj = new ClientManager().GetClientSurveyForm(Guid.Parse(surveyID));
            int State = (int)EntityState.New;
            IEnumerable<Survey> SurveyList = new ClientManager().GetClientAllSurveyForm();
            if (SurveyList == null || SurveyList.Count() == 0)
            {
                if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.MarketResearch_Survey))
                {
                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Questionnaire",
                        ActionName = "Index",
                        ModuleName = Module.MarketResearch.ToString(),
                        SectionName = ModuleSection.MarketResearch_Survey.ToString()
                    });
                }

            }
            return View();
        }

        public ActionResult Create(string id)
        {
            Master obj = new Master();

            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.Template = obj.GetSingleSurveyTemplate(Guid.Parse(id));
                ViewBag.Title = ViewBag.Template.Title;
                ViewBag.Template = ViewBag.Template.Template;
            }

            else
            {
                ViewBag.Title = null;
                ViewBag.Template = "[]";
            }
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Survey);
            return View();
        }


        public ActionResult QuestionnaireList()
        {
            return PartialView("_QuestionnairePartial", new ClientManager().GetClientAllSurveyForm());
        }

        public ActionResult GetSurveyData(string id)
        {
            IEnumerable<SurveyData> modelList = new ClientManager().GetSurveyData(Guid.Parse(id));
            return View("SurveyData", modelList);
        }

        public ActionResult SurveyAnalytics(string id)
        {
            ViewBag.SurveyAnalyticsForm = new ClientManager().GetClientSurveyForm(Guid.Parse(id));
            ViewBag.SurveyData = new ClientManager().GetSurveyData(Guid.Parse(id));
            return View("SurveyAnalytics");
        }

        public ActionResult SurveyTemplates(string id)
        {
            Master obj = new Master();
            if (string.IsNullOrEmpty(id))
            {

                ViewBag.SurveyTemplate = obj.GetAllSurveyTemplates();
                return View();
            }
            else
            {
                ViewBag.Template = obj.GetSingleSurveyTemplate(Guid.Parse(id));
                return View("SurveyTemplatesDetail");
            }
        }

        public ActionResult GetSurveyTemplate(string id)
        {
            Master obj = new Master();
            ViewBag.SurveyTemplate = obj.GetSingleSurveyTemplate(Guid.Parse(id));
            return View("Form");
        }


        //public JsonResult GetClientIdeaProgressData()
        //{
        //    string surveyID = "2022fc15-8e34-4216-9271-995e27cd2fc0";

        //    return Json(new ClientManager().GetClientSurveyForm(Guid.Parse(surveyID)), JsonRequestBehavior.AllowGet);
        //}

        public string AddSurvey(Survey Model)
        {
            return new ClientManager().AddClientSurvey(Model);
        }

        public string EditSurveyStatus(string SurveyID)
        {
            return new ClientManager().UpdateSurveyStatus(Guid.Parse(SurveyID));
        }

        public string DeleteSurvey(string surveyID)
        {
            return new ClientManager().DeleteSurvey(Guid.Parse(surveyID));
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
                return ResponseType.NotRedirect.ToString();
                //  ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.MarketResearch, SectionValue);

                //if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                //    return ResponseType.Redirect.ToString();
                //else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }

    }
}