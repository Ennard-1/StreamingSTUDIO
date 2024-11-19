using Microsoft.Maui.Storage;
using StreamingSTUDIO.Models;
using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages;

public partial class UpdatePage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly Video _video;
    private FileResult? _selectedVideo;
    private FileResult? _selectedThumbnail;

    public UpdatePage(Video video)
    {
        InitializeComponent();
        _apiService = new ApiService();
        _video = video;

        
        TituloEntry.Text = video.Titulo;
        TipoEntry.Text = video.Tipo;
    }

    private async void OnSelectThumbnailClicked(object sender, EventArgs e)
    {
        _selectedThumbnail = await FilePicker.Default.PickAsync();
    }

    private async void OnSelectVideoClicked(object sender, EventArgs e)
    {
        _selectedVideo = await FilePicker.Default.PickAsync();
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        await _apiService.UpdateContent(
            _video.Id,
            TituloEntry.Text,
            TipoEntry.Text,
            _selectedVideo?.OpenReadAsync().Result,
            _selectedThumbnail?.OpenReadAsync().Result
        );

        await Navigation.PopAsync();
    }
}
