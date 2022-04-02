using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using FluentAssertions;
using api.Models;
using System.Security.Claims;
using Xunit;


namespace api.Tests.IntegrationTest
{
    public class CourseControllerTests : IClassFixture<AppInstance>
    {

        private readonly AppInstance _instance;

        public CourseControllerTests(AppInstance instance)
        {
            _instance = instance;
        }

        [Fact]
        public async Task Getcourses_WithoutTwoCourses_ReturnsTwoCourses()
        {
            // Arrange
            var _httpClient = _instance
            .AuthenticatedInstance(new Claim(ClaimTypes.Role, "Admin"))
            .CreateClient(new()
            {
                AllowAutoRedirect = false
            });

            // Act
            var response = await _httpClient.GetAsync("/api/Course");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Course>>()).Should().HaveCount(2);
        }
    }
}