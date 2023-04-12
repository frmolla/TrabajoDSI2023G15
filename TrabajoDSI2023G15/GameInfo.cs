using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoDSI2023G15
{
    public class GameInfo
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Explicacion { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int RX { get; set; }
        public int RY { get; set; }



        public GameInfo() { }
    }

    public class ModelGameInfo
    {
        public static List<GameInfo> History = new List<GameInfo>()
        {
            new GameInfo()
            {
                Id = 0,
                Nombre = "Game 1",
                Explicacion = @" 05/03/2023  |  Rival 1  |  Deck 7  |  Win!  ",
                X = 10,
                Y = 10,
                RX =100,
                RY =30,
             },
            new GameInfo()
            {
                Id = 1,
                Nombre = "Game 2",
                Explicacion = @" 03/03/2023  |  Rival 8  |  Deck 6  |  Lose!  ",
                X = 50,
                Y = 50,
                RX =150,
                RY =50,
             },
            new GameInfo()
            {
                Id = 2,
                Nombre = "Game 3",
                Explicacion = @" 03/03/2023  |  Rival 30  |  Deck 1  |  Win!  ",
                X = 100,
                Y = 100,
                RX =50,
                RY =130,
             },
            new GameInfo()
            {
                Id = 3,
                Nombre = "Game 4",
                Explicacion = @" 02/03/2023  |  Rival 20  |  Deck 2  |  Lose!  ",
                X = 150,
                Y = 150,
                RX =200,
                RY =80,
             },
            new GameInfo()
            {
                Id = 4,
                Nombre = "Game 5",
                Explicacion = @" 02/03/2023  |  Rival 19  |  Deck 4  |  Win!  ",
                X = 200,
                Y = 200,
                RX =100,
                RY =140,
             },
            new GameInfo()
            {
                Id = 5,
                Nombre = "Game 6",
                Explicacion = @" 01/03/2023  |  Rival 8  |  Deck 4  |  Win!  ",
                X = 250,
                Y = 250,
                RX =30,
                RY =50,
             }         
          };


        public static IList<GameInfo> GetAllGames()
        {
            return History;
        }

        public static GameInfo GetGameById(int id)
        {
            return History[id];
        }
    }
}
