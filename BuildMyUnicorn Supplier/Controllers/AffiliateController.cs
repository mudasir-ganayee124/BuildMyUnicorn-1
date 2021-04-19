using BuildMyUnicorn_Supplier.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class AffiliateController : WebController
    {
        // GET: Affilate
        public ActionResult Index()
        {
            ViewBag.AffiliateRules = new MasterManager().GetOptionMasterList((int)OptionType.AffiliateRules);
            List<Affiliate> Model = new AffiliateManager().GetAffiliateList().ToList();
            ViewBag.AmountEarned = Model.Sum(x=> x.AmountEarned);
            ViewBag.Affiliate = Model;
            return View();
        }
    }
}