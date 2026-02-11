using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BackEnd.Application.Services
{
    /// <summary>
    /// Default HTTP-based implementation of <see cref="IAuthApiClient"/>.
    /// Uses configuration section AuthApi:BaseUrl and AuthApi:InternalApiKey.
    /// </summary>
    public class AuthApiClient : IAuthApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> DeleteIdentityUserAsync(string authUserId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(authUserId))
            {
                throw new ArgumentException("authUserId must not be null or empty.", nameof(authUserId));
            }

            var baseUrl = _configuration["AuthApi:BaseUrl"];
            var apiKey = _configuration["AuthApi:InternalApiKey"];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException("AuthApi:BaseUrl is not configured.");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("AuthApi:InternalApiKey is not configured.");
            }

            // Ensure base address
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(baseUrl);
            }

            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Auth/users/{authUserId}");
            request.Headers.Add("X-Internal-Api-Key", apiKey);

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}

