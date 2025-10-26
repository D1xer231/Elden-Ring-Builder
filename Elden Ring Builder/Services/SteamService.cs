using dotenv.net; // NuGet: DotEnv.Core
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EldenRingBuilder.Services
{
    public class SteamService
    {
        private readonly string _envPath;
        private readonly Image _steamImg;
        private readonly TextBlock _steamName;
        private readonly TextBlock _steamId;
        private readonly TextBlock _steamGameCount;
        private readonly TextBlock _totalHrsCount;
        private readonly TextBlock _hrsPlayedEldenRing;
        private readonly TextBlock _totalBadgesCount;
        private readonly TextBlock _friendsCount;
        private readonly Grid _statsTable;
        private readonly Image _steamLogo;
        private readonly Image _catImg;
        private readonly StackPanel _nameId;

        public SteamService(
            string envPath,
            Image steamImg,
            TextBlock steamName,
            TextBlock steamId,
            TextBlock steamGameCount,
            TextBlock totalHrsCount,
            TextBlock hrsPlayedEldenRing,
            TextBlock totalBadgesCount,
            TextBlock friendsCount,
            Grid statsTable,
            Image steamLogo,
            Image catImg,
            StackPanel nameId)
        {
            _envPath = envPath;
            _steamImg = steamImg;
            _steamName = steamName;
            _steamId = steamId;
            _steamGameCount = steamGameCount;
            _totalHrsCount = totalHrsCount;
            _hrsPlayedEldenRing = hrsPlayedEldenRing;
            _totalBadgesCount = totalBadgesCount;
            _friendsCount = friendsCount;
            _statsTable = statsTable;
            _steamLogo = steamLogo;
            _catImg = catImg;
            _nameId = nameId;
        }

        public async Task LoadSteamInfoAsync(string steamId)
        {
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { _envPath }));
            string? apiKey = Environment.GetEnvironmentVariable("STEAM_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                Debug.WriteLine("Error: variable STEAM_API_KEY not found. Check .env file.");
                return;
            }

            string url_user = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={apiKey}&steamids={steamId}";
            string url_games = $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={apiKey}&steamid={steamId}&include_appinfo=true";
            string url_friends = $"https://api.steampowered.com/ISteamUser/GetFriendList/v1/?key={apiKey}&steamid={steamId}&relationship=friend";
            string url_badges = $"https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v1/?key={apiKey}&steamid={steamId}&appid=1245620";

            using HttpClient client = new HttpClient();

            try
            {
                //------------------User Info----------------------------------------//
                string response = await client.GetStringAsync(url_user);
                var jsonDoc = JsonDocument.Parse(response);
                var player = jsonDoc.RootElement.GetProperty("response").GetProperty("players")[0];

                string? personaName = player.GetProperty("personaname").GetString();
                string? profileid = player.GetProperty("steamid").GetString();
                string? user_img = player.GetProperty("avatarfull").GetString();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(user_img, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                _steamImg.Source = bitmap;
                _steamName.Text = personaName;
                _steamId.Text = profileid;

                //------------------User Games---------------------------------------//
                string response_games = await client.GetStringAsync(url_games);
                var jsonDoc_games = JsonDocument.Parse(response_games);

                if (jsonDoc_games.RootElement.TryGetProperty("response", out JsonElement gamesRoot))
                {
                    int game_count = gamesRoot.GetProperty("game_count").GetInt32();
                    _steamGameCount.Text = game_count.ToString();

                    int totalMinutes = 0;
                    bool eldenRingFound = false;

                    if (gamesRoot.TryGetProperty("games", out JsonElement gamesArray))
                    {
                        foreach (var game in gamesArray.EnumerateArray())
                        {
                            if (game.TryGetProperty("playtime_forever", out JsonElement playtime))
                                totalMinutes += playtime.GetInt32();

                            if (!eldenRingFound &&
                                game.TryGetProperty("name", out JsonElement nameElement) &&
                                nameElement.GetString() == "ELDEN RING" &&
                                game.TryGetProperty("playtime_forever", out JsonElement playtimeER))
                            {
                                _hrsPlayedEldenRing.Text = $"{playtimeER.GetInt32() / 60} hrs";
                                eldenRingFound = true;
                            }
                        }
                    }

                    _totalHrsCount.Text = $"{totalMinutes / 60} hrs";

                    if (!eldenRingFound)
                        _hrsPlayedEldenRing.Text = "0 hrs";
                }

                //------------------Achievements-----------------------------------//
                try
                {
                    string response_badge = await client.GetStringAsync(url_badges);
                    using JsonDocument json = JsonDocument.Parse(response_badge);

                    if (json.RootElement.TryGetProperty("playerstats", out JsonElement playerStats) &&
                        playerStats.TryGetProperty("achievements", out JsonElement achievements))
                    {
                        int total = achievements.GetArrayLength();
                        int unlocked = achievements.EnumerateArray().Count(a =>
                            a.TryGetProperty("achieved", out var ach) && ach.GetInt32() == 1);

                        _totalBadgesCount.Text = $"{unlocked}/{total}";
                    }
                    else
                    {
                        _totalBadgesCount.Text = "0/0";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while getting achievements: {ex.Message}");
                }

                //------------------Friends----------------------------------------//
                string response_friends = await client.GetStringAsync(url_friends);
                var jsonDoc_friends = JsonDocument.Parse(response_friends);
                var friendsRoot = jsonDoc_friends.RootElement.GetProperty("friendslist");

                if (friendsRoot.TryGetProperty("friends", out JsonElement friendsArray))
                    _friendsCount.Text = friendsArray.GetArrayLength().ToString();
                else
                    _friendsCount.Text = "0";

                //------------------UI Show----------------------------------------//
                _statsTable.Visibility = Visibility.Visible;
                _steamLogo.Visibility = Visibility.Visible;
                _catImg.Visibility = Visibility.Visible;
                _nameId.Visibility = Visibility.Visible;
                _steamImg.Visibility = Visibility.Visible;

                Debug.WriteLine($"✅ Player: {personaName} ({profileid})");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while request: {ex.Message}");
            }
        }
    }
}