using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using FluentAssertions;
using api.Models;
using Xunit;


namespace api.Tests
{
    public class CourseControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Getcourses_WithoutAnyCourses_ReturnsEmptyResponse()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/api/Course");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Course>>()).Should().BeEmpty();
        }
    }
}