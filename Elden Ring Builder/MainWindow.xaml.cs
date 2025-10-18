using Elden_Ring_Builder.models;
using Elden_Ring_Builder.ViewModels;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Elden_Ring_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext db;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            AppDbContext db = new AppDbContext();

            List<builds> builds = db.Builds.ToList();
            List<weapons> weapons = db.Weapons.ToList();
            List<runes> runes = db.Runes.ToList();
            List<gallery> gallery = db.Gallery.ToList();

            BuildsList.ItemsSource = builds;
            WeaponsList.ItemsSource = weapons;
            RunesList.ItemsSource = runes;
            GallerList.ItemsSource = gallery;
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

        private void steam_btn_Click(object sender, RoutedEventArgs e)
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
        }

        private void web_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.WebView);
            web_open("https://en.bandainamcoent.eu/elden-ring/elden-ring");
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

        private void adress_input_btn_Click(object sender, RoutedEventArgs e)
        {
            string url = adress_input.Text.Trim();
            if (!string.IsNullOrEmpty(url))
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "http://" + url;
                }
                web_open(url);
                webView2.Visibility = Visibility.Visible;
            }
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

            switch (type)
            {
                case ScreenType.Main: main_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Runes: runes_list_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Settings: settings_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.WebView: web_view_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Gallery: gallery_screen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Weapons: weapons_grid.Visibility = Visibility.Visible; break;
            }
        }
        public enum ScreenType
        {
            Main,
            Runes,
            Settings,
            WebView,
            Gallery,
            Weapons
        }

        private void web_open(string url)
        {
            webView2.Source = new Uri(url);
            webView2.ZoomFactor = 0.8;
        }

        private void refreshData()
        {
            using (var db = new AppDbContext())
            {
                BuildsList.ItemsSource = db.Builds.ToList();
                WeaponsList.ItemsSource = db.Weapons.ToList();
                RunesList.ItemsSource = db.Runes.ToList();
                GallerList.ItemsSource = db.Gallery.ToList();

                MessageBox.Show("Updated!\n\nUse support button\nif any problems", "Elden Ring Builder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}