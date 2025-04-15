using MudskipApp; // 👈 Ez kellett, hogy lássa a HighscoreListPage osztályt

namespace MudskipApp;

public partial class HighscorePage : ContentPage
{
    public HighscorePage()
    {
        InitializeComponent();
    }

    private async void OnMyHighscoreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("my")); // ✅ HELYES: csak "my"
    }

    private async void OnTutorialClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("6")); // tutorial szint ID: 6
    }

    private async void OnLevel1Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("1"));
    }

    private async void OnLevel2Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("2"));
    }

    private async void OnLevel3Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("3"));
    }

    private async void OnLevel4Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("4"));
    }

    private async void OnLevel5Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HighscoreListPage("5"));
    }
}
