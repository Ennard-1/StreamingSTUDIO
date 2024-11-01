using Microsoft.Maui.Storage;

namespace StreamingSTUDIO.Services
{
    public class TokenService
    {
        private const string TokenKey = "jwt_token";

        public static void SaveToken(string token)
        {
            Preferences.Set(TokenKey, token);
        }

        public static string? GetToken()
        {
            return Preferences.Get(TokenKey, null);
        }

        public static void DeleteToken()
        {
            Preferences.Remove(TokenKey);
        }

        public static bool HasToken()
        {
            return !string.IsNullOrEmpty(TokenService.GetToken());
        }
    }
}
