using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            }
                Console.Clear();
            for (int i = 0; i < players.Count; i++)
            {
                string name = null;
                while (name == null)
                {
                    Console.Write("Enter player {0}'s name: ", (i + 1));
                    try
                    {
                        name = Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        throw;
                    }
                }
                players[i].name = name;
            }
            NewHands();
            foreach (Player player in players)
            {
                while (player.playing)          //plays each players hand
                {
                    Console.Clear();
                    Console.WriteLine(player.name + "'s turn");
                    foreach (Card card in player.hand)
                    {
                        Console.Write(card.GetCard() + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine(player.GetPoints());

                    if (player.GetPoints() == 21)
                    {
                        if (player.hand.Count == 2)
                        {
                            Console.WriteLine("Blackjack");
                        }
                        Thread.Sleep(3000);
                        break;
                    }
                    if (player.GetPoints() > 21)            //if they go bust, gives a 3 second delay to see their points before continueing to the next player
                    {
                        player.bust = true;
                        Thread.Sleep(3000);
                        break;
                    }

                    char awn = player.TakeTurn();           //the player selects what they want to do
                    switch (awn)
                    {
                        case 'h':
                            Hit(player);
                            break;
                        case 'd':
                            DoubleDown(player);
                            break;
                        case 'p':
                            Console.WriteLine("this function is not avalable at this time");
                            Console.ReadLine();
                            //SplitHand(player);
                            break;
                        case 's':
                            Stand(player);
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.ReadKey();
        }
        public void AddPlayer(int i)
        {
            for (int j = 0; j < i; j++)
            {
                players.Add(new Player());
            }
        }
        public void AddPlayer(Card c, Player p)
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
