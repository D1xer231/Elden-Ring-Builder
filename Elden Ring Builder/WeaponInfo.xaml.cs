using Elden_Ring_Builder.models;
using Elden_Ring_Builder.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using static Elden_Ring_Builder.ViewModels.MainViewModel;

namespace Elden_Ring_Builder
{
    /// <summary>
    /// Логика взаимодействия для WeaponInfo.xaml
    /// </summary>
    public partial class WeaponInfo : Window
    {
        public WeaponInfo(weapons selectedWeapon)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            this.DataContext = selectedWeapon;

        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void close_window_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void show_loc_btn_Click (object sender, RoutedEventArgs e)
        //{
        //    AppDbContext db = new AppDbContext();
        //    UrlOpenning(db.Weapons.Find((this.DataContext as weapons).id).location);
        //}
    }
}
