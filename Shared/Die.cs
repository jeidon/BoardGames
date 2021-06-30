using System;
using System.Collections.Generic;
using System.Text;

public class Die_T
{
    public int Roll(int numberOfDie, int dieType)
    {
        //Generate a random number to pick a color for this card
        Random rnd = new Random();

        int outcome = 0;
        for (int die = 0; die < numberOfDie; die++)
        {
            outcome += rnd.Next(1, dieType); // creates a number between 1 and 6
        }
        return outcome;
    }
}
