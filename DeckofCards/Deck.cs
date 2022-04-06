using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    public class Deck
    {
        public List<Card> deck { get; set; } = new List<Card>();
        char[] suites = { '♥', '♦', '♣', '♠' };

        public void FillDeck(int num)
        {
            deck = new List<Card>();
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < suites.Length; j++)
                {
                    for (int k = 1; k < 14; k++)
                    {
                        deck.Add(new Card(k, suites[j]));
                        
                    }
                }
            }
        }
        public void FillDeck(int num, int Jokers)
        {
            for (int j = 0; j < suites.Length; j++)
            {
                for (int k = 0; k < 13; k++)
                {
                    deck.Add(new Card(k, suites[j]));
                }
            }
            for (int i = 0; i < Jokers; i++)
            {
                if (i%2==0)
                {
                deck.Add(new Card(0,'R'));
                }
                else
                {
                deck.Add(new Card(0, 'B'));
                }
            }
        }
        public Card DrawCard()
        {
            Random rng = new Random();

            int x = rng.Next(deck.Count);
            Card card = deck[x];
            deck.RemoveAt(x);

            return card;
        }
        public Card[] DrawCard(int draw)
        {
            Card[] hand = new Card[draw];
            Random rng = new Random();
            for (int i = 0; i < draw; i++)
            {
                Card card = deck[rng.Next(deck.Count)];
                hand[i] = card;
            }
            return hand;
        }
    }
}
