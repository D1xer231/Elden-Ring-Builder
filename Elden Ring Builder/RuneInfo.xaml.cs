using Elden_Ring_Builder.models;
using Elden_Ring_Builder.Services;
using Elden_Ring_Builder.ViewModels;
using EldenRingBuilder.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;

namespace Elden_Ring_Builder
{
    /// <summary>
    /// Логика взаимодействия для RuneInfo.xaml
    /// </summary>
    public partial class RuneInfo : Window
    {
        public RuneInfo(runes selectedRune)
        {
            InitializeComponent();

            DataContext = new MainViewModel();
            this.DataContext = selectedRune;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void close_window_btn_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
