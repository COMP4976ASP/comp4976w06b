using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxLab.Controllers
{
    public class AjaxHelperController : Controller
    {
        // GET: AjaxHelper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PureJavaScriptAjax()
        {
            return View();
        }

        public ActionResult SimpleJqueryAjax()
        {
            return View();
        }
    }
}