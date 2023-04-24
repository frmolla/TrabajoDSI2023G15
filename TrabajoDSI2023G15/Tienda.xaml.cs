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
    public class GlobalVariables
    {
        public static int Money { get; set; }
    }


    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Tienda : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int Money;
        private int[] Prices;
        private Button[] ShopButtons;
        public Tienda()
        {
            this.InitializeComponent();
            Money = GlobalVariables.Money;
            Prices = new int[] { 50, 100, 200, 400, 800 };
            ShopButtons = new Button[] { Shop_0, Shop_1, Shop_2, Shop_3, Shop_4 };
            for (int i = 0; i < Prices.Length; i++)
            {
                ShopButtons[i].IsEnabled = Money >= Prices[i];
            }
        }


        private void Shop_OnClick(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            int id = (int)(but.Name[but.Name.Length-1]);
            Money -= Prices[id - '0'];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Money)));
            for (int i = 0; i < Prices.Length; i++){
                ShopButtons[i].IsEnabled = Money >= Prices[i];
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                GlobalVariables.Money = Money;
                Frame.GoBack();
            }
        }

    }
}
