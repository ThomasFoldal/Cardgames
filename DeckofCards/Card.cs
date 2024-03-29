﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DeckOfCards
{
    public class Card
    {
        private int face;
        private char suite;

        public Card(int face, char suite)
        {
            this.face = face;
            this.suite = suite;
        }

        public string GetCard()
        {
            string card;

            switch (face)
            {
                case 1:
                    card = "A" + suite;
                    break;
                case 11:
                    card = "J" + suite;
                    break;
                case 12:
                    card = "Q" + suite;
                    break;
                case 13:
                    card = "K" + suite;
                    break;
                default:
                    card = Convert.ToString(face) + suite;
                    break;
            }
            return card;
        }
        public int GetFace()
        {
            return face;
        }
        public char GetSuite()
        {
            return suite;
        }
        public void AceSwap()
        {
            face = 14;
        }
    }
}
