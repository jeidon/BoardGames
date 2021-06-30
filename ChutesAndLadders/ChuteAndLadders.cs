using System;

namespace ChutesAndLadders
{
    class ChutesAndLadders
    {
        public static bool gameOver;
        public static Board_T Board = new Board_T();
        public static Players_T Players = new Players_T();
        public static Die_T Die = new Die_T();

        static void Main(string[] args)
        {
            Console.WriteLine("Let's get started");
            while (!gameOver)
            {
                gameOver = GameLoop();
            }
        }

        private static bool GameLoop()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("It is {0}'s turn, Press Enter to Draw", Players.currentPlayer.playerName);
            Console.ReadLine();

            int roll = Die.Roll(1, 6);
            Console.WriteLine("{0} rolled a {1}", Players.currentPlayer.playerName, roll);

            Board.Move(Players.currentPlayer, roll);
            if(Players.countActivePlayers() > 1)
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
