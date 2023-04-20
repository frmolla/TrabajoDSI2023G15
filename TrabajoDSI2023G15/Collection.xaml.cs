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
    public sealed partial class Collection : Page
    {

        public ObservableCollection<VMCardInfo> ListaCartas { get; } = new ObservableCollection<VMCardInfo>();

        public Collection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaCartas != null)
                foreach (Carta card in Model.GetAllCards())
                {
                    VMCardInfo VMitem = new VMCardInfo(card);
                    ListaCartas.Add(VMitem);
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

        }

        private void DeckDrag_Starting(object sender, DragItemsStartingEventArgs e)
        {
            VMCardInfo Item = e.Items[0] as VMCardInfo;
            e.Data.SetText(Item.Imagen.ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;

            e.Data.SetData("nombre", Item.Nombre);
        }

        private void CollectionDrag_Over(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void CollectionDrop(object sender, DragEventArgs e)
        {
            var s = await e.DataView.GetTextAsync();

            Image MiCartaImage = new Image();
            MiCartaImage.Source = new BitmapImage(new Uri("ms-appx:///" + s));

            Carta c = new Carta();

            c.Nombre = ;

            c.Mana = ;
            c.Rareza = ;
            c.Vida = ;

            VMCardInfo vmc = new VMCardInfo(c);

            c.Imagen = MiCartaImage;

            ImageGridView.Items.Add(MiCartaImage);

            //ContentControl MiDron = new ContentControl();

            //Image MiDronImage = new Image();
            ////Object ob = e.Data.Properties.TryGetValue("sourceImage", out ob);
            ////string name = ob.ToString();
            //MiDronImage.Source = new BitmapImage(new Uri("ms-appx:///" + s));
            //MiDronImage.Width = ImageSlider.Value;
            //MiDronImage.Height = ImageSlider.Value;

            //MiDron.Content = MiDronImage;
            //MiDron.IsTabStop = true;
            //MiDron.UseSystemFocusVisuals = true;

            //MiCanvas.Children.Add(MiDron);
            //MiDron.SetValue(Canvas.LeftProperty, PD.X);
            //MiDron.SetValue(Canvas.TopProperty, PD.Y);

            //MiDron.ManipulationMode = ManipulationModes.All;
            //MiDron.ManipulationDelta += NewImg_ManipulationDelta;
            //Transformacion = new CompositeTransform();
            //MiDron.RenderTransform = Transformacion;

            //MiDron.KeyDown += MiDron_KeyDown;

            //ListView target = (ListView)sender;

            //if (e.DataView.Contains(StandardDataFormats.Text))
            //{
            //    DragOperationDeferral def = e.GetDeferral();
            //    string s = await e.DataView.GetTextAsync();
            //    string[] items = s.Split('\n');
            //    foreach (string item in items)
            //    {

            //        // Create Contact object from string, add to existing target ListView
            //        string[] info = item.Split(" ", 3);
            //        Carta temp = new Carta();

            //        // Find the insertion index:
            //        Windows.Foundation.Point pos = e.GetPosition(target.ItemsPanelRoot);

            //        // If the target ListView has items in it, use the height of the first item
            //        //      to find the insertion index.
            //        int index = 0;
            //        if (target.Items.Count != 0)
            //        {
            //            // Get a reference to the first item in the ListView
            //            ListViewItem sampleItem = (ListViewItem)target.ContainerFromIndex(0);

            //            // Adjust itemHeight for margins
            //            double itemHeight = sampleItem.ActualHeight + sampleItem.Margin.Top + sampleItem.Margin.Bottom;

            //            // Find index based on dividing number of items by height of each item
            //            index = Math.Min(target.Items.Count - 1, (int)(pos.Y / itemHeight));

            //            // Find the item being dropped on top of.
            //            ListViewItem targetItem = (ListViewItem)target.ContainerFromIndex(index);

            //            // If the drop position is more than half-way down the item being dropped on
            //            //      top of, increment the insertion index so the dropped item is inserted
            //            //      below instead of above the item being dropped on top of.
            //            Windows.Foundation.Point positionInItem = e.GetPosition(targetItem);
            //            if (positionInItem.Y > itemHeight / 2)
            //            {
            //                index++;
            //            }

            //            // Don't go out of bounds
            //            index = Math.Min(target.Items.Count, index);
            //        }
            //        // Only other case is if the target ListView has no items (the dropped item will be
            //        //      the first). In that case, the insertion index will remain zero.

            //        // Find correct source list
            //        if (target.Name == "ImageGridView")
            //        {
            //            // Find the ItemsSource for the target ListView and insert
            //            VMCardInfo vmc = new VMCardInfo(temp);
            //            ImageGridView.Items.Add(vmc);
            //            //Go through source list and remove the items that are being moved
            //            foreach (Carta contact in ImageGridView.Items)
            //            {
            //                if (contact.Nombre == temp.Nombre && contact.Imagen == temp.Imagen && contact.Text == temp.Text)
            //                {
            //                    ImageGridViewDeck.Items.Remove(contact);
            //                    break;
            //                }
            //            }
            //        }
            //        else if (target.Name == "ImageGridViewDeck")
            //        {
            //            ImageGridViewDeck.Items.Add(temp);
            //            foreach (Carta contact in ImageGridViewDeck.Items)
            //            {
            //                if (contact.Nombre == temp.Nombre && contact.Imagen == temp.Imagen && contact.Text == temp.Text)
            //                {
            //                    ImageGridView.Items.Remove(contact);
            //                    break;
            //                }
            //            }
            //        }
            //    }

            //    e.AcceptedOperation = DataPackageOperation.Move;
            //    def.Complete();
            //}
        }

        private void DeckDrag_Over(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private void DeckDrop(object sender, DragEventArgs e)
        {

        }
    }
}
