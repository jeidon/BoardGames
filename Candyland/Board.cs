using System;
using System.Collections.Generic;
using System.Text;

namespace Candyland
{
    public class TileColor
    {
        public int colorIndex;
        public string colorName;

        public TileColor(int ColorIndex, string ColorName)
        {
            colorIndex = ColorIndex;
            colorName = ColorName;
        }
    }
    public class BoardTile
    {
        public TileColor color;
        //public bool special = false;
        public int targetTile = 0;
        public string targetName;
        public bool sticky = false;

        public BoardTile(TileColor Color)//, bool Special)
        {
            this.color = Color;
            //this.special = Special;
        }
    }

    public class Board_T
    {
        public List<BoardTile> Tiles = new List<BoardTile>();

        public void Generate(int creationYear, List<TileColor> Colors)
        {
            for (int i = 0; i < 21; i++)
            {
                Tiles.Add(new BoardTile(Colors[0])); //, false));
                Tiles.Add(new BoardTile(Colors[1])); //, false));
                Tiles.Add(new BoardTile(Colors[2])); //, false));
                Tiles.Add(new BoardTile(Colors[3])); //, false));
                Tiles.Add(new BoardTile(Colors[4])); //, false));
                Tiles.Add(new BoardTile(Colors[5])); //, false));
            }
            Tiles.Add(new BoardTile(Colors[0])); //, false));
            Tiles.Add(new BoardTile(Colors[1])); //, false));

            //TODO Change some squares to be the special ones (Grammy Nuts, Mudslides, whatever)
            // e.g. Squares[15].special = true;
            if (creationYear == 1984)
            {
                //Add the shortcuts
                Tiles[4].targetTile = 58; Tiles[4].targetName = "Rainbow Trail";
                Tiles[33].targetTile = 46; Tiles[33].targetName = "Gumdrop Pass";
                //Add the "sticky" spots
                Tiles[47].sticky = true;
                Tiles[85].sticky = true;
                Tiles[120].sticky = true;
                //Add the special spots (i.e. Mr. Mint, Plumpy)
                Tiles.Insert(8, new BoardTile(Colors[6])); //, true));    //Plumpy
                Tiles.Insert(17, new BoardTile(Colors[6])); //, true));   //Mr Mint
                Tiles.Insert(42, new BoardTile(Colors[6])); //, true));   //Jolly
                Tiles.Insert(74, new BoardTile(Colors[6])); //, true));   //Gramma Nut
                Tiles.Insert(95, new BoardTile(Colors[6])); //, true));   //Princess Lolly
                Tiles.Insert(103, new BoardTile(Colors[6])); //, true));  //Queen Frostine
            }
            else if (creationYear == 2010)
            {
                //Add some chutes to get further, faster
                /*Tiles[3].special = true; */
                Tiles[3].targetTile = 59;
                /*Tiles[28].special = true; */
                Tiles[28].targetTile = 40;
                //TODO Add some "sticky" spots and create a handler for being stuck
                Tiles[50].sticky = true;
                Tiles[81].sticky = true;
            }
        }

        public void Move(Player thisPlayer, Card_T newCard)
        {
            //This is a temp variable to "look ahead" and see if we are going to overshoot
            int newPosition = thisPlayer.currentPosition;

            //Is this character stuck in the tar?
            if (Tiles[newPosition].sticky)
            {
                //Does the new card match the tile so we can get unstuck?
                if (Tiles[newPosition].color.colorIndex == newCard.color.colorIndex)
                {
                    Console.WriteLine("Sweet, time to get going");
                }
                //Nope, that's too bad
                else
                {
                    Console.WriteLine("Still Stuck, you need a {0}", Tiles[newPosition].color.colorName);
                    return;
                }
            }


            //Take a step forward and check the color
            newPosition++;
            if (newPosition >= Tiles.Count) { return; }

            //Keep stepping forward until they match
            while (Tiles[newPosition].color.colorIndex != newCard.color.colorIndex)
            {
                newPosition++;
                if (newPosition >= Tiles.Count) { return; }
            }

            //If the card is a double, do it again
            if (newCard.isDoubles)
            {
                newPosition++;
                if (newPosition >= Tiles.Count) { return; }
                while (Tiles[newPosition].color.colorIndex != newCard.color.colorIndex)
                {
                    newPosition++;
                    if (newPosition >= Tiles.Count) { return; }
                }
            }

            //Check to see if this tile is past the end of the game board
            if (newPosition >= Tiles.Count)
            {
                Console.WriteLine("Overshoot");
                return;
            }

            //We aren't past the end, move the token here
            thisPlayer.currentPosition = newPosition;

            //Does this tile have a new target?
            if (Tiles[thisPlayer.currentPosition].targetTile != 0)
            {
                Console.WriteLine("and landed on {0} which moved you to tile {1} of {2}", Tiles[thisPlayer.currentPosition].targetName, Tiles[thisPlayer.currentPosition].targetTile, Tiles.Count);
                //Move us to the target
                thisPlayer.currentPosition = Tiles[thisPlayer.currentPosition].targetTile;
            }
            else
            {
                //Just a normal tile
                Console.WriteLine("and moved to tile {0} of {1}", thisPlayer.currentPosition, Tiles.Count);
            }

            // After all that, are we at the end?
            if (thisPlayer.currentPosition == Tiles.Count)
            {
                thisPlayer.active = false;
                return;
            }
            //Ok, not the end, is this tile sticky?
            else if (Tiles[thisPlayer.currentPosition].sticky)
            {
                Console.WriteLine("On no! You are stuck in the tar");
            }

            return;
        }
    }
}
