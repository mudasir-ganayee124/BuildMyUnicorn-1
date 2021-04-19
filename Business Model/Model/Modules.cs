using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Modules
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public int ModuleID { get; set; }
        public int ModuleSectionID { get; set; }
        public string DisplayAs { get; set; }
        public string SectionDisplayAs { get; set; }
        public Guid SectionID { get; set; }
    }
    public class ModulesSection
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public int ModuleID { get; set; }
        public int ModuleSectionID { get; set; }
        public string DisplayAs { get; set; }
    }
}
