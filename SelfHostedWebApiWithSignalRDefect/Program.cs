using System;
using Microsoft.Owin.Hosting;

namespace SelfHostedWebApiWithSignalRDefect
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webApp = WebApp.Start<Startup>(url: "http://localhost:8888"))
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}
