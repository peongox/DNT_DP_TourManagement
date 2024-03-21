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
            session["BackgroundColor_Client_Header"] = "rgba(225,225,225,0.3)";
            var data = new
            {
                backgroundColor = session["BackgroundColor_CLient"],
                header = session["BackgroundColor_Client_Header"]
            };
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}