using System.Net;

namespace WebApiTests;

public class CommuteIntegrationTests(WebApiTestFixture fixture) : IClassFixture<WebApiTestFixture>
{
    [Fact]
    public async Task CommuteListEndpoint_IsRegistered()
    {
        var response = await fixture.HttpClient.GetAsync("/commutes");
        Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CommuteStatisticsEndpoint_IsRegistered()
    {
        var response = await fixture.HttpClient.GetAsync("/commutes/statistics");
        Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
