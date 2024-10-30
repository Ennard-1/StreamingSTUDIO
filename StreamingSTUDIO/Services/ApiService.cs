using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        public ApiService(TokenService tokenService)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.streamingtv.com") // Ajuste para a URL da sua API
            };
            _tokenService = tokenService;
        }

        private void AddAuthHeader()
        {
            var token = _tokenService.GetToken();
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<HttpResponseMessage> Register(string nome, string email, string senha)
        {
            var payload = new { nome, email, senha };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("/api/Auth/register", content);
        }

        public async Task<HttpResponseMessage> Login(string email, string senha)
        {
            var payload = new { email, senha };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("/api/Auth/login", content);
        }


        public async Task<HttpResponseMessage> GetUserInfo()
        {
            AddAuthHeader();
            return await _httpClient.GetAsync("/api/Auth/info");
        }

        public async Task<HttpResponseMessage> GetUserContent()
        {
            AddAuthHeader();
            return await _httpClient.GetAsync("/api/Conteudo/usuario");
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
