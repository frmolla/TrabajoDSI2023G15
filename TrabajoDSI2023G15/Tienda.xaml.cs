using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Tienda : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int money;
        int[] precios;
        Button[] shopButtons;
        public Tienda()
        {
            this.InitializeComponent();
            money = 1200;
            precios = new int[] { 50, 100, 200, 400, 800 };
            shopButtons = new Button[] { Shop_0, Shop_1, Shop_2, Shop_3, Shop_4 };
            for (int i = 0; i < precios.Length; i++)
            {
                shopButtons[i].IsEnabled = money >= precios[i];
            }
        }


        private void Shop_OnClick(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            int id = (int)(but.Name[but.Name.Length-1]);
            money -= precios[id - '0'];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(money)));
            for (int i = 0; i < precios.Length; i++){
                shopButtons[i].IsEnabled = money>=precios[i];
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

    }
}
