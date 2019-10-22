using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using LevelApp.API.Routes;
using LevelApp.API.Tests.IntegrationTests;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.DAL.Models.Core;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace LevelApp.API.Tests.Core
{
    [ExcludeFromCodeCoverage]
    public class UsersControllerIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;
        private const string UsersModule = "users";
        
        // Tests SetUp
        public UsersControllerIntegrationTests()
        {
            var factory = new CustomWebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Can_Get_Users()
        {
            // Arrange
            var endpoint = IntegrationTestsHelper.GenerateEndpoint(new List<string>(){ BaseRoutes.Controller }, UsersModule);
            var httpResponse = await _client.GetAsync(endpoint);
            
            // Must be successful
            httpResponse.EnsureSuccessStatusCode();
            
            // Deserialize and examine results
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<AppUser>>(stringResponse);
            Assert.Equal(10, users.Count);
        }

        [Fact]
        public async Task Can_Register_New_User()
        {
            // Arrange
            var userToRegister = IntegrationTestsHelper.GenerateHttpContent(new UserRegisterDto()
            {
                Email = "test@test.com",
                Password = "testPassword"
            });
            
            var endpoint = IntegrationTestsHelper.GenerateEndpoint(new List<string>()
                {BaseRoutes.Controller, UserRoutes.Register}, UsersModule);

            // Act
            var httpResponse = await _client.PostAsync(endpoint, userToRegister);
            
            // Assert
            httpResponse.EnsureSuccessStatusCode();
            
            // Deserialize and examine results
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var userId = JsonConvert.DeserializeObject<int>(stringResponse);
            Assert.NotEqual(0, userId);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}