using Microsoft.Maui.Controls;
using StreamingSTUDIO.Services;
using System;

namespace StreamingSTUDIO.Pages
{
    public partial class AccountPage : ContentPage
    {
        private readonly TokenService _tokenService;
        private readonly ApiService _apiService;

        public AccountPage()
        {
            InitializeComponent();
            _apiService = new ApiService();



            LoadUserData();
        }

        private async void LoadUserData()
        {
            try
            {
                var token = TokenService.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    await DisplayAlert("Aviso", "Usuário não está logado", "OK");
                    return;
                }

                var userInfo = await _apiService.GetUserInfo();
                if (userInfo != null)
                {
                    NomeUsuarioLabel.Text = userInfo.Nome;
                    EmailUsuarioLabel.Text = userInfo.Email;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha ao carregar informações do usuário: {ex.Message}", "OK");
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            TokenService.DeleteToken();
            await DisplayAlert("Logout", "Você foi deslogado.", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}
