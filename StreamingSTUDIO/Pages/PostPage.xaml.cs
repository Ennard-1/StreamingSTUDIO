using Microsoft.Maui.Storage;
using StreamingStudio.Services;

namespace StreamingStudio.Pages;

public partial class PostPage : ContentPage
{
    private readonly ApiService _apiService;
    private FileResult? _selectedVideo;
    private FileResult? _selectedThumbnail;

    public PostPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnSelectThumbnailClicked(object sender, EventArgs e)
    {
        _selectedThumbnail = await FilePicker.Default.PickAsync();
    }

    private async void OnSelectVideoClicked(object sender, EventArgs e)
    {
        _selectedVideo = await FilePicker.Default.PickAsync();
    }

    private async void OnPostClicked(object sender, EventArgs e)
    {
        await _apiService.UploadContent(
            TituloEntry.Text,
            TipoEntry.Text,
            _selectedVideo?.OpenReadAsync().Result,
            _selectedThumbnail?.OpenReadAsync().Result
        );

        await Navigation.PopAsync();
    }
}
