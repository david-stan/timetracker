﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TimeTrackerApp.Tests.IntegrationTests
{
    public class UsersApiTests
    {
        private readonly HttpClient _client;
        private readonly string _nonAdminToken;
        private readonly string _adminToken;

        public UsersApiTests()
        {
            const string issuer = "https://localhost:44349";
            const string key = "nemosodleecheva_faoeifhaohhhoiehfoaiehfoiefasf";

            var server = new TestServer(new WebHostBuilder()
                .UseSetting("Tokens:Issuer", issuer)
                .UseSetting("Tokens:Key", key)
                .UseSetting("ConnectionStrings:DefaultConnection", "DataSource=:memory:")
                .UseStartup<Startup>()
                .UseUrls("https://localhost:44349"))
            {
                BaseAddress = new Uri("https://localhost:44349")
            };

            _client = server.CreateClient();

            _nonAdminToken = JwtTokenGenerator.Generate(
                "etf-workshop", false, issuer, key);
            _adminToken = JwtTokenGenerator.Generate(
                "etf-workshop", true, issuer, key);
        }

        [Fact]
        public async Task Delete_NoAuthorizationHeader_ReturnsUnauthorized()
        {
            _client.DefaultRequestHeaders.Clear();

            var result = await _client.DeleteAsync("/api/users/1");

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task Delete_NotAdmin_ReturnsForbidden()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_nonAdminToken}" });

            var result = await _client.DeleteAsync("/api/users/1");

            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Fact]
        public async Task Delete_NoId_ReturnsMethodNotAllowed()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_adminToken}" });

            var result = await _client.DeleteAsync("/api/users/ ");

            Assert.Equal(HttpStatusCode.MethodNotAllowed, result.StatusCode);
        }

        [Fact]
        public async Task Delete_NonExistingId_ReturnsNotFound()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_adminToken}" });

            var result = await _client.DeleteAsync("/api/users/0");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsOk()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_adminToken}" });

            var result = await _client.DeleteAsync("/api/users/1");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
