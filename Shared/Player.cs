using System;
using System.Collections.Generic;
using System.Text;

public class Player
{
    public bool active;
    public string playerName;
    public int currentPosition;

    public Player(string playerName)
    {
        this.playerName = playerName;
        this.active = true;
        this.currentPosition = 0;
    }
}
public class Players_T
{
    static int activePlayerIndex = 0;
    public int activePlayerCount = 0;
    public Player currentPlayer;
    public List<Player> PlayerList = new List<Player>();
    public Players_T()
    {
        int numPlayers = 0;
        while (numPlayers < 2)
        {
            Console.WriteLine("How many players? (Min 2)");
            string sPlayers = Console.ReadLine();
            numPlayers = Convert.ToInt32(sPlayers);
        }

        for (int i = 0; i < numPlayers; i++)
        {
            string playerName;
            Console.WriteLine("What is Player {0}'s name?", i + 1);
            playerName = Console.ReadLine();
            AddPlayer(new Player(playerName));
        }
    }

    public int countActivePlayers()
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
        if (currentPlayer is null)
        {
            currentPlayer = PlayerList[0];
        }
        activePlayerCount++;
    }
    public void GetNextPlayer()
    {
        //Increment to the next player
        while (true)
        {
            activePlayerIndex++;

            //Does this player exist?
            if (activePlayerIndex >= PlayerList.Count)
                activePlayerIndex = 0;

            if (PlayerList[activePlayerIndex].active)
                break;
        }
        //Is this player still active
        currentPlayer = PlayerList[activePlayerIndex];
    }
}
