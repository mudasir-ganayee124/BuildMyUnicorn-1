using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class ManageMasterController : Controller
    {
        // GET: ManageMaster
        public ActionResult Index(string id)
        {
          
            return View();
        }

        public ActionResult Manage(string id)
        {
            ViewBag.MasterType = id;
            ViewBag.Label =  GetLabel(int.Parse(id));
            return View();
        }


        public ActionResult Type(string id)
        {
            string MasterType = id;
            return View();
        }

        public ActionResult IdeaProgress()
        {
            return View();
        }
        public ActionResult FeedBack()
        {

            return View();
        }

        public ActionResult CompanyLegalStructure()
        {

            return View();
        }

        public ActionResult ProductServiceProduce()
        {

            return View();
        }

        public ActionResult MVPReason()
        {

            return View();
        }

        public ActionResult ManageDevelopment()
        {
            return View();
        }

        public JsonResult GetAll(int Type)
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> list = obj.GetOptionMasterList(Type);
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(Guid ID, int Type)
        {
            return Json(new Master().GetSingleOptionMaster(Type, ID), JsonRequestBehavior.AllowGet);
        }

        public string Add(Option Model)
        {
            
            return new Master().AddNewOptionMaster(Model);
        }

        public string Update(Option Model)
        {
          
            return new Master().UpdateOptionMaster(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteOptionMaster(ID);
        }

        public string GetLabel(int id)
        {
            switch (id)
            {
                case 1:
                    return "Work Location";
                case 2:
                    return "Business Placement";
                case 3:
                    return "Charge";
                case 4:
                    return "Money Raise";
                case 5:
                    return "Selling";
                case 6:
                    return "Startup";
                case 7:
                    return "Technology";
                case 8:
                    return "Company Type";
                case 9:
                    return "Affiliate Rules";
                case 10:
                    return "Idea Progress";
                case 11:
                    return "Feedback";
                case 12:
                    return "Role in Company";
                case 13:
                    return "Company Legal Structure";
                case 14:
                    return "Selling Type";
                case 15:
                    return "Product Service Selling";
                case 16:
                    return "Product Service Produce";
                case 17:
                    return "Reason";
                case 18:
                    return "Product Service Produced";
                case 19:
                    return "Delivery Method";
                case 20:
                    return "Third Pasrties Invloved";
                case 21:
                    return "Payment Method";
                case 22:
                    return "Staff Work";
                case 23:
                    return "Age";
                case 24:
                    return "Income";
                case 25:
                    return "Gender";
                case 26:
                    return "Stage";
                case 27:
                    return "Achieve";
                case 28:
                    return "Content";
                case 29:
                    return "Traffic";
                case 30:
                    return "Function Necessary";
                case 31:
                    return "Goals";
                case 32:
                    return "Audience Reach";
                case 33:
                    return "Brand Touch Point";
                case 34:
                    return "Development Far";
                case 35:
                    return "Personal Survive Budget Expenses";
                case 36:
                    return "Personal Survive Budget Income";
                case 37:
                    return "Cash Flow Forecast Income";
                case 38:
                    return "Cash Flow Forecast Expenditure";
                case 39:
                    return "";
                case 40:
                    return "Product Demand";
                case 41:
                    return "In Market";
                case 42:
                    return "Scalable";
                case 43:
                    return "End Goal";
                case 44:
                    return "Support Technically";
                case 45:
                    return "Build It";
                case 46:
                    return "Selling To";
                case 47:
                    return "Plan Sales Staff";
                case 48:
                    return "Business Cost Launch";
                case 49:
                    return "Feedback Rate";
                case 50:
                    return "Interview Keyfing";
                case 51:
                    return "Observation Keyfinding";
                case 52:
                    return "Survey Keyfinding";
                case 53:
                    return "Online Research Keyfinding";
             
            }
            return "";

        }

    }
}