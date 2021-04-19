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
            int State = (int)EntityState.New;
            if (TeamList == null || TeamList.Count() == 0)
            {
                if (ResponseType.Redirect.ToString() == CheckModuleCourse(State, (int)ModuleSection.TheBusiness_Team))
                {
                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Team",
                        ActionName = "Index",
                        ModuleName = Module.TheBusiness.ToString(),
                        SectionName = ModuleSection.TheBusiness_Team.ToString()
                    });
                }

            }
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Team);
            return View(TeamList);
        }

        public string AddClientTeam(ClientTeam Model)
        {
            if (Model.MemberType == MemberType.Contributor)
            {
                Random rnd = new Random();
                Model.Password = UniqueKey.GetUniqueKey();
            }
            return new ClientManager().AddTeamMemeber(Model);
        }

        public string AddTeamInfo(string TeamInfo)
        {
            return new ClientManager().AddTeamInfo(TeamInfo);
        }

        public string UpdateClientTeam(ClientTeam Model)
        {
            return new ClientManager().UpdateClientTeam(Model);
        }

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
            ViewBag.CountryList =  new CountryManager().GetCountryList();
            return View();
        }

        public ActionResult Edit()
        {
            return View();
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

        public string CheckModuleCourse(int State, int SectionValue)
        {
            if (State == 0)
            {
                string getValue = "0";
                string getClientID = string.Empty;
                string LoginUserID = User.Identity.Name.ToString();
                string SectionName = Enum.GetName(typeof(ModuleSection), SectionValue);
                string CookieID = SectionName.ToString() + LoginUserID;
                if (Request.Cookies[CookieID.ToString()] != null)
                {
                    HttpCookie aCookie = Request.Cookies[CookieID.ToString()];
                    getValue = aCookie.Values["Status"];
                }
                else
                {
                    HttpCookie appCookie = new HttpCookie(CookieID.ToString());
                    appCookie.Values["Status"] = "0";
                    appCookie.Values["ClientID"] = User.Identity.Name.ToString();
                    appCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(appCookie);
                }
                ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.TheBusiness, SectionValue);

                if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                    return ResponseType.Redirect.ToString();
                else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }
    }
}