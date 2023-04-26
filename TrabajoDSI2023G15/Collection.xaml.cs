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
using Windows.UI.Xaml.Documents;
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

        public static class GlobalList
        {
            public static ObservableCollection<VMCardInfo> ListaCartas { get; } = new ObservableCollection<VMCardInfo>();
            public static ObservableCollection<VMCardInfo> ListaCartasDeck { get; } = new ObservableCollection<VMCardInfo>();
        }


        public ObservableCollection<VMCardInfo> localListaCartas = GlobalList.ListaCartas;
        public ObservableCollection<VMCardInfo> localListaCartasDeck = GlobalList.ListaCartasDeck;

        public Collection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (localListaCartas != null)
            {
                if (localListaCartas.Count <= 0)
                {
                    foreach (Carta card in Model.GetAllCards())
                    {
                        VMCardInfo VMitem = new VMCardInfo(card);
                        if (!localListaCartas.Contains(VMitem))
                            localListaCartas.Add(VMitem);
                    }
                }
            }
            if (localListaCartasDeck != null)
            {
                if (localListaCartasDeck.Count <= 0)
                {
                    foreach (Carta card in Model.GetAllCards())
                    {
                        VMCardInfo VMitem = new VMCardInfo(card);
                        if (!localListaCartasDeck.Contains(VMitem))
                            localListaCartasDeck.Add(VMitem);
                    }
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
            if (localListaCartas.Count > 1)
            {
                VMCardInfo Item = e.Items[0] as VMCardInfo;

                var index = localListaCartas.IndexOf(Item);

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
            }
        }

        private void DeckDrag_Starting(object sender, DragItemsStartingEventArgs e)
        {
            if (localListaCartasDeck.Count > 1)
            {
                VMCardInfo Item = e.Items[0] as VMCardInfo;

                var index = localListaCartasDeck.IndexOf(Item);

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

                localListaCartasDeck.RemoveAt((int)indice);
                localListaCartas.Add(vmc);
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

                localListaCartas.RemoveAt((int)indice);
                localListaCartasDeck.Add(vmc);
            }
        }

        private void ImageGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            VMCardInfo Item = e.ClickedItem as VMCardInfo;

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

            localListaCartas.Remove(e.ClickedItem as VMCardInfo);
            localListaCartasDeck.Add(vmc);
        }

        private void ImageGridView_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                VMCardInfo Item = ImageGridView.SelectedItem as VMCardInfo;

                var index = localListaCartasDeck.IndexOf(Item);

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

                localListaCartas.Remove(ImageGridView.SelectedItem as VMCardInfo);
                localListaCartasDeck.Add(vmc);
            }
        }

        private void ImageGridViewDeck_ItemClick(object sender, ItemClickEventArgs e)
        {
            VMCardInfo Item = e.ClickedItem as VMCardInfo;

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

            localListaCartasDeck.Remove(e.ClickedItem as VMCardInfo);
            localListaCartas.Add(vmc);
        }

        private void ImageGridViewDeck_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                VMCardInfo Item = ImageGridView.SelectedItem as VMCardInfo;

                var index = localListaCartasDeck.IndexOf(Item);

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

                localListaCartasDeck.Remove(ImageGridView.SelectedItem as VMCardInfo);
                localListaCartas.Add(vmc);
            }
        }
    }
}
