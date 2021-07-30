using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;

namespace Business_Model.Model
{
    public class SurveyTemplates : Common
    {
        public Guid TemplateID { get; set; }
        public string Title { get; set; }
        public string BgColor { get; set; }
        public string Icon { get; set; }
        public string Template { get; set; }
        public EntityState EntityState { get; set; }
        public SurveyTemplateType SurveyTemplateType { get; set; }
    }
}
