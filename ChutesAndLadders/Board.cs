using System;
using System.Collections.Generic;
using System.Text;

namespace ChutesAndLadders
{
    //public class TileColor
    //{
    //    public int colorIndex;
    //    public string colorName;

    //    public TileColor(int ColorIndex, string ColorName)
    //    {
    //        colorIndex = ColorIndex;
    //        colorName = ColorName;
    //    }

    //}
    public class BoardTile
    {
        //public TileColor color;
        //public bool special = false;
        public int targetTile = 0;
        //public string targetName;
        //public bool sticky = false;

        public BoardTile(/*TileColor Color*/)
        {
            //this.color = Color;
            //this.special = Special;
        }
    }

    public class Board_T
    {
        public List<BoardTile> Tiles = new List<BoardTile>();

        public Board_T()
        {
            //Add all the tiles
            for (int i = 0; i < 100; i++)
            {
                Tiles.Add(new BoardTile());
            }

            //Add the Ladders
            Tiles[1].targetTile = 38;
            Tiles[4].targetTile = 14;
            Tiles[9].targetTile = 31;
            Tiles[21].targetTile = 42;
            Tiles[28].targetTile = 84;
            Tiles[36].targetTile = 44;
            Tiles[51].targetTile = 67;
            Tiles[71].targetTile = 91;
            Tiles[80].targetTile = 100;

            //Add the chutes
            Tiles[16].targetTile = 6;
            Tiles[48].targetTile = 26;
            Tiles[49].targetTile = 11;
            Tiles[56].targetTile = 53;
            Tiles[62].targetTile = 19;
            Tiles[64].targetTile = 60;
            Tiles[87].targetTile = 24;
            Tiles[93].targetTile = 73;
            Tiles[95].targetTile = 75;
            Tiles[98].targetTile = 78;
        }

        public void Move(Player thisPlayer, int roll)
        {
            int newPosition = thisPlayer.CurrentPosition;
            newPosition += roll;

            //Does it overshoot
            if (newPosition > Tiles.Count)
            {
                Console.WriteLine("but that roll exceeds the board. Discarding");
                return;
            }

            //Is it a Shoot or ladder
            if (newPosition < 100 && Tiles[newPosition].targetTile != 0)
            {
                //Move us to the target tile
                if (newPosition > Tiles[newPosition].targetTile)
                {
                    Console.WriteLine("and landed on {0} which slid them down to tile {1} of {2}", newPosition, Tiles[newPosition].targetTile, Tiles.Count);
                }
                else
                {
                    Console.WriteLine("and landed on {0} which lifted them to tile {1} of {2}", newPosition, Tiles[newPosition].targetTile, Tiles.Count);
                }
                thisPlayer.CurrentPosition = Tiles[newPosition].targetTile;
            }
            else
            {
                //Just a normal tile
                Console.WriteLine("and moved to tile {0} of {1}", newPosition, Tiles.Count);
                thisPlayer.CurrentPosition = newPosition;
            }

            //Does this roll win?
            if (thisPlayer.CurrentPosition == Tiles.Count)
            {
                Console.WriteLine("{0} makes it to the top and goes out!", thisPlayer.Name);
                thisPlayer.active = false;
            }

        }
    }
}
