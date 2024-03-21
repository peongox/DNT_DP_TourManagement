using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL.Patterns.Observer
{
    public class NotificationHub : Hub
    {
        public void SendNotificationToAdmin(string message)
      {
          Clients.All.receiveNotification(message);
      }
    }
}