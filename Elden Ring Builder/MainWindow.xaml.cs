using Elden_Ring_Builder.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QRCoder;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
            AppDbContext db = new AppDbContext();

            welcome_user_text();
            
            web_open("https://en.bandainamcoent.eu/elden-ring/elden-ring");
            webView2.Visibility = Visibility.Visible;

            List<builds> builds = db.Builds.ToList();
            List<weapons> weapons = db.Weapons.ToList();
            List<runes> runes = db.Runes.ToList();
            BuildsList.ItemsSource = builds;
            WeaponsList.ItemsSource = weapons;
            RunesList.ItemsSource = runes;
        }


        private void welcome_user_text() => welcome_user_txt.Text = Environment.UserName;

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void about_app_Click(object sender, RoutedEventArgs e)
        {
            string aboutText = "Elden Ring Builder\n" +
                               "Version: 1.2\n";
            string descriptionText = "A simple application to help you build your character in Elden Ring.\n" +
                                     "This application is not affiliated with FromSoftware or Bandai Namco Entertainment.";
            DateTime dateTime = DateTime.Now;
            MessageBox.Show(aboutText + "\n" + descriptionText + "\n\n" + dateTime);
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

        private void author_steam_btn_Click(object sender, RoutedEventArgs e)
        {
            url_openning("steam://openurl/https://steamcommunity.com/profiles/76561199220453620/");
        }

        private void github_btn_Click(object sender, RoutedEventArgs e)
        {
            url_openning("https://github.com/D1xer231");
        }

        private void author_github_btn_Click(object sender, RoutedEventArgs e)
        {
            url_openning("https://github.com/D1xer231/Elden-Ring-Builder");
        }

        private void telegram_btn_Click(object sender, RoutedEventArgs e)
        {
            url_openning("https://t.me/d1xer_231");
        }
        private void tg_bot_Click(object sender, RoutedEventArgs e)
        {
            url_openning("https://t.me/elden_builder_bot");
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
            main_screen_grid.Visibility = Visibility.Hidden;
            runes_list_grid.Visibility = Visibility.Hidden;
            settings_screen_grid.Visibility = Visibility.Hidden;
            web_view_screen_grid.Visibility = Visibility.Hidden;
            weapons_grid.Visibility = Visibility.Visible;
        }

        private void change_to_mainscreen_click(object sender, RoutedEventArgs e)
        {
            weapons_grid.Visibility = Visibility.Hidden;
            runes_list_grid.Visibility = Visibility.Hidden;
            settings_screen_grid.Visibility = Visibility.Hidden;
            web_view_screen_grid.Visibility = Visibility.Hidden;
            main_screen_grid.Visibility = Visibility.Visible;
        }

        private void runeslist_btn_Click(object sender, RoutedEventArgs e)
        {
            main_screen_grid.Visibility = Visibility.Hidden;
            weapons_grid.Visibility = Visibility.Hidden;
            settings_screen_grid.Visibility = Visibility.Hidden;
            web_view_screen_grid.Visibility = Visibility.Hidden;
            runes_list_grid.Visibility = Visibility.Visible;
        }

        private void Wiki_web_Click(object sender, RoutedEventArgs e)
        {
            main_screen_grid.Visibility = Visibility.Hidden;
            weapons_grid.Visibility = Visibility.Hidden;
            runes_list_grid.Visibility = Visibility.Hidden;
            settings_screen_grid.Visibility = Visibility.Hidden;
            web_view_screen_grid.Visibility = Visibility.Visible;
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

        private void settings_btn_Click(object sender, RoutedEventArgs e)
        {
            QrImage.Source = GenerateQr("https://i.pinimg.com/736x/2a/f1/cd/2af1cde161d06fe92a1239f70c01154a.jpg");

            main_screen_grid.Visibility = Visibility.Hidden;
            weapons_grid.Visibility = Visibility.Hidden;
            runes_list_grid.Visibility = Visibility.Hidden;
            web_view_screen_grid.Visibility = Visibility.Hidden;
            settings_screen_grid.Visibility = Visibility.Visible;
        }

        private void refresh_data_btn_Click(object sender, RoutedEventArgs e)
        {
           refreshData();
        }

        private void refreshData() 
        {
            using (var db = new AppDbContext())
            {
                BuildsList.ItemsSource = db.Builds.ToList();
                WeaponsList.ItemsSource = db.Weapons.ToList();
                RunesList.ItemsSource = db.Runes.ToList();

                MessageBox.Show("Updated!\n\nUse support button\nif any problems", "Elden Ring Builder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void hide_app_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void check_for_updates_btn_Click(object sender, RoutedEventArgs e)
        {
           url_openning("https://github.com/D1xer231/Elden-Ring-Builder/releases");
        }

        private void url_openning (string url)
        {
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
                MessageBox.Show("Error, while opening");
            }
        }

        private void web_open (string url)
        {
            webView2.Source = new Uri(url);
            webView2.ZoomFactor = 0.8;
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
    }
}