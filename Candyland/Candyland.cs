using System;
using System.Collections.Generic;

namespace Candyland
{
    class Candyland
    {
        public static bool gameOver;
        public static List<TileColor> Colors = new List<TileColor>();

        public static Board_T Board = new Board_T();
        public static CardDeck_T CardDeck = new CardDeck_T();
        public static Players_T Players = new Players_T();
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Candy Land");
            
            //Normal Tiles
            Colors.Add(new TileColor(0, "Red"));
            Colors.Add(new TileColor(1, "Purple"));
            Colors.Add(new TileColor(2, "Yellow"));
            Colors.Add(new TileColor(3, "Blue"));
            Colors.Add(new TileColor(4, "Orange"));
            Colors.Add(new TileColor(5, "Green"));

            //Special Tiles
            Colors.Add(new TileColor(6, "Pink"));
            Board.Generate(1984, Colors);
            CardDeck.Generate();

            while (!gameOver)
            {
                gameOver = GameLoop();
            }
            ShowWinScreen();
        }

        private static void ShowWinScreen()
        {
            Console.WriteLine("{0} is the Winner", Players.currentPlayer.playerName);
        }

        private static bool GameLoop()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("It is {0}'s turn, Press Enter to Draw", Players.currentPlayer.playerName);
            Console.ReadLine();
            Card_T newCard = CardDeck.Draw();
            if (newCard.isDoubles)
            {
                Console.WriteLine("{0} drew a double {1}!", Players.currentPlayer.playerName, newCard.color.colorName);
            }
            else
            {
                Console.WriteLine("{0} drew a {1}", Players.currentPlayer.playerName, newCard.color.colorName);
            }
            Board.Move(Players.currentPlayer, newCard);
            if (Players.countActivePlayers() > 1)
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
