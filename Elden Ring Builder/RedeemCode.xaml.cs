using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Elden_Ring_Builder.models;
namespace Elden_Ring_Builder
{
    /// <summary>
    /// Логика взаимодействия для RedeemCode.xaml
    /// </summary>
    public partial class RedeemCode : Window
    {
        public RedeemCode()
        {
            InitializeComponent();
        }
        private void send_email_btn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "mailto:aalexandr397@gmail.com?subject=APP_PROBLEM_OR_ETC&body= Describe the problem",
                UseShellExecute = true
            });
        }

        private void RedeemButton_Click(object sender, RoutedEventArgs e)
        {
            string input = CodeTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                if (input == "kapitan_Moshonka")
                {
                    redeemed_code.Text = "Life if like dick\nsomethimes its hard\nsomethimes its soft\nbut you must always keep it hard\nand never give up";
                }
            }
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            string input = CodeTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                redeemed_code.Text = "";
                CodeTextBox.Text = "";
            }
        }
        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            RedeemCode redeemCode = new RedeemCode();
            this.Close();
        }
    }
}
