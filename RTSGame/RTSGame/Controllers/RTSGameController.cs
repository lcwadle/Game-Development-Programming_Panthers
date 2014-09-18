using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTSGame.Controllers
{
    public class RTSGameController : Controller
    {
        /// <summary>
        /// Index Action is the start up action for MVC class. This action will get the data for controller and 
        /// will fire up the system.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }

    }
}
