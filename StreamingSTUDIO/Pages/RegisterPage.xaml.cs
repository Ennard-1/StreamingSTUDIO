using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService;

    public RegisterPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var response = await _apiService.Register(NomeEntry.Text, EmailEntry.Text, SenhaEntry.Text);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Sucesso", "Registrado com sucesso", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Registro falhou", "OK");
        }
    }
}
