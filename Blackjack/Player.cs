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
        public bool canDouble { get; set; } = true;
        public bool bust { get; set; } = false;
        public char TakeTurn()
        {
            char awn;
            Console.WriteLine("(H)it");
            if (canDouble)
            {
                Console.WriteLine("(D)ouble Down");
            }
            if (hand.Count == 2 && hand[0].face == hand[1].face)
            {
                Console.WriteLine("S(p)lit");
            }
            Console.WriteLine("(S)tand");

            awn = Console.ReadKey().KeyChar;
            return awn;
        }
    }
}
