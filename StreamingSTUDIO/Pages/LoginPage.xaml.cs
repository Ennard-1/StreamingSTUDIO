using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;


    public LoginPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(SenhaEntry.Text))
        {
            await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
            return;
        }

        var response = await _apiService.Login(EmailEntry.Text, SenhaEntry.Text);
        if (response.IsSuccessStatusCode)
        {

            await Navigation.PushAsync(new AccountPage());
        }
        else
        {
            await DisplayAlert("Erro", "Login falhou", "OK");
        }
    }

}
