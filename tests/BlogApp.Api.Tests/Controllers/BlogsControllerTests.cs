using BlogApp.Api.Controllers.Models;
using BlogApp.Data.Entities;
using FluentAssertions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Api.Tests.Controllers
{
    [Collection("Database")]
    public sealed class BlogsControllerTests : IClassFixture<BlogWebApplicationFactory>
    {
        private readonly BlogWebApplicationFactory _factory;

        public BlogsControllerTests(BlogWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_ShouldCreateBlog()
        {
            // Arrange
            var createRequest = new CreateBlogRequest
            {
                Url = "https://aspnet-core-is-cool.net"
            };

            var client = _factory.CreateClient();

            // Act
            var postResponse = await client.PostAsync("/v1/blogs", new JsonContent<CreateBlogRequest>(createRequest));
            postResponse.EnsureSuccessStatusCode();
            var blogCreateResponse = await postResponse.Content.ReadAsJsonAsync<Blog>();

            // Assert - by calling the Get/id and comparing the results
            var getResponse = await client.GetAsync($"/v1/blogs/{blogCreateResponse.Id}");
            var blogGetResponse =  await getResponse.Content.ReadAsJsonAsync<Blog>();

            blogGetResponse.Should().BeEquivalentTo(blogCreateResponse);
        }

        [Fact]
        public async Task Update_ShouldUpdateBlogUrl()
        {
            // Arrange
            var client = _factory.CreateClient();

            var initialBlog = await CreateRandomBlog(client);

            var updateRequest = new UpdateBlogRequest
            {
                // not a real url but just something random.. we don't have any validation for now
                Url = Guid.NewGuid().ToString()
            };

            // Act
            var response = await client.PutAsync($"/v1/blogs/{initialBlog.Id}", new JsonContent<UpdateBlogRequest>(updateRequest));
            response.EnsureSuccessStatusCode();

            // Assert
            var getResponse = await client.GetAsync($"/v1/blogs/{initialBlog.Id}");
            var actual = await getResponse.Content.ReadAsJsonAsync<Blog>();

            Assert.Equal(updateRequest.Url, actual.Url);
        }

        private async ValueTask<Blog> CreateRandomBlog(HttpClient client)
        {
            // Arrange
            var createRequest = new CreateBlogRequest
            {
                Url = "https://aspnet-core-is-cool.net"
            };

            // Act
            var postResponse = await client.PostAsync("/v1/blogs", new JsonContent<CreateBlogRequest>(createRequest));
            postResponse.EnsureSuccessStatusCode();
            return await postResponse.Content.ReadAsJsonAsync<Blog>();
        }
    }
}
