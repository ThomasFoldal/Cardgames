using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckOfCards;

namespace Blackjack
{
    internal class Player : Entity
    {
        private Wallet wallet;
        private string name;
        public Player(string name)
        {
            this.name = name;
            wallet = new Wallet(1000);
        }
        public bool playing { get; set; } = true;
        public bool canDouble { get; set; } = true;
        public bool bust { get; set; } = false;
        public int bet { get; set; }
        public void Reset()
        {
            hand.Clear();
            playing = true;
            canDouble = true;
            bust = false;
        }
        public string GetName()
        {
            return name;
        }
        public int CheckWallet()
        {
            return wallet.Contains();
        }
        public void AddToWallet(int money)
        {
            wallet.Add(money);
        }
        public int TakeFromWallet(int money)
        {
            return wallet.Remove(money);
        }
    }
}
