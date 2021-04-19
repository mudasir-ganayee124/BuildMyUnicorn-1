using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    public class FinancialProjectionController : WebController
    {
        // GET: FinancialProjection
        public ActionResult Index()
        {
       
            return View();
        }

        public ActionResult GetFinancialProjection()
        {
            IEnumerable<FinancialProjection> ModelList = new FinanceManager().GetFinanceProjection();
            return PartialView("_FinancialProjectionPartial", ModelList);
        }

        public ActionResult AddFinancialProjection(FinancialProjection Model)
        {
            Model.ProjectionID = Guid.NewGuid();
            string result = new FinanceManager().AddFinanceProjection(Model);
            IEnumerable<FinancialProjection> ModelList = new FinanceManager().GetFinanceProjection();
            return PartialView("_FinancialProjectionPartial", ModelList);

        }

      
    }
}