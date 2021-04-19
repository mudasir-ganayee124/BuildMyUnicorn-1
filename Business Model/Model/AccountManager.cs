using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class AccountManager : Common
    {
        public Guid ID { get; set; }
        public Guid AccountApprovalID { get; set; }
        public Guid AccountApprovalMasterID { get; set; }
        public AccountType AccountType { get; set; }
        public Guid EntityID { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public Guid ManagedBy { get; set; }
        public DateTime ManagedDateTime { get; set; }


    }
    public class _AccountManager
    {
        public Guid ID { get; set; }
        public Guid EntityID { get; set; }
        public Guid ManagedBy { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }

    }

    public class ChangePassword
   {
        public Guid ClientID { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

   }
   
}
