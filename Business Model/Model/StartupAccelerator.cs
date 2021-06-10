using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
   public class StartupAccelerator :Common
    {
        public Guid StartupAcceleratorID { get; set; }
        public Guid LinkID { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string MainContact { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int ClientNumber { get; set; }
        public string ImageID { get; set; }
        public Guid ConfirmationID { get; set; }
    }
}
