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
    public class BusinessController : WebController
    {
        // GET: Business
        public ActionResult BusinessOverview(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new BusinessManager().ExistBusinessOverview(Guid.Empty);
            else
            {
                Guid BusinessOverviewID;
                bool isValid = Guid.TryParse(id, out BusinessOverviewID);
                if (isValid) Count = new BusinessManager().ExistBusinessOverview(BusinessOverviewID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.TheBusiness;
                objlog.ModuleSectionID = ModuleSection.TheBusiness_Businessoverview;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Business",
                        ActionName = "BusinessOverview",
                        ModuleID = (int)Module.TheBusiness,
                        SectionID = (int)ModuleSection.TheBusiness_Businessoverview
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);
            return View(State);          
        }

        public ActionResult ProductService(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new BusinessManager().ExistProductService(Guid.Empty);
            else
            {
                Guid ProductServiceID;
                bool isValid = Guid.TryParse(id, out ProductServiceID);
                if (isValid) Count = new BusinessManager().ExistProductService(ProductServiceID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.TheBusiness;
                objlog.ModuleSectionID = ModuleSection.TheBusiness_ProductorService;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Business",
                        ActionName = "ProductService",
                        ModuleID = (int)Module.TheBusiness,
                        SectionID = (int)ModuleSection.TheBusiness_ProductorService
                    });
                }
            }
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService);
            return View(State);
        }

        public ActionResult RunningBusiness(string id)
        {
            //int State = (int)EntityState.New;
            //RunningBusiness Model = new BusinessManager().GetRunningBusiness();
            //if (Model != null && Model.RunningBusinessID.ToString() != id) State = (int)EntityState.Old;
            //ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_RunningtheBusiness);
            //if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_RunningtheBusiness))
            //{
            //    return RedirectToAction("Index", "ModuleCourse", new
            //    {
            //        ControllerName = "Business",
            //        ActionName = "RunningBusiness",
            //        ModuleName = Module.TheBusiness.ToString(),
            //        SectionName = ModuleSection.TheBusiness_RunningtheBusiness.ToString()
            //    });
            //}
            return View();
        }

        public ActionResult CompetitorAnalysis(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new BusinessManager().ExistBusinessCompetitorsAnalysis(Guid.Empty);
            else
            {
                Guid ClientID;
                bool isValid = Guid.TryParse(id, out ClientID);
                if (isValid) Count = new BusinessManager().ExistBusinessCompetitorsAnalysis(ClientID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.TheBusiness;
                objlog.ModuleSectionID = ModuleSection.TheBusiness_Competitors;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Business",
                        ActionName = "CompetitorAnalysis",
                        ModuleID = (int)Module.TheBusiness,
                        SectionID = (int)ModuleSection.TheBusiness_Competitors
                    });
                }
            }
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
            return View(State);
        }

        public ActionResult BusinessOperation(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new BusinessManager().ExistBusinessOperation(Guid.Empty);
            else
            {
                Guid BusinessOperationID;
                bool isValid = Guid.TryParse(id, out BusinessOperationID);
                if (isValid) Count = new BusinessManager().ExistBusinessOperation(BusinessOperationID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.TheBusiness;
                objlog.ModuleSectionID = ModuleSection.TheBusiness_BusinessOperation;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Business",
                        ActionName = "BusinessOperation",
                        ModuleID = (int)Module.TheBusiness,
                        SectionID = (int)ModuleSection.TheBusiness_BusinessOperation
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);
            return View(State);
        }

        public ActionResult New(int Type)
        {
             
            if ((int)BusinessSection.BusinessOverview == Type)
            {
                 Business obj = new Business();
                _Business Model = new BusinessManager().GetBusinessOverview();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetBusinessOverviewDataDependency();

                });


                var GetModelData = Task.Factory.StartNew(() =>
                {
                    Founder _Founder = new Founder();
                    CompanyDetails _CompanyDetails = new CompanyDetails();

                    if (Model != null)
                    {
                        obj.ClientID = Model.ClientID;
                        obj.BusinessOverID = Model.BusinessOverID;
                        obj.EntityState = EntityState.Old;
                        _Founder.BusinessRun = Model.BusinessRun;
                        _Founder.HobbiesInterest = Model.HobbiesInterest;
                        _Founder.IdeaComeup = Model.IdeaComeup;
                        _Founder.PreviousWorkExperience = Model.PreviousWorkExperience;
                        _Founder.Qualification = Model.Qualification;
                        _Founder.WhyYou = Model.WhyYou;
                        _CompanyDetails.BusinessAddress = Model.BusinessAddress;
                        _CompanyDetails.BusinessRequirePremises = Model.BusinessRequirePremises;
                        _CompanyDetails.LandlordCostStatus = Model.LandlordCostStatus;
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
                });

                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewBusinessOverviewPartial", obj);
            }

            else if ((int)BusinessSection.ProductService == Type)
            {
                ProductService obj = new ProductService();
                _ProductService Model = new BusinessManager().GetProductService();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetProductServiceDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {
                    AboutProduct _AboutProduct = new AboutProduct();
                    MVP _MVP = new MVP();

                    if (Model != null)
                    {
                       
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
                        ViewBag.VersionList = new List<MultipleMaster>().Select(x => x.Value1).ToList(); 
                        obj.EntityState = EntityState.New;
                        obj.AboutProduct = _AboutProduct;
                        obj.MVP = _MVP;
                    }

                });
                if (Model != null)
                 ViewBag.VersionList = new Master().GetSingleMultipleMaster((int)ModuleName.ProductService, Model.ProductServiceID).Select(x => x.Value1).ToList();
                else ViewBag.VersionList = new List<MultipleMaster>().Select(x => x.Value1).ToList();
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewProductServicePartial", obj);
            }

            else if ((int)BusinessSection.RunningBusiness == Type)
            {

                ViewBag.OperationTitle = TypeDescriptor.GetProperties(typeof(Business_Model.Model.RunningBusiness))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);

                ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_RunningtheBusiness);
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
                Competitor obj = new Competitor();
                IEnumerable<CompetitorAnalysis> ModelCompetitorAnalysis = new BusinessManager().GetCompetitorAnalysis();
                SWOT ModelSWOT = new BusinessManager().GetCompetitorSWOT();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetBusinessCompetitorsDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {
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
                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewCompetitorsPartial", obj);
            }

            else if ((int)BusinessSection.BusinessOperation == Type)
            {
                BusinessOperation obj = new BusinessOperation();
                ViewBag.TeamMemberList = new ClientManager().GetClientTeam();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetBusinessOperationDataDependency();

                });
                //var GetModelData = Task.Factory.StartNew(() =>
                //{
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

                //});
                Task.WaitAll(new Task[] { GetModelDependency });
                return PartialView("_NewBusinessOperationPartial", obj);
            }

            return PartialView("_NewBusinessOverviewPartial");
        }

        public ActionResult Detail(int Type)
        {
  
            if ((int)BusinessSection.BusinessOverview == Type)
            {
                Business obj = new Business();
                _Business Model = new BusinessManager().GetBusinessOverview();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetBusinessOverviewDataDependency();

                });


                var GetModelData = Task.Factory.StartNew(() =>
                {
                    Founder _Founder = new Founder();
                    CompanyDetails _CompanyDetails = new CompanyDetails();
                        obj.ClientID = Model.ClientID;
                        obj.BusinessOverID = Model.BusinessOverID;
                        obj.EntityState = EntityState.Old;
                        _Founder.BusinessRun = Model.BusinessRun;
                        _Founder.HobbiesInterest = Model.HobbiesInterest;
                        _Founder.IdeaComeup = Model.IdeaComeup;
                        _Founder.PreviousWorkExperience = Model.PreviousWorkExperience;
                        _Founder.Qualification = Model.Qualification;
                        _Founder.WhyYou = Model.WhyYou;
                        _CompanyDetails.BusinessAddress = Model.BusinessAddress;
                        _CompanyDetails.BusinessRequirePremises = Model.BusinessRequirePremises;
                        _CompanyDetails.LandlordCostStatus = Model.LandlordCostStatus;
                        _CompanyDetails.BusinessPhone = Model.BusinessPhone;
                        _CompanyDetails.Founded = Model.Founded;
                        _CompanyDetails.CompanyLegalStructureID = Model.CompanyLegalStructureID;
                        _CompanyDetails.CompanyNumber = Model.CompanyNumber;
                        _CompanyDetails.VatNumber = Model.VatNumber;
                        _CompanyDetails.CompanyRegisterdName = Model.CompanyRegisterdName;
                        _CompanyDetails.NumberofFounder = Model.NumberofFounder;
                        obj.Founder = _Founder;
                        obj.CompanyDetails = _CompanyDetails;
               
                });

                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_DetailBusinessOverviewPartial", obj);
            }

            else if ((int)BusinessSection.ProductService == Type)
            {
                ProductService obj = new ProductService();
                _ProductService Model = new BusinessManager().GetProductService();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetProductServiceDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {
                    AboutProduct _AboutProduct = new AboutProduct();
                    MVP _MVP = new MVP();
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
                

                });
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_DetailProductServicePartial", obj);

            }

            else if ((int)BusinessSection.RunningBusiness == Type)
            {

                ViewBag.OperationTitle = TypeDescriptor.GetProperties(typeof(Business_Model.Model.RunningBusiness))
                                        .Cast<PropertyDescriptor>()
                                        .ToDictionary(p => p.Name, p => p.Description);
                ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_RunningtheBusiness);
                ViewBag.RateInfo = new Master().GetRateInfo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_RunningtheBusiness);
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
                Competitor obj = new Competitor();
                IEnumerable<CompetitorAnalysis> ModelCompetitorAnalysis = new BusinessManager().GetCompetitorAnalysis();
                SWOT ModelSWOT = new BusinessManager().GetCompetitorSWOT();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetBusinessCompetitorsDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {
                    obj.CompetitorAnalysis = ModelCompetitorAnalysis;
                    obj.SWOT = ModelSWOT;
                    if (ModelSWOT != null)
                    {
                        obj.ClientID = ModelSWOT.ClientID;
                        obj.EntityState = EntityState.Old;
                    }
                    else
                    {
                        obj.EntityState = EntityState.New;
                        obj.SWOT = new SWOT();
                        obj.CompetitorAnalysis = new List<CompetitorAnalysis>();
                    }
                });
                //ViewBag.TitleCompetitorAnalysis = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompetitorAnalysis))
                //                        .Cast<PropertyDescriptor>()
                //                        .ToDictionary(p => p.Name, p => p.Description);
                //ViewBag.TitleSWOT = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SWOT))
                //                       .Cast<PropertyDescriptor>()
                //                       .ToDictionary(p => p.Name, p => p.Description);
                //ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
                //ViewBag.RateInfo = new Master().GetRateInfo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
                //Competitor obj = new Competitor();
                //IEnumerable<CompetitorAnalysis> ModelCompetitorAnalysis = new BusinessManager().GetCompetitorAnalysis();
                //SWOT ModelSWOT = new BusinessManager().GetCompetitorSWOT();
                //ViewBag.CompetitorAnalysis = 0;
                //if (ModelCompetitorAnalysis != null)
                //{
                //    if (ModelCompetitorAnalysis.Count() > 0)
                //    {
                //        ViewBag.CompetitorAnalysis = 100;
                //        //ModelCompetitorAnalysis.ForEach(x => x.Completed = 100m);
                //    }
                //}

                //obj.SWOT = ModelSWOT;
                //obj.SWOT.Completed += obj.SWOT.Strengths == null ? 0.00m : 25.00m;
                //obj.SWOT.Completed += obj.SWOT.Weaknesses == null ? 0.00m : 25.00m;
                //obj.SWOT.Completed += obj.SWOT.Opportunities == null ? 0.00m : 25.00m;
                //obj.SWOT.Completed += obj.SWOT.Threats == null ? 0.00m : 25.00m;
                //obj.SWOT.Completed = Math.Round(obj.SWOT.Completed);
                //obj.CompetitorAnalysis = ModelCompetitorAnalysis;
                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_DetailCompetitorsPartial", obj);
            }

            else if ((int)BusinessSection.BusinessOperation == Type)
            {
                BusinessOperation obj = new BusinessOperation();
                ViewBag.TeamMemberList = new ClientManager().GetClientTeam();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetBusinessOperationDataDependency();

                });
                //var GetModelData = Task.Factory.StartNew(() =>
                //{
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
                //ViewBag.TitleWhoDoesWhat = TypeDescriptor.GetProperties(typeof(Business_Model.Model.WhoDoesWhat))
                //                        .Cast<PropertyDescriptor>()
                //                        .ToDictionary(p => p.Name, p => p.Description);
                //ViewBag.TitleOperations = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Operations))
                //                        .Cast<PropertyDescriptor>()
                //                        .ToDictionary(p => p.Name, p => p.Description);
                //ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);
                //ViewBag.DeliveryMethod = new Master().GetOptionMasterList((int)OptionType.Operation_DeliveryMethod);
                //ViewBag.ThirdPartiesInvloved = new Master().GetOptionMasterList((int)OptionType.Operation_ThirdPartiesInvolved);
                //ViewBag.PaymentMethod = new Master().GetOptionMasterList((int)OptionType.Operation_PaymentMethod);
                //ViewBag.StaffWork = new Master().GetOptionMasterList((int)OptionType.Operation_StaffWork);
                //ViewBag.RateInfo = new Master().GetRateInfo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);
                //ViewBag.TeamMemberList = new ClientManager().GetClientTeam();


                //BusinessOperation obj = new BusinessOperation();
                //_BusinessOperation Model = new BusinessManager().GetBusinessOperation();
                //WhoDoesWhat objWhoDoesWhat = new WhoDoesWhat();
                //Operations objOperations = new Operations();

                //if (Model != null)
                //{

                //    obj.EntityState = EntityState.Old;
                //    obj.BusinessOperationID = Model.BusinessOperationID;
                //    obj.ClientID = Model.ClientID;

                //    objWhoDoesWhat.FacilitiesResponsible = Model.FacilitiesResponsible;
                //    objWhoDoesWhat.FinanceManagementResponsible = Model.FinanceManagementResponsible;
                //    objWhoDoesWhat.FinanceOperationalResponsible = Model.FinanceOperationalResponsible;
                //    objWhoDoesWhat.HRresponsible = Model.HRresponsible;
                //    objWhoDoesWhat.LegalMatterResponsible = Model.LegalMatterResponsible;
                //    objWhoDoesWhat.MarektingResponsible = Model.MarektingResponsible;
                //    objWhoDoesWhat.OperationResponsible = Model.OperationResponsible;
                //    objWhoDoesWhat.SalesResponsible = Model.SalesResponsible;
                //    objOperations.DeliveryMethodID = Model.DeliveryMethodID;
                //    objOperations.HaveInsurance = Model.HaveInsurance;
                //    objOperations.InsuranceType = Model.InsuranceType;
                //    objOperations.LicenseType = Model.LicenseType;
                //    objOperations.NeedLicense = Model.NeedLicense;
                //    objOperations.NeedQualification = Model.NeedQualification;
                //    objOperations.Needsoftware = Model.Needsoftware;
                //    objOperations.PaymentMethodID = Model.PaymentMethodID;
                //    objOperations.ProductProduced = Model.ProductProduced;
                //    objOperations.QualificationType = Model.QualificationType;
                //    objOperations.QualityControlMethod = Model.QualityControlMethod;
                //    objOperations.SoftwareType = Model.SoftwareType;
                //    objOperations.StaffCount = Model.StaffCount;
                //    objOperations.StaffCountNextYear = Model.StaffCountNextYear;
                //    objOperations.StaffWorkID = Model.StaffWorkID;
                //    objOperations.ThirdPartyInvovedID = Model.ThirdPartyInvovedID;
                //    obj.WhoDoesWhat = objWhoDoesWhat;
                //    obj.Operations = objOperations;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.FacilitiesResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.FinanceManagementResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.FinanceOperationalResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.HRresponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.LegalMatterResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.MarektingResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.OperationResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed += objWhoDoesWhat.SalesResponsible == null ? 0.00m : 14.28m;
                //    objWhoDoesWhat.Completed = Math.Round(objWhoDoesWhat.Completed);
                //    objOperations.Completed += objOperations.DeliveryMethodID == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.InsuranceType == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.LicenseType == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.QualificationType == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.SoftwareType == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.PaymentMethodID == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.ProductProduced == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.QualityControlMethod == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.StaffCount == null ? 0.00m : 10.00m;
                //    objOperations.Completed += objOperations.StaffCountNextYear == null ? 0.00m : 10.00m;
                //    objOperations.Completed = Math.Round(objOperations.Completed);
                //    ViewBag.OperationPartyList = new Master().GetSingleMultipleMaster((int)ModuleName.BusinessOperation, Model.BusinessOperationID);
                //}
                //else
                //{
                //    obj.EntityState = EntityState.New;
                //    obj.WhoDoesWhat = new WhoDoesWhat();
                //    obj.Operations = new Operations();
                //    ViewBag.OperationPartyList = null;

                //}
                Task.WaitAll(new Task[] {  GetModelDependency });
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

        public string AddBusinessOverview(Business BusinessModel)
        {

            if (BusinessModel.EntityState == EntityState.New)
            {
                BusinessModel.BusinessOverID = Guid.NewGuid();
            }

            return new BusinessManager().AddBusniessOverview(BusinessModel);
        }

        public string AddProductService(ProductService Model, List<MultipleMaster> MultipleMaster)
        {

            if (Model.EntityState == EntityState.New)
            {
                Model.ProductServiceID = Guid.NewGuid();
            }

            //if (Model.AboutProduct.PSHaveIPAddress == 2) Model.AboutProduct.PSIPAddress = "";
            //if (Model.AboutProduct.PSHaveTradeMark == 2) Model.AboutProduct.PSTradeMark = "";
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
            if (Model.EntityState == EntityState.New)
            {
                Model.SWOT.SWOTID = Guid.NewGuid();
             
            }
            
            return new BusinessManager().AddCompetitor(Model);
        }

        public ActionResult DeleteBuyerPersona(Guid BuyerPersonaID, Guid CustomerID)
        {
            new BusinessManager().DeleteBuyerPersona(BuyerPersonaID);
            IEnumerable<BuyerPersona> ModelList = new SellingManager().GetCustomerbuyerPersona(CustomerID);
            return PartialView("_CustomerBuyerPersonaPartial", ModelList);
        }

        public void GetBusinessOverviewDataDependency()
        {
            ViewBag.DisplayFounder = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Founder))
                              .Cast<PropertyDescriptor>()
                              .ToDictionary(p => p.Name, p => p.Description);

            ViewBag.DisplayCompanyDetails = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompanyDetails))
                                            .Cast<PropertyDescriptor>()
                                            .ToDictionary(p => p.Name, p => p.Description);

            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);
            ViewBag.RoleInCompany = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.BusinessOverview);
            ViewBag.CompanyLegalStructure = new Master().GetOptionMasterList((int)OptionType.CompanyLegalStructure);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview);

        }

        public void GetProductServiceDataDependency()
        {
            ViewBag.DisplayAboutProduct = TypeDescriptor.GetProperties(typeof(Business_Model.Model.AboutProduct))
                          .Cast<PropertyDescriptor>()
                          .ToDictionary(p => p.Name, p => p.Description);

            ViewBag.DisplayMVP = TypeDescriptor.GetProperties(typeof(Business_Model.Model.MVP))
                                            .Cast<PropertyDescriptor>()
                                            .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService);
            ViewBag.DevelopmentFarList = new Master().GetOptionMasterList((int)OptionType.ProductService_MVPDevelopmentFar);
            ViewBag.ProductServiceSelling = new Master().GetOptionMasterList((int)OptionType.ProductServiceSelling);
            ViewBag.ModuleVideo = new Master().GetSingleModuleVideo((int)ModuleName.ProductService);
            ViewBag.ProductServiceProduce = new Master().GetOptionMasterList((int)OptionType.ProductServiceProduce);
            ViewBag.MVPReason = new Master().GetOptionMasterList((int)OptionType.MVPReason);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_ProductorService);

        }
        public void GetBusinessCompetitorsDataDependency()
        {
            ViewBag.TitleCompetitorAnalysis = TypeDescriptor.GetProperties(typeof(Business_Model.Model.CompetitorAnalysis))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleSWOT = TypeDescriptor.GetProperties(typeof(Business_Model.Model.SWOT))
                                   .Cast<PropertyDescriptor>()
                                   .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Competitors);

        }

        public void GetBusinessOperationDataDependency()
        {
            ViewBag.TitleWhoDoesWhat = TypeDescriptor.GetProperties(typeof(Business_Model.Model.WhoDoesWhat))
                              .Cast<PropertyDescriptor>()
                              .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleOperations = TypeDescriptor.GetProperties(typeof(Business_Model.Model.Operations))
                                    .Cast<PropertyDescriptor>()
                                    .ToDictionary(p => p.Name, p => p.Description);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);
            ViewBag.DeliveryMethod = new Master().GetOptionMasterList((int)OptionType.Operation_DeliveryMethod);
            ViewBag.ThirdPartiesInvloved = new Master().GetOptionMasterList((int)OptionType.Operation_ThirdPartiesInvolved);
            ViewBag.PaymentMethod = new Master().GetOptionMasterList((int)OptionType.Operation_PaymentMethod);
            ViewBag.StaffWork = new Master().GetOptionMasterList((int)OptionType.Operation_StaffWork);
            ViewBag.Choice = new Master().GetOptionMasterList((int)OptionType.GeneralTwoOption);
            ViewBag.ModuleQustionVideo = new Master().GetModuleQuestionVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_BusinessOperation);

        }




    }

   

}