using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class AppendixSearchLog
    {
        public Guid AppdendixLogID { get; set; }
        public string Keyword { get; set; }
        public bool IsFound { get; set; }
        public DateTime QueryDate { get; set; }
        public AppendixType AppendixType { get; set; }
        public Guid ClientID { get; set; }
        public bool IsDeleted { get; set; }
     
    }

    public class _AppendixSearchLog
    {
        public Guid AppdendixLogID { get; set; }
        public string Keyword { get; set; }
        public bool IsFound { get; set; }
        public DateTime QueryDate { get; set; }
        public AppendixType AppendixType { get; set; }
        public Guid ClientID { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public string CountryName { get; set; }
        public string StartupName { get; set; }
    }
}
