using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/plans")]
    public class GetPlansController : ApiController
    {

        public HttpResponseMessage Get()
        {

            var response = Request.CreateResponse(HttpStatusCode.OK, new Master().GetAllPlan().OrderBy(x => x.DisplayOrder).ToList());
            return response;


        }
        // GET api/<controller>  

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/getp")]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
    }
}
