using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;

    // Modifique o construtor para aceitar um ApiService
    public LoginPage()
    {
        InitializeComponent();
        _apiService = new ApiService(); // Inicialize a variável
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Verificando se os campos de entrada não estão vazios
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(SenhaEntry.Text))
        {
            await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
            return;
        }

        var response = await _apiService.Login(EmailEntry.Text, SenhaEntry.Text);
        if (response.IsSuccessStatusCode)
        {
            // Navegar diretamente para a AccountPage
            await Navigation.PushAsync(new AccountPage());
        }
        else
        {
            await DisplayAlert("Erro", "Login falhou", "OK");
        }
    }

}
