using Business_Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Language: Common
    {
        public Guid LanguageID { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set;   }
        public EntityState EntityState { get; set; }

    }
    public class LanguageModule : Common
    {
        public Guid LanguageModuleID { get; set; }
        public Guid LanguageID { get; set; }
        public int ModuleID { get; set; }
        public int ModuleSectionID { get; set; }
        public bool IsDefault { get; set; }
        public int DisplayOrder { get; set; }
        public string QuestionText { get; set; }
        public string PlaceHolderText { get; set; }
        public string PropertyName { get; set; }
        public string TableName { get; set; }
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string SectionName { get; set; }


    }

    public class QuestionVideo : Common
    {
        public Guid QuestionVideoID { get; set; }
        public int ModuleID { get; set; }
        public int ModuleSectionID { get; set; }
        public int DisplayOrder { get; set; }
        public string PropertyName { get; set; }
        public string TableName { get; set; }
        public string VideoUrl { get; set; }
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string SectionName { get; set; }


    }

}
