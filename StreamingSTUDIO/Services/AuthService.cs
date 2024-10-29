using StreamingSTUDIO.Models;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "https://sua-api.com/";

    public async Task<string> LoginAsync(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}auth/login",
            new { email, senha });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return result.token;
        }
        throw new Exception("Falha no login.");
    }

    public async Task<Usuario> ObterInfoUsuarioAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return await _httpClient.GetFromJsonAsync<Usuario>($"{BaseUrl}auth/info");
    }

    public async Task RegistrarAsync(string nome, string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}auth/register",
            new { nome, email, senha });

        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao registrar.");
    }
}
