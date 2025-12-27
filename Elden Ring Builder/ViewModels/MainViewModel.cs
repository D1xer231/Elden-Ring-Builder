using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Web.WebView2.WinForms;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Elden_Ring_Builder.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? userName = Environment.UserName;

        [RelayCommand]
        private void AboutApp()
        {
            string aboutText = "Elden Ring Builder\n" +
                               "Version: 2.5\n";
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
                try
                {
                    string currentDir = Directory.GetCurrentDirectory();
                    string path = Path.Combine(currentDir, url);
                    path = Path.GetFullPath(path);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                } catch
                {
                    MessageBox.Show($"Unable to open the link or file.\n\nError while {url} opening");
                }
            }
        }

    }
}
