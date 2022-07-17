using System;
using System.Collections.Generic;
using System.Linq;
using CV19.Web;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class WebServerTest
    {
        public static void Run()
        {
            var server = new WebServer(8080);
            server.RequestReceiver += Server_RequestReceiver;

            server.Start();
            Console.WriteLine("Сервер запущен");

            Console.ReadLine();
        }

        private static void Server_RequestReceiver(object? sender, RequestReceiverEventArgs e)
        {
            var context = e.Context;
            Console.WriteLine($"Connection {context.Request.UserHostAddress}");
            using var writter = new StreamWriter(context.Response.OutputStream);
            writter.WriteLine("Hello from Test Web Server");
        }
    }
}
