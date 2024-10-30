using Microsoft.Maui.Storage;

namespace StreamingSTUDIO.Services
{
    public class TokenService
    {
        private const string TokenKey = "jwt_token";

        public void SaveToken(string token)
        {
            Preferences.Set(TokenKey, token);
        }

        public string? GetToken()
        {
            return Preferences.Get(TokenKey, null);
        }

        public void DeleteToken()
        {
            Preferences.Remove(TokenKey);
        }

        public bool HasToken()
        {
            return !string.IsNullOrEmpty(GetToken());
        }
    }
}
