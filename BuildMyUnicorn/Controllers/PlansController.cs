using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;

namespace BuildMyUnicorn.Controllers
{
    [AllowCrossSite]
    public class PlansController : Controller
    {
        // GET: Plans
        public ActionResult Index()
        {          
            return View(new Master().GetAllPlan().OrderBy(x => x.DisplayOrder));
        }
        
        [HttpGet]
        public JsonResult Get()
        {
          var list = new Master().GetAllPlan().OrderBy(x => x.DisplayOrder);
          return  Json(new { plan = list, total = list.Count() }, JsonRequestBehavior.AllowGet);

        }
    }
}