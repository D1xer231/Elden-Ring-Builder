using dotenv.net;
using Elden_Ring_Builder.models;
using Elden_Ring_Builder.Services;
using Elden_Ring_Builder.ViewModels;
using EldenRingBuilder.Services;
using HidSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Printing;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Telegram.Bot.Types;
using static Elden_Ring_Builder.ViewModels.MainViewModel;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;

namespace Elden_Ring_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext? db;
        private SteamService _steamService;
        private DualSenceFinder _finder;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            
            _steamService = new SteamService(
               @"D:\c# projects\MyProjectsC#\Elden Ring  Builder\Elden Ring Builder\Elden Ring Builder\.env",
               steam_img,
               steam_name,
               steam_id,
               steam_gameCount,
               total_hrs_count,
               hrs_played_elden_ring,
               total_badges_count,
               friends_count,
               statsTable,
               steam_logo,
               cat_img,
               nameId
           );
            _finder = new DualSenceFinder(DualSence_status, DualSence_img);

            DataContext = new MainViewModel();
            AppDbContext db = new AppDbContext();

            //List<builds> builds = db.Builds.ToList();
            ////BuildsList.ItemsSource = builds;
            //List<gallery> gallery = db.Gallery.ToList();
            ////GallerList.ItemsSource = gallery;
            //List<weapons> weapons = db.Weapons.ToList();
            ////WeaponsList.ItemsSource = weapons;
            //List<runes> runes = db.Runes.ToList();
            ////RunesList.ItemsSource = runes;

            //Parallel.Invoke(
            //    () => BuildsList.ItemsSource = builds,
            //    () => GallerList.ItemsSource = gallery,
            //    () => WeaponsList.ItemsSource = weapons,
            //    () => RunesList.ItemsSource = runes 
            //    );
        }
        //--------------- Buttons Clicks ---------------//
        private void hide_app_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void users_steam_btn_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Steam);
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
            adress_input.Text = "";
            webView2.Visibility = Visibility.Visible;
        }

        private void settings_btn_Click(object sender, RoutedEventArgs e)
        {
            QrImage.Source = QrGenerator.GenerateQr("https://i.pinimg.com/736x/84/a5/ca/84a5ca952332030fa8a91600e9ecd239.jpg");
            ShowScreen(ScreenType.Settings);
        }

        private void refresh_data_btn_Click(object sender, RoutedEventArgs e)
        {
            refreshData();
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

        private void webview_nav_Click(object sender, RoutedEventArgs e)
        {
            //var tag = (sender as Button)?.Tag?.ToString();
            //if (tag == "back" && webView2.CanGoBack) webView2.GoBack();
            //else if (tag == "forward" && webView2.CanGoForward) webView2.GoForward(); by tags Tag="back" or Tag="forward"

            if (sender == webview_back && webView2.CanGoBack) webView2.GoBack();
            else if (sender == webview_forward && webView2.CanGoForward) webView2.GoForward();
        }

        private void Balck_DualSence_Checked(object sender, RoutedEventArgs e)
        {
            string path = "pack://application:,,,/img/dualsence-black.png";
            BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.Absolute));
            DualSence_img.Source = bitmap;
        }
        private void White_DualSence_UnChecked(object sender, RoutedEventArgs e)
        {
            string path = "pack://application:,,,/img/dualsence-white.png";
            BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.Absolute));
            DualSence_img.Source = bitmap;
        }

        // ------------------------------------------------//


        //----------------- Methods ------------------//
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        
        private WeaponInfo? currentWeaponInfoWindow = null;
        private void Weapon_info_LeftBtnUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is weapons weapon)
            {
                // Закрываем старое окно, если оно есть
                if (currentWeaponInfoWindow != null)
                {
                    currentWeaponInfoWindow.Close();
                }

                // Создаём и показываем новое окно
                currentWeaponInfoWindow = new WeaponInfo(weapon);
                currentWeaponInfoWindow.Show();
            }
            //e.Handled = true;

            //if (sender is Border border && border.DataContext is weapons weapon)
            //{
            //    // Закрываем старое окно
            //    if (currentWeaponInfoWindow != null)
            //        currentWeaponInfoWindow.Close();

            //    // Открываем новое окно
            //    currentWeaponInfoWindow = new WeaponInfo(weapon);
            //    currentWeaponInfoWindow.Show();

            //    // Сброс выделения
            //    WeaponsList.SelectedItem = null;
            //}
        }

        private async void get_steam_info(object sender, RoutedEventArgs e)
        {
            string? id = insert_steam_id.Text?.Trim();

            if (string.IsNullOrEmpty(id))
            {
                insert_steam_id.Text = "76561199220453620";

                statsTable.Visibility = Visibility.Hidden;
                steam_logo.Visibility = Visibility.Hidden;
                cat_img.Visibility = Visibility.Hidden;
                nameId.Visibility = Visibility.Hidden;
                steam_img.Visibility = Visibility.Hidden;

                Debug.WriteLine("⚠️ Steam ID not found!");
                return;
            }

            try
            {
                (sender as Button)!.IsEnabled = false;
                steam_name.Text = "";
                steam_id.Text = "";

                await _steamService.LoadSteamInfoAsync(id);

                Debug.WriteLine($"✅ Data for SteamID {id} loaded.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error while Steam data loading: {ex.Message}");
            }
            finally
            {
                (sender as Button)!.IsEnabled = true;
            }
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

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            _finder.Initialize(handle);

            var buildsTask = Task.Run(() => {
                using var db = new AppDbContext();
                return db.Builds.ToList();
            });

            var galleryTask = Task.Run(() => {
                using var db = new AppDbContext();
                return db.Gallery.ToList();
            });

            var weaponsTask = Task.Run(() => {
                using var db = new AppDbContext();
                return db.Weapons.ToList();
            });

            var runesTask = Task.Run(() => {
                using var db = new AppDbContext();
                return db.Runes.ToList();
            });

            await Task.WhenAll(buildsTask, galleryTask, weaponsTask, runesTask);

            BuildsList.ItemsSource = buildsTask.Result;
            GallerList.ItemsSource = galleryTask.Result;
            WeaponsList.ItemsSource = weaponsTask.Result;
            RunesList.ItemsSource = runesTask.Result;
        }

        //------------------------------------------------//
    }
}