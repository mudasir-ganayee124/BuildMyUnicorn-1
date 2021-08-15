using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class GetPlansController : ApiController
    {
        
        public HttpResponseMessage Get()
        {
             
            var response = Request.CreateResponse(HttpStatusCode.OK, new Master().GetAllPlan().OrderBy(x => x.DisplayOrder).ToList());
            return response;
            

        }
    }
}
