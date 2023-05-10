using eshopBackend.API;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class TestWebApplicationFactory: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.AddTransient<CartRepository>();
            services.AddTransient<CategoryRepository>();
            services.AddTransient<ManufacturerRepository>();
            services.AddTransient<ProductRepository>();
        });
    }
}
