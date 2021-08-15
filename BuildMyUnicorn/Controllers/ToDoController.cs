using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class ToDoController : WebController
    {
        private readonly ToDoTaskManager _todoManager;

        public ToDoController()
        {
            _todoManager = new ToDoTaskManager();
        }


        public ActionResult Index()
        {
            ToDoTaskManager obj=   new ToDoTaskManager();
            ViewBag.AssignedTo = obj.GetTeamMembers();
            return View();
        }


        public ActionResult TodoList()
        {
            return PartialView("_TodoPartial", _todoManager.GetTodoList());
        }


        public ActionResult Add(string id)
        {
            var StatusEnumData = from Status e in Enum.GetValues(typeof(Status))
                                 select new
                                 {
                                     ID = (int)e,
                                     Name = e.ToString()
                                 };
            var PriorityEnumData = from Priority e in Enum.GetValues(typeof(Priority))
                                   select new
                                   {
                                       ID = (int)e,
                                       Name = e.ToString()
                                   };
            ViewBag.Status = new SelectList(StatusEnumData, "ID", "Name");

            ViewBag.Priority = new SelectList(PriorityEnumData, "ID", "Name");
            ToDoTaskManager obj = new ToDoTaskManager();
            ViewBag.AssignedTo = obj.GetTeamMembers();
            ToDoTask Model = new ToDoTask();
            Model.EntityState = EntityState.New;
            if (id != null)
            {
                Model.EntityState = EntityState.Old;
                Model = obj.GetSingleToDo(Guid.Parse(id));
            }
           
            return View(Model);
        }


        [HttpPost]
        public string Add(ToDoTask Model)
        {
           
            if(Model.EntityState == EntityState.New)
                Model.ToDoTaskID = Guid.NewGuid();
            ToDoTaskManager obj = new ToDoTaskManager();
            ViewBag.AssignedTo = obj.GetTeamMembers();
            return obj.AddTodo(Model);
            //ResponseModel response = new ResponseModel();
            //if (ModelState.IsValid)
            //{
            //    response = _todoManager.SaveToDo(todo);

            //    if (!response.HasError)
            //    {
            //        response.Message = "To-Do created successfully ";
            //        return Json(response);
            //    }

            //}
            //else
            //{
            //    response.Error = "Model Validation Error";
            //}
            //Response.StatusCode = 400;
            //return Json(response);
        }

        [HttpPost]
        public string Update(ToDoTask Model)
        {
          
            Model.EntityState = EntityState.Old;
            ToDoTaskManager obj = new ToDoTaskManager();
            ViewBag.AssignedTo = obj.GetTeamMembers();
            return obj.AddTodo(Model);
            //ResponseModel response = new ResponseModel();
            //if (ModelState.IsValid)
            //{
            //    response = _todoManager.SaveToDo(todo);

            //    if (!response.HasError)
            //    {
            //        response.Message = "To-Do created successfully ";
            //        return Json(response);
            //    }

            //}
            //else
            //{
            //    response.Error = "Model Validation Error";
            //}
            //Response.StatusCode = 400;
            //return Json(response);
        }

        public ActionResult Edit(Guid id)
        {
            var todoTask = _todoManager.GetTodoItem(id);
            FillTeamMemberDropDown(todoTask.AssignedMappings.Select(x => x.AssignedToId).ToList());
            return View(todoTask);
        }

        [HttpPost]
        public JsonResult Edit(ToDoTask todo)
        {
            ResponseModel response = new ResponseModel();
            if (ModelState.IsValid)
            {
                response = _todoManager.UpdateToDo(todo);

                if (!response.HasError)
                {
                    response.Message = "To-Do updated successfully ";
                    return Json(response);
                }

            }
            else
            {
                response.Error = "Model Validation Error";
            }
            Response.StatusCode = 400;
            return Json(response);
        }


        [HttpPost]
        public JsonResult DeleteTodo(Guid id)
        {
            return Json(_todoManager.DeleteToDo(id));
        }


        public void FillTeamMemberDropDown(List<Guid> ids = null)
        {
            ViewBag.AssignedTo = new SelectList(_todoManager.GetTeamMembers(), "Key", "Value", ids);
        }
    }
}