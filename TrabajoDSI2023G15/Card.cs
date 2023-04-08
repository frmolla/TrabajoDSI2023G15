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
                Nombre = "Modelo",
                Imagen = "Assets\\play_background.png",
                Text = "Texto de ejemplo",
                Mana = 3,
                Vida = 3,
                Ataque = 3,
                Rareza = 3
            }

        };
    } 
}
