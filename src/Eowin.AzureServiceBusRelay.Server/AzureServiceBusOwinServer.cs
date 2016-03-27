﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Engine;
using Microsoft.Owin.Hosting.ServerFactory;
using Microsoft.Owin.Hosting.Services;
using Owin;

namespace Eowin.AzureServiceBusRelay.Server
{
    // Based on Microsoft.Owin.Testing.TestServer
    public class AzureServiceBusOwinServer : IDisposable
    {
        public static AzureServiceBusOwinServer Create(AzureServiceBusOwinServiceConfiguration config, Action<IAppBuilder> startup)
        {
            var server = new AzureServiceBusOwinServer();
            server.Configure(config, startup);
            return server;
        }

        public static AzureServiceBusOwinServer Create<TStartup>(AzureServiceBusOwinServiceConfiguration config)
        {
            var server = new AzureServiceBusOwinServer();
            server.Configure<TStartup>(config);
            return server;
        }

        protected void Configure<TStartup>(AzureServiceBusOwinServiceConfiguration config)
        {
            Configure<TStartup>(config, null);
        }

        protected void Configure<TStartup>(AzureServiceBusOwinServiceConfiguration config, StartOptions options)
        {
            // Compare with WebApp.StartImplementation
            options = options ?? new StartOptions();
            options.AppStartup = typeof(TStartup).AssemblyQualifiedName;

            var serverFactory = new AzureServiceBusOwinServerFactory(config);
            IServiceProvider services = ServicesFactory.Create();
            var engine = services.GetService<IHostingEngine>();
            var context = new StartContext(options);
            context.ServerFactory = new ServerFactoryAdapter(serverFactory);
            _started = engine.Start(context);
            //_next = serverFactory.Invoke;
        }

        private void Configure(AzureServiceBusOwinServiceConfiguration config, Action<IAppBuilder> startup)
        {
            if (startup == null)
            {
                throw new ArgumentNullException("startup");
            }

            var options = new StartOptions();
            if (string.IsNullOrWhiteSpace(options.AppStartup))
            {
                // Populate AppStartup for use in host.AppName
                options.AppStartup = startup.Method.ReflectedType.FullName;
            }

            var serverFactory = new AzureServiceBusOwinServerFactory(config);
            var services = ServicesFactory.Create();
            var engine = services.GetService<IHostingEngine>();
            var context = new StartContext(options)
            {
                ServerFactory = new ServerFactoryAdapter(serverFactory),
                Startup = startup
            };
            _started = engine.Start(context);
        }

        public void Dispose()
        {
            _started.Dispose();
        }

        private IDisposable _started;
    }
}