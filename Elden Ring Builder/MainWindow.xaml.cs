using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //LoadData();
            welcome_user_txt.Text = Environment.UserName;
        }

        //private void LoadData()
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        var builds = db.Builds.ToList();
        //         dataGrid.ItemsSource = builds;
        //    }
        //}

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
    }
}