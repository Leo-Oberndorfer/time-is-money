using System.Net;

namespace WebApiTests;

public class TravelIntegrationTests(WebApiTestFixture fixture) : IClassFixture<WebApiTestFixture>
{
    [Theory]
    [InlineData("/commutes")]
    [InlineData("/commutes/1")]
    [InlineData("/commutes/statistics")]
    public async Task DeclaredEndpoints_AreReachable_ButNotImplemented(string route)
    {
        var response = await fixture.HttpClient.GetAsync(route);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
