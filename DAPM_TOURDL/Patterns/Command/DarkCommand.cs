using DAPM_TOURDL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_TOURDL.Patterns.Command
{
    public class DarkCommand:ICommand
    {
        public ActionResult Execute(HttpSessionStateBase session)
        {
            session["BackgroundColor_CLient"] = "#212121";
            session["BackgroundColor_Client_Header"] = "rgb(73, 73, 73, 0.5)";
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