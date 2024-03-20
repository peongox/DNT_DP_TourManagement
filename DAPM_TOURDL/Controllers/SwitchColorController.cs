using DAPM_TOURDL.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_TOURDL.Controllers
{
    public class SwitchColorController : Controller
    {
        public ActionResult ChangeMode()
        {
            Invoker invoker = new Invoker(Session);
            return invoker.ChangeMode();
        }
    }
}