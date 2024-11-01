using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StreamingSTUDIO.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace StreamingSTUDIO.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public string BaseUrl { get; } = "http://localhost:5152";
        private readonly TokenService _tokenService;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _tokenService = new TokenService();
        }

        private void AddAuthHeader()
        {
            var token = TokenService.GetToken();
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<HttpResponseMessage> Register(string nome, string email, string senha)
        {
            var payload = new { nome, email, senha };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("/api/Auth/register", content);
        }

        public async Task<HttpResponseMessage> Login(string email, string senha)
        {
            var payload = new { email, senha };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Aqui você deve pegar o token do corpo da resposta
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                // Supondo que o token esteja em uma propriedade chamada "token"
                if (loginResponse?.token != null)
                {
                    TokenService.SaveToken(loginResponse.token);
                }
            }

            return response;
        }


        public async Task<Usuario?> GetUserInfo()
        {
            AddAuthHeader();
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Auth/info");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
                return usuario;
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetUserContent()
        {
            AddAuthHeader();
            return await _httpClient.GetAsync("/api/Conteudo/usuario");
        }
        public async Task<ImageSource> GetThumbnailImage(string thumbnail)
        {
            var url = $"/api/Conteudo/thumbnails/{thumbnail}"; // URL da API
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return ImageSource.FromStream(() => stream);
            }
            else
            {
                // Retorne uma imagem padrão em caso de erro
                return ImageSource.FromFile("default_thumbnail.png"); // Substitua pelo seu caminho padrão
            }
        }

        public async Task<HttpResponseMessage> UploadContent(string titulo, string tipo, Stream file, Stream thumbnail)
        {
            AddAuthHeader();
            var content = new MultipartFormDataContent
            {
                { new StringContent(titulo), "Titulo" },
                { new StringContent(tipo), "Tipo" },
                { new StreamContent(file), "File", "video.mp4" },
                { new StreamContent(thumbnail), "Thumbnail", "image.jpg" }
            };
            return await _httpClient.PostAsync("/api/Conteudo", content);
        }

        public async Task<HttpResponseMessage> UpdateContent(int id, string titulo, string tipo, Stream? file = null, Stream? thumbnail = null)
        {
            AddAuthHeader();
            var content = new MultipartFormDataContent
            {
                { new StringContent(titulo), "Titulo" },
                { new StringContent(tipo), "Tipo" }
            };

            if (file != null) content.Add(new StreamContent(file), "File", "video.mp4");
            if (thumbnail != null) content.Add(new StreamContent(thumbnail), "Thumbnail", "image.jpg");

            return await _httpClient.PutAsync($"/api/Conteudo/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteContent(int id)
        {
            AddAuthHeader();
            return await _httpClient.DeleteAsync($"/api/Conteudo/{id}");
        }

        public async Task<Stream> GetThumbnail(string thumbnail)
        {
            AddAuthHeader();
            return await _httpClient.GetStreamAsync($"/api/Conteudo/thumbnails/{thumbnail}");
        }

        public async Task<Stream> StreamVideo(string fileName)
        {
            AddAuthHeader();
            return await _httpClient.GetStreamAsync($"/api/Conteudo/stream/{fileName}");
        }
    }
}
