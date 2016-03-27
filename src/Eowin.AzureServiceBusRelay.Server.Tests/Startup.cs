using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eowin.AzureServiceBusRelay.Server.Tests
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("ApiDefault", "webapi/{controller}/{id}", new { id = RouteParameter.Optional });

            app.Use((ctx, next) =>
            {
                Trace.TraceInformation(ctx.Request.Uri.ToString());
                return next();
            });
            app.UseWebApi(config);
        }
    }
}
