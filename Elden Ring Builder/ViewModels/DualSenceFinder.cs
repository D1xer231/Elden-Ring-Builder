using Elden_Ring_Builder.models;
using HidSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using static Elden_Ring_Builder.ViewModels.MainViewModel;

namespace Elden_Ring_Builder.ViewModels
{
    public class DualSenceFinder
    {
        public readonly TextBlock _connectionStatus;
        public readonly Image _dualSenceImg;
        public DualSenceFinder(TextBlock connectionStatus, Image dualSenceImg)
        {
            _connectionStatus = connectionStatus;
            _dualSenceImg = dualSenceImg;
        }

        private const int WM_DEVICECHANGE = 0x0219;

        public void Initialize(IntPtr windowHandle)
        {
            var source = HwndSource.FromHwnd(windowHandle);
            source.AddHook(WndProc);

            CheckForDualSense();
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DEVICECHANGE)
            {
                CheckForDualSense();
            }
            return IntPtr.Zero;
        }

        private void CheckForDualSense()
        {
            try
            {
                var list = DeviceList.Local.GetHidDevices();
                bool dualSenseFound = list.Any(dev =>
                {
                    var info = dev.GetProductName();
                    var manu = dev.GetManufacturer();
                    return (info?.ToLower().Contains("dualsense") ?? false)
                        || (manu?.ToLower().Contains("sony") ?? false)
                        || dev.VendorID == 0x054C;
                });
                if (dualSenseFound)
                {
                    Debug.WriteLine("🎮 DualSense found!");
                    _connectionStatus.Text = "DualSense Connected";
                    //_dualSenceImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/dualsense-black.png"));
                }
                else
                {
                    Debug.WriteLine("🕹️ DualSense not found");
                    _connectionStatus.Text = "Not Connected";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
