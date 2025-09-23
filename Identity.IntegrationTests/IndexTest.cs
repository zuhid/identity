// using Microsoft.AspNetCore.Mvc.Testing;
// using Zuhid.Identity;

// namespace Identity.IntegrationTests;

// public class IndexTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
// {
//     [Fact]
//     public async Task GetIndex()
//     {
//         var client = factory.CreateClient();
//         var response = await client.GetAsync("/");
//         response.EnsureSuccessStatusCode();
//         var responseString = await response.Content.ReadAsStringAsync();
//         Assert.Contains("View Swagger", responseString);
//     }
// }
