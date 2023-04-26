using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TrabajoDSI2023G15
{
    public class VMGameInfo : GameInfo
    {
        public Image Img;
        public ContentControl CCImg;
        public int Zoom;
        public VMGameInfo(GameInfo gameInfo)
        {
            Id = gameInfo.Id;
            Nombre = gameInfo.Nombre;
            Explicacion = gameInfo.Explicacion;
            X = gameInfo.X;
            Y = gameInfo.Y;
            RX = gameInfo.RX;
            RY = gameInfo.RY;
            CCImg = new ContentControl();
            CCImg.Content = Img;
            CCImg.UseSystemFocusVisuals = true;
            //CCImg.Visibility = Windows.UI.Xaml.Visibility.Visible;//.Collapsed;
            //Mapa.Children.Add(CCImg);
            //Mapa.Children.Last().SetValue(Canvas.LeftProperty, X - 25);
            //Mapa.Children.Last().SetValue(Canvas.TopProperty, Y - 25);
        }
    }

    public class VMCardInfo : Carta
    {
        public Image Img;
        public ContentControl CCImg;
        public int Zoom;
        public VMCardInfo(Carta card)
        {
            Nombre = card.Nombre;
            Text = card.Text;
            Imagen = card.Imagen;
            Mana= card.Mana;
            Ataque = card.Ataque;
            Vida= card.Vida;
            ManaImagen = card.ManaImagen;
            AtaqueImagen= card.AtaqueImagen;
            VidaImagen = card.VidaImagen;
            RarezaImagen= card.RarezaImagen;
            CCImg = new ContentControl();
            CCImg.Content = Img;
            CCImg.UseSystemFocusVisuals = true;
            //CCImg.Visibility = Windows.UI.Xaml.Visibility.Visible;//.Collapsed;
            //Mapa.Children.Add(CCImg);
            //Mapa.Children.Last().SetValue(Canvas.LeftProperty, X - 25);
            //Mapa.Children.Last().SetValue(Canvas.TopProperty, Y - 25);
        }
    }
}
