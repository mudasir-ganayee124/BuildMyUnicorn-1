using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Helper;

namespace Administration.Controllers
{
    public class ManageModuleCourseController : Controller
    {
        // GET: ModuleCourse
        public ActionResult Index()
        {
            ViewBag.Modules = new Master().GetModuleList();
            return View();
        }
        public JsonResult GetAll()
        {
            Master obj = new Master();
            IEnumerable<ModuleCourse> list = obj.GetModuleCourseList();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(string ID)
        {
      
            return Json(new Master().GetSingleModuleCourse(ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModuleSection(int ModuleID)
        {
            List<Modules> ModuleList = new Master().GetModuleList().ToList();
            return Json(ModuleList.Where(x=>x.ModuleID == ModuleID), JsonRequestBehavior.AllowGet);
        }

        public string Add(ModuleCourse Model)
        {
            Model.EntityState = EntityState.New ;
            Model.ModuleCourseID= Guid.NewGuid();
            return new Master().AddModuleCourse(Model);
        }

        public string Update(ModuleCourse Model)
        {

            Model.EntityState = EntityState.Old;
            return new Master().AddModuleCourse(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteModuleCourse(ID);
        }
    }
}