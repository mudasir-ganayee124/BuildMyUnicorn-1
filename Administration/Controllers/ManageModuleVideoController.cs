using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;

namespace Administration.Controllers
{
    public class ManageModuleVideoController : Controller
    {
        // GET: ManageModuleVideo
        public ActionResult Index()
        {
            ViewBag.Modules = new Master().GetModuleList();
            return View();
        }
        public JsonResult GetAll()
        {
            Master obj = new Master();
            IEnumerable<ModuleVideo> list = obj.GetModuleVideoList();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(string ID)
        {
            return Json(new Master().GetSingleModuleVideo(Guid.Parse(ID)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModuleSection(int ModuleID)
        {
            List<Modules> ModuleList = new Master().GetModuleList().ToList();
            return Json(ModuleList.Where(x => x.ModuleID == ModuleID), JsonRequestBehavior.AllowGet);
        }

        public string Add(ModuleVideo Model)
        {
            Model.EntityState = EntityState.New;
            Model.ModuleVideoID = Guid.NewGuid();
            return new Master().AddModuleVideo(Model);
        }

        public string Update(ModuleVideo Model)
        {

            Model.EntityState = EntityState.Old;
            return new Master().AddModuleVideo(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteModuleVideo(ID);
        }
     
        
    }
}