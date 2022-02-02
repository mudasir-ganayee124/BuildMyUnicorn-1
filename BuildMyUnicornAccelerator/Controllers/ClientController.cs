using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicornAccelerator.Business_Layer;
using Business_Model.Model;

namespace BuildMyUnicornAccelerator.Controllers
{
    public class ClientController : WebController
    {
        // GET: Client
        public ActionResult Index(string id)
        {
            ViewBag.ClientID = id;

       
            ViewBag.Client  = new ClientManager().GetClient(Guid.Parse(id));
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.Client = new ClientManager().GetClient(Guid.Parse(id));
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.ProgressIdea = new Master().GetModuleTotalProgress(Module.MyIdea, Guid.Parse(id));
            ViewBag.ProgressMarketResearch = new Master().GetModuleTotalProgress(Module.MarketResearch, Guid.Parse(id)); //new Master().GetModuleTotalProgress(Module.MarketResearch);
            ViewBag.ProgressBusiness = new Master().GetModuleTotalProgress(Module.TheBusiness, Guid.Parse(id));
            ViewBag.ProgressSelling = new Master().GetModuleTotalProgress(Module.Selling, Guid.Parse(id));
            ViewBag.ProgressFinance = new Master().GetModuleTotalProgress(Module.Finance, Guid.Parse(id));
            ViewBag.ProgressMarketing = new Master().GetModuleTotalProgress(Module.Marketing, Guid.Parse(id));
            ViewBag.ProgressAnalytic = new Master().GetClientProgressAnalytic(Guid.Parse(id));
            ViewBag.Idea = new ClientManager().GetIdea(Guid.Parse(id));
            return View();
        }

        public ActionResult Get(string id, string ClientID)
        {

            ViewBag.ClientID = ClientID;
            ViewBag.Client = new ClientManager().GetClient(Guid.Parse(ClientID));
            switch (int.Parse(id))
            {
                case (int)Module.MyIdea:
                    ViewBag.Language = new ClientManager().GetDefaultModuleLanguage((int)Module.MyIdea, (int)ModuleSection.MyIdea_Ideaoutofhead);
                    ViewBag.Idea = new ClientManager().GetIdea(Guid.Parse(ClientID));
                    return View("_MyIdea");
                case (int)Module.MarketResearch:
                    ViewBag.Observation = new ClientManager().GetDefaultModuleLanguage((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_Observation);
                    ViewBag.Keyfinding = new ClientManager().GetDefaultModuleLanguage((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_KeyFindings);
                    ViewBag.OnlineResearch = new ClientManager().GetDefaultModuleLanguage((int)Module.MarketResearch, (int)ModuleSection.MarketResearch_OnlineResearch);
                    ViewBag.ModelObservation = new ClientManager().GetObservation(Guid.Parse(ClientID));
                    ViewBag.ModelKeyFinding = new ClientManager().GetKeyFinding(Guid.Parse(ClientID));
                    ViewBag.ModelOnlineResearch = new ClientManager().GetOnlineResearch(Guid.Parse(ClientID));
                    return View("_MarketResearch");

                case (int)Module.TheBusiness:
                    ViewBag.BusinessOverview = new ClientManager().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);
                    ViewBag.ProductService = new ClientManager().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService);
                    ViewBag.BusinessOperation = new ClientManager().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);
                    ViewBag.Competitors = new ClientManager().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
                    ViewBag.ModelBusinessOverview = new ClientManager().GetBusinessOverview(Guid.Parse(ClientID));
                    ViewBag.ModelProductService = new ClientManager().GetProductService(Guid.Parse(ClientID));
                    ViewBag.ModelBusinessOperation = new ClientManager().GetBusinessOperation(Guid.Parse(ClientID));
                    ViewBag.ModelSWOT = new ClientManager().GetCompetitorSWOT(Guid.Parse(ClientID));
                    ViewBag.ModelCompetitors = new ClientManager().GetCompetitorAnalysis(Guid.Parse(ClientID));
                    return View("_TheBusiness");
                case (int)Module.Selling:
                    ViewBag.Pricing = new ClientManager().GetDefaultModuleLanguage((int)Module.Selling, (int)ModuleSection.Selling_Pricing);
                    ViewBag.Customer = new ClientManager().GetDefaultModuleLanguage((int)Module.Selling, (int)ModuleSection.Selling_Customers);
                    ViewBag.ModelSelling = new ClientManager().GetPricingProductService(Guid.Parse(ClientID));
                    ViewBag.ModelCustomer = new ClientManager().GetBusinessCustomer(Guid.Parse(ClientID));
                    return View("_Selling");
                case (int)Module.Marketing:
                    ViewBag.ModelOnlinePresence = new ClientManager().GetOnlinePresance(Guid.Parse(ClientID));
                    ViewBag.ModelMarketingBrand = new ClientManager().GetMarketingBrand(Guid.Parse(ClientID));
                    ViewBag.OnlinePresence = new ClientManager().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_OnlinePresence);
                    ViewBag.MarketingPlan =  new ClientManager().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_MarketingPlan);
                    ViewBag.MarketingBrand = new ClientManager().GetDefaultModuleLanguage((int)Module.Marketing, (int)ModuleSection.Marketing_Brand);
                    return View("_Marketing");
                case (int)Module.Finance:
                    ViewBag.ModelFinanceInvestors= new ClientManager().GetFinanceInvestor(Guid.Parse(ClientID));
                    ViewBag.FindingInvestors = new ClientManager().GetDefaultModuleLanguage((int)Module.Finance, (int)ModuleSection.Finance_FindingInvestors);
                    return View("_Finance");

            }
            return View("_Finance");


        }
        

        public string AddRateInfo(RateInfo Model)
        {
            return new ClientManager().AddRateInfo(Model);
        }

        public JsonResult GetRateInfo(RateInfo Model)
        {
           return Json(new ClientManager().GetRateInfo(Model),  JsonRequestBehavior.AllowGet);
        }

        

    }
}