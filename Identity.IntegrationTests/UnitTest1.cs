using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Zuhid.Identity;

namespace Identity.IntegrationTests;

public class UnitTest1
{
    private TestServer _server;
    private HttpClient _client;

    [Fact]
    public async Task Test1()
    {
        _client = new WebApplicationFactory<Program>().CreateClient();
        var result = await _client.GetAsync("/");
        var content = await result.Content.ReadAsStringAsync(); //.Should().Be("Hello World!");
        Console.WriteLine(result.StatusCode);
        Console.WriteLine(content);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Contains("View Swagger", content);
    }
}
