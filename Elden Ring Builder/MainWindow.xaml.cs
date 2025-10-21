using Elden_Ring_Builder.models;
using Elden_Ring_Builder.ViewModels;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Printing;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using dotenv.net;
using static Elden_Ring_Builder.ViewModels.MainViewModel;

namespace Elden_Ring_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext? db;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            AppDbContext db = new AppDbContext();

            List<builds> builds = db.Builds.ToList();
            BuildsList.ItemsSource = builds;
            List<gallery> gallery = db.Gallery.ToList();
            GallerList.ItemsSource = gallery;
            List<weapons> weapons = db.Weapons.ToList();
            WeaponsList.ItemsSource = weapons;
            List<runes> runes = db.Runes.ToList();
            RunesList.ItemsSource = runes;
        }
        private void hide_app_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void open_steam_app_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "steam://open/main",
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Error, while steam opening");
                var url = "https://store.steampowered.com/login/";
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Error, while steam opening");
                }
            }
        }

        private async void users_steam_btn_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Steam);
            //steam_Api.SteamInfo();
            string id = insert_steam_id.Text;
            await SteamInfo(id);

        }
        private async void get_steam_info(object sender, RoutedEventArgs e) 
        { 
            string id = insert_steam_id.Text;
            await SteamInfo(id);
        }

        private void redeemcode_btn_Click(object sender, RoutedEventArgs e)
        {
            RedeemCode redeemCode = new RedeemCode();
            redeemCode.Show();
        }

        private void send_email_btn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "mailto:aalexandr397@gmail.com?subject=APP_PROBLEM_OR_ETC&body= Describe the problem",
                UseShellExecute = true
            });
        }

        private void weapons_screen_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Weapons);
        }

        private void change_to_mainscreen_click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Main);
        }

        private void runeslist_btn_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Runes);
            AppDbContext db = new AppDbContext();
        }

        private void web_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.WebView);
            web_open("https://en.bandainamcoent.eu/elden-ring/elden-ring");
            adress_input.Text = "";
            webView2.Visibility = Visibility.Visible;
        }

        private void settings_btn_Click(object sender, RoutedEventArgs e)
        {
            QrImage.Source = GenerateQr("https://i.pinimg.com/736x/2a/f1/cd/2af1cde161d06fe92a1239f70c01154a.jpg");

            ShowScreen(ScreenType.Settings);
        }

        private static BitmapImage GenerateQr(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using Bitmap qrBitmap = qrCode.GetGraphic(20);

            return BitmapToImageSource(qrBitmap);
        }

        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;

            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            return img;
        }

        private void refresh_data_btn_Click(object sender, RoutedEventArgs e)
        {
            refreshData();
        }

        private void OpenUrlFromInput()
        {
            string url = adress_input.Text.Trim();
            if (string.IsNullOrEmpty(url)) return;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;

            web_open(url);
            webView2.Visibility = Visibility.Visible;
        }
        private void adress_input_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenUrlFromInput();
        }
        private void adress_input_btn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OpenUrlFromInput();
        }

        private void gallery_grid_btn_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Gallery);
        }

        private void ShowScreen(ScreenType type)
        {
            main_screen_grid.Visibility = Visibility.Hidden;
            runes_list_grid.Visibility = Visibility.Hidden;
            settings_screen_grid.Visibility = Visibility.Hidden;
            web_view_screen_grid.Visibility = Visibility.Hidden;
            gallery_screen_grid.Visibility = Visibility.Hidden;
            weapons_grid.Visibility = Visibility.Hidden;
            users_steam_grid.Visibility = Visibility.Hidden;

            switch (type)
            {
                case ScreenType.Main: main_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Runes: runes_list_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Settings: settings_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.WebView: web_view_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Gallery: gallery_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Weapons: weapons_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Steam: users_steam_grid.Visibility = Visibility.Visible; break;
            }
        }
        public enum ScreenType
        {
            Main,
            Runes,
            Settings,
            WebView,
            Gallery,
            Weapons,
            Steam
        }

        private void web_open(string url)
        {
            webView2.ZoomFactor = 0.8;
            try 
            {
                webView2.Source = new System.Uri(url);
            }
            catch (System.UriFormatException)
            {
                adress_input.Text = "";
                MessageBox.Show("Invalid URL format. Please check the address and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void refreshData()
        {
            using (var db = new AppDbContext())
            {
                BuildsList.ItemsSource = db.Builds.ToList();
                WeaponsList.ItemsSource = db.Weapons.ToList();
                RunesList.ItemsSource = db.Runes.ToList();
                GallerList.ItemsSource = db.Gallery.ToList();

                web_open("https://en.bandainamcoent.eu/elden-ring/elden-ring");
                adress_input.Text = "";

                MessageBox.Show("Updated!\n\nUse support button\nif any problems", "Elden Ring Builder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void webview_nav_Click(object sender, RoutedEventArgs e)
        {
            //var tag = (sender as Button)?.Tag?.ToString();
            //if (tag == "back" && webView2.CanGoBack) webView2.GoBack();
            //else if (tag == "forward" && webView2.CanGoForward) webView2.GoForward(); by tags Tag="back" or Tag="forward"

            if (sender == webview_back && webView2.CanGoBack) webView2.GoBack();
            else if (sender == webview_forward && webView2.CanGoForward) webView2.GoForward();
        }

        private async Task SteamInfo(string steamId)
        {
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { @"D:\c# projects\MyProjectsC#\Elden Ring  Builder\Elden Ring Builder\Elden Ring Builder\.env" }));
            string? apiKey = Environment.GetEnvironmentVariable("STEAM_API_KEY");

            //string steamId = "76561199220453620";

            if (string.IsNullOrEmpty(apiKey))
            {
                Debug.WriteLine("Error: variable STEAM_API_KEY not found. Check .env file.");
                return;
            }


            string url = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={apiKey}&steamids={steamId}";

            using HttpClient client = new HttpClient();
            try
            {
                string response = await client.GetStringAsync(url);
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

                steam_img.Source = bitmap;
                steam_name.Text = personaName;
                steam_id.Text = profileid;

                Debug.WriteLine($"player: {personaName}");
                Debug.WriteLine($"profile: {profileid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while request: " + ex.Message);
            }
        }
    }
}