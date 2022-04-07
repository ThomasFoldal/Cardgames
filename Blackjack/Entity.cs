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
        public int GetPoints()
        {
            int points = 0;
            bool soft = false;
            foreach (Card c in hand)
            {
                switch (c.face)
                {
                    case 11:
                        points += 10;
                        break;
                    case 12:
                        points += 10;
                        break;
                    case 13:
                        points += 10;
                        break;
                    case 1:
                        points += 11;
                        soft = true;
                        break;
                    default:
                        points += c.face;
                        break;
                }
            }
            while (soft && points > 21)
            {
                points = 0;
                foreach (Card c in hand)
                {
                    switch (c.face)
                    {
                        case 11:
                            points += 10;
                            break;
                        case 12:
                            points += 10;
                            break;
                        case 13:
                            points += 10;
                            break;
                        case 1:
                            if (soft)
                            {
                                points += 1;
                                c.face = 14;
                            }
                            else
                            {
                                points += 11;
                                soft = true;
                            }
                            break;
                        case 14:
                            points += 1;
                            break;
                        default:
                            points += c.face;
                            break;
                    }
                }
            }

            return points;
        }
        public void ResteHand()
        {
            hand.Clear();
        }
    }
}
