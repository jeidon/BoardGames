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
        public string Name;
        public bool sticky = false;

        public BoardTile(TileColor Color, string Name = "")
        {
            this.color = Color;
            this.Name = Name;
        }
    }

    public class Board_T
    {
        public List<BoardTile> Tiles = new List<BoardTile>();

        public void Generate(int creationYear, List<TileColor> Colors)
        {
            //Add a starting tile at position 0 before the first one
            Tiles.Add(new BoardTile(Colors[6]));    //Start tile

            //
            for (int i = 0; i < 21; i++)
            {
                Tiles.Add(new BoardTile(Colors[0])); //, false));
                Tiles.Add(new BoardTile(Colors[1])); //, false));
                Tiles.Add(new BoardTile(Colors[2])); //, false));
                Tiles.Add(new BoardTile(Colors[3])); //, false));
                Tiles.Add(new BoardTile(Colors[4])); //, false));
                Tiles.Add(new BoardTile(Colors[5])); //, false));
            }

            //TODO Change some squares to be the special ones (Gramma Nutt, Mudslides, whatever)
            // e.g. Squares[15].special = true;
            if (creationYear == 1984)
            {
                //Add the two remaining tiles
                Tiles.Add(new BoardTile(Colors[0])); //, false));
                Tiles.Add(new BoardTile(Colors[1])); //, false));

                //Add the shortcuts, "sticky" spots, and special spots (i.e. Mr. Mint, Plumpy)
                Tiles[5].targetTile = 59; Tiles[5].Name = "Rainbow Trail";
                Tiles.Insert(9, new BoardTile(Colors[6], "Mr Plumpy"));
                Tiles.Insert(18, new BoardTile(Colors[6], "Mr. Mint"));
                Tiles[34].targetTile = 47; Tiles[34].Name = "Gumdrop Pass";
                Tiles.Insert(43, new BoardTile(Colors[6], "Jolly"));
                Tiles[48].sticky = true; Tiles[48].Name = "Gooey Gumdrops";
                Tiles.Insert(75, new BoardTile(Colors[6], "Gramma Nut"));
                Tiles[86].sticky = true; Tiles[86].Name = "Lollipop Woods";
                Tiles.Insert(96, new BoardTile(Colors[6], "Princess Lolly"));
                Tiles.Insert(104, new BoardTile(Colors[6], "Queen Frostine"));
                Tiles[121].sticky = true; Tiles[121].Name = "Molasses Swamp";
                Tiles[134].Name = "King Kandy";
            }
            else if (creationYear == 2010)
            {
                //Add the shortcuts, "sticky" spots, and special spots (i.e. Mr. Mint, Plumpy)
                Tiles[4].targetTile = 48; Tiles[4].Name = "Peppermint Pass";
                Tiles.Insert(9, new BoardTile(Colors[6], "Cupcake Commons"));
                Tiles.Insert(20, new BoardTile(Colors[6], "Ice Cream Slopes"));
                Tiles[29].targetTile = 41; Tiles[29].Name = "Gummy Pass";
                Tiles.Insert(30, new BoardTile(Colors[6], "Gummy Hills"));
                Tiles[33].sticky = true; Tiles[33].Name = "Licorice";
                Tiles.Insert(57, new BoardTile(Colors[6], "Gingerbread House"));
                Tiles[64].sticky = true; Tiles[64].Name = "Licorice";
                Tiles.Insert(80, new BoardTile(Colors[6], "Lollipop Woods"));
                Tiles.Insert(90, new BoardTile(Colors[6], "Ice Palace"));
                Tiles.Insert(105, new BoardTile(Colors[6], "Chocolate Mountain"));
                Tiles[121].Name = "King Kandy";
            }
        }

        public void Move(Player thisPlayer, Card_T newCard, int creationYear)
        {
            //This is a temp variable to "look ahead" and see if we are going to overshoot
            int newPosition = thisPlayer.CurrentPosition;

            //First handle our current situation
            //--------------------------------------------------
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

            //--------------------------------------------------
            // Next step forward until the tile matches the newCard
            try
            {
                do
                {
                    newPosition++;
                } while (Tiles[newPosition].color.colorIndex != newCard.color.colorIndex);

                //If the card is a double, do it again
                if (newCard.isDoubles)
                {
                    do
                    {
                        newPosition++;
                    } while (Tiles[newPosition].color.colorIndex != newCard.color.colorIndex);
                }

                //We aren't past the end, move the token here
                thisPlayer.CurrentPosition = newPosition;

                //Does this tile have a new target?
                if (Tiles[newPosition].targetTile != 0)
                {
                    Console.WriteLine("... and landed on {0} which moved them to tile {1} of {2}", Tiles[newPosition].Name, Tiles[newPosition].targetTile, Tiles.Count - 1);
                    //Move us to the target
                    thisPlayer.CurrentPosition = Tiles[newPosition].targetTile;
                }
                else
                {
                    //Just a normal tile
                    Console.WriteLine("and moved to tile {0} of {1}", thisPlayer.CurrentPosition, Tiles.Count - 1);
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                //Check to see if this tile is past the end of the game board
                if (creationYear == 1984 && newPosition >= Tiles.Count)
                {
                    Console.WriteLine("... but overshot the board. You are still on {0}", Tiles[thisPlayer.CurrentPosition].color.colorName);
                    return;
                }
            }

            // After all that, are we at the end?
            if (creationYear == 1984 && Tiles[thisPlayer.CurrentPosition].Name == "King Kandy")
            {
                Console.WriteLine("You have reached King Kandy. Congratulations, you go out");
                thisPlayer.active = false;
                return;
            }
            else if (creationYear == 2010 && newPosition >= Tiles.Count)
            {
                Console.WriteLine("You have reached King Kandy. Congratulations, you go out");
                thisPlayer.active = false;
                thisPlayer.CurrentPosition = newPosition;
                return;
            }
            //Ok, not the end, is this tile sticky?
            else if (Tiles[thisPlayer.CurrentPosition].sticky)
            {
                Console.WriteLine("On no! They are stuck in the tar until they draw a {0}", Tiles[thisPlayer.CurrentPosition].color.colorName);
            }

            return;
        }
    }
}
