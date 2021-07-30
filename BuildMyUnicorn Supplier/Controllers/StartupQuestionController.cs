using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn_Supplier.Business_Layer;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class StartupQuestionController : WebController
    {
        // GET: StartupQuestion
        public ActionResult Index()
        {
            ViewBag.question = new SupplierManager().GetSupplierQustionForm();
            if (ViewBag.question == null)
            {
                ViewBag.question = new SupplierQuestion();
                ViewBag.question.QuestionForm = "[]";
            } 
            return View();
        }

        public ActionResult Analytics(string id)
        {
            ViewBag.SurveyAnalyticsForm = new SupplierManager().GetSupplierQustionForm();
            ViewBag.SurveyData = new SupplierManager().GetSurveyData(Guid.Parse(id));
            return View();
        }

        public string AddQuestion(Supplier Model)
        {
            if (Model.OrginalValue != Model.QuestionForm)
                return new SupplierManager().AddNewQuestionForm(Model);
            else
                return "NOTCHANGED";
        }
    }
}