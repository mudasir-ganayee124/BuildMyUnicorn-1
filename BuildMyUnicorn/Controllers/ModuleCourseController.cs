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
        public ActionResult Index(string ControllerName, string ActionName, string ModuleName, string SectionName)
        {
            
            ViewBag.ControllerName = ControllerName;
            ViewBag.ActionName = ActionName;
            ViewBag.ModuleName = SectionName;
            int ModuleID = (int)Enum.Parse(typeof(Module), ModuleName);
            int SectionID = (int)Enum.Parse(typeof(ModuleSection), SectionName);
            ModuleCourse objCourse = new Master().GetSingleModuleCourse(ModuleID, SectionID);
            ViewBag.BusinessSupplier = new Master().GetBusinessSupplierList(ModuleID, SectionID);
            if (objCourse.ModuleCourseID == Guid.Empty)
            {
                return RedirectToAction(ActionName, ControllerName);
            }
            return View(objCourse);
        }
        public ActionResult Action(string ControllerName, string ActionName, string ModuleName)
        {
            string CookieID = ModuleName.ToString() + User.Identity.Name.ToString();
            Response.Cookies[CookieID.ToString()].Values["Status"] = "1";
            return RedirectToAction(ActionName, ControllerName);
        }
        //public ActionResult Return(string ControllerName, string ActionName)
        //{

        //   //Response.Cookies[SectionName.ToString()].Values["Status"] = "0";
        //   return RedirectToAction("Index", "ModuleCourse", new
        //    {
        //        ControllerName = ControllerName,
        //        ActionName = ActionName
               
        //    });
        //}
    }
}