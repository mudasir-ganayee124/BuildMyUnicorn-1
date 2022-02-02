using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;

namespace BuildMyUnicorn.Controllers
{
    public class TeamController : WebController
    {
        // GET: Team
        public ActionResult Index()
        {

            IEnumerable<ClientTeam> TeamList = new ClientManager().GetClientTeam();
            int State = TeamList.Count() == 0 ? (int)EntityState.New : (int)EntityState.Old;
            if (State == (int)EntityState.New)
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.TheBusiness;
                objlog.ModuleSectionID = ModuleSection.TheBusiness_Team;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Team) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Team",
                        ActionName = "Index",
                        ModuleID = (int)Module.TheBusiness,
                        SectionID = (int)ModuleSection.TheBusiness_Team
                    });
                }
            }
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Team);
            return View(TeamList);
        }

        public string AddClientTeam(ClientTeam Model)
        {
            bool EmailSend = false;
            if (Model.EntityState == EntityState.New)
            {
                Model.ClientID = Guid.NewGuid();
                Model.TeamClientID = new ClientManager().GetMainClientID(Guid.Parse(User.Identity.Name));
            }

            if (Model.MemberType == MemberType.Contributor)
            {
                if (string.IsNullOrEmpty(Model.Password))
                {
                    Random rnd = new Random();
                    Model.Password = UniqueKey.GetUniqueKey();
                    EmailSend = true;
                }
            }
            return new ClientManager().AddTeamMemeber(Model, EmailSend);
        }

        public string AddTeamInfo(string TeamInfo)
        {
            return new ClientManager().AddTeamInfo(TeamInfo);
        }

        //public string UpdateClientTeam(ClientTeam Model)
        //{
        //    return new ClientManager().UpdateClientTeam(Model);
        //}

        public string UpdateClientProfile(Client Model)
        {
            return new ClientManager().UpdateClientProfile(Model);
        }
        public string UpdateTeamProfile(Client Model)
        {
            return new ClientManager().UpdateTeamProfile(Model);
        }

        public ActionResult Create()

        {
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.CountryList = new CountryManager().GetCountryList();
            Client Model = new Client();
            Model.EntityState = EntityState.New;
            return View(Model);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.CountryList = new CountryManager().GetCountryList();
            Client Model = new ClientManager().GetClient(Guid.Parse(id));
            Model.EntityState = EntityState.Old;
            return View("Create", Model);
        }

        public string DeleteTeamMember(Guid ClientID)
        {
            return new ClientManager().DeleteTeamMember(ClientID);
        }

        public string FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string FileName = System.IO.Path.GetFileName(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string basePath = Server.MapPath("~/Content/Images/");
                string filePath = System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), FileName);
                file.SaveAs(filePath);
                string fileGuid = guid + Path.GetExtension(filePath);
                var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), fileGuid);
                System.IO.File.Move(filePath, newFilePath);
                return fileGuid;
            }
            else
            {
                return "!OK";
            }

        }


    }
}