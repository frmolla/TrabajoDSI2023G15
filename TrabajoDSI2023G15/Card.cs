using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDSI2023G15
{
    public class Carta
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string ManaImagen { get; set; }
        public string AtaqueImagen { get; set; }
        public string VidaImagen { get; set; }
        public string RarezaImagen { get; set; }
        public string Text { get; set; }
        public int Mana { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }
        public int Rareza { get; set; }

        public Carta() { }
    }

    public class Model
    {
        public static List<Carta> Cartas = new List<Carta>()
        {
            new Carta()
            {
                Nombre = "Modelo 1",
                Imagen = "Assets\\Carta.png",
                ManaImagen = "Assets\\Mana.png",
                AtaqueImagen = "Assets\\Ataque.png",
                VidaImagen = "Assets\\Vida.png",
                RarezaImagen = "Assets\\Rareza_Comun.png",
                Text = "Texto de ejemplo",
                Mana = 3,
                Vida = 3,
                Ataque = 3,
                Rareza = 3
            },
            new Carta()
            {
                Nombre = "Modelo 2",
                Imagen = "Assets\\Carta.png",
                ManaImagen = "Assets\\Mana.png",
                AtaqueImagen = "Assets\\Ataque.png",
                VidaImagen = "Assets\\Vida.png",
                RarezaImagen = "Assets\\Rareza_Comun.png",
                Text = "Texto de ejemplo",
                Mana = 3,
                Vida = 3,
                Ataque = 3,
                Rareza = 3
            },
            new Carta()
            {
                Nombre = "Modelo 3",
                Imagen = "Assets\\Carta.png",
                ManaImagen = "Assets\\Mana.png",
                AtaqueImagen = "Assets\\Ataque.png",            
                VidaImagen = "Assets\\Vida.png",
                RarezaImagen = "Assets\\Rareza_Comun.png",
                Text = "Texto de ejemplo",
                Mana = 3,
                Vida = 3,
                Ataque = 3,
                Rareza = 3
            },
            new Carta()
            {
                Nombre = "Modelo 4",
                Imagen = "Assets\\Carta.png",
                ManaImagen = "Assets\\Mana.png",
                AtaqueImagen = "Assets\\Ataque.png",
                VidaImagen = "Assets\\Vida.png",
                RarezaImagen = "Assets\\Rareza_Comun.png",
                Text = "Texto de ejemplo",
                Mana = 3,
                Vida = 3,
                Ataque = 3,
                Rareza = 3
            },
            new Carta()
            {
                Nombre = "Modelo 5",
                Imagen = "Assets\\Carta.png",
                ManaImagen = "Assets\\Mana.png",
                AtaqueImagen = "Assets\\Ataque.png",
                VidaImagen = "Assets\\Vida.png",
                RarezaImagen = "Assets\\Rareza_Comun.png",
                Text = "Texto de ejemplo",
                Mana = 3,
                Vida = 3,
                Ataque = 3,
                Rareza = 3
            }

        };

        public static IList<Carta> GetAllCards()
        {
            return Cartas;
        }
    } 
}
