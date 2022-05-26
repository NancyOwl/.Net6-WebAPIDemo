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
    public class SchoolTests
    {
        [Fact(DisplayName = "透過 Get 操作測試 root API，預期回應 \"Hello ASP.NET Core WebApplication API~~~\" 。")]
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

        [Fact(DisplayName = "透過 Get 操作測試 schools API，預期回應無任何學校資料。")]
        public async Task GetSchoolsApi()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var expected = new List<Models.School>();

            //Act
            var actual = await client.GetFromJsonAsync<List<Models.School>>("/schools");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "透過 Post 操作測試 addschool API，帶入 School 的 Json 資料，預期回應 Created。")]
        public async Task PostSchoolApi()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var expected = HttpStatusCode.Created;


            //Act
            var result = await client.PostAsJsonAsync("/addschool", new Models.School
                                                                        {
                                                                             Name = "國立中興大學",
                                                                             Logo = "nchu",
                                                                             Address = "402台中市南區興大路145號",
                                                                             Tel = "04-22873181",
                                                                             Email = "dowdot@nchu.edu.tw"
                                                                        });
            var actual = result.StatusCode;

            //Assert        
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "透過 Get 操作測試 findschool API，帶入 Id 是 1 預期回應 OK。")]
        public async Task GetFindSchoolApi_Id_1_is_OK()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "國立中興大學",
                Logo = "nchu",
                Address = "402台中市南區興大路145號",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 1;
            var expected = HttpStatusCode.OK;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.GetAsync($"/findschool/{Id}");

            //Assert
            Assert.Equal(expected , response.StatusCode);
        }

        [Fact(DisplayName = "透過 Get 操作測試 findschool API，帶入 Id 是 3 預期回應 NotFound。")]
        public async Task GetFindSchoolApi_Id_3_is_NotFound()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "國立中興大學",
                Logo = "nchu",
                Address = "402台中市南區興大路145號",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 3;
            var expected = HttpStatusCode.NotFound;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.GetAsync($"/findschool/{Id}");

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }


        [Fact(DisplayName = "透過 Put 操作測試 editschool API，帶入 Id 是 1 預期回應 NoContent。")]
        public async Task PutEditSchoolApi_Id_1_is_NoContent()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "國立中興大學",
                Logo = "nchu",
                Address = "402台中市南區興大路145號",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 1;
            var expected = HttpStatusCode.NoContent;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.PutAsJsonAsync<Models.School>($"/editschool/{Id}",school);

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }


        [Fact(DisplayName = "透過 Put 操作測試 editschool API，帶入 Id 是 3 預期回應 NotFound。")]
        public async Task PutEditSchoolApi_Id_3_is_NotFound()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "國立中興大學",
                Logo = "nchu",
                Address = "402台中市南區興大路145號",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 3;
            var expected = HttpStatusCode.NotFound;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.PutAsJsonAsync<Models.School>($"/editschool/{Id}", school);

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact(DisplayName = "透過 Delete 操作測試 removeschool API，帶入 Id 是 1 預期回應 NoContent。")]
        public async Task DeleteRemoveSchoolApi_Id_1_is_NoContent()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "國立中興大學",
                Logo = "nchu",
                Address = "402台中市南區興大路145號",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 1;
            var expected = HttpStatusCode.NoContent;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.DeleteAsync($"/removeschool/{Id}");

            //Assert
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact(DisplayName = "透過 Delete 操作測試 removeschool API，帶入 Id 是 3 預期回應 NotFound。")]
        public async Task DeleteRemoveSchoolApi_Id_3_is_NotFound()
        {
            //Arrange
            await using var application = new SchoolApplication();
            var client = application.CreateClient();
            var school = new Models.School()
            {
                Name = "國立中興大學",
                Logo = "nchu",
                Address = "402台中市南區興大路145號",
                Tel = "04-22873181",
                Email = "dowdot@nchu.edu.tw"
            };
            var Id = 3;
            var expected = HttpStatusCode.NotFound;

            await client.PostAsJsonAsync("/addschool", school);

            //Act
            var response = await client.DeleteAsync($"/removeschool/{Id}");

            //Assert
            Assert.Equal(expected, response.StatusCode);
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