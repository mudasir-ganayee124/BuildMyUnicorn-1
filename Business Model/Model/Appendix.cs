using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Appendix : Common
    {
        public Guid AppendixID { get; set; }
        public string Keyword { get; set; }
        public string Category { get; set; }
        public string Definition { get; set; }
   

    }
    public class _Appendix 
    {
        public Guid AppendixID { get; set; }
        public string Keyword { get; set; }
        public string Category { get; set; }
        public string Definition { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
