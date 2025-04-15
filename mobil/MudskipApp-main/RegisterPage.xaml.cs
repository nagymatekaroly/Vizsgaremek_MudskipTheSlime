using System.Text;
using System.Text.Json;

namespace MudskipApp;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var fullName = FullNameEntry.Text;
        var username = UsernameEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        var confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(fullName) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Hiba", "Minden mezõt ki kell tölteni!", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Hiba", "A két jelszó nem egyezik!", "OK");
            return;
        }

        var client = new HttpClient();
        var data = new
        {
            username = username,
            fullname = fullName,
            emailAddress = email,
            passwordHash = password
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("https://mudskipdb.onrender.com/api/User/register", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Siker", "Sikeres regisztráció! Most már bejelentkezhetsz.", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Hiba", $"Regisztráció sikertelen: {errorMsg}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hálózati hiba", ex.Message, "OK");
        }
    }

    private async void OnGoToLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
