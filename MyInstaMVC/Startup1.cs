using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using MyInstaMVC.Controllers;
using MyInstaMVC.signalr;
using Owin;

[assembly: OwinStartup(typeof(MyInstaMVC.Startup1))]

namespace MyInstaMVC
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            var idProvider = new ChatController();

            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider),() => idProvider);
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
