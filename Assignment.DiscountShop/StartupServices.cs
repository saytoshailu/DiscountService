using Assignment.DiscountShop.Contracts;
using Assignment.DiscountShop.DiscountShopService;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.DiscountShop
{
    internal static class StartupServices
    {
        internal static void Configure(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}