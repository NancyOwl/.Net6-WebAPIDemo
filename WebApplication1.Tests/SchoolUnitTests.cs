using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

using System.Threading.Tasks;

using System.Collections.Generic;
using System.Net.Http.Json;

using System.Net;

using WebApplication1.DataContext;
using Xunit;


namespace WebApplication1.Tests
{
    public class SchoolUnitTests
    {
        [Fact]
        public async Task GetRootApi()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var expected = "Hello ASP.NET Core WebApplication API~~~";

            //Act
            var actual = await client.GetStringAsync("/");

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    class SchoolApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<MyDb>));

                services.AddDbContext<MyDb>(options => options.UseInMemoryDatabase("TestingDB", root));
            });

            return base.CreateHost(builder);
        }
    }
}