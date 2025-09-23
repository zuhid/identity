// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Text;
// using System.Text.Json;
// using Zuhid.BaseApi;
// using Zuhid.Identity;

// namespace Identity.IntegrationTests;

// public class UserTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
// {
//     [Fact]
//     public async Task CrudTest()
//     {
//         // Get
//         var client = factory.CreateClient();
//         var response = await client.GetAsync("/User");
//         // response.EnsureSuccessStatusCode();
//         var responseString = await response.Content.ReadAsStringAsync();
//         // Assert.Equal("[]", responseString);

//         // Get
//         response = await client.GetAsync("/User?Id=00000000-0000-0000-0000-000000000001");
//         // response.EnsureSuccessStatusCode();
//         responseString = await response.Content.ReadAsStringAsync();
//         // Assert.Equal("[]", responseString);

//         // Add
//         response = await client.PostAsync("/User", new StringContent(
//             JsonSerializer.Serialize(new
//             {
//                 Id = new Guid("00000000-0000-0000-0000-000000000001"),
//                 Updated = new DateTime(2021, 1, 1),
//                 updatedBy = "Someone",
//                 isActive = true,
//                 email = "users@email.com",
//                 password = "password",
//                 phone = "333-333-3333",
//             }),
//             Encoding.UTF8,
//             "application/json"));
//         responseString = await response.Content.ReadAsStringAsync();
//         var saveRespose = JsonSerializer.Deserialize<SaveRespose>(responseString);
//         Console.WriteLine(responseString);
//         // response.EnsureSuccessStatusCode();

//         // Update
//         response = await client.PutAsync("/User", new StringContent(
//             JsonSerializer.Serialize(new
//             {
//                 Id = new Guid("00000000-0000-0000-0000-000000000001"),
//                 Updated = saveRespose.Updated,
//                 updatedBy = "Someone",
//                 isActive = true,
//                 email = "users@email.com",
//                 password = "password",
//                 phone = "333-333-3333",
//             }),
//             Encoding.UTF8,
//             "application/json"));
//         // response.EnsureSuccessStatusCode();

//         // Delete
//         response = await client.DeleteAsync("/User/Id/00000000-0000-0000-0000-000000000001");
//         response.EnsureSuccessStatusCode();
//     }
// }
