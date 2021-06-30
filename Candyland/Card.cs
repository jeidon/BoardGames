using System;
using System.Collections.Generic;
using System.Text;

namespace Candyland
{
    public class CardDeck_T
    {
        private int currentCard;
        public List<Card_T> Deck = new List<Card_T>();
        public void Generate()
        {
            //Generate a random number to pick a color for this card
            Random rnd = new Random();

            //Add the normal cards that include the doubles
            for (int i = 0; i < 128; i++)
            {
                TileColor newColor;
                bool isSpecial = false;
                bool isDoubles = false;
                newColor = Candyland.Colors[rnd.Next(0, 5)]; // creates a number between 1 and 6

                //Should this be a special card? 1 in 128 chance
                if (rnd.Next(0, 127) == 0)
                {
                    isSpecial = true;
                }
                else
                {
                    //Should this be doubles? 1 in 24 chance
                    if (rnd.Next(0, 23) == 0)
                    {
                        isDoubles = true;
                    }
                }

                //Add this card to the list
                Deck.Add(new Card_T(newColor, isDoubles, isSpecial));
            }
            //Adding the target cards
            Deck.Add(new Card_T(Candyland.Colors[6], false, true, 8)); // Plumpy
            Deck.Add(new Card_T(Candyland.Colors[6], false, true, 17)); // Mr. Mint

            Console.WriteLine("Card Deck Generated!");
        }
        public Card_T Draw()
        {
            currentCard++;
            if (currentCard >= Deck.Count)
            {
                currentCard = 0;
            }
            Card_T newCard = Deck[currentCard];
            return newCard;
        }
    }
    public class Card_T
    {
        public TileColor color;
        public bool isDoubles;
        public bool special;
        public int targetTile;
        public Card_T(TileColor Color, bool isDoubles = false, bool Special = false, int TargetTile = 0)
        {
            this.color = Color;
            this.isDoubles = isDoubles;
            this.special = Special;
            this.targetTile = TargetTile;
        }
    }
}
