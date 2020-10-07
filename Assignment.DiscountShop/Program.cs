using System;
using Assignment.DiscountShop.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.DiscountShop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var services = new ServiceCollection();

            StartupServices.Configure(services);
            // ...

            // Add other services like logging etc

            // ...

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<ICustomerService>();

        }
    }
}