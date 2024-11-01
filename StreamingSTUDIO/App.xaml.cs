using StreamingSTUDIO.Pages;
using StreamingSTUDIO.Services;

namespace StreamingSTUDIO
{
    public partial class App : Application

    {
        private readonly ApiService _apiService;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
