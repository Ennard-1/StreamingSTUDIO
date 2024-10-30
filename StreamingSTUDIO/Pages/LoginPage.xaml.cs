using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;

    public LoginPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var response = await _apiService.Login(EmailEntry.Text, SenhaEntry.Text);
        if (response.IsSuccessStatusCode)
        {
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Login falhou", "OK");
        }
    }
}
