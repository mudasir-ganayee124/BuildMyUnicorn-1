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
    public class BusinessController : WebController
    {
        // GET: Business
        public ActionResult BusinessOverview(string id)
        {
             int State = (int)EntityState.New;
            _Business Model = new BusinessManager().GetBusiness();
            if (Model != null && Model.BusinessID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_Businessoverview))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Business",
                    ActionName = "BusinessOverview",
                    ModuleName = Module.TheBusiness.ToString(),
                    SectionName = ModuleSection.TheBusiness_Businessoverview.ToString()
                });
            }
            return View(State);
        }

        public ActionResult ProductService(string id)
        {
            int State =  (int)EntityState.New;
           _ProductService Model = new BusinessManager().GetProductService();
            if (Model != null && Model.ProductServiceID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_ProductorService))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Business",
                    ActionName = "ProductService",
                    ModuleName = Module.TheBusiness.ToString(),
                    SectionName = ModuleSection.TheBusiness_ProductorService.ToString()
                });
            }
   
            return View(State);
        }

        public ActionResult RunningBusiness(string id)
        {
            int State = (int)EntityState.New;
            RunningBusiness Model = new BusinessManager().GetRunningBusiness();
            if (Model != null && Model.RunningBusinessID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_RunningtheBusiness);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_RunningtheBusiness))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Business",
                    ActionName = "RunningBusiness",
                    ModuleName = Module.TheBusiness.ToString(),
                    SectionName = ModuleSection.TheBusiness_RunningtheBusiness.ToString()
                });
            }
            return View(State);
        }

        public ActionResult Competitors(bool? Edit)
        {
            int State = (int)EntityState.New;
            SWOT Model = new BusinessManager().GetCompetitorSWOT();
            if (Model != null && Edit != true) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_Competitors))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Business",
                    ActionName = "Competitors",
                    ModuleName = Module.TheBusiness.ToString(),
                    SectionName = ModuleSection.TheBusiness_Competitors.ToString()
                });
            }

            return View(State);
        }

        public ActionResult Customer(string id)
        {
            int State = (int)EntityState.New;
            _Customer Model = new BusinessManager().GetBusinessCustomer();
            if (Model != null && Model.CustomerID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Selling, (int)ModuleSection.Selling_Customers);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.Selling_Customers))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Business",
                    ActionName = "Customer",
                    ModuleName = Module.Selling.ToString(),
                    SectionName = ModuleSection.Selling_Customers.ToString()
                });
            }
            return View(State);
        }

        public ActionResult BusinessOperation(string id)
        {
            int State = (int)EntityState.New;
            _BusinessOperation Model = new BusinessManager().GetBusinessOperation();
            if (Model != null && Model.BusinessOperationID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_BusinessOperation))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Business",
                    ActionName = "BusinessOperation",
                    ModuleName = Module.TheBusiness.ToString(),
                    SectionName = ModuleSection.TheBusiness_BusinessOperation.ToString()
                });
            }
            return View(State);
        }

        public ActionResult New(int Type)
        {
            ViewBag.Choice = new List<Choice>
            {
                    new Choice { ID=1, Value="Yes"},
                    new Choice { ID=2, Value="No"}

            };

            if ((int)BusinessSection.BusinessOverview == Type)
            {
                ViewBag.DisplayFounder = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Founder))
                                         .Cast<PropertyDescriptor>()
                                         .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.DisplayCompanyDetails = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompanyDetails))
                                                .Cast<PropertyDescriptor>()
                                                .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.RoleInCompany = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
                ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.BusinessOverview);
                ViewBag.CompanyLegalStructure = new Master().GetOptionMasterList((int)OptionType.CompanyLegalStructure);

                Business obj = new Business();
                Founder _Founder = new Founder();
                CompanyDetails _CompanyDetails = new CompanyDetails();
                _Business Model = new BusinessManager().GetBusiness();
                if (Model != null)
                {
                    obj.ClientID = Model.ClientID;
                    obj.BusinessID = Model.BusinessID;
                    obj.EntityState = EntityState.Old;
                    _Founder.BusinessRun = Model.BusinessRun;
                    _Founder.HobbiesInterest = Model.HobbiesInterest;
                    _Founder.IdeaComeup = Model.IdeaComeup;
                    _Founder.PreviousWorkExperience = Model.PreviousWorkExperience;
                    _Founder.Qualification = Model.Qualification;
                    _Founder.WhyYou = Model.WhyYou;
                    _CompanyDetails.BusinessAddress = Model.BusinessAddress;
                    _CompanyDetails.BusinessPhone = Model.BusinessPhone;
                    _CompanyDetails.Founded = Model.Founded;
                    _CompanyDetails.CompanyLegalStructureID = Model.CompanyLegalStructureID;
                    _CompanyDetails.CompanyNumber = Model.CompanyNumber;
                    _CompanyDetails.VatNumber = Model.VatNumber;
                    _CompanyDetails.CompanyRegisterdName = Model.CompanyRegisterdName;
                    _CompanyDetails.NumberofFounder = Model.NumberofFounder;
                    obj.Founder = _Founder;
                    obj.CompanyDetails = _CompanyDetails;
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.Founder = _Founder;
                    obj.CompanyDetails = _CompanyDetails;
                }
                return PartialView("_NewBusinessOverviewPartial", obj);
            }

            else if  ((int)BusinessSection.ProductService == Type)
            {

                ViewBag.DisplayAboutProduct = TypeDescriptor.GetProperties(typeof(Business_Model.Model.AboutProduct))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.DisplayMVP = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MVP))
                                                .Cast<PropertyDescriptor>()
                                                .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.DevelopmentFarList = new Master().GetOptionMasterList((int)OptionType.ProductService_MVPDevelopmentFar);
                ViewBag.ProductServiceSelling = new Master().GetOptionMasterList((int)OptionType.ProductServiceSelling);
                ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.ProductService);
                ViewBag.ProductServiceProduce = new Master().GetOptionMasterList((int)OptionType.ProductServiceProduce);
                ViewBag.MVPReason = new Master().GetOptionMasterList((int)OptionType.MVPReason);

                ProductService obj = new ProductService();
                AboutProduct _AboutProduct = new AboutProduct();
                MVP _MVP = new MVP();
                _ProductService Model = new BusinessManager().GetProductService();
                if (Model != null)
                {
                    ViewBag.VersionList = new Master().GetSingleMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID).Select(x => x.Value1).ToList();
                    obj.ClientID = Model.ClientID;
                    obj.ProductServiceID = Model.ProductServiceID;
                    obj.EntityState = EntityState.Old;
                    _AboutProduct.PSSelling = Model.PSSelling;
                    _AboutProduct.PSDescription = Model.PSDescription;
                    _AboutProduct.PSProduce = Model.PSProduce;
                    _AboutProduct.PSDevelopStart = Model.PSDevelopStart;
                    _AboutProduct.PSReadylaunch = Model.PSReadylaunch;
                    _AboutProduct.PSVariations = Model.PSVariations;
                    _AboutProduct.PSHaveIPAddress = Model.PSHaveIPAddress;
                    _AboutProduct.PSIPAddress = Model.PSIPAddress;
                    _AboutProduct.PSHaveTradeMark = Model.PSHaveTradeMark;
                    _AboutProduct.PSTradeMark = Model.PSTradeMark;
                    _AboutProduct.PSHaveTradeMark = Model.PSHaveTradeMark;
                    _AboutProduct.PSTradeMark = Model.PSTradeMark;
                    _AboutProduct.PSHaveTechnologyRoadMap = Model.PSHaveTechnologyRoadMap;
                    _MVP.MVPHavePrototype = Model.MVPHavePrototype;
                    _MVP.MVPDevelopmentFarID = Model.MVPDevelopmentFarID;
                    _MVP.MVPPrototype = Model.MVPPrototype;
                    _MVP.MVPCreate = Model.MVPCreate;
                    _MVP.MVPReasonID = Model.MVPReasonID;
                    obj.AboutProduct = _AboutProduct;
                    obj.MVP = _MVP;
                }
                else
                {
                    ViewBag.VersionList = new List<MultipleMaster>().Select(x => x.Value1).ToList(); ;
                    obj.EntityState = EntityState.New;
                    obj.AboutProduct = _AboutProduct;
                    obj.MVP = _MVP;
                }
                return PartialView("_NewProductServicePartial", obj);
            }

            else if ((int)BusinessSection.RunningBusiness == Type)
            {

                ViewBag.OperationTitle = TypeDescriptor.GetProperties(typeof(Business_Model.Model.RunningBusiness))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.ProductServiceProduced = new Master().GetOptionMasterList((int)OptionType.Operation_ProductServicebeproduced);
                ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.RunningBusiness);
                ViewBag.DeliveryMethod = new Master().GetOptionMasterList((int)OptionType.Operation_DeliveryMethod);
                ViewBag.ThirdPartiesInvloved = new Master().GetOptionMasterList((int)OptionType.Operation_ThirdPartiesInvolved);
                ViewBag.PaymentMethod = new Master().GetOptionMasterList((int)OptionType.Operation_PaymentMethod);
                ViewBag.StaffWork = new Master().GetOptionMasterList((int)OptionType.Operation_StaffWork);
                RunningBusiness obj = new RunningBusiness();
                RunningBusiness Model = new BusinessManager().GetRunningBusiness();
                if (Model != null)
                {
                    obj = Model;
                    obj.EntityState = EntityState.Old;
                }
                else
                    obj.EntityState = EntityState.New;
                return PartialView("_NewRunningBusinessPartial", obj);
            }

            else if ((int)BusinessSection.Competitors == Type)
            {


                ViewBag.TitleCompetitorAnalysis = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompetitorAnalysis))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleSWOT = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SWOT))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);

                Competitor obj = new Competitor();
              
                IEnumerable<CompetitorAnalysis> ModelCompetitorAnalysis = new BusinessManager().GetCompetitorAnalysis();
            
                SWOT ModelSWOT = new BusinessManager().GetCompetitorSWOT();
                obj.CompetitorAnalysis = ModelCompetitorAnalysis;
                obj.SWOT = ModelSWOT;
                if (ModelSWOT != null)
                {

                    obj.EntityState = EntityState.Old;
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.SWOT = new SWOT();
                    obj.CompetitorAnalysis = new List<CompetitorAnalysis>();
                }
                return PartialView("_NewCompetitorsPartial", obj);
            }

            else if ((int)BusinessSection.Customer == Type)
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
                ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
                ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
                ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);

                Customer obj = new Customer();
                _Customer Model = new BusinessManager().GetBusinessCustomer();
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
                return PartialView("_NewCustomerPartial", obj);
            }

            else if ((int)BusinessSection.BusinessOperation == Type)
            {

                ViewBag.TitleWhoDoesWhat = TypeDescriptor.GetProperties(typeof(Business_Model.Model.WhoDoesWhat))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleOperations = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Operations))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.DeliveryMethod = new Master().GetOptionMasterList((int)OptionType.Operation_DeliveryMethod);
                ViewBag.ThirdPartiesInvloved = new Master().GetOptionMasterList((int)OptionType.Operation_ThirdPartiesInvolved);
                ViewBag.PaymentMethod = new Master().GetOptionMasterList((int)OptionType.Operation_PaymentMethod);
                ViewBag.StaffWork = new Master().GetOptionMasterList((int)OptionType.Operation_StaffWork);
                ViewBag.TeamMemberList = new ClientManager().GetClientTeam();
              

                BusinessOperation obj = new BusinessOperation();
                _BusinessOperation Model = new BusinessManager().GetBusinessOperation();
                WhoDoesWhat objWhoDoesWhat = new WhoDoesWhat();
                Operations objOperations = new Operations();

                if (Model != null)
                {

                    obj.EntityState = EntityState.Old;
                    obj.BusinessOperationID = Model.BusinessOperationID;
                    obj.ClientID = Model.ClientID;

                    objWhoDoesWhat.FacilitiesResponsible = Model.FacilitiesResponsible;
                    objWhoDoesWhat.FinanceManagementResponsible = Model.FinanceManagementResponsible;
                    objWhoDoesWhat.FinanceOperationalResponsible = Model.FinanceOperationalResponsible;
                    objWhoDoesWhat.HRresponsible = Model.HRresponsible;
                    objWhoDoesWhat.LegalMatterResponsible = Model.LegalMatterResponsible;
                    objWhoDoesWhat.MarektingResponsible = Model.MarektingResponsible;
                    objWhoDoesWhat.OperationResponsible = Model.OperationResponsible;
                    objWhoDoesWhat.SalesResponsible = Model.SalesResponsible;
                    objOperations.DeliveryMethodID = Model.DeliveryMethodID;
                    objOperations.HaveInsurance = Model.HaveInsurance;
                    objOperations.InsuranceType = Model.InsuranceType;
                    objOperations.LicenseType = Model.LicenseType;
                    objOperations.NeedLicense = Model.NeedLicense;
                    objOperations.NeedQualification = Model.NeedQualification;
                    objOperations.Needsoftware = Model.Needsoftware;
                    objOperations.PaymentMethodID = Model.PaymentMethodID;
                    objOperations.ProductProduced = Model.ProductProduced;
                    objOperations.QualificationType = Model.QualificationType;
                    objOperations.QualityControlMethod = Model.QualityControlMethod;
                    objOperations.SoftwareType = Model.SoftwareType;
                    objOperations.StaffCount = Model.StaffCount;
                    objOperations.StaffCountNextYear = Model.StaffCountNextYear;
                    objOperations.StaffWorkID = Model.StaffWorkID;
                    objOperations.ThirdPartyInvovedID = Model.ThirdPartyInvovedID;
                    obj.WhoDoesWhat = objWhoDoesWhat;
                    obj.Operations = objOperations;
                    ViewBag.OperationPartyList = new Master().GetSingleMultipleMaster((int)ModuleName.BusinessOperation, Model.BusinessOperationID);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.WhoDoesWhat = new WhoDoesWhat();
                    obj.Operations = new Operations();
         

                }
                return PartialView("_NewBusinessOperationPartial", obj);
            }

            return PartialView("_NewBusinessOverviewPartial");
        }

        public ActionResult Detail(int Type)
        {
            ViewBag.Choice = new List<Choice>
            {
                    new Choice { ID=1, Value="Yes"},
                    new Choice { ID=2, Value="No"}

            };
            if ((int)BusinessSection.BusinessOverview == Type)
            {
                ViewBag.DisplayFounder = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Founder))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.DisplayCompanyDetails = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompanyDetails))
                                                .Cast<PropertyDescriptor>()
                                                .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.RoleInCompany = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
                ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.BusinessOverview);
                ViewBag.CompanyLegalStructure = new Master().GetOptionMasterList((int)OptionType.CompanyLegalStructure);

                Business obj = new Business();
                Founder _Founder = new Founder();
                CompanyDetails _CompanyDetails = new CompanyDetails();
                _Business Model = new BusinessManager().GetBusiness();
                if (Model != null)
                {
                    obj.ClientID = Model.ClientID;
                    obj.BusinessID = Model.BusinessID;
                    obj.EntityState = EntityState.Old;
                    _Founder.BusinessRun = Model.BusinessRun;
                    _Founder.HobbiesInterest = Model.HobbiesInterest;
                    _Founder.IdeaComeup = Model.IdeaComeup;
                    _Founder.PreviousWorkExperience = Model.PreviousWorkExperience;
                    _Founder.Qualification = Model.Qualification;
                    _Founder.WhyYou = Model.WhyYou;
                    _CompanyDetails.BusinessAddress = Model.BusinessAddress;
                    _CompanyDetails.BusinessPhone = Model.BusinessPhone;
                    _CompanyDetails.Founded = Model.Founded;
                    //_CompanyDetails.CompanyLegalStructureID = Model.CompanyLegalStructureID;
                    _CompanyDetails.CompanyNumber = Model.CompanyNumber;
                    _CompanyDetails.VatNumber = Model.VatNumber;
                    _CompanyDetails.CompanyRegisterdName = Model.CompanyRegisterdName;
                    _CompanyDetails.NumberofFounder = Model.NumberofFounder;
                    obj.Founder = _Founder;
                    obj.CompanyDetails = _CompanyDetails;

                    obj.Founder.Completed += obj.Founder.BusinessRun == null ? 0.00m : 16.66m;
                    obj.Founder.Completed += obj.Founder.HobbiesInterest == null ? 0.00m : 16.66m;
                    obj.Founder.Completed += obj.Founder.IdeaComeup == null ? 0.00m : 16.66m;
                    obj.Founder.Completed += obj.Founder.PreviousWorkExperience == null ? 0.00m : 16.66m;
                    obj.Founder.Completed += obj.Founder.Qualification == null ? 0.00m : 16.66m;
                    obj.Founder.Completed += obj.Founder.WhyYou == null ? 0.00m : 16.66m;
                    obj.Founder.Completed = Math.Round(obj.Founder.Completed);

                    obj.CompanyDetails.Completed += obj.CompanyDetails.BusinessAddress == null ? 0.00m : 16.66m;
                    // obj.CompanyDetails.Completed += obj.CompanyDetails.BusinessPhone == null ? 0.00m : 14.28m;
                    obj.CompanyDetails.Completed += obj.CompanyDetails.Founded == null ? 0.00m : 16.66m;
                    obj.CompanyDetails.Completed += obj.CompanyDetails.CompanyNumber == null ? 0.00m : 16.66m;
                    obj.CompanyDetails.Completed += obj.CompanyDetails.VatNumber == null ? 0.00m : 16.66m;
                    obj.CompanyDetails.Completed += obj.CompanyDetails.CompanyRegisterdName == null ? 0.00m : 16.66m;
                    obj.CompanyDetails.Completed += obj.CompanyDetails.NumberofFounder == null ? 0.00m : 16.66m;
                    obj.CompanyDetails.Completed = Math.Round(obj.CompanyDetails.Completed);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.Founder = _Founder;
                    obj.CompanyDetails = _CompanyDetails;
                }
                return PartialView("_DetailBusinessOverviewPartial", obj);
            }
            else if ((int)BusinessSection.ProductService == Type)
            {
                ViewBag.DisplayAboutProduct = TypeDescriptor.GetProperties(typeof(Business_Model.Model.AboutProduct))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.DisplayMVP = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MVP))
                                                .Cast<PropertyDescriptor>()
                                                .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.DevelopmentFarList = new Master().GetOptionMasterList((int)OptionType.ProductService_MVPDevelopmentFar);
                ViewBag.ProductServiceSelling = new Master().GetOptionMasterList((int)OptionType.ProductServiceSelling);
                ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.ProductService);
                ViewBag.ProductServiceProduce = new Master().GetOptionMasterList((int)OptionType.ProductServiceProduce);
                ViewBag.MVPReason = new Master().GetOptionMasterList((int)OptionType.MVPReason);

                ProductService obj = new ProductService();
                AboutProduct _AboutProduct = new AboutProduct();
                MVP _MVP = new MVP();
                _ProductService Model = new BusinessManager().GetProductService();
                if (Model != null)
                {
                    ViewBag.VersionList = new Master().GetSingleMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID).Select(x => x.Value1).ToList();
                    obj.ClientID = Model.ClientID;
                    obj.ProductServiceID = Model.ProductServiceID;
                    obj.EntityState = EntityState.Old;
                    _AboutProduct.PSSelling = Model.PSSelling;
                    _AboutProduct.PSDescription = Model.PSDescription;
                    _AboutProduct.PSProduce = Model.PSProduce;
                    _AboutProduct.PSDevelopStart = Model.PSDevelopStart;
                    _AboutProduct.PSReadylaunch = Model.PSReadylaunch;
                    _AboutProduct.PSVariations = Model.PSVariations;
                    _AboutProduct.PSHaveIPAddress = Model.PSHaveIPAddress;
                    _AboutProduct.PSIPAddress = Model.PSIPAddress;
                    _AboutProduct.PSTradeMark = Model.PSTradeMark;
                    _AboutProduct.PSHaveTradeMark = Model.PSHaveTradeMark;
                    _AboutProduct.PSHaveTechnologyRoadMap = Model.PSHaveTechnologyRoadMap;
                    _MVP.MVPHavePrototype = Model.MVPHavePrototype;
                    _MVP.MVPPrototype = Model.MVPPrototype;
                    _MVP.MVPDevelopmentFarID = Model.MVPDevelopmentFarID;
                    _MVP.MVPCreate = Model.MVPCreate;
                    _MVP.MVPReasonID = Model.MVPReasonID;
                    obj.AboutProduct = _AboutProduct;
                    obj.MVP = _MVP;

                    obj.AboutProduct.Completed += obj.AboutProduct.PSSelling == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSDescription == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSProduce == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSDevelopStart == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSReadylaunch == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSVariations == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSIPAddress == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSTradeMark == null ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSHaveTradeMark == 0 ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed += obj.AboutProduct.PSHaveTechnologyRoadMap == 0 ? 0.00m : 10.00m;
                    obj.AboutProduct.Completed = Math.Round(obj.AboutProduct.Completed);

                   // obj.MVP.Completed += obj.MVP.MVPHavePrototype == 0 ? 0.00m : 25.00m;
                    obj.MVP.Completed += obj.MVP.MVPDevelopmentFarID == null ? 0.00m : 33.33m;
                    obj.MVP.Completed += obj.MVP.MVPCreate == null ? 0.00m : 33.33m;
                    obj.MVP.Completed += obj.MVP.MVPReasonID == Guid.Empty ? 0.00m : 33.33m;
                    obj.MVP.Completed = Math.Round(obj.MVP.Completed);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.AboutProduct = _AboutProduct;
                    obj.MVP = _MVP;
                }
                return PartialView("_DetailProductServicePartial", obj);

            }
            else if ((int)BusinessSection.RunningBusiness == Type)
            {

                ViewBag.OperationTitle = TypeDescriptor.GetProperties(typeof(Business_Model.Model.RunningBusiness))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);

                RunningBusiness obj = new RunningBusiness();
                RunningBusiness Model = new BusinessManager().GetRunningBusiness();
                if (Model != null)
                {
                    obj = Model;
                    obj.EntityState = EntityState.Old;
                    obj.Completed += obj.RBDeliveryMethodID == null ? 0.00m : 7.69m;
                    obj.Completed += obj.RBEquipmentSoftware == null ? 0.00m : 7.69m;
                    obj.Completed += obj.RBHaveEquipmentSoftware == 0 ? 0.00m : 7.69m;
                    obj.Completed += obj.RBHaveInsurance == 0 ? 0.00m : 7.69m;
                    obj.Completed += obj.RBNeedSpecificeQualification == 0 ? 0.00m : 7.69m;
                    obj.Completed += obj.RBNeedSpecificLicense == 0 ? 0.00m : 7.69m;
                    obj.Completed += obj.RBPaymentMethodID == null ? 0.00m : 7.69m;
                    obj.Completed += obj.RBProducedID == null ? 0.00m : 7.69m;
                    obj.Completed += obj.RBQualityControlMethod == null ? 0.00m : 7.69m;
                    obj.Completed += obj.RBStaffNumber == 0 ? 0.00m : 7.69m;
                    obj.Completed += obj.RBStaffNumberNextYear == 0 ? 0.00m : 7.69m;
                    obj.Completed += obj.RBStaffWorkID == null ? 0.00m : 7.69m;
                    obj.Completed += obj.RBThirdPartyInvovedID == null ? 0.00m : 7.69m;
                    obj.Completed = Math.Round(obj.Completed);

                }
                else
                    obj.EntityState = EntityState.New;

                ViewBag.ProductServiceProduced = new Master().GetOptionMasterList((int)OptionType.Operation_ProductServicebeproduced).Where(x => x.ID == Model.RBProducedID).Select(x => x.Value).FirstOrDefault();
                ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.RunningBusiness);
                ViewBag.DeliveryMethod = new Master().GetOptionMasterList((int)OptionType.Operation_DeliveryMethod).Where(x => x.ID == Model.RBDeliveryMethodID).Select(x => x.Value).FirstOrDefault();
                ViewBag.ThirdPartiesInvloved = new Master().GetOptionMasterList((int)OptionType.Operation_ThirdPartiesInvolved).Where(x => x.ID == Model.RBThirdPartyInvovedID).Select(x => x.Value).FirstOrDefault();
                ViewBag.PaymentMethod = new Master().GetOptionMasterList((int)OptionType.Operation_PaymentMethod).Where(x => x.ID == Model.RBPaymentMethodID).Select(x => x.Value).FirstOrDefault();
                ViewBag.StaffWork = new Master().GetOptionMasterList((int)OptionType.Operation_StaffWork).Where(x => x.ID == Model.RBStaffWorkID).Select(x => x.Value).FirstOrDefault();
                return PartialView("_DetailRunningBusinessPartial", obj);
            }
            else if ((int)BusinessSection.Competitors == Type)
            {

                ViewBag.TitleCompetitorAnalysis = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompetitorAnalysis))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleSWOT = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SWOT))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);

                Competitor obj = new Competitor();
                IEnumerable<CompetitorAnalysis> ModelCompetitorAnalysis = new BusinessManager().GetCompetitorAnalysis();
                SWOT ModelSWOT = new BusinessManager().GetCompetitorSWOT();
                ViewBag.CompetitorAnalysis = 0;
                if (ModelCompetitorAnalysis != null)
                {
                    if (ModelCompetitorAnalysis.Count() > 0)
                    {
                        ViewBag.CompetitorAnalysis = 100;
                        //ModelCompetitorAnalysis.ForEach(x => x.Completed = 100m);
                    }
                }

                obj.SWOT = ModelSWOT;
                obj.SWOT.Completed += obj.SWOT.Strengths == null ? 0.00m : 25.00m;
                obj.SWOT.Completed += obj.SWOT.Weaknesses == null ? 0.00m : 25.00m;
                obj.SWOT.Completed += obj.SWOT.Opportunities == null ? 0.00m : 25.00m;
                obj.SWOT.Completed += obj.SWOT.Threats == null ? 0.00m : 25.00m;
                obj.SWOT.Completed = Math.Round(obj.SWOT.Completed);
                obj.CompetitorAnalysis = ModelCompetitorAnalysis;
                return PartialView("_DetailCompetitorsPartial", obj);
            }
            else if ((int)BusinessSection.Customer == Type)
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
                ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
                ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
                ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);

                Customer obj = new Customer();
                _Customer Model = new BusinessManager().GetBusinessCustomer();
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
                return PartialView("_DetailCustomerPartial", obj);
            }
            else if ((int)BusinessSection.BusinessOperation == Type)
            {

                ViewBag.TitleWhoDoesWhat = TypeDescriptor.GetProperties(typeof(Business_Model.Model.WhoDoesWhat))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.TitleOperations = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Operations))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.DeliveryMethod = new Master().GetOptionMasterList((int)OptionType.Operation_DeliveryMethod);
                ViewBag.ThirdPartiesInvloved = new Master().GetOptionMasterList((int)OptionType.Operation_ThirdPartiesInvolved);
                ViewBag.PaymentMethod = new Master().GetOptionMasterList((int)OptionType.Operation_PaymentMethod);
                ViewBag.StaffWork = new Master().GetOptionMasterList((int)OptionType.Operation_StaffWork);
                ViewBag.TeamMemberList = new ClientManager().GetClientTeam();


                BusinessOperation obj = new BusinessOperation();
                _BusinessOperation Model = new BusinessManager().GetBusinessOperation();
                WhoDoesWhat objWhoDoesWhat = new WhoDoesWhat();
                Operations objOperations = new Operations();

                if (Model != null)
                {

                    obj.EntityState = EntityState.Old;
                    obj.BusinessOperationID = Model.BusinessOperationID;
                    obj.ClientID = Model.ClientID;

                    objWhoDoesWhat.FacilitiesResponsible = Model.FacilitiesResponsible;
                    objWhoDoesWhat.FinanceManagementResponsible = Model.FinanceManagementResponsible;
                    objWhoDoesWhat.FinanceOperationalResponsible = Model.FinanceOperationalResponsible;
                    objWhoDoesWhat.HRresponsible = Model.HRresponsible;
                    objWhoDoesWhat.LegalMatterResponsible = Model.LegalMatterResponsible;
                    objWhoDoesWhat.MarektingResponsible = Model.MarektingResponsible;
                    objWhoDoesWhat.OperationResponsible = Model.OperationResponsible;
                    objWhoDoesWhat.SalesResponsible = Model.SalesResponsible;
                    objOperations.DeliveryMethodID = Model.DeliveryMethodID;
                    objOperations.HaveInsurance = Model.HaveInsurance;
                    objOperations.InsuranceType = Model.InsuranceType;
                    objOperations.LicenseType = Model.LicenseType;
                    objOperations.NeedLicense = Model.NeedLicense;
                    objOperations.NeedQualification = Model.NeedQualification;
                    objOperations.Needsoftware = Model.Needsoftware;
                    objOperations.PaymentMethodID = Model.PaymentMethodID;
                    objOperations.ProductProduced = Model.ProductProduced;
                    objOperations.QualificationType = Model.QualificationType;
                    objOperations.QualityControlMethod = Model.QualityControlMethod;
                    objOperations.SoftwareType = Model.SoftwareType;
                    objOperations.StaffCount = Model.StaffCount;
                    objOperations.StaffCountNextYear = Model.StaffCountNextYear;
                    objOperations.StaffWorkID = Model.StaffWorkID;
                    objOperations.ThirdPartyInvovedID = Model.ThirdPartyInvovedID;
                    obj.WhoDoesWhat = objWhoDoesWhat;
                    obj.Operations = objOperations;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.FacilitiesResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.FinanceManagementResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.FinanceOperationalResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.HRresponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.LegalMatterResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.MarektingResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.OperationResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed += objWhoDoesWhat.SalesResponsible == null ? 0.00m : 14.28m;
                    objWhoDoesWhat.Completed = Math.Round(objWhoDoesWhat.Completed);
                    objOperations.Completed += objOperations.DeliveryMethodID == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.InsuranceType == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.LicenseType == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.QualificationType == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.SoftwareType == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.PaymentMethodID == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.ProductProduced == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.QualityControlMethod == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.StaffCount == null ? 0.00m : 10.00m;
                    objOperations.Completed += objOperations.StaffCountNextYear == null ? 0.00m : 10.00m;
                    objOperations.Completed = Math.Round(objOperations.Completed);
                    ViewBag.OperationPartyList = new Master().GetSingleMultipleMaster((int)ModuleName.BusinessOperation, Model.BusinessOperationID);
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.WhoDoesWhat = new WhoDoesWhat();
                    obj.Operations = new Operations();
                    ViewBag.OperationPartyList = null;

                }
                return PartialView("_DetailBusinessOperationPartial", obj);
            }
            return PartialView("_DetailProductService");

        }

        public ActionResult Edit(Guid BuyerPersonaID)
        {
            ViewBag.TitleBuyerPersona = TypeDescriptor.GetProperties(typeof(Business_Model.Model.BuyerPersona))
                                  .Cast<PropertyDescriptor>()
                                  .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.AgeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Age);
            ViewBag.IncomeList = new Master().GetOptionMasterList((int)OptionType.BuyerPersona_Income);
            ViewBag.GenderList = new Master().GetOptionMasterList((int)OptionType.Gender);
            var obj = new BusinessManager().GetbuyerPersona(BuyerPersonaID);
            return View("CustomerBuyerPersonal", obj);
        }

        public string Add(Business BusinessModel, List<Client> Client)
        {
       
            if (BusinessModel.EntityState == EntityState.New)
            {
                BusinessModel.BusinessID = Guid.NewGuid();
            }
            return new BusinessManager().AddBusniess(BusinessModel, Client);
        }

        public string AddProductService(ProductService Model, List<MultipleMaster> MultipleMaster)
        {

            if (Model.EntityState == EntityState.New)
            {
                Model.ProductServiceID = Guid.NewGuid();
            }

            if (Model.AboutProduct.PSHaveIPAddress == 2) Model.AboutProduct.PSIPAddress = "";
            if (Model.AboutProduct.PSHaveTradeMark == 2) Model.AboutProduct.PSTradeMark = "";
            //if (Model.MVP.MVPHavePrototype == 2) Model.MVP.MVPPrototype = "";
           return new BusinessManager().AddProductService(Model, MultipleMaster);
        }

        public string AddRunningBusiness(RunningBusiness Model)
        {

            if (Model.EntityState == EntityState.New)
            {
                Model.RunningBusinessID = Guid.NewGuid();
            }

            if (Model.RBHaveEquipmentSoftware == 2) Model.RBEquipmentSoftware = "";
            return new BusinessManager().AddRunningBusiness(Model);
        }

        public string AddBusinessOperation(BusinessOperation Model, List<MultipleMaster> ModelList)
        {

            if (Model.EntityState == EntityState.New)
            {
                Model.BusinessOperationID = Guid.NewGuid();
            }
            return new BusinessManager().AddBusinessOperation(Model, ModelList);
        }

        public string AddCompetitors(Competitor Model)
        {
          return new BusinessManager().AddCompetitor(Model);
        }

        public string AddCustomer(Customer Model)
        {

            if (Model.EntityState == EntityState.New && Model.CustomerID == Guid.Empty)
            {
                
                Model.CustomerID = Guid.NewGuid();
            }
            return new BusinessManager().AddCustomer(Model);
        }

        public ActionResult DeleteBuyerPersona(Guid BuyerPersonaID, Guid CustomerID)
        {
            new BusinessManager().DeleteBuyerPersona(BuyerPersonaID);
            IEnumerable<BuyerPersona> ModelList = new BusinessManager().GetCustomerbuyerPersona(CustomerID);
            return PartialView("_CustomerBuyerPersonaPartial", ModelList);
        }

        public JsonResult AddBuyerPersona(BuyerPersona Model)
        {
            if (Model.BuyerPersonaID == Guid.Empty) 
            Model.BuyerPersonaID = Guid.NewGuid();
            if (Model.CustomerID == Guid.Empty)
                Model.CustomerID = Guid.NewGuid();
               return Json(new { Status = new BusinessManager().AddBuyerPersona(Model), CustomerID = Model.CustomerID }, JsonRequestBehavior.AllowGet);
          

        }

        public ActionResult GetBuyerPersona(Guid CustomerID)
        {
            IEnumerable<BuyerPersona> ModelList = new BusinessManager().GetCustomerbuyerPersona(CustomerID);
            return PartialView("_CustomerBuyerPersonaPartial", ModelList);

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