using DAPM_TOURDL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_TOURDL.Patterns.Command
{
    public class LightCommand:ICommand
    {
        public ActionResult Execute(HttpSessionStateBase session)
        {
            session["BackgroundColor_CLient"] = "#ffffff";
            return new JsonResult
            {
                Data = session["BackgroundColor_Client"],
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}