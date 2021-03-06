﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Xunit;
using Owin;

namespace Eowin.AzureServiceBusRelay.Server.Tests
{
    public class IntegrationTests : IntegrationTestsBase, IDisposable
    {


        private readonly AzureServiceBusOwinServer _server;

        public IntegrationTests()
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