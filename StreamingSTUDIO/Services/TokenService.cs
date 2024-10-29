public class TokenService
{
    private const string TokenKey = "auth_token";

    public void SaveToken(string token) =>
        Preferences.Set(TokenKey, token);

    public string GetToken() =>
        Preferences.Get(TokenKey, string.Empty);

    public void ClearToken() =>
        Preferences.Remove(TokenKey);
}
