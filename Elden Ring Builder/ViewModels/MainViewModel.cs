using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;

namespace Elden_Ring_Builder.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userName = Environment.UserName;

        [RelayCommand]
        private void AboutApp()
        {
            string aboutText = "Elden Ring Builder\n" +
                               "Version: 1.3\n";
            string descriptionText = "A simple application to help you build your character in Elden Ring.\n" +
                                     "This application is not affiliated with FromSoftware or Bandai Namco Entertainment.";
            DateTime dateTime = DateTime.Now;
            MessageBox.Show(aboutText + "\n" + descriptionText + "\n\n" + dateTime + "\n\n" + userName);
        }

        [RelayCommand]
        private void UrlOpenning(string url)
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

    }
}
