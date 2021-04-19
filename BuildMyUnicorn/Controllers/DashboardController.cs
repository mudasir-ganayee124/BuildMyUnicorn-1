using System;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;

namespace BuildMyUnicorn.Controllers
{
   
    public class DashboardController : WebController
    {
        private readonly ToDoTaskManager _todoManager;

        public DashboardController()
        {
            _todoManager = new ToDoTaskManager();
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            return View();
        }

        public JsonResult GetClientIdeaProgressData()
        {
            return Json(new Dashboard().GetIdeaProgressData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TodoList()
        {
            ViewBag.AssignedTo = new SelectList(_todoManager.GetTeamMembers(), "Key", "Value");
            return PartialView("_ToDoList", _todoManager.GetTodoList());
        }

        public ActionResult AssignedToDoList()
        {
            return PartialView("_AssignedToDo", _todoManager.GetAssignedToDoList());
        }

        public JsonResult UpdateToDoStatus(ToDoStatus status, Guid toDoTaskId)
        {
            return Json(_todoManager.UpdateToDoStatus(status, toDoTaskId));
        }

    }
}