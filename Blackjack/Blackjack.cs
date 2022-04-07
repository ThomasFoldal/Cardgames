using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckOfCards;

namespace Blackjack
{
    internal class Blackjack
    {
        Deck deck;
        List<Player> players = new List<Player>();
        Dealer dealer;
        public void StartGame()
        {
            deck = new Deck();
            deck.FillDeck(1);           //making a deck and filling it with cards

            dealer = new Dealer();
            while (true)
            {
                Console.WriteLine("Select number of players");
                int playerNum;
                try
                {
                    playerNum = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    throw;
                }
                if (playerNum <= 5)
                {
                    AddPlayer(playerNum);           //takes desired number of players and adds that many players to list of players
                    break;
                }
                Console.Clear();
            }
            NewHands();
            foreach (Player player in players)
            {
                while (player.playing)          //plays each players hand
                {
                    Console.Clear();
                    foreach (Card card in player.hand)
                    {
                        Console.Write(card.GetCard() + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine(player.GetPoints());
                    
                    char awn = player.TakeTurn();
                    switch (awn)
                    {
                        case 'h':
                            Hit(player);
                            break;
                        case 'd':
                            DoubleDown(player);
                            break;
                        case 'p':
                            SplitHand(player);
                            break;
                        case 's':
                            Stand(player);
                            break;
                        default:
                            break;
                    }

                }
            }
            Console.ReadLine();
        }
        public void AddPlayer(int i)
        {
            for (int j = 0; j < i; j++)
            {
                players.Add(new Player());
            }
        }
        public void AddPlayer(Card c)
        {

        }
        public void NewHands()
        {
            foreach (Player player in players)
            {
                player.hand.Add(deck.DrawCard());
                player.hand.Add(deck.DrawCard());
            }
        }
        public void Hit(Player p)
        {
            p.hand.Add(deck.DrawCard());
            p.canDouble = false;
        }
        public void DoubleDown(Player p)
        {
            p.hand.Add(deck.DrawCard());
            p.playing = false;
        }
        public void Stand(Player p)
        {
            p.playing = false;
        }
        public void SplitHand(Player p)
        {

        }
    }
}
