using Elden_Ring_Builder.models;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

            date_and_time_Tick();
            welcome_user_text();

            List<builds> builds = db.Builds.ToList();
            List<weapons> weapons = db.Weapons.ToList();

            BuildsList.ItemsSource = builds;
            WeaponsList.ItemsSource = weapons;
        }
        private void welcome_user_text() => welcome_user_txt.Text = Environment.UserName;
        private void date_and_time_Tick() => date_and_time_txt.Text = DateTime.Now.ToString("dd.MM.yyyy");
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
                               "Version: 1.0.0\n";
            string descriptionText = "A simple application to help you build your character in Elden Ring.\n" +
                                     "This application is not affiliated with FromSoftware or Bandai Namco Entertainment.";
            MessageBox.Show(aboutText + "\n" + descriptionText);
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
            try
            {
                string steamProfileUrl = "steam://openurl/https://steamcommunity.com/profiles/76561199220453620/";

                Process.Start(new ProcessStartInfo
                {
                    FileName = steamProfileUrl,
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Error, while steam opening");
            }
        }

        private void github_btn_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://github.com/D1xer231";
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
                MessageBox.Show("Error, while github opening");
            }
        }

        private void author_github_btn_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://github.com/D1xer231/Elden-Ring-Builder";
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
                MessageBox.Show("Error, while github opening");
            }
        }

        private void telegram_btn_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://t.me/d1xer_231";
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
                MessageBox.Show("Error, while telegram opening");
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

        private void apptheme_btn_Click(object sender, RoutedEventArgs e)
        {
            // create change theme window to light or dark
           
        }

        private void weapons_screen_Click(object sender, RoutedEventArgs e)
        {
            main_screen_grid.Visibility = Visibility.Hidden;
            weapons_grid.Visibility = Visibility.Visible;
        }

        private void change_to_mainscreen_click(object sender, RoutedEventArgs e)
        {
            weapons_grid.Visibility = Visibility.Hidden;
            main_screen_grid.Visibility = Visibility.Visible;
        }

    }
}