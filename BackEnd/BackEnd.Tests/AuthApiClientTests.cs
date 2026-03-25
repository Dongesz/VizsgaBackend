using System.Net;
using System.Text;
using BackEnd.Application.Services;
using BackEnd.Tests.TestHelpers;
using Microsoft.Extensions.Configuration;

namespace BackEnd.Tests;

public class AuthApiClientTests
{
    private static IConfiguration CreateAuthConfiguration()
    {
        const string json = """
        {
          "AuthApi": {
            "BaseUrl": "http://auth.test/",
            "InternalApiKey": "unit-test-internal-key"
          }
        }
        """;
        return new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
            .Build();
    }

    [Fact]
    public async Task DeleteIdentityUserAsync_EmptyAuthUserId_ThrowsArgumentException()
    {
        var handler = new StubHttpMessageHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
        using var httpClient = new HttpClient(handler);
        var sut = new AuthApiClient(httpClient, CreateAuthConfiguration());

        await Assert.ThrowsAsync<ArgumentException>(() =>
            sut.DeleteIdentityUserAsync("   "));
    }

    [Fact]
    public async Task DeleteIdentityUserAsync_Success_ReturnsTrue_AndSendsDeleteWithApiKey()
    {
        HttpRequestMessage? captured = null;
        var handler = new StubHttpMessageHandler((req, _) =>
        {
            captured = req;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
        });
        using var httpClient = new HttpClient(handler);
        var sut = new AuthApiClient(httpClient, CreateAuthConfiguration());

        var ok = await sut.DeleteIdentityUserAsync("user-guid-123");

        Assert.True(ok);
        Assert.NotNull(captured);
        Assert.Equal(HttpMethod.Delete, captured!.Method);
        Assert.Contains("user-guid-123", captured.RequestUri!.ToString(), StringComparison.Ordinal);
        Assert.True(captured.Headers.TryGetValues("X-Internal-Api-Key", out var keys));
        Assert.Contains("unit-test-internal-key", keys);
    }
}
