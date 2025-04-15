using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

namespace MudskipApp
{
    public partial class HighscoreListPage : ContentPage
    {
        private readonly string levelId;

        public HighscoreListPage(string levelId)
        {
            InitializeComponent();
            this.levelId = levelId;
            LoadHighscores();
        }

        private async void LoadHighscores()
        {
            try
            {
                string url = levelId == "my"
                    ? "https://mudskipdb.onrender.com/api/Highscore/my-highscores"
                    : $"https://mudskipdb.onrender.com/api/Highscore/{levelId}";

                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    await DisplayAlert("Hiba", $"A megadott URL nem érvényes:\n{url}", "OK");
                    return;
                }

                var response = await ApiSession.Client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (levelId == "my")
                {
                    List<MyScore> items = null;

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        items = new List<MyScore>();
                    }
                    else if (!response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Hiba", $"Nem sikerült lekérni a pontokat.\n{response.StatusCode} - {json}", "OK");
                        return;
                    }
                    else
                    {
                        items = JsonSerializer.Deserialize<List<MyScore>>(json);
                    }

                    var allLevels = new List<string> { "Tutorial", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" };

                    var formatted = allLevels.Select(levelName =>
                    {
                        var score = items?.FirstOrDefault(x => x.LevelName == levelName);
                        return new DisplayScore
                        {
                            Username = levelName,
                            ScoreText = score != null ? $"{score.Highscore} pont" : "❌ Még nincs pontod"
                        };
                    }).ToList();

                    HighscoreList.ItemsSource = formatted;
                }
                else
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Hiba", $"Nem sikerült lekérni a pontokat.\n{response.StatusCode} - {json}", "OK");
                        return;
                    }

                    var items = JsonSerializer.Deserialize<List<LeaderboardItem>>(json);

                    if (items == null || items.Count == 0)
                    {
                        HighscoreList.ItemsSource = new List<DisplayScore>
                        {
                            new DisplayScore { Rank = "", Username = "Ehhez a pályához nincs pontszám.", ScoreText = "" }
                        };
                        return;
                    }

                    var formatted = items
                        .OrderByDescending(x => x.HighscoreValue)
                        .Select((x, i) => new DisplayScore
                        {
                            Rank = $"{i + 1}.",
                            Username = x.Username,
                            ScoreText = $"{x.HighscoreValue} pont"
                        }).ToList();

                    HighscoreList.ItemsSource = formatted;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hiba", $"Kivétel történt:\n{ex.Message}", "OK");
            }
        }

       

        private class MyScore
        {
            [JsonPropertyName("levelName")]
            public string LevelName { get; set; }

            [JsonPropertyName("highscore")]
            public int Highscore { get; set; }
        }

        private class LeaderboardItem
        {
            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("highscoreValue")]
            public int HighscoreValue { get; set; }
        }

        public class DisplayScore
        {
            public string Rank { get; set; }
            public string Username { get; set; }
            public string ScoreText { get; set; }
        }
    }
}
