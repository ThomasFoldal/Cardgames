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
            bool quit = false;
            AddPlayers();
            while (quit == false)
            {
                Console.Clear();
                dealer.ResetHand();
                foreach (Player player in players)
                {
                    PlaceBet(player);
                }
                Console.Clear();
                DealerDraw(2, dealer);
                NewHands();
                foreach (Player player in players)
                {
                    Turn(player);
                }
                DealerTurn(dealer);
                TjekWinner();

                Console.WriteLine("(C)ontinue");
                Console.WriteLine("(Q)uit");
                char turnEnd = Console.ReadKey().KeyChar;
                while (true)
                {
                    if (turnEnd == 'c')
                    {
                        break;
                    }
                    else if (turnEnd == 'q')
                    {
                        quit = true;
                        break;
                    }
                }
                Console.Clear();
            }
        }
        private void AddPlayer(string[] names)
        {
            foreach (string name in names)
            {
                players.Add(new Player(name));
            }
        }
        private void NewHands()
        {
            foreach (Player player in players)
            {
                player.AddToHand(deck.DrawCard());
                player.AddToHand(deck.DrawCard());
            }
        }
        private void Hit(Player p)
        {
            p.AddToHand(deck.DrawCard());
            p.canDouble = false;
        }
        private void DoubleDown(Player p, Dealer d)
        {
            p.AddToHand(deck.DrawCard());
            p.playing = false;
            DisplayBoard(p, d);
            Thread.Sleep(3000);
        }
        private void Stand(Player p)
        {
            p.playing = false;
        }
        private void DisplayBoard(Player player, Dealer dealer)
        {
            Console.Clear();
            Console.WriteLine(player.GetName() + "'s turn");
            Console.WriteLine();
            Console.WriteLine("Dealer");
            Console.WriteLine(dealer.CardStart());
            Console.WriteLine(dealer.PointsStart());
            Console.WriteLine();

            DisplayHand(player);
        }
        private void DealerDraw(int j, Dealer house)
        {
            for (int i = 0; i < j; i++)
            {
                house.AddToHand(deck.DrawCard());
            }
        }
        private void DisplayHand(Entity entity)
        {
            foreach (Card card in entity.GetHand())
            {
                Console.Write(card.GetCard() + " ");
            }
            Console.WriteLine();
            Console.WriteLine(GetPoints(entity));
        }
        private int GetPoints(Entity entity)
        {
            int points = 0;
            bool soft = false;
            foreach (Card c in entity.GetHand())
            {
                switch (c.GetFace())
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
                    case 14:
                        points += 1;
                        break;
                    default:
                        points += c.GetFace();
                        break;
                }
            }
            while (soft && points > 21)
            {
                points = 0;
                foreach (Card c in entity.GetHand())
                {
                    switch (c.GetFace())
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
                                c.AceSwap();
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
                            points += c.GetFace();
                            break;
                    }
                }
            }

            return points;
        }
        public char TakeTurn(Player player)
        {
            char awn;
            Console.WriteLine("(H)it");
            if (player.canDouble)
            {
                Console.WriteLine("(D)ouble Down");
            }
            if (player.GetHand().Count == 2 && player.GetHand()[0].GetFace() == player.GetHand()[1].GetFace())
            {
                Console.WriteLine("S(p)lit");
            }
            Console.WriteLine("(S)tand");

            awn = Console.ReadKey().KeyChar;
            return awn;
        }
        private void AddPlayers()
        {
            int playerNum;
            while (true)
            {
                Console.WriteLine("Select number of players");
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
                    //takes desired number of players and adds that many players to list of players
                    break;
                }
                Console.WriteLine("Max number of player is 5");
            }

            Console.Clear();
            string[] names = new string[playerNum];
            for (int i = 0; i < playerNum; i++)
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
                names[i] = name;
            }
            AddPlayer(names);
        }
        public void PlaceBet(Player player)
        {
            while (true)
            {
                player.Reset();
                Console.WriteLine(player.GetName());
                Console.WriteLine("Select betting amount. (from 5 to 100)");
                try
                {
                    int bet = Convert.ToInt32(Console.ReadLine());
                    if (bet <= 100 && bet >= 5)
                    {
                        player.bet = bet;
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
        public void Turn(Player player)
        {
            while (player.playing)          //plays each players hand
            {
                DisplayBoard(player, dealer);

                if (GetPoints(player) == 21)
                {
                    if (player.GetHand().Count == 2)
                    {
                        Console.WriteLine("Blackjack");
                    }
                    Thread.Sleep(3000);
                    break;
                }
                if (GetPoints(player) > 21)            //if they go bust, gives a 3 second delay to see their points before continueing to the next player
                {
                    player.bust = true;
                    Thread.Sleep(3000);
                    break;
                }

                char awn = TakeTurn(player);           //the player selects what they want to do
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
        public void DealerTurn(Dealer dealer)
        {
            if (GetPoints(dealer) < 17)
            {
                while (GetPoints(dealer) < 17)
                {
                    dealer.AddToHand(deck.DrawCard());
                    Console.Clear();
                    Console.WriteLine("Dealer");
                    DisplayHand(dealer);
                    Console.WriteLine();

                    foreach (Player player in players)
                    {
                        Console.WriteLine(player.GetName());
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
                    Console.WriteLine(player.GetName());
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
        public void TjekWinner()
        {
            if (GetPoints(dealer) > 21)
            {
                foreach (Player player in players)
                {
                    if (!player.bust)
                    {
                        player.AddToWallet(player.bet);
                        Console.WriteLine(player.GetName());
                        Console.WriteLine("Won " + player.bet);
                        Console.WriteLine("Wallet: " + player.CheckWallet());
                    }
                    else
                    {
                        player.TakeFromWallet(player.bet);
                        Console.WriteLine(player.GetName());
                        Console.WriteLine("Lost " + player.bet);
                        Console.WriteLine("Wallet: " + player.CheckWallet());
                    }
                }
            }
            else
            {
                foreach (Player player in players)
                {
                    if (!player.bust)
                    {
                        if (GetPoints(player) > GetPoints(dealer))
                        {
                            player.AddToWallet(player.bet);
                            Console.WriteLine(player.GetName());
                            Console.WriteLine("Won " + player.bet);
                            Console.WriteLine("Wallet: " + player.CheckWallet());
                        }
                        else if (GetPoints(player) == GetPoints(dealer))
                        {
                            Console.WriteLine(player.GetName());
                            Console.WriteLine("Push");
                            Console.WriteLine("Wallet: " + player.CheckWallet());
                        }
                        else
                        {
                            player.TakeFromWallet(player.bet);
                            Console.WriteLine(player.GetName());
                            Console.WriteLine("Lost " + player.bet);
                            Console.WriteLine("Wallet: " + player.CheckWallet());
                        }
                    }
                    else
                    {
                        player.TakeFromWallet(player.bet);
                        Console.WriteLine(player.GetName());
                        Console.WriteLine("Lost " + player.bet);
                        Console.WriteLine("Wallet: " + player.CheckWallet());
                    }
                }
            }
        }
    }
}
