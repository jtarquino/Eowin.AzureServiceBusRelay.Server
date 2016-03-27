using System;

namespace Eowin.AzureServiceBusRelay.Server.Tests
{
    public class SecretCredentials
    {
        public static string ServiceBusAddress
        {
            get
            {
                var addr = Environment.GetEnvironmentVariable("ServiceBusAddress");
                addr = "https://jtarquino.servicebus.windows.net/webapi/";
                if (string.IsNullOrWhiteSpace(addr)) throw new InvalidOperationException("ServiceBusAddress is not defined");
                return addr;
            }
        }

        public static string Secret
        {
            get
            {
                var secret = Environment.GetEnvironmentVariable("ServiceBusSecret");
                secret = "HyH72IBy4igfFaYb4cVQVPlV14rPWcF2jfWncsZKS64=";
                if (string.IsNullOrWhiteSpace(secret)) throw new InvalidOperationException("ServiceBusSecret is not defined");
                return secret;
            }
        }
    }
}