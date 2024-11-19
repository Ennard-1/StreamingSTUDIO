using StreamingSTUDIO.Models;
using StreamingSTUDIO.Services;
using System.Diagnostics;
using System.Net.Http.Json;

namespace StreamingSTUDIO.Pages;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

   

     public MainPage()
    {
        InitializeComponent();
     
        _apiService = new ApiService();
        LoadVideos(); 
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing(); 
        await LoadVideos();
    }
    private async Task LoadVideos()
    {
        try
        {
            var response = await _apiService.GetUserContent();
            if (response.IsSuccessStatusCode)
            {
                var videos = await response.Content.ReadFromJsonAsync<List<Video>>();

               
                foreach (var video in videos)
                {
                    video.Thumbnail = $"{_apiService.BaseUrl}/api/Conteudo/thumbnails/{video.Thumbnail}"; 
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
        await Navigation.PushAsync(new PostPage(_apiService, null)); 
    }

    private async void OnUpdateVideoClicked(object sender, EventArgs e)
    {
        var video = (sender as Button)?.BindingContext as Video;
        if (video != null)
        {
            
            await Navigation.PushAsync(new UpdatePage(video));
        }
    }


    private async void OnDeleteVideoClicked(object sender, EventArgs e)
    {
        var video = (sender as Button)?.BindingContext as Video;
        if (video != null)
        {
            await _apiService.DeleteContent(video.Id);
            LoadVideos();
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
