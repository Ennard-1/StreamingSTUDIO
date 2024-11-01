using StreamingSTUDIO.Pages;
using StreamingSTUDIO.Services;

namespace StreamingSTUDIO.Pages
{
    public partial class LoginRegisterPage : ContentPage
    {
        public LoginRegisterPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage()); // Certifique-se de ter a LoginPage implementada
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage()); // Certifique-se de ter a RegisterPage implementada
        }
    }
}
