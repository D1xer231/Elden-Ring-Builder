using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Elden_Ring_Builder.ViewModels
{
    internal class WebOpen
    {
        public readonly WebView2 _webView2;
        public readonly TextBox _adress_input;
        public WebOpen(WebView2 webView2, TextBox adress_input)
        {
            _webView2 = webView2;
            _adress_input = adress_input;
        }
        public void web_open(string url)
        {
            _webView2.ZoomFactor = 0.8;
            try
            {
                _webView2.Source = new System.Uri(url);
            }
            catch (System.UriFormatException)
            {
                _adress_input.Text = "";
                MessageBox.Show("Invalid URL format. Please check the address and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
