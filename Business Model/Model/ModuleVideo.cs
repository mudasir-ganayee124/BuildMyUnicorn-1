using Business_Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class ModuleVideo :Common
    {
        public Guid ModuleVideoID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleSectionName { get; set; }
        public int ModuleID { get; set; }
        public int ModuleSectionID { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public int Duration { get; set; }
        public EntityState EntityState { get; set; }
    }
}
