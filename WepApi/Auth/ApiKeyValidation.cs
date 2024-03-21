using System.Security.Cryptography.X509Certificates;

namespace WepApi.Auth
{
    public class ApiKeyValidation : IApiKeyValidation
    {

        private readonly IConfiguration _configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool IsValid(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return false;
            }

            string? serverApiKey = _configuration.GetValue<string>(Constants.apiKeyName);

            if (serverApiKey == null || apiKey != serverApiKey)
            {
                return false;
            }
            return true;

        }
    }
}
