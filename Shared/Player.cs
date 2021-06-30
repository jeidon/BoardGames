using System;
using System.Collections.Generic;
using System.Text;

public class Player
{
    public bool active;
    public string Name;
    public int CurrentPosition;

    public Player(string PlayerName)
    {
        this.Name = PlayerName;
        this.active = true;
        this.CurrentPosition = 0;
    }
}
public class Players_T
{
    static int ActivePlayerIndex = 0;
    public int ActivePlayerCount = 0;
    public Player CurrentPlayer;
    public List<Player> PlayerList = new List<Player>();
    public void GetPlayers()
    {
        int numPlayers = 0;
        do
        {
            Console.WriteLine("How many players? (Min 2)");
            string sPlayers = Console.ReadLine();
            if (int.TryParse(sPlayers, out numPlayers))
            {
                if (numPlayers < 2)
                {
                    Console.WriteLine("Please ener a number greater than 1");
                }
            }
        } while (numPlayers < 2);

        for (int i = 0; i < numPlayers; i++)
        {
            string playerName;
            Console.WriteLine("What is Player {0}'s name?", i + 1);
            playerName = Console.ReadLine();
            AddPlayer(new Player(playerName));
        }
    }

    public int CountActivePlayers()
    {
        int activePlayers = 0;
        for(int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i].active)
            {
                activePlayers++;
            }
        }
        return activePlayers;
    }

    public void AddPlayer(Player newPlayer)
    {
        PlayerList.Add(newPlayer);
        if (CurrentPlayer is null)
        {
            CurrentPlayer = PlayerList[0];
        }
        ActivePlayerCount++;
    }
    public void GetNextPlayer()
    {
        //Increment to the next player
        while (true)
        {
            ActivePlayerIndex++;

            //Does this player exist?
            if (ActivePlayerIndex >= PlayerList.Count)
                ActivePlayerIndex = 0;

            if (PlayerList[ActivePlayerIndex].active)
                break;
        }
        //Is this player still active
        CurrentPlayer = PlayerList[ActivePlayerIndex];
    }
}
