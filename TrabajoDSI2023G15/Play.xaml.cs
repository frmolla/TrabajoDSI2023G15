using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrabajoDSI2023G15
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Play : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int life, mana, enemyLife, enemyMana;
        Carta[] myHand;
        int enemyHandSize;
        public Play()
        {
            this.InitializeComponent();
            life = 15; mana = 10;  enemyLife= 19; enemyMana = 6;
            enemyHandSize = new Random().Next(0,5);
            for (int i = 0; i < enemyHandSize; i++)
            {
                Image img = new Image();
                img.Source  = new BitmapImage(new Uri("ms-appx:///Assets/Dorso.png"));
                img.Width = 188;
                img.Height = 136;
                img.Margin = new Thickness(20,0,20,0);
                EnemyHand.Children.Add(img);
            }
        }

        private void Options_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opciones));
        }

        private void Surrender_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}
