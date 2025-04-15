namespace MudskipApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell(); // vagy amit használsz kezdőoldalnak
    }
}
