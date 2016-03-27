using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using System.Diagnostics;

namespace Eowin.AzureServiceBusRelay.Server.Tests
{
    public class IntegrationTestsGenerics : IntegrationTestsBase, IDisposable
    {
        private readonly AzureServiceBusOwinServer _server;
        public IntegrationTestsGenerics()
        {
            var sbConfig = new AzureServiceBusOwinServiceConfiguration(
                issuerName: "RootManageSharedAccessKey",
                issuerSecret: SecretCredentials.Secret,
                address: SecretCredentials.ServiceBusAddress);
            _server = AzureServiceBusOwinServer.Create(sbConfig, app =>
            {
                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute("ApiDefault", "webapi/{controller}/{id}", new { id = RouteParameter.Optional });

                app.Use((ctx, next) =>
                {
                    Trace.TraceInformation(ctx.Request.Uri.ToString());
                    return next();
                });
                app.UseWebApi(config);
            });
        }


        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
