using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Wallet
    {
        private int balance;
        public Wallet() { }
        public Wallet(int i)
        {
            balance = i;
        }
        public int Contains()
        {
            return balance;
        }
        public int Remove(int i)
        {
            if (i <= balance)
            {
                balance -= i;
                return i;
            }
            else
            {
                return 0;
            }
        }
        public void Add(int i)
        {
            balance += i;
        } 
    }
}
