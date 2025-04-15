using System.Text;
using System.Text.Json;

namespace MudskipApp;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Hiba", "Töltsd ki az összes mezőt!", "OK");
            return;
        }

        var data = new
        {
            Username = username, // 🔄 FONTOS: nagybetűs kulcsok!
            Password = password
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await ApiSession.Client.PostAsync(
                "https://mudskipdb.onrender.com/api/User/login", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Siker", "Sikeres bejelentkezés!", "OK");

                // Most már van session cookie! 🎉
                await Shell.Current.GoToAsync("//HighscorePage");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Hiba", $"Sikertelen bejelentkezés!\n{error}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hiba", $"Hálózati hiba: {ex.Message}", "OK");
        }
    }

    private async void OnGoToRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }
}
