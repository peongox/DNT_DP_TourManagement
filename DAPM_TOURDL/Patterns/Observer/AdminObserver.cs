using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL.Patterns.Observer
{
    public class AdminObserver:IObserver
    {
        private string notificationAdmin;
        public void Update(string message)
        {
            notificationAdmin = message;
        }
        public string getNotificationMessage()
        {
            return notificationAdmin;
        }
    }
}