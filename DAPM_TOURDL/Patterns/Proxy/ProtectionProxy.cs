using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAPM_TOURDL.Controllers;
using DAPM_TOURDL.Models;

namespace DAPM_TOURDL.Patterns.Proxy
{
    public class ProtectionProxy : IService
    {
        private User user;
        private LoggingController service;
        private TourDLEntities db = new TourDLEntities();

        public ProtectionProxy(User user, TourDLEntities db)
        {
            this.user = user;
            this.db = db;
        }

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

        public void NavigateTo()
        {
            //if(CheckAccess(this.user) == "cus")
            //{
            //    //dieu huong qua trang kh


            //}
            //else if(CheckAccess(this.user) == "emp")
            //{
            //    //dieu huong qua trang nv
            //}
            //else
            //{

            //}
        }
    }
}