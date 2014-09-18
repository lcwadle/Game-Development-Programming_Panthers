using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RTSGame.Hubs
{
    public class updateHub : Hub
    {
        public void callServer(String msg)
        {
            Clients.All.callClient(msg);
        }
    }
}