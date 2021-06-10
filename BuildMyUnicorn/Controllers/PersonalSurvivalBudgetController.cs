using BuildMyUnicorn.Business_Layer;
using System.Web.Mvc;
using Business_Model.Model;

namespace BuildMyUnicorn.Controllers
{
    public class PersonalSurvivalBudgetController : WebController
    {
        // GET: PersonalSurvivalBudget
        public ActionResult Index()
        {
         
            ViewBag.Expenses = new Master().GetOptionMasterList((int)OptionType.PersonalSurvivalBudget_Expenses);
            ViewBag.Income = new Master().GetOptionMasterList((int)OptionType.PersonalSurvivalBudget_Income);
            return View();
        }
    }
}