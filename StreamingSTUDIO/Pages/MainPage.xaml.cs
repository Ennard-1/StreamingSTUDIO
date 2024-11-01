using StreamingSTUDIO.Models;
using StreamingSTUDIO.Services;
using System.Diagnostics;
using System.Net.Http.Json;

namespace StreamingSTUDIO.Pages;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    // Removendo o Video, pois não está sendo utilizado na classe
    // private readonly Video _video;

     public MainPage()
    {
        InitializeComponent();
        // Inicializando o ApiService
        _apiService = new ApiService();
        LoadVideos(); // Chamando LoadVideos após a inicialização do _apiService
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing(); // Chama o método base
        await LoadVideos(); // Recarrega os vídeos ao voltar para a página
    }
    private async Task LoadVideos()
    {
        try
        {
            var response = await _apiService.GetUserContent();
            if (response.IsSuccessStatusCode)
            {
                var videos = await response.Content.ReadFromJsonAsync<List<Video>>();

                // Atualiza a propriedade Thumbnail para a URL completa usando a BaseUrl
                foreach (var video in videos)
                {
                    video.Thumbnail = $"{_apiService.BaseUrl}/api/Conteudo/thumbnails/{video.Thumbnail}"; // Construa a URL completa
                }

                VideosCollectionView.ItemsSource = videos;
                Debug.WriteLine($"Vídeos carregados: {videos.Count}");
            }
            else
            {
                Debug.WriteLine($"Erro ao carregar vídeos: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exceção ao carregar vídeos: {ex.Message}");
        }
    }

    private async void OnAddVideoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PostPage(_apiService, null)); // Passando null se não houver um vídeo
    }

    private async void OnUpdateVideoClicked(object sender, EventArgs e)
    {
        var video = (sender as Button)?.BindingContext as Video;
        if (video != null)
        {
            // Passando o objeto video completo para a UpdatePage
            await Navigation.PushAsync(new UpdatePage(video));
        }
    }


    private async void OnDeleteVideoClicked(object sender, EventArgs e)
    {
        var video = (sender as Button)?.BindingContext as Video;
        if (video != null)
        {
            await _apiService.DeleteContent(video.Id);
            LoadVideos(); // Recarregar vídeos após a exclusão
        }
    }

    private async void OnAccountClicked(object sender, EventArgs e)
    {
        if (TokenService.HasToken())
        {
            await Navigation.PushAsync(new AccountPage());
        }
        else
        {
            await Navigation.PushAsync(new LoginRegisterPage());
        }
    }


}
