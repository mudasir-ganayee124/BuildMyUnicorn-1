using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    public class PlansController : Controller
    {
        // GET: Plans
        public ActionResult Index()
        {          
            return View(new Master().GetAllPlan().OrderBy(x => x.DisplayOrder));
        }
    }
}