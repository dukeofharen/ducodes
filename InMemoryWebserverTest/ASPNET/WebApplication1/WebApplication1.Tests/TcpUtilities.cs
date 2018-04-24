﻿using System.Net;
using System.Net.Sockets;

namespace WebApplication1.Tests
{
   public static class TcpUtilities
   {
      public static int GetFreeTcpPort()
      {
         var listener = new TcpListener(IPAddress.Loopback, 0);
         listener.Start();
         var port = ((IPEndPoint)listener.LocalEndpoint).Port;
         listener.Stop();
         return port;
      }
   }
}
