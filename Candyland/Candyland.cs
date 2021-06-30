using System;
using System.Collections.Generic;

namespace Candyland
{
    class Candyland
    {
        public static bool gameOver;
        public static int creationYear = 0;
        public static List<TileColor> Colors = new List<TileColor>();

        public static Board_T Board = new Board_T();
        public static CardDeck_T CardDeck = new CardDeck_T();
        public static Players_T Players = new Players_T();
        public static void Main(string[] args)
        {
            Console.WriteLine("Thank you for playing Candy Land");

            //Normal Tiles
            Colors.Add(new TileColor(0, "Red"));
            Colors.Add(new TileColor(1, "Purple"));
            Colors.Add(new TileColor(2, "Yellow"));
            Colors.Add(new TileColor(3, "Blue"));
            Colors.Add(new TileColor(4, "Orange"));
            Colors.Add(new TileColor(5, "Green"));

            //Special Tiles
            Colors.Add(new TileColor(6, "Pink"));
            Board.Generate(creationYear, Colors);
            CardDeck.Generate();

            do
            {
                Console.WriteLine("Would you like to play the version from (1) 1984 or (2) 2010?");
                string creationYearStr = Console.ReadLine();
                if (int.TryParse(creationYearStr, out creationYear))
                {
                    if (creationYear == 1)
                    {
                        creationYear = 1984;
                    }
                    else if (creationYear == 2)
                    {
                        creationYear = 2010;
                    }
                    else if (creationYear != 1984 && creationYear != 2010)
                    {
                        Console.WriteLine("Please select from the list");
                        creationYear = 0;
                    }
                }
            } while (creationYear == 0);

            Players.GetPlayers();

            while (!gameOver)
            {
                gameOver = GameLoop();
            }
            ShowWinScreen();
        }

        private static void ShowWinScreen()
        {
            Console.WriteLine("Great Job Everyone! Thanks for playing");
        }

        private static bool GameLoop()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("It is {0}'s turn, Press Enter to Draw", Players.CurrentPlayer.Name);
            Console.ReadLine();
            Card_T newCard = CardDeck.Draw();
            if (newCard.isDoubles)
            {
                Console.WriteLine("{0} drew a double {1}!", Players.CurrentPlayer.Name, newCard.color.colorName);
            }
            else
            {
                Console.WriteLine("{0} drew a {1}", Players.CurrentPlayer.Name, newCard.color.colorName);
            }
            Board.Move(Players.CurrentPlayer, newCard, creationYear);
            if (Players.CountActivePlayers() > 1)
            {
                Players.GetNextPlayer();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
