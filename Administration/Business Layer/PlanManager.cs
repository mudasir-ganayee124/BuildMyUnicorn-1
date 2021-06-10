using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administration.Business_Layer
{
    public class PlanManager
    {
        public IEnumerable<Plan> GetAllPlan()
        {
            var query = $@"select * from tbl_plan";
            return SharedManager.GetList<Plan>(query).ToList();

        }
    }
}