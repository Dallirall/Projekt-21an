using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public class CardDeck
    {
        //public string[] suit = { "Spades", "Hearts, Diamonds", "Clubs" };
        //public string[] Suit { get { return suit; } }

        public List<Card> deck;
        public List<Card> Deck 
        { 
            get 
            {
                return deck;
            }
            set
            {
                for (int i = 0; i < Enum.GetValues(typeof(CardSuits)).Length; i++)
                {
                    CardSuits suit = (CardSuits)i;

                    for (int j = 0; j < Enum.GetValues(typeof(CardValueName)).Length; j++)
                    {
                        string cardValueName = Enum.GetName(typeof(CardValueName), (j + 1));
                        Deck.Add(Card((j + 1), suit, $"{cardValueName} of {suit}"));
                    }
                }
            }
        }




        public struct Card
        {
            public int CardValue;
            public CardSuits Suit;
            public string CardName;

            public Card(int cardValue, CardSuits suit, string cardName)
            {
                CardValue = cardValue;
                Suit = suit;
                CardName = cardName;
            }
        }
        
        public enum CardSuits
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs
        }
        
        public enum CardValueName
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }
    }
}



//Man ska kunna från ett annat program dra ett kort. Det man får är ett objekt med Prop suit,value,name. 
//Programmet kan sedan adda ett value t.ex. eller displaya i text vilket kort som drogs, tex "Queen of Hearts", eller dispplaya i text vilken suit, tex "Spades"


//alt 1: dra random 1-52 från List<Card> CardDeck innehållande Card-objects 

// card 1: prop val:1, prop suit: clubs, prop name: "Ace(enum) of ({suit})Clubs"
//prop list<Card> Deck: {
//get {return deck}
//set {
//for 4 loops : suit = suit[int i]
// for 13 loops, add Card(int j +1, suit, "{CardValName} of {suit})
//}
//}
//public Card DrawCard()
//{ return Deck[Random.Next(1, 53)] }

//alt 2?: 

//Gör senare en översättning till svenska
