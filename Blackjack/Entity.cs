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
        protected List<Card> hand { get; set; } = new List<Card>();
        public void ResetHand()
        {
            hand.Clear();
        }
        public List<Card> GetHand()
        {
            return hand;
        }
        public void AddToHand(Card card)
        {
            hand.Add(card);
        }
    }
}
