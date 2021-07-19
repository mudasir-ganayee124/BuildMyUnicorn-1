using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Model.Helper;
using Business_Model.Model;

namespace Business_Model.Model
{
    class Widgets
    {


    }

    public class SubscribedPackages
    {
        public OrderType OrderType { get; set; }
        public string PackageTitle { get; set; }
        public decimal PackageAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartupName { get; set; }
        public string CompanyName { get; set; }
        public DateTime SubscribedDate { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
