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
            //while (true)
            //{
            //    Console.WriteLine("Select number of players");
            //    try
            //    {
            //        playerNum = Convert.ToInt32(Console.ReadLine());
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("Invalid Input");
            //        throw;
            //    }
            //    if (playerNum <= 5)
            //    {
            //                   //takes desired number of players and adds that many players to list of players
            //        break;
            //    }
            //    Console.WriteLine("Max number of player is 5");
            //}
            //Console.Clear();
            //string[] names = new string[playerNum];
            //for (int i = 0; i < playerNum; i++)
            //{
            //    string name = null;
            //    while (name == null)
            //    {
            //        Console.Write("Enter player {0}'s name: ", (i + 1));        //make everyone enter their name
            //        try
            //        {
            //            name = Console.ReadLine();
            //        }
            //        catch (Exception)
            //        {
            //            Console.Clear();
            //            Console.WriteLine("Invalid Input");
            //        }
            //    }
            //    names[i] = name;
            //}
            //AddPlayer(names);
            while (quit == false)
            {
                Console.Clear();
                dealer.ResetHand();
                foreach (Player player in players)
                {
                    PlaceBet(player);
                    //while (true)
                    //{
                    //    player.Reset();
                    //    Console.WriteLine(player.GetName());
                    //    Console.WriteLine("Select betting amount. (from 5 to 100)");
                    //    try
                    //    {
                    //        int bet = Convert.ToInt32(Console.ReadLine());
                    //        if (bet <= 100 && bet >= 5)
                    //        {
                    //            player.bet = bet;
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine();
                    //        }
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Console.WriteLine("Invalid Input");
                    //    }
                    //}
                }
                Console.Clear();
                DealerDraw(2, dealer);
                NewHands();
                foreach (Player player in players)
                {
                    //while (player.playing)          //plays each players hand
                    //{
                    //    DisplayBoard(player, dealer);

                    //    if (GetPoints(player) == 21)
                    //    {
                    //        if (player.hand.Count == 2)
                    //        {
                    //            Console.WriteLine("Blackjack");
                    //        }
                    //        Thread.Sleep(3000);
                    //        break;
                    //    }
                    //    if (GetPoints(player) > 21)            //if they go bust, gives a 3 second delay to see their points before continueing to the next player
                    //    {
                    //        player.bust = true;
                    //        Thread.Sleep(3000);
                    //        break;
                    //    }

                    //    char awn = TakeTurn(player);           //the player selects what they want to do
                    //    switch (awn)
                    //    {
                    //        case 'h':
                    //            Hit(player);
                    //            break;
                    //        case 'd':
                    //            DoubleDown(player, dealer);
                    //            break;
                    //        case 'p':
                    //            Console.WriteLine("this function is not avalable at this time");
                    //            Console.ReadLine();
                    //            //SplitHand(player);
                    //            break;
                    //        case 's':
                    //            Stand(player);
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    Turn(player);
                }
                //if (GetPoints(dealer) < 17)
                //{
                //    while (GetPoints(dealer) < 17)
                //    {
                //        dealer.hand.Add(deck.DrawCard());
                //        Console.Clear();
                //        Console.WriteLine("Dealer");
                //        DisplayHand(dealer);
                //        Console.WriteLine();

                //        foreach (Player player in players)
                //        {
                //            Console.WriteLine(player.GetName());
                //            if (player.bust)
                //            {
                //                Console.WriteLine("Busted");
                //            }
                //            else
                //            {
                //                DisplayHand(player);
                //            }
                //            Console.WriteLine();
                //        }
                //        Thread.Sleep(2000);
                //    }
                //}
                //else
                //{
                //    Console.Clear();
                //    Console.WriteLine("Dealer");
                //    DisplayHand(dealer);
                //    Console.WriteLine();

                //    foreach (Player player in players)
                //    {
                //        Console.WriteLine(player.GetName());
                //        if (player.bust)
                //        {
                //            Console.WriteLine("Busted");
                //        }
                //        else
                //        {
                //            DisplayHand(player);
                //        }
                //        Console.WriteLine();
                //    }
                //    Thread.Sleep(2000);
                //}
                DealerTurn(dealer);

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
        public void AddPlayer(string[] names)
        {
            foreach (string name in names)
            {
                players.Add(new Player(name));
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
            DisplayBoard(p, d);
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
            Console.WriteLine(player.GetName() + "'s turn");
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
                house.hand.Add(deck.DrawCard());
            }
        }
        public void DisplayHand(Entity entity)
        {
            foreach (Card card in entity.hand)
            {
                Console.Write(card.GetCard() + " ");
            }
            Console.WriteLine();
            Console.WriteLine(GetPoints(entity));
        }
        public int GetPoints(Entity entity)
        {
            int points = 0;
            bool soft = false;
            foreach (Card c in entity.hand)
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
                    case 14:
                        points += 1;
                        break;
                    default:
                        points += c.face;
                        break;
                }
            }
            while (soft && points > 21)
            {
                points = 0;
                foreach (Card c in entity.hand)
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
        public char TakeTurn(Player player)
        {
            char awn;
            Console.WriteLine("(H)it");
            if (player.canDouble)
            {
                Console.WriteLine("(D)ouble Down");
            }
            if (player.hand.Count == 2 && player.hand[0].face == player.hand[1].face)
            {
                Console.WriteLine("S(p)lit");
            }
            Console.WriteLine("(S)tand");

            awn = Console.ReadKey().KeyChar;
            return awn;
        }
        public string[] AddPlayers()
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
                    if (player.hand.Count == 2)
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
                    dealer.hand.Add(deck.DrawCard());
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

        }
    }
}
