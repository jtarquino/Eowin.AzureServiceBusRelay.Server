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

            _server = AzureServiceBusOwinServer.Create<Startup>(sbConfig);
        }


        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
