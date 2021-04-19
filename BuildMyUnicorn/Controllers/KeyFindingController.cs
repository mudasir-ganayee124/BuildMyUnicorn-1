using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class KeyFindingController : WebController
    {
        public ActionResult Index(string id)
        {
            int State = (int)EntityState.New;
            _KeyFinding Model = new KeyfindingManager().GetKeyFinding();
            if (Model != null && Model.KeyFindingID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSingleModuleVideo((int)ModuleName.KeyFinding);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.MarketResearch_KeyFindings))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "KeyFinding",
                    ActionName = "Index",
                    ModuleName = Module.MarketResearch.ToString(),
                    SectionName = ModuleSection.MarketResearch_KeyFindings.ToString()
                });
            }
          
            return View(State);
        }
       
        public ActionResult New()
        {
            ViewBag.CustomerFeedback = new List<BusinessCost>
            {
                    new BusinessCost { ID=1, Value="Yes"},
                    new BusinessCost { ID=0, Value="No"}

            };
            ViewBag.FeedbackRate = new List<BusinessCost>
            {
                    new BusinessCost { ID=1, Value="1"},
                    new BusinessCost { ID=2, Value="2"},
                    new BusinessCost { ID=3, Value="3"},
                    new BusinessCost { ID=4, Value="4"},
                    new BusinessCost { ID=5, Value="5"}

            };
            ViewBag.FeedbackReceived = new Master().GetOptionMasterList((int)OptionType.FeedBack);
            ViewBag.IdeaProgressed = new Master().GetOptionMasterList((int)OptionType.IdeaProgress);
            ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.IdeaOutofMyHead);
            KeyFinding obj = new KeyFinding();
            _KeyFinding Model = new KeyfindingManager().GetKeyFinding();
            BigPictureResearch _BigPictureResearch = new BigPictureResearch();
            FocussedResearch _FocussedResearch = new FocussedResearch();
            ViewBag.MarketKeyPlayer = new List<MarketKeyPlayer>();
            if (Model != null)
            {
                obj.ClientID = Model.ClientID;
                obj.KeyFindingID = Model.KeyFindingID;
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
                
                ViewBag.MarketKeyPlayer = new KeyfindingManager().GetMarketKeyPlayer(Model.KeyFindingID).ToList();
                ViewBag.ActionType = "UPDATE";
            }
            else
            {
            
                obj.BigPictureResearch = new BigPictureResearch();
                obj.FocussedResearch = new FocussedResearch();
            }
            return PartialView("_NewPartial", obj);
          

        }

        public ActionResult Detail()
        {


            ViewBag.CustomerFeedback = new List<BusinessCost>
            {
                    new BusinessCost { ID=1, Value="Yes"},
                    new BusinessCost { ID=0, Value="No"}

            };
            ViewBag.FeedbackRate = new List<BusinessCost>
            {
                    new BusinessCost { ID=1, Value="1"},
                    new BusinessCost { ID=2, Value="2"},
                    new BusinessCost { ID=3, Value="3"},
                    new BusinessCost { ID=4, Value="4"},
                    new BusinessCost { ID=5, Value="5"}

            };
            ViewBag.FeedbackReceived = new Master().GetOptionMasterList((int)OptionType.FeedBack);
            ViewBag.IdeaProgressed = new Master().GetOptionMasterList((int)OptionType.IdeaProgress);
            _KeyFinding Model = new KeyfindingManager().GetKeyFinding();
            ViewBag.MarketKeyPlayer = new KeyfindingManager().GetMarketKeyPlayer(Model.KeyFindingID).ToList();
             KeyFinding obj = new KeyFinding();
             BigPictureResearch _BigPictureResearch = new BigPictureResearch();
             FocussedResearch _FocussedResearch = new FocussedResearch();
          
           
                obj.ClientID = Model.ClientID;
               
                obj.KeyFindingID = Model.KeyFindingID;
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

            obj.BigPictureResearch.Completed += obj.BigPictureResearch.CaptureInitially == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.CaptureShare == 0 ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.IndustryTrends == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.MarketKeyPlayerID == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.MarketSize == 0 ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.MarketShare == 0 ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.MarketShareCaptureDuration == 0 ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.ResearchCarriedOutBit1 == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.ResearchCarriedOutBit2 == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.ResearchCarriedOutBit3 == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed += obj.BigPictureResearch.ResearchCarriedOutBit1 == null ? 0.00m : 9.09m;
            obj.BigPictureResearch.Completed = Math.Round(obj.BigPictureResearch.Completed);

            obj.FocussedResearch.Completed += obj.FocussedResearch.CustomerFeedback == 0 ? 0.00m : 25.00m;
            obj.FocussedResearch.Completed += obj.FocussedResearch.FeedbackKeyfinding == null ? 0.00m : 25.00m;
            obj.FocussedResearch.Completed += obj.FocussedResearch.FeedbackRate == null ? 0.00m : 25.00m;
            obj.FocussedResearch.Completed += obj.FocussedResearch.FeedbackReceived == null ? 0.00m : 25.00m;
            obj.FocussedResearch.Completed = Math.Round(obj.FocussedResearch.Completed);
            return PartialView("_DetailPartial", obj);

        }

        public string Add(KeyFinding KeyfindingModel, List<MarketKeyPlayer> MarketKeyPlayer)
        {

            return new KeyfindingManager().AddNewKeyFinding(KeyfindingModel, MarketKeyPlayer);
        }

        public string Update(KeyFinding KeyfindingModel, List<MarketKeyPlayer> MarketKeyPlayer)
        {

            return new KeyfindingManager().UpdateKeyFinding(KeyfindingModel, MarketKeyPlayer);
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