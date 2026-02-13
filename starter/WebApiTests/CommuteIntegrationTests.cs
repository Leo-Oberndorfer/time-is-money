using System.Net;

namespace WebApiTests;

public class CommuteIntegrationTests(WebApiTestFixture fixture) : IClassFixture<WebApiTestFixture>
{
    [Fact]
    public async Task GetCommutes_EndpointIsRegistered()
    {
        var response = await fixture.HttpClient.GetAsync("/commutes");

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task ImportCommute_EndpointIsRegistered()
    {
        using var content = new MultipartFormDataContent();
        var response = await fixture.HttpClient.PostAsync("/commutes/import", content);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
