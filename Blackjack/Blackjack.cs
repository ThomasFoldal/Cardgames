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
                    Console.WriteLine("Invalid Input");
                    throw;
                }
                if (playerNum <= 5)
                {
                    AddPlayer(playerNum);           //takes desired number of players and adds that many players to list of players
                    break;
                }
                Console.WriteLine("Max number of player is 5");
            }
            Console.Clear();
            for (int i = 0; i < players.Count; i++)
            {
                string name = null;
                while (name == null)
                {
                    Console.Write("Enter player {0}'s name: ", (i + 1));        //make everyone enter their name
                    try
                    {
                        name = Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Input");
                    }
                }
                players[i].name = name;
            }
            while (true)
            {
                foreach (Player player in players)
                {
                    while (true)
                    {
                        Console.Clear();
                        player.ResteHand();
                        Console.WriteLine(player.name);
                        Console.WriteLine("Select betting amount. (from 5 to 100)");
                        try
                        {
                            player.bet = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid Input");
                        }
                    }
                }
                DealerDraw(2, dealer);
                NewHands();
                foreach (Player player in players)
                {
                    while (player.playing)          //plays each players hand
                    {
                        DisplayBoard(player, dealer);

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
                                DoubleDown(player, dealer);
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
                if (dealer.GetPoints() < 17)
                {
                    while (dealer.GetPoints() < 17)
                    {
                        dealer.hand.Add(deck.DrawCard('z'));
                        Console.Clear();
                        Console.WriteLine("Dealer");
                        DisplayHand(dealer);
                        Console.WriteLine();

                        foreach (Player player in players)
                        {
                            Console.WriteLine(player.name);
                            if (player.bust)
                            {
                                Console.WriteLine("Busted");
                            }
                            else
                            {
                                DisplayHand(player);
                            }
                            Console.WriteLine();
                        }
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Dealer");
                    DisplayHand(dealer);
                    Console.WriteLine();

                    foreach (Player player in players)
                    {
                        Console.WriteLine(player.name);
                        if (player.bust)
                        {
                            Console.WriteLine("Busted");
                        }
                        else
                        {
                            DisplayHand(player);
                        }
                        Console.WriteLine();
                    }
                    Thread.Sleep(2000);
                }

                Thread.Sleep(4000);
                if (dealer.GetPoints() > 21)
                {
                    foreach (Player player in players)
                    {
                        if (!player.bust)
                        {
                            player.wallet += player.bet;
                            Console.WriteLine(player.name);
                            Console.WriteLine("Won " + player.bet);
                            Console.WriteLine("Wallet: " + player.wallet);
                        }
                        else
                        {
                            player.wallet -= player.bet;
                            Console.WriteLine(player.name);
                            Console.WriteLine("Lost " + player.bet);
                            Console.WriteLine("Wallet: " + player.wallet);
                        }
                    }
                }
                else
                {
                    foreach (Player player in players)
                    {
                        if (!player.bust)
                        {
                            if (player.GetPoints() > dealer.GetPoints())
                            {
                                player.wallet += player.bet;
                                Console.WriteLine(player.name);
                                Console.WriteLine("Won " + player.bet);
                                Console.WriteLine("Wallet: " + player.wallet);
                            }
                            else
                            {
                                player.wallet -= player.bet;
                                Console.WriteLine(player.name);
                                Console.WriteLine("Lost " + player.bet);
                                Console.WriteLine("Wallet: " + player.wallet);
                            }
                        }
                        else
                        {
                            player.wallet -= player.bet;
                            Console.WriteLine(player.name);
                            Console.WriteLine("Lost " + player.bet);
                            Console.WriteLine("Wallet: " + player.wallet);
                        }
                    }
                }
                Console.ReadKey();
            }
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
        public void DoubleDown(Player p, Dealer d)
        {
            p.hand.Add(deck.DrawCard());
            p.playing = false;
            DisplayBoard(p,d);
            Thread.Sleep(3000);
        }
        public void Stand(Player p)
        {
            p.playing = false;
        }
        public void SplitHand(Player p)
        {

        }
        public void DisplayBoard(Player player, Dealer dealer)
        {
            Console.Clear();
            Console.WriteLine(player.name + "'s turn");
            Console.WriteLine();
            Console.WriteLine("Dealer");
            Console.WriteLine(dealer.CardStart());
            Console.WriteLine(dealer.PointsStart());
            Console.WriteLine();

            DisplayHand(player);
        }
        public void DealerDraw(int j, Dealer house)
        {
            for (int i = 0; i < j; i++)
            {
                house.hand.Add(deck.DrawCard('•'));
            }
        }
        public void DisplayHand(Entity entity)
        {
            foreach (Card card in entity.hand)
            {
                Console.Write(card.GetCard() + " ");
            }
            Console.WriteLine();
            Console.WriteLine(entity.GetPoints());
        }
    }
}
