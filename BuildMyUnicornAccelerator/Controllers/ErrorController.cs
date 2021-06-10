using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicornAccelerator.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Errror Code 400
        /// </summary>
        /// <returns></returns>
        public ActionResult BadRequest()
        {

            ViewBag.Title = "Not Found";
            return View();
        }
        /// <summary>
        /// Errror Code 401
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorized()
        {

            return View();
        }
        /// <summary>
        /// Errror Code 402
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentRequired()
        {

            return View();
        }
        /// <summary>
        /// Errror Code 403
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {

            return View();
        }
        /// <summary>
        /// Errror Code 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {

            return View();
        }
    }
}