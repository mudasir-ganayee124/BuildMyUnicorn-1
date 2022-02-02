using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(BuildMyUnicorn.Startup))]

namespace BuildMyUnicorn
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
            //app.MapSignalR();
        }
    }
}
