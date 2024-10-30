using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    public MainPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
        LoadVideos();
    }

    private async void LoadVideos()
    {
        var response = await _apiService.GetUserContent();
        if (response.IsSuccessStatusCode)
        {
            var videos = await response.Content.ReadFromJsonAsync<List<Video>>();
            VideosCollectionView.ItemsSource = videos;
        }
    }

    private async void OnAddVideoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PostPage(_apiService));
    }

    private async void OnUpdateVideoClicked(object sender, EventArgs e)
    {
        var video = (sender as Button)?.BindingContext as Video;
        if (video != null)
        {
            await Navigation.PushAsync(new PostPage(_apiService, video));
        }
    }

    private async void OnDeleteVideoClicked(object sender, EventArgs e)
    {
        var video = (sender as Button)?.BindingContext as Video;
        if (video != null)
        {
            await _apiService.DeleteContent(video.id);
            LoadVideos();
        }
    }

    private async void OnAccountClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AccountPage(_apiService));
    }
}
