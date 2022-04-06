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
            deck.FillDeck(1);

            dealer = new Dealer();
            Console.WriteLine("Select number of players");
            int playerNum;
            try
            {
                playerNum = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                throw;
            }
            AddPlayer(playerNum);
            NewHands();
            foreach (Player player in players)
            {
                Console.WriteLine(player.GetPoints());
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
    }
}
