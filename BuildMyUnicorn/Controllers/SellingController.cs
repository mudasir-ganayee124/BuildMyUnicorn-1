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
    public class SellingController : WebController
    {
        // GET: Pricing
        public ActionResult ProductServicePricing(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new SellingManager().ExistPricingProductService(Guid.Empty);
            else
            {
                Guid ProductServicePricingID;
                bool isValid = Guid.TryParse(id, out ProductServicePricingID);
                if (isValid) Count = new SellingManager().ExistPricingProductService(ProductServicePricingID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.Selling;
                objlog.ModuleSectionID = ModuleSection.Selling_Pricing;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.Selling, (int)ModuleSection.Selling_Customers) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Selling",
                        ActionName = "ProductServicePricing",
                        ModuleID = (int)Module.Selling,
                        SectionID = (int)ModuleSection.Selling_Pricing
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Selling, (int)ModuleSection.Selling_Pricing);
            return View(State);

        }

        public ActionResult Customer(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new SellingManager().ExistCustomer(Guid.Empty);
            else
            {
                Guid CustomerID;
                bool isValid = Guid.TryParse(id, out CustomerID);
                if (isValid) Count = new SellingManager().ExistCustomer(CustomerID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.Selling;
                objlog.ModuleSectionID = ModuleSection.Selling_Customers;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.Selling, (int)ModuleSection.Selling_Customers) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Selling",
                        ActionName = "Customer",
                        ModuleID = (int)Module.Selling,
                        SectionID = (int)ModuleSection.Selling_Customers
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Selling, (int)ModuleSection.Selling_Customers);
            return View(State);

        }

        public ActionResult New(int Type)
        {


            if ((int)ModuleSection.Selling_Pricing == Type)
            {
                PricingProductService obj = new PricingProductService();

                _PricingProductService Model = new SellingManager().GetPricingProductService();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetServicePricingDataDependency();

                });

                var GetModelData = Task.Factory.StartNew(() =>
                {


                    ChosePricingStrategy objChosePricingStrategy = new ChosePricingStrategy();
                    if (Model != null)
                    {
                        obj.ProductServicePricingID = Model.ProductServicePricingID;
                        obj.ClientID = Model.ClientID;
                        objChosePricingStrategy.CostDeliverPicture = Model.CostDeliverPicture;
                        objChosePricingStrategy.CustomerBuy = Model.CustomerBuy;
                        objChosePricingStrategy.CustomersOftenToPay = Model.CustomersOftenToPay;
                        objChosePricingStrategy.CustomersValue = Model.CustomersValue;
                        objChosePricingStrategy.CustomersWillingToPay = Model.CustomersWillingToPay;
                        objChosePricingStrategy.OfferCustomers = Model.OfferCustomers;
                        objChosePricingStrategy.UsersBringValue = Model.UsersBringValue;
                        objChosePricingStrategy.OfferLevels = Model.OfferLevels;
                        objChosePricingStrategy.OfferOpportunity = Model.OfferOpportunity;
                        objChosePricingStrategy.PricingStrategy = Model.PricingStrategy;
                        objChosePricingStrategy.PricingStrategyChosen = Model.PricingStrategyChosen;
                        objChosePricingStrategy.ProductUnique = Model.ProductUnique;
                        objChosePricingStrategy.ProductValue = Model.ProductValue;
                        objChosePricingStrategy.SalesCertainPeriod = Model.SalesCertainPeriod;
                        obj.ChosePricingStrategy = objChosePricingStrategy;

                        obj.EntityState = EntityState.Old;
                    }
                    else
                    {
                        obj.EntityState = EntityState.New;
                        obj.ChosePricingStrategy = objChosePricingStrategy;
                    }
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewProductServicePricingPartial", obj);
            }

            else if ((int)ModuleSection.Selling_Customers == Type)
            {
                Customer obj = new Customer();
                _Customer Model = new SellingManager().GetCustomers();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetCustomerDataDependency();

                });

                var GetModelData = Task.Factory.StartNew(() =>
                {
                    BuyerPersona objPersona = new BuyerPersona();
                    BuyerPurchaseCycle objPurchaseCycle = new BuyerPurchaseCycle();
                    

                    if (Model != null)
                    {

                        obj.EntityState = EntityState.Old;
                        obj.CustomerID = Model.CustomerID;
                        obj.About = Model.About;
                        obj.Based = Model.Based;
                        obj.Buy = Model.Buy;
                        obj.Factors = Model.Factors;
                        obj.ClientID = Model.ClientID;
                        objPurchaseCycle.RecognitionNeed = Model.RecognitionNeed;
                        objPurchaseCycle.InformationSearch = Model.InformationSearch;
                        objPurchaseCycle.AlternativeEvaluation = Model.AlternativeEvaluation;
                        objPurchaseCycle.PurchaseDecision = Model.PurchaseDecision;
                        objPurchaseCycle.PurchaseEvaluation = Model.PurchaseDecision;
                        obj.BuyerPurchaseCycle = objPurchaseCycle;
                    }
                    else
                    {
                        obj.EntityState = EntityState.New;
                        obj.BuyerPersona = new List<BuyerPersona>();
                        obj.BuyerPurchaseCycle = new BuyerPurchaseCycle();

                    }
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewCustomerPartial", obj);
            }
            return PartialView("_NewObservationPartial");
        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.Selling_Pricing == Type)
            {

                PricingProductService obj = new PricingProductService();

                _PricingProductService Model = new SellingManager().GetPricingProductService();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetServicePricingDataDependency();

                });

                var GetModelData = Task.Factory.StartNew(() =>
                {


                    ChosePricingStrategy objChosePricingStrategy = new ChosePricingStrategy();
                    if (Model != null)
                    {
                        obj.ProductServicePricingID = Model.ProductServicePricingID;
                        obj.ClientID = Model.ClientID;
                        objChosePricingStrategy.CostDeliverPicture = Model.CostDeliverPicture;
                        objChosePricingStrategy.CustomerBuy = Model.CustomerBuy;
                        objChosePricingStrategy.CustomersOftenToPay = Model.CustomersOftenToPay;
                        objChosePricingStrategy.CustomersValue = Model.CustomersValue;
                        objChosePricingStrategy.CustomersWillingToPay = Model.CustomersWillingToPay;
                        objChosePricingStrategy.OfferCustomers = Model.OfferCustomers;
                        objChosePricingStrategy.UsersBringValue = Model.UsersBringValue;
                        objChosePricingStrategy.OfferLevels = Model.OfferLevels;
                        objChosePricingStrategy.OfferOpportunity = Model.OfferOpportunity;
                        objChosePricingStrategy.PricingStrategy = Model.PricingStrategy;
                        objChosePricingStrategy.PricingStrategyChosen = Model.PricingStrategyChosen;
                        objChosePricingStrategy.ProductUnique = Model.ProductUnique;
                        objChosePricingStrategy.ProductValue = Model.ProductValue;
                        objChosePricingStrategy.SalesCertainPeriod = Model.SalesCertainPeriod;
                        obj.ChosePricingStrategy = objChosePricingStrategy;

                        obj.EntityState = EntityState.Old;
                    }
                    else
                    {
                        obj.EntityState = EntityState.New;
                        obj.ChosePricingStrategy = objChosePricingStrategy;
                    }
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });

                return PartialView("_DetailProductServicePricingPartial", obj);
            }

            else if ((int)ModuleSection.Selling_Customers == Type)
            {
                Customer obj = new Customer();
                _Customer Model = new SellingManager().GetCustomers();

                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetCustomerDataDependency();

                });
                //temporary
                IEnumerable<BuyerPersona> ModelList = new SellingManager().GetCustomerbuyerPersona(obj.CustomerID);
                if (ModelList.Count() > 0) ViewBag.ProgressBuyerPersonaUnit = 100; else ViewBag.ProgressBuyerPersonaUnit = 0;
                var GetModelData = Task.Factory.StartNew(() =>
                {
                    BuyerPersona objPersona = new BuyerPersona();
                    BuyerPurchaseCycle objPurchaseCycle = new BuyerPurchaseCycle();

                    if (Model != null)
                    {

                        obj.EntityState = EntityState.Old;
                        obj.CustomerID = Model.CustomerID;
                        obj.About = Model.About;
                        obj.Based = Model.Based;
                        obj.Buy = Model.Buy;
                        obj.Factors = Model.Factors;
                        obj.ClientID = Model.ClientID;
                        objPurchaseCycle.RecognitionNeed = Model.RecognitionNeed;
                        objPurchaseCycle.InformationSearch = Model.InformationSearch;
                        objPurchaseCycle.AlternativeEvaluation = Model.AlternativeEvaluation;
                        objPurchaseCycle.PurchaseDecision = Model.PurchaseDecision;
                        objPurchaseCycle.PurchaseEvaluation = Model.PurchaseDecision;
                        obj.BuyerPurchaseCycle = objPurchaseCycle;
                    }
                    else
                    {
                        obj.EntityState = EntityState.New;
                        obj.BuyerPersona = new List<BuyerPersona>();
                        obj.BuyerPurchaseCycle = new BuyerPurchaseCycle();

                    }
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_DetailCustomerPartial", obj);
            }

            return PartialView("_NewObservationPartial");


        }

        public ActionResult GetNewBuyerPersona(Guid CustomerID)
        {
            IEnumerable<BuyerPersona> ModelList = new SellingManager().GetCustomerbuyerPersona(CustomerID);
            return PartialView("_NewCustomerBuyerPersonaPartial", ModelList);

        }
        public ActionResult GetDetailBuyerPersona(Guid CustomerID)
        {
            IEnumerable<BuyerPersona> ModelList = new SellingManager().GetCustomerbuyerPersona(CustomerID);
            return PartialView("_DetailCustomerBuyerPersonaPartial", ModelList);

        }

        public string AddCustomer(Customer Model)
        {

            if (Model.EntityState == EntityState.New && Model.CustomerID == Guid.Empty)
            {

                Model.CustomerID = Guid.NewGuid();
            }
            return new SellingManager().AddCustomer(Model);
        }

        public JsonResult AddBuyerPersona(BuyerPersona Model)
        {
            if (Model.BuyerPersonaID == Guid.Empty)
                Model.BuyerPersonaID = Guid.NewGuid();
            if (Model.CustomerID == Guid.Empty)
                Model.CustomerID = Guid.NewGuid();
            return Json(new { Status = new SellingManager().AddBuyerPersona(Model), CustomerID = Model.CustomerID }, JsonRequestBehavior.AllowGet);


        }

        public string AddProductServicePricing(PricingProductService Model)
        {

            if (Model.EntityState == EntityState.New)
                Model.ProductServicePricingID = Guid.NewGuid();
            return new SellingManager().AddPricingProductService(Model);
        }

        public void GetServicePricingDataDependency()
        {
            ViewBag.TitleChosePricingStrategy = TypeDescriptor.GetProperties(typeof(Business_Model.Model.ChosePricingStrategy))
                                     .Cast<PropertyDescriptor>()
                                     .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.Selling, (int)ModuleSection.Selling_Pricing);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Selling, (int)ModuleSection.Selling_Pricing);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.Selling, (int)ModuleSection.Selling_Pricing);

        }

        public void GetCustomerDataDependency()
        {
            ViewBag.TitleCustomer = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Customer))
                                      .Cast<PropertyDescriptor>()
                                      .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleBuyerPersona = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BuyerPersona))
                                    .Cast<PropertyDescriptor>()
                                    .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleBuyerPurchaseCycle = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BuyerPurchaseCycle))
                                   .Cast<PropertyDescriptor>()
                                   .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Selling, (int)ModuleSection.Selling_Customers);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.Selling, (int)ModuleSection.Selling_Customers);
            ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.Selling, (int)ModuleSection.Selling_Customers);

        }


    }
}