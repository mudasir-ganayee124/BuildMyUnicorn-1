using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;
using System.Web.Mvc;

namespace Business_Model.Model
{
   public  class RecommendDocument : Common
    {
        public Guid RecommendDocumentID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public EntityState EntityState { get; set; }
    }
}
