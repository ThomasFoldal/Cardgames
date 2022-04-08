using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Wallet
    {
        private int capital;

        public Wallet() { }
        public Wallet(int i)
        {
            capital = i;
        }
        public int Contains()
        {
            return capital;
        }
        public int Remove(int i)
        {
            if (i <= capital)
            {
                capital -= i;
                return i;
            }
            else
            {
                return 0;
            }
        }
        public void Add(int i)
        {
            capital += i;
        } 
    }
}
