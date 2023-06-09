﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrabajoDSI2023G15
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {

        public ObservableCollection<VMGameInfo> ListaPartidas { get; } = new ObservableCollection<VMGameInfo>();
        public Profile()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaPartidas != null)
                foreach (GameInfo gameInfo in ModelGameInfo.GetAllGames())
                {
                    VMGameInfo VMitem = new VMGameInfo(gameInfo);
                    ListaPartidas.Add(VMitem);
                }
            //GameLoop.GameTimer.Start();

            base.OnNavigatedTo(e);
        }

        private void ChangeSelect(object sender, SelectionChangedEventArgs e)
        {
            GameInfo gameInfo = ImageGridView.SelectedItem as GameInfo;
            Texto.Text = gameInfo.Explicacion;
        }

        private void Profile_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Profile));
        }

        private void Options_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opciones));
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
