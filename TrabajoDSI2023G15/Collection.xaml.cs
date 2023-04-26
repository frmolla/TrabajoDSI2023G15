using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrabajoDSI2023G15
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Collection : Page
    {

        public ObservableCollection<VMCardInfo> ListaCartas { get; } = new ObservableCollection<VMCardInfo>();
        public ObservableCollection<VMCardInfo> ListaCartasDeck { get; } = new ObservableCollection<VMCardInfo>();


        public Collection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaCartas != null)
            {
                foreach (Carta card in Model.GetAllCards())
                {
                    VMCardInfo VMitem = new VMCardInfo(card);
                    ListaCartas.Add(VMitem);
                }
            }
            if (ListaCartas != null)
            {
                foreach (Carta card in Model.GetAllCards())
                {
                    VMCardInfo VMitem = new VMCardInfo(card);
                    ListaCartasDeck.Add(VMitem);
                }
            }
            //GameLoop.GameTimer.Start();

            base.OnNavigatedTo(e);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void CollectionDrag_Starting(object sender, DragItemsStartingEventArgs e)
        {
            if (ListaCartas.Count > 1)
            {
                VMCardInfo Item = e.Items[0] as VMCardInfo;
                //e.Data.SetText(Item.Nombre.ToString());
                //e.Data.SetText(Item.Imagen.ToString());
                //e.Data.SetText(Item.Text.ToString());
                //e.Data.RequestedOperation = DataPackageOperation.Move;

                var index = ListaCartas.IndexOf(Item);

                e.Data.SetData("nombre", Item.Nombre.ToString());
                e.Data.SetData("text", Item.Text.ToString());
                e.Data.SetData("img", Item.Imagen.ToString());
                e.Data.SetData("mana", Item.Mana);
                e.Data.SetData("vida", Item.Vida);
                e.Data.SetData("ataque", Item.Ataque);
                e.Data.SetData("rareza", Item.Rareza);
                e.Data.SetData("indice", index);
            }
        }

        private void DeckDrag_Starting(object sender, DragItemsStartingEventArgs e)
        {
            if (ListaCartasDeck.Count > 1)
            {
                VMCardInfo Item = e.Items[0] as VMCardInfo;
                //e.Data.SetText(Item.Nombre.ToString());
                //e.Data.SetText(Item.Imagen.ToString());
                //e.Data.SetText(Item.Text.ToString());
                //e.Data.RequestedOperation = DataPackageOperation.Move;

                var index = ListaCartasDeck.IndexOf(Item);

                e.Data.SetData("nombre", Item.Nombre.ToString());
                e.Data.SetData("text", Item.Text.ToString());
                e.Data.SetData("img", Item.Imagen.ToString());
                e.Data.SetData("imgMana", Item.ManaImagen.ToString());
                e.Data.SetData("imgAtaque", Item.AtaqueImagen.ToString());
                e.Data.SetData("imgVida", Item.VidaImagen.ToString());
                e.Data.SetData("mana", Item.Mana);
                e.Data.SetData("vida", Item.Vida);
                e.Data.SetData("ataque", Item.Ataque);
                e.Data.SetData("rareza", Item.Rareza);
                e.Data.SetData("indice", index);
            }
        }

        private void CollectionDrag_Over(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void CollectionDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var nombre = await e.DataView.GetDataAsync("nombre");
                var img = await e.DataView.GetDataAsync("img");
                var imgMana = await e.DataView.GetDataAsync("imgMana");
                var imgAtaque = await e.DataView.GetDataAsync("imgAtaque");
                var imgVida = await e.DataView.GetDataAsync("imgVida");
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
                c.Text = text.ToString();
                c.Mana = (int)mana;
                c.Vida = (int)vida;
                c.Ataque = (int)ataque;
                c.Rareza = (int)rareza;
                VMCardInfo vmc = new VMCardInfo(c);

                vmc.CCImg = new ContentControl();
                vmc.CCImg.Content = new BitmapImage(new Uri("ms-appx:///" + vmc.Imagen));
                vmc.CCImg.UseSystemFocusVisuals = true;

                ListaCartasDeck.RemoveAt((int)indice);
                ListaCartas.Add(vmc);
            }      
        }

        private void DeckDrag_Over(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void DeckDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var nombre = await e.DataView.GetDataAsync("nombre");
                var img = await e.DataView.GetDataAsync("img");
                var text = await e.DataView.GetDataAsync("text");
                var mana = await e.DataView.GetDataAsync("mana");
                var vida = await e.DataView.GetDataAsync("vida");
                var ataque = await e.DataView.GetDataAsync("ataque");
                var rareza = await e.DataView.GetDataAsync("rareza");
                var indice = await e.DataView.GetDataAsync("indice");


                Carta c = new Carta();
                c.Nombre = nombre.ToString();
                c.Imagen = img.ToString();
                c.Text = text.ToString();
                c.Mana = (int)mana;
                c.Vida = (int)vida;
                c.Ataque = (int)ataque;
                c.Rareza = (int)rareza;
                VMCardInfo vmc = new VMCardInfo(c);

                vmc.CCImg = new ContentControl();
                vmc.CCImg.Content = new BitmapImage(new Uri("ms-appx:///" + vmc.Imagen));
                vmc.CCImg.UseSystemFocusVisuals = true;

                ListaCartas.RemoveAt((int)indice);
                ListaCartasDeck.Add(vmc);
            }
        }
    }
}
