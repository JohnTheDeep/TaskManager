using System.Text.Json;
using System.Text;
using TaskManager.Core.Interfaces;
using TaskManager.Core.DTOs.Responses.User;
using TaskManager.Core.DTOs.Requests.User;

namespace TaskManager.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<LoginUserResponse> LoginAsync(string login, string password)
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Content = new StringContent(JsonSerializer.Serialize(new { login, password }), Encoding.UTF8, "application/json")
                };

                var _httpClient = _httpClientFactory.CreateClient("Auth");

                using var responseMessage = await _httpClient.PostAsync("Auth/Login", requestMessage.Content);

                var response = await responseMessage.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<LoginUserResponse>(response, _jsonOptions)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed login", ex);
            }
        }

        public async Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request)
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Content = new StringContent(JsonSerializer.Serialize(new
                    {
                        Login = request.Login,
                        Email = request.Email,
                        Password = request.Password,
                        Name = request.Name
                    }), Encoding.UTF8, "application/json")
                };


                var _httpClient = _httpClientFactory.CreateClient("Auth");

                using var responseMessage = await _httpClient.PostAsync("Register/RegisterUser", requestMessage.Content);

                var response = await responseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<RegisterUserResponse>(response, _jsonOptions)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed Register", ex);
            }
        }
    }
}
