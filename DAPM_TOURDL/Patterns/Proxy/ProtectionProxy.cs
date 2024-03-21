using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using DAPM_TOURDL.Controllers;
using DAPM_TOURDL.Models;
using static System.Net.WebRequestMethods;

namespace DAPM_TOURDL.Patterns.Proxy
{
    public class ProtectionProxy : IService
    {
        private User user;
        private Service service;
        private TourDLEntities db = new TourDLEntities();

        public ProtectionProxy(User user, TourDLEntities db)
        {
            service = new Service();
            this.user = user;
            this.db = db;
        }

        //kiểm tra user là kh hay nv
        public object CheckAccess()
        {
            var kh = db.KHACHHANGs.FirstOrDefault(s => s.Mail_KH.Equals(user.Username) && s.MatKhau.Equals(user.Password));
            if(kh != null)
            {
                return kh;
            }
            else
            {
                var nv = db.NHANVIENs.FirstOrDefault(s => s.Mail_NV.Equals(user.Username) && s.MatKhau.Equals(user.Password));
                return nv;
            }
        }

        //điều hướng user là nv qua controller của nv
        public ActionResult NavigateTo(HttpSessionStateBase S)
        {
            var result = CheckAccess();
            if (result != null)
            {
                if (result is NHANVIEN)
                {
                    var nv=(NHANVIEN)result;
                    S["IDUserAdmin"] = nv.ID_NV;
                    S["HoTen"] = nv.HoTen_NV;
                    S["Email"] = nv.Mail_NV;
                    return service.NavigateTo(S);
                }
                else
                {
                    var kh = (KHACHHANG)result;
                    S["UsernameSS"] = kh.HoTen_KH.ToString();
                    S["IDUser"] = kh.ID_KH;
                    HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                    var urlHelper = new UrlHelper(httpContext.Request.RequestContext);
                    var clientUrl = urlHelper.Action("HomePage", "Home", new { id = S["IDUser"] });

                    return new RedirectResult(clientUrl);
                }
            }
            else return null;
        }
    }
}