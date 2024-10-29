namespace StreamingSTUDIO{
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Verificar se há token na memória e navegar para a página correta
        MainPage = new NavigationPage(new AuthPage());
    }
}
  }