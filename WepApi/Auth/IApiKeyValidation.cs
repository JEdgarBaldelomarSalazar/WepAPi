namespace WepApi.Auth
{
    public interface IApiKeyValidation
    {
        bool IsValid(string apiKey);
    }
}
