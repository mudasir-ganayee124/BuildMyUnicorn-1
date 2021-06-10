using Business_Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class ModuleCourse : Common
    {
        public Guid ModuleCourseID { get; set; }
        public string ContollerName { get; set; }
        public string ActionName { get; set; }
        public string ModuleName { get; set; }
        public string ModuleSectionName { get; set; }
        public Module ModuleID { get; set; }
        public ModuleSection ModuleSectionID { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public IEnumerable<ModuleCourseOption> ModuleCourseOption { get; set; }
        public EntityState EntityState { get; set; }
    }
    public class ModuleCourseOption
    {
        public Guid MCOptionID { get; set; }
        public Guid ModuleCourseID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }

    }
    public class _ModuleCourse : Common
    {
        public Guid ModuleCourseID { get; set; }
        public string ModuleName { get; set; }
        public Module ModuleID { get; set; }
        public ModuleSection ModuleSectionID { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public Guid MCOptionID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }


    }
    public class ModuleCourselog : Common
    {
        public Guid ModuleCourseLogID { get; set; }
        public Guid ClientID { get; set; }
        public Module ModuleID { get; set; }
        public ModuleSection ModuleSectionID { get; set; }
        public int Status { get; set; }
    }
}
