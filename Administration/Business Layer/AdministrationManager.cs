using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administration.Business_Layer
{
    public class AdministrationManager
    {
        public IEnumerable<Gateway> GetAllGateway()
        {
            var query = $@"select * from tbl_gateway";
            return SharedManager.GetList<Gateway>(query).ToList();

        }
    }
}