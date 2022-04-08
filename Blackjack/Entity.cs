using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckOfCards;

namespace Blackjack
{
    internal abstract class Entity
    {
        public List<Card> hand { get; set; } = new List<Card>();
        public void ResetHand()
        {
            hand.Clear();
        }
    }
}
