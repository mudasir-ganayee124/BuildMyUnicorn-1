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
    public class SellingController : WebController
    {
        // GET: Pricing
        public ActionResult ProductServicePricing(string id)
        {
            int State = (int)EntityState.New;
            _PricingProductService Model = new SellingManager().GetPricingProductService();
            if (Model != null && Model.ProductServicePricingID.ToString() != id) State = (int)EntityState.Old;
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Selling, (int)ModuleSection.Selling_Pricing);
            if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.Selling_Pricing))
            {
                return RedirectToAction("Index", "ModuleCourse", new
                {
                    ControllerName = "Selling",
                    ActionName = "ProductServicePricing",
                    ModuleName = Module.Selling.ToString(),
                    SectionName = ModuleSection.Selling_Pricing.ToString()
                });
            }
            return View(State);
        }


        public ActionResult New(int Type)
        {


            if ((int)ModuleSection.Selling_Pricing == Type)
            {


                ViewBag.TitleChosePricingStrategy = TypeDescriptor.GetProperties(typeof(Business_Model.Model.ChosePricingStrategy))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                PricingProductService obj = new PricingProductService();
                ChosePricingStrategy objChosePricingStrategy = new ChosePricingStrategy();
                _PricingProductService Model = new SellingManager().GetPricingProductService();

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
                    obj.ChosePricingStrategy = objChosePricingStrategy;
                    obj.EntityState = EntityState.Old;
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.ChosePricingStrategy = objChosePricingStrategy;
                }


                return PartialView("_NewProductServicePricingPartial", obj);
            }
            return PartialView("_NewObservationPartial");
        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.Selling_Pricing == Type)
            {


                ViewBag.TitleChosePricingStrategy = TypeDescriptor.GetProperties(typeof(Business_Model.Model.ChosePricingStrategy))
                                       .Cast<PropertyDescriptor>()
                                       .ToDictionary(p => p.Name, p => p.Description);
                PricingProductService obj = new PricingProductService();
                ChosePricingStrategy objChosePricingStrategy = new ChosePricingStrategy();
                _PricingProductService Model = new SellingManager().GetPricingProductService();

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
                    obj.ChosePricingStrategy = objChosePricingStrategy;
                    obj.EntityState = EntityState.Old;
                }
                else
                {
                    obj.EntityState = EntityState.New;
                    obj.ChosePricingStrategy = objChosePricingStrategy;
                }


                return PartialView("_DetailProductServicePricingPartial", obj);
            }
            return PartialView("_NewObservationPartial");


        }

        public string AddProductServicePricing(PricingProductService Model)
        {

            if (Model.EntityState == EntityState.New)
                Model.ProductServicePricingID = Guid.NewGuid();
            return new SellingManager().AddPricingProductService(Model);
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
                ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.Selling, SectionValue);

                if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                    return ResponseType.Redirect.ToString();
                else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }

    }
}