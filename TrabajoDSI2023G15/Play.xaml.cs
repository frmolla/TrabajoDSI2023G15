using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
        public ObservableCollection<VMCardInfo> MyHand { get; } = new ObservableCollection<VMCardInfo>();
        public ObservableCollection<VMCardInfo> MyUnits { get; } = new ObservableCollection<VMCardInfo>();
        public ObservableCollection<VMCardInfo> EnemyUnits { get; } = new ObservableCollection<VMCardInfo>();

        int myLife, myMana, enemyLife, enemyMana;
        int myHandSize;
        int enemyHandSize;
        int enemyUnitNumber;
        public Play()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myLife = 15; myMana = 10; enemyLife = 19; enemyMana = 6;
            enemyHandSize = new Random().Next(1, 5);
            myHandSize = new Random().Next(1, 5);
            enemyUnitNumber = new Random().Next(1, 4);

            for (int i = 0; i < enemyHandSize; i++)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("ms-appx:///Assets/Dorso.png"));
                img.Width = 188;
                img.Height = 136;
                img.Margin = new Thickness(20, 0, 20, 0);
                EnemyHand.Children.Add(img);
            }
            for (int i = 0; i < myHandSize; i++)
            {
                int card = new Random().Next(0, Model.GetAllCards().Count);
                VMCardInfo VMitem = new VMCardInfo(Model.GetAllCards()[i]);
                MyHand.Add(VMitem);
            }
            for (int i = 0; i < enemyUnitNumber; i++)
            {
                int card = new Random().Next(0, Model.GetAllCards().Count);
                VMCardInfo VMitem = new VMCardInfo(Model.GetAllCards()[i]);
                EnemyUnits.Add(VMitem);
            }

            base.OnNavigatedTo(e);
        }
        private void HandDrag_Starting(object sender, DragItemsStartingEventArgs e)
        {
            if (MyHand.Count >= 1)
            {
                VMCardInfo Item = e.Items[0] as VMCardInfo;

                var index = MyHand.IndexOf(Item);

                e.Data.SetData("nombre", Item.Nombre.ToString());
                e.Data.SetData("text", Item.Text.ToString());
                e.Data.SetData("img", Item.Imagen.ToString());
                e.Data.SetData("imgMana", Item.ManaImagen.ToString());
                e.Data.SetData("imgAtaque", Item.AtaqueImagen.ToString());
                e.Data.SetData("imgVida", Item.VidaImagen.ToString());
                e.Data.SetData("imgRareza", Item.RarezaImagen.ToString());
                e.Data.SetData("mana", Item.Mana);
                e.Data.SetData("vida", Item.Vida);
                e.Data.SetData("ataque", Item.Ataque);
                e.Data.SetData("rareza", Item.Rareza);
                e.Data.SetData("indice", index);
                e.Data.SetData("unidad", false);

            }
        }
        private async void BattlefieldDrag_Over(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains("mana")) { 
                var mana = await e.DataView.GetDataAsync("mana");
                if ((int)mana <= myMana)
                    e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private async void BattlefieldDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var nombre = await e.DataView.GetDataAsync("nombre");
                var img = await e.DataView.GetDataAsync("img");
                var imgMana = await e.DataView.GetDataAsync("imgMana");
                var imgAtaque = await e.DataView.GetDataAsync("imgAtaque");
                var imgVida = await e.DataView.GetDataAsync("imgVida");
                var imgRareza = await e.DataView.GetDataAsync("imgRareza");
                var text = await e.DataView.GetDataAsync("text");
                var mana = await e.DataView.GetDataAsync("mana");
                var vida = await e.DataView.GetDataAsync("vida");
                var ataque = await e.DataView.GetDataAsync("ataque");
                var rareza = await e.DataView.GetDataAsync("rareza");
                var indice = await e.DataView.GetDataAsync("indice");

                Carta c = new Carta();
                c.Nombre = nombre.ToString();
                c.Imagen = img.ToString();
                c.ManaImagen = imgMana.ToString();
                c.AtaqueImagen = imgAtaque.ToString();
                c.VidaImagen = imgVida.ToString();
                c.RarezaImagen = imgRareza.ToString();
                c.Text = text.ToString();
                c.Mana = (int)mana;
                c.Vida = (int)vida;
                c.Ataque = (int)ataque;
                c.Rareza = (int)rareza;
                VMCardInfo vmc = new VMCardInfo(c);

                vmc.CCImg = new ContentControl();
                vmc.CCImg.Content = new BitmapImage(new Uri("ms-appx:///" + vmc.Imagen));
                vmc.CCImg.UseSystemFocusVisuals = true;

                MyHand.RemoveAt((int)indice);
                MyUnits.Add(vmc);
                myMana -= (int)mana;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(myMana)));

            }
        }

        private void BattlefieldDrag_Starting(object sender, DragItemsStartingEventArgs e)
        {
            VMCardInfo Item = e.Items[0] as VMCardInfo;
            e.Data.SetData("ataque", Item.Ataque);
            e.Data.SetData("unidad", true);
        }

        private async void EnemyDrag_Over(object sender, DragEventArgs e)
        {
            var unidad = await e.DataView.GetDataAsync("unidad");
            if ((bool)unidad)
                e.AcceptedOperation = DataPackageOperation.Move;
        }

        private void Hand_ItemClick(object sender, ItemClickEventArgs e)
        {
            VMCardInfo Item = e.ClickedItem as VMCardInfo;

            if (Item.Mana <= myMana)
            {
        
                Carta c = new Carta();
                c.Nombre = Item.Nombre;
                c.Imagen = Item.Imagen;
                c.ManaImagen = Item.ManaImagen;
                c.AtaqueImagen = Item.AtaqueImagen;
                c.VidaImagen = Item.VidaImagen;
                c.RarezaImagen = Item.RarezaImagen;
                c.Text = Item.Text;
                c.Mana = Item.Mana;
                c.Vida = Item.Vida;
                c.Ataque = Item.Ataque;
                c.Rareza = Item.Rareza;
                VMCardInfo vmc = new VMCardInfo(c);

                vmc.CCImg = new ContentControl();
                vmc.CCImg.Content = new BitmapImage(new Uri("ms-appx:///" + vmc.Imagen));
                vmc.CCImg.UseSystemFocusVisuals = true;

                MyHand.Remove(e.ClickedItem as VMCardInfo);
                MyUnits.Add(vmc);

                myMana -= Item.Mana;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(myMana)));
            }
        }

        private void Hand_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                VMCardInfo Item = Hand.SelectedItem as VMCardInfo;
                if (Item.Mana <= myMana)
                {
                    Carta c = new Carta();
                    c.Nombre = Item.Nombre;
                    c.Imagen = Item.Imagen;
                    c.ManaImagen = Item.ManaImagen;
                    c.AtaqueImagen = Item.AtaqueImagen;
                    c.VidaImagen = Item.VidaImagen;
                    c.RarezaImagen = Item.RarezaImagen;
                    c.Text = Item.Text;
                    c.Mana = Item.Mana;
                    c.Vida = Item.Vida;
                    c.Ataque = Item.Ataque;
                    c.Rareza = Item.Rareza;
                    VMCardInfo vmc = new VMCardInfo(c);

                    vmc.CCImg = new ContentControl();
                    vmc.CCImg.Content = new BitmapImage(new Uri("ms-appx:///" + vmc.Imagen));
                    vmc.CCImg.UseSystemFocusVisuals = true;

                    MyHand.Remove(Hand.SelectedItem as VMCardInfo);
                    MyUnits.Add(vmc);

                    myMana -= Item.Mana;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(myMana)));
                }
            }
        }

        private void PlayerBoard_ItemClick(object sender, ItemClickEventArgs e)
        {
            VMCardInfo Item = e.ClickedItem as VMCardInfo;

            enemyLife -= Item.Ataque;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(enemyLife)));

            if (enemyLife <= 0)
                Frame.Navigate(typeof(MainPage));
        }

        private void PlayerBoard_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                VMCardInfo Item = Hand.SelectedItem as VMCardInfo;

                enemyLife -= Item.Ataque;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(enemyLife)));

                if (enemyLife <= 0)
                    Frame.Navigate(typeof(MainPage));
            }
        }

        private async void EnemyDrop(object sender, DragEventArgs e)
        {
            var ataque = await e.DataView.GetDataAsync("ataque");
            enemyLife -= (int)ataque;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(enemyLife)));

            if (enemyLife <= 0)
                Frame.Navigate(typeof(MainPage));
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
