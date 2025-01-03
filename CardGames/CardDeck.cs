using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public class CardDeck
    {

        public List<Card> deck;
        public List<Card> Deck { get { return deck; } private set { deck = value; } }          
        

        public CardDeck()
        {
            deck = new List<Card>();
            foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
            {
                foreach (CardValueName cardValueName in Enum.GetValues(typeof(CardValueName)))
                {
                    int cardValue = (int)cardValueName;
                    deck.Add(new Card(cardValue, suit, $"{cardValueName} of {suit}"));
                }
            }
        }

        public Card DrawACard()
        {
            Random rnd = new Random();
            return Deck[rnd.Next(0, (Deck.Count + 1))];
        }

        public void DisplayDrawnCardValues()
        {
            Card card = DrawACard();
            Console.WriteLine($"Your card is {card.CardName}. ");
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
