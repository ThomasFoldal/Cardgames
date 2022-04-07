using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckOfCards;

namespace Blackjack
{
    internal class Dealer:Entity
    {
        public int PointsStart()
        {
            switch (hand[1].face)
            {
                case 1:
                    return 11;
                case 11:
                    return 10;
                case 12:
                    return 10;
                case 13:
                    return 10;
                default:
                    return hand[1].face;
            }
        }
        public string CardStart()
        {
            return hand[1].GetCard();
        }
    }
}
