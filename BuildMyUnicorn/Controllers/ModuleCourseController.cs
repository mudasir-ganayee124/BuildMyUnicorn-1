using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    public class ModuleCourseController : WebController
    {

        public ActionResult Index(string ControllerName, string ActionName, int ModuleID, int SectionID)
        {

            ModuleCourse objCourse = new Master().GetSingleModuleCourse(ModuleID, SectionID);
            objCourse.ContollerName = ControllerName;
            objCourse.ActionName = ActionName;
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList(ModuleID, SectionID);
            return View(objCourse);
        }

        public ActionResult GetStarted(ModuleCourse Model)
        {
            ModuleCourselog objlog = new ModuleCourselog();
            objlog.ModuleID = Model.ModuleID;
            objlog.ModuleSectionID = Model.ModuleSectionID;
            objlog.Status = (int)ResponseType.NotRedirect;
            var obj = new Master().AddModuleCourselog(objlog);
            return RedirectToAction(Model.ActionName.ToString(), Model.ContollerName.ToString());

        }
        
    }
}