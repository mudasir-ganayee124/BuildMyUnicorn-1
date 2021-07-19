using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Security;

namespace BuildMyUnicorn.Controllers
{
    public class RecommendDocumentationController : WebController
    {
        // GET: RecommendDocumentation
        public ActionResult Index()
        {
            ViewBag.RecommendDocumentSupplier = new Master().GetRecommendedDocumentSupplierList();
            ViewBag.RecommendDocument = new Master().GetAllRecommendDocument();
            return View();
        }
        public ActionResult Packages(string id)
        {
            ViewBag.SupplierID = id;
            ViewBag.Packages = new Master().GetAllSupplierPackages(Guid.Parse(id)) ;
            return View();
        }

        public ActionResult Questions(string id)
        {

            ViewBag.question = new Master().GetQuestionForm(Guid.Parse(id));
            if (ViewBag.question == null)
            {
                ViewBag.question = new SupplierQuestion();
                ViewBag.question.QuestionForm = "[]";
            }
            return View();
        }

        public string AddQuestionData(string id)
        {
          
            Stream req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            string jsonData = new StreamReader(req).ReadToEnd();
            dynamic dyn = JsonConvert.DeserializeObject(jsonData);
            string JsonData = Convert.ToString(dyn.jsonData);
            List<SurveyData> SurveyDataList = new List<SurveyData>();
           
            foreach (var item in dyn.formData)
            {//displayValue

                string key = Convert.ToString(item.title);
                string value = Convert.ToString(item.value);
                SurveyDataList.Add(new SurveyData() { KeyField = key, KeyValue = value });
               
            }

            new ClientManager().AddSurveyData(SurveyDataList, Guid.Parse(id), JsonData);
            return "OK";
           
        }

        public async Task<JsonResult> AddPackageOrder(string id)
        {
            Client Model = new Client();
           // Client Model = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
            if (Model != null)
            { 
                   
                    string CustomerID = await new ClientManager().AddCustomerinGateway(Model);
                    string PublicId = await new ClientManager().AddOrderinGateway(Model, CustomerID);

                    Order OrderObj = new Order();
                    OrderObj.OrderID = Guid.NewGuid();
                    OrderObj.ClientID = Model.ClientID;
                    OrderObj.OrderStatus = OrderStatus.Pending;
                    OrderObj.Order_ID = Keygen.Random();
                    OrderObj.PlanID = Model.PlanID;
                    OrderObj.OrderType = OrderType.Package;
                    OrderObj.GatewayClientID = Guid.Parse(CustomerID);
                    OrderObj.GatewayOrderID = Guid.Parse(PublicId);
                    OrderObj.OrderPublicID = Guid.Parse(PublicId);
                    Session.Add("OrderID", OrderObj.OrderID);
                    new ClientManager().AddNewOrder(OrderObj);
                    Order order = new ClientManager().GetClientOrder(Model.ClientID);
                    return Json(new { status = "SUCCESS", data = order }, JsonRequestBehavior.AllowGet);
                 }
                 else
                   return Json(new { status = "FAILED", msg = "Check the log" }, JsonRequestBehavior.AllowGet);

            //}
            //else
            //    return Json(new { status = "FAILED", msg = "The " + Model.Email.ToString() + " already exist in the system" }, JsonRequestBehavior.AllowGet);

        }

    }
}