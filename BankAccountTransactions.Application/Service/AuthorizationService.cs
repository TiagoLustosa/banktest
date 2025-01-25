using BankAccountTransactions.Domain.Service;
using System.Net.Http.Json;

namespace BankAccountTransactions.Application.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;

        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AuthorizeTransactionAsync()
        {
            var response = await _httpClient.GetAsync("https://util.devi.tools/api/v2/authorize");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro de comunicação com o serviço de autorização.");
            }

            var content = await response.Content.ReadFromJsonAsync<AuthorizationResponse>();

            if (content?.Status?.Equals("fail", StringComparison.OrdinalIgnoreCase) == true)
            {
                throw new Exception("Erro de autorização.");
            }

            return content?.Data?.Authorization ?? false;
        }
    }

}

public class AuthorizationResponse
{
    public string Status { get; set; }
    public AuthorizationData Data { get; set; }
}

public class AuthorizationData
{
    public bool Authorization { get; set; }
}