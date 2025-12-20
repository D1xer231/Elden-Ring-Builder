using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebView2 = Microsoft.Web.WebView2.Wpf.WebView2;

namespace Elden_Ring_Builder.ViewModels
{
    internal class WebView2Settings
    {
        public void WebView_PreeSet(WebView2 WebView2)
        {
            WebView2.ZoomFactor = 0.8;

            var s = WebView2.CoreWebView2.Settings;

            s.IsScriptEnabled = false;
            s.IsWebMessageEnabled = false;
            s.AreDevToolsEnabled = false;
            s.AreDefaultContextMenusEnabled = false;
            s.IsStatusBarEnabled = false;
            WebView2.CoreWebView2.DownloadStarting += (s, e) =>
            {
                e.Cancel = true;
            };
        }
        public void Web_Link_Open(WebView2 WebView2, TextBox adress_input, string url)
        {
            try
            {
                WebView2.Source = new System.Uri(url);
            }
            catch (System.UriFormatException)
            {
                adress_input.Text = "";
                MessageBox.Show("Invalid URL format. Please check the address and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Open_Url_From_Input(WebView2 WebView2, TextBox adress_input)
        {
            string url = adress_input.Text.Trim();
            if (string.IsNullOrEmpty(url)) return;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;

            Web_Link_Open(WebView2, adress_input, url);
            WebView2.Visibility = Visibility.Visible;
        }
    }
}
