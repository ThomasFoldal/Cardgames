using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckOfCards;

namespace Blackjack
{
    internal class Player : Entity
    {
        public Player()
        {

        }
        public Player(Card c)
        {
            hand.Add(c);
        }
        public bool playing { get; set; } = true;
        bool canDouble = true;
        //public void Hit()
        //{
        //    hand.Add(deck.DrawCard());
        //    canDouble = false;
        //}
        //public void DoubleDown()
        //{
        //    hand.Add(deck.DrawCard());
        //    playing = false;
        //}
        //public void Stand()
        //{
        //    playing = false;
        //}
        //public void NewHand()
        //{
        //    playing = true;
        //    canDouble = true;
        //    hand.Add(deck.DrawCard(2));
        //}
        //public void SplitHand()
        //{

        //}
        public void TakeTurn()
        {
            Console.WriteLine();
        }
    }
}
