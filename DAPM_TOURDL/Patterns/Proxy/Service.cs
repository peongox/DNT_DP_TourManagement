using DAPM_TOURDL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_TOURDL.Patterns.Proxy
{
    public class Service : IService
    {
        public ActionResult NavigateTo(HttpSessionStateBase S)
        {
            HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
            var urlHelper = new UrlHelper(httpContext.Request.RequestContext);
            var clientUrl = urlHelper.Action("GetData", "NHANVIENs");

            return new RedirectResult(clientUrl);
        }
    }
}