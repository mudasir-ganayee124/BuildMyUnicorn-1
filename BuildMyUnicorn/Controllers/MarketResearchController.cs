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
    public class MarketResearchController : WebController
    {
        // GET: MarketResearch
        public ActionResult Observation(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new MarketResearchManager().ExistObervation(Guid.Empty);
            else
            {
                Guid ObervationID;
                bool isValid = Guid.TryParse(id, out ObervationID);
                if (isValid) Count = new MarketResearchManager().ExistObervation(ObervationID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.MarketResearch;
                objlog.ModuleSectionID = ModuleSection.MarketResearch_Observation;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "MarketResearch",
                        ActionName = "Observation",
                        ModuleID = (int)Module.MarketResearch,
                        SectionID = (int)ModuleSection.MarketResearch_Observation
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation);
            return View(State);


        }

        // GET: MarketResearch
        public ActionResult KeyFinding(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new MarketResearchManager().ExistKeyfinding(Guid.Empty);
            else
            {
                Guid KeyFindingID;
                bool isValid = Guid.TryParse(id, out KeyFindingID);
                if (isValid) Count = new MarketResearchManager().ExistKeyfinding(KeyFindingID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.MarketResearch;
                objlog.ModuleSectionID = ModuleSection.MarketResearch_KeyFindings;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "MarketResearch",
                        ActionName = "KeyFinding",
                        ModuleID = (int)Module.MarketResearch,
                        SectionID = (int)ModuleSection.MarketResearch_KeyFindings
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings);
            return View(State);

        }

        public ActionResult OnlineResearch(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new MarketResearchManager().ExistOnlineResearch(Guid.Empty);
            else
            {
                Guid OnlineResearchID;
                bool isValid = Guid.TryParse(id, out OnlineResearchID);
                if (isValid) Count = new MarketResearchManager().ExistOnlineResearch(OnlineResearchID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.MarketResearch;
                objlog.ModuleSectionID = ModuleSection.MarketResearch_OnlineResearch;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_OnlineResearch) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "MarketResearch",
                        ActionName = "OnlineResearch",
                        ModuleID = (int)Module.MarketResearch,
                        SectionID = (int)ModuleSection.MarketResearch_OnlineResearch
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_OnlineResearch);
            return View(State);


        }


        public ActionResult New(int Type)
        {
            if ((int)ModuleSection.MarketResearch_Observation == Type)
            {
                OurObservation obj = new OurObservation();
                OurObservation Model = new MarketResearchManager().GetObservation();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetObservationDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {


                    if (Model != null)
                    {
                        obj.ObervationID = Model.ObervationID;
                        obj.Observation = Model.Observation;
                        obj.Collection = Model.Collection;
                        obj.AnyPatterns = Model.AnyPatterns;
                        obj.Patterns = Model.Patterns;
                        obj.KeyMoments = Model.KeyMoments;
                        obj.EntityState = EntityState.Old;
                    }
                    else
                        obj.EntityState = EntityState.New;
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewObservationPartial", obj);
            }

            if ((int)ModuleSection.MarketResearch_KeyFindings == Type)
            {
                KeyFinding obj = new KeyFinding();
                _KeyFinding Model = new MarketResearchManager().GetKeyFinding();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetKeyfindingDataDependency();

                });

                var GetModelData = Task.Factory.StartNew(() =>
                {
                    MarketResearchResults objMarketResearchResults = new MarketResearchResults();
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


                });

                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewKeyFindingPartial", obj);
            }

            if ((int)ModuleSection.MarketResearch_OnlineResearch == Type)
            {
                OnlineResearch obj = new OnlineResearch();
                _OnlineResearch Model = new MarketResearchManager().GetOnlineResearch();

                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetMarketResearchDataDependency();

                });

                BigPictureResearch _BigPictureResearch = new BigPictureResearch();
                FocussedResearch _FocussedResearch = new FocussedResearch();
                ViewBag.MarketKeyPlayer = new List<MarketKeyPlayer>();
                if (Model != null)
                {
                    obj.ClientID = Model.ClientID;
                    obj.OnlineResearchID = Model.OnlineResearchID;
                    _BigPictureResearch.CaptureInitially = Model.CaptureInitially;
                    _BigPictureResearch.CaptureShare = Model.CaptureShare;
                    _BigPictureResearch.IndustryTrends = Model.IndustryTrends;
                    _BigPictureResearch.MarketKeyPlayerID = Model.MarketKeyPlayerID;
                    _BigPictureResearch.MarketSize = Model.MarketSize;
                    _BigPictureResearch.MarketShare = Model.MarketShare;
                    _BigPictureResearch.ResearchCarriedOutBit1 = Model.ResearchCarriedOutBit1;
                    _BigPictureResearch.ResearchCarriedOutBit2 = Model.ResearchCarriedOutBit2;
                    _BigPictureResearch.ResearchCarriedOutBit3 = Model.ResearchCarriedOutBit3;
                    _BigPictureResearch.MarketShareCaptureDuration = Model.MarketShareCaptureDuration;
                    _FocussedResearch.CustomerFeedback = Model.CustomerFeedback;
                    _FocussedResearch.FeedbackKeyfinding = Model.FeedbackKeyfinding;
                    _FocussedResearch.FeedbackRate = Model.FeedbackRate;
                    _FocussedResearch.FeedbackReceived = Model.FeedbackReceived;
                    _FocussedResearch.IdeaProgressed = Model.IdeaProgressed;
                    obj.BigPictureResearch = _BigPictureResearch;
                    obj.FocussedResearch = _FocussedResearch;
                    obj.EntityState = EntityState.Old;
                    ViewBag.MarketKeyPlayer = new MarketResearchManager().GetMarketKeyPlayer(Model.OnlineResearchID).ToList();

                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.BigPictureResearch = new BigPictureResearch();
                    obj.FocussedResearch = new FocussedResearch();
                }

                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_NewOnlineResearchPartial", obj);
            }
            return PartialView("_NewObservationPartial");
        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.MarketResearch_Observation == Type)
            {


                OurObservation obj = new OurObservation();
                OurObservation Model = new MarketResearchManager().GetObservation();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetObservationDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {


                    if (Model != null)
                    {
                        obj.ObervationID = Model.ObervationID;
                        obj.Observation = Model.Observation;
                        obj.Collection = Model.Collection;
                        obj.AnyPatterns = Model.AnyPatterns;
                        obj.Patterns = Model.Patterns;
                        obj.KeyMoments = Model.KeyMoments;
                        obj.EntityState = EntityState.Old;
                    }
                    else
                        obj.EntityState = EntityState.New;
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_DetailObservationPartial", obj);
            }
            else if ((int)ModuleSection.MarketResearch_KeyFindings == Type)
            {

                KeyFinding obj = new KeyFinding();
                _KeyFinding Model = new MarketResearchManager().GetKeyFinding();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetKeyfindingDataDependency();

                });

                var GetModelData = Task.Factory.StartNew(() =>
                {
                    MarketResearchResults objMarketResearchResults = new MarketResearchResults();
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


                });

                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });

                return PartialView("_DetailKeyFindingPartial", obj);
            }
            if ((int)ModuleSection.MarketResearch_OnlineResearch == Type)
            {
                OnlineResearch obj = new OnlineResearch();
                _OnlineResearch Model = new MarketResearchManager().GetOnlineResearch();

                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetMarketResearchDataDependency();

                });

                BigPictureResearch _BigPictureResearch = new BigPictureResearch();
                FocussedResearch _FocussedResearch = new FocussedResearch();
                ViewBag.MarketKeyPlayer = new List<MarketKeyPlayer>();
                if (Model != null)
                {
                    obj.ClientID = Model.ClientID;
                    obj.OnlineResearchID = Model.OnlineResearchID;
                    _BigPictureResearch.CaptureInitially = Model.CaptureInitially;
                    _BigPictureResearch.CaptureShare = Model.CaptureShare;
                    _BigPictureResearch.IndustryTrends = Model.IndustryTrends;
                    _BigPictureResearch.MarketKeyPlayerID = Model.MarketKeyPlayerID;
                    _BigPictureResearch.MarketSize = Model.MarketSize;
                    _BigPictureResearch.MarketShare = Model.MarketShare;
                    _BigPictureResearch.ResearchCarriedOutBit1 = Model.ResearchCarriedOutBit1;
                    _BigPictureResearch.ResearchCarriedOutBit2 = Model.ResearchCarriedOutBit2;
                    _BigPictureResearch.ResearchCarriedOutBit3 = Model.ResearchCarriedOutBit3;
                    _BigPictureResearch.MarketShareCaptureDuration = Model.MarketShareCaptureDuration;
                    _FocussedResearch.CustomerFeedback = Model.CustomerFeedback;
                    _FocussedResearch.FeedbackKeyfinding = Model.FeedbackKeyfinding;
                    _FocussedResearch.FeedbackRate = Model.FeedbackRate;
                    _FocussedResearch.FeedbackReceived = Model.FeedbackReceived;
                    _FocussedResearch.IdeaProgressed = Model.IdeaProgressed;
                    obj.BigPictureResearch = _BigPictureResearch;
                    obj.FocussedResearch = _FocussedResearch;
                    obj.EntityState = EntityState.Old;
                    ViewBag.MarketKeyPlayer = new MarketResearchManager().GetMarketKeyPlayer(Model.OnlineResearchID).ToList();

                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.BigPictureResearch = new BigPictureResearch();
                    obj.FocussedResearch = new FocussedResearch();
                }

                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_DetailOnlineResearchPartial", obj);
            }

            return PartialView("_DetailObservationPartial");


        }

        public JsonResult AddObservation(OurObservation Model)
        {

            if (Model.EntityState == EntityState.New)
                Model.ObervationID = Guid.NewGuid();
            return Json(new MarketResearchManager().AddObservation(Model), JsonRequestBehavior.AllowGet);


        }

        public JsonResult AddKeyfinding(KeyFinding Model)
        {

            if (Model.EntityState == EntityState.New)
                Model.KeyFindingID = Guid.NewGuid();
            return Json(new MarketResearchManager().AddKeyFinding(Model), JsonRequestBehavior.AllowGet);

        }

        public JsonResult AddOnlineResearch(OnlineResearch OnlineResearch, List<MarketKeyPlayer> MarketKeyPlayer)
        {
            if (OnlineResearch.EntityState == EntityState.New)
                OnlineResearch.OnlineResearchID = Guid.NewGuid();
            return Json(new MarketResearchManager().AddOnlineResearch(OnlineResearch, MarketKeyPlayer), JsonRequestBehavior.AllowGet);

        }

        public void GetObservationDataDependency()
        {
            ViewBag.TitleObservation = TypeDescriptor.GetProperties(typeof(Business_Model.Model.OurObservation))
                                      .Cast<PropertyDescriptor>()
                                      .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation);

            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation);

        }

        public void GetKeyfindingDataDependency()
        {
            ViewBag.TitleMarketResearchResults = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MarketResearchResults))
                                     .Cast<PropertyDescriptor>()
                                     .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.InterviewKeyFinding = new Master().GetOptionMasterList((int)OptionType.MarketReserach_InterviewKeyFindingConfident);
            ViewBag.ObservationKeyFinding = new Master().GetOptionMasterList((int)OptionType.MarketReserach_ObservationKeyFindingConfident);
            ViewBag.OnlineResearchKeyFinding = new Master().GetOptionMasterList((int)OptionType.MarketReserach_OnlineResearchKeyFindingConfident);
            ViewBag.SurveyKeyFinding = new Master().GetOptionMasterList((int)OptionType.MarketReserach_SurveyKeyFindingConfident);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings);

        }

        public void GetMarketResearchDataDependency()
        {
            ViewBag.FeedbackRate = new Master().GetOptionMasterList((int)OptionType.MarketResearch_FeedBackRate);
            ViewBag.FeedbackReceived = new Master().GetOptionMasterList((int)OptionType.MarketResearch_FeedBack);
            ViewBag.IdeaProgressed = new Master().GetOptionMasterList((int)OptionType.MarketResearch_IdeaProgress);
            ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.IdeaOutofMyHead);
            ViewBag.CustomerFeedback = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_OnlineResearch);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_OnlineResearch);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_OnlineResearch);

        }



    }
}