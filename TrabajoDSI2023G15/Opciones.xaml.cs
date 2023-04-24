using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrabajoDSI2023G15
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Opciones : Page
    {
        public Opciones()
        {
            this.InitializeComponent();
            FullScreenCheck.IsChecked = ApplicationView.GetForCurrentView().IsFullScreenMode;
        }

        private void SaveChanges_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ApplicationView.GetForCurrentView().TryEnterFullScreenMode())
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ApplicationView.GetForCurrentView().IsFullScreenMode)
            {
                ApplicationView.GetForCurrentView().ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            }
        }

        private void Resolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = ResolutionBox.SelectedItem as ComboBoxItem;
            string resolution = selectedItem.Content.ToString();

            switch (resolution)
            {
                case "1920 x 1080":
                    if (ApplicationView.GetForCurrentView().TryEnterFullScreenMode())
                    {
                        ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                        FullScreenCheck.IsChecked = true;
                    }
                    break;
                case "1680 x 950":
                    if (ApplicationView.GetForCurrentView().IsFullScreenMode)
                    {
                        ApplicationView.GetForCurrentView().ExitFullScreenMode();
                        ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                        FullScreenCheck.IsChecked = false;
                    }
                    ApplicationView.GetForCurrentView().TryResizeView(new Size(1680, 950));
                    break;
                case "1366 x 768":
                    if (ApplicationView.GetForCurrentView().IsFullScreenMode)
                    {
                        ApplicationView.GetForCurrentView().ExitFullScreenMode();
                        ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                        FullScreenCheck.IsChecked = false;
                    }
                    ApplicationView.GetForCurrentView().TryResizeView(new Size(1366, 768));
                    break;
                case "1280 x 720":
                    if (ApplicationView.GetForCurrentView().IsFullScreenMode)
                    {
                        ApplicationView.GetForCurrentView().ExitFullScreenMode();
                        ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                        FullScreenCheck.IsChecked = false;
                    }
                    ApplicationView.GetForCurrentView().TryResizeView(new Size(1280, 720));
                    break;
                default:
                    break;
            }
        }
    }
}
