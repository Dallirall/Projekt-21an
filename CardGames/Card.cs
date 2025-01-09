using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public class Card
    {
        public int CardValue { get; set; }
        public string Suit { get; set; }
        public string CardName { get; set; }

        public Card(int cardValue, string suit, string cardName)
        {
            CardValue = cardValue;
            Suit = suit;
            CardName = cardName;
        }
    }
}
