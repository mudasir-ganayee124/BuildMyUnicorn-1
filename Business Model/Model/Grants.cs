using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
  
    public class Grants : Common
    {
        public Guid GrantID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string VideoUrl { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

}
