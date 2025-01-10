using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public class CardDeck
    {

        private List<Card> _deck;
        public virtual List<Card> Deck { get { return _deck; } set { _deck = value; } }          
        

        public CardDeck()
        {
            InitializeDeck();
        }

        public virtual void InitializeDeck()
        {
            Deck = new List<Card>();

            foreach (CardConstants.CardSuits suit in Enum.GetValues(typeof(CardConstants.CardSuits)))
            {
                foreach (CardConstants.CardValueName cardValueName in Enum.GetValues(typeof(CardConstants.CardValueName)))
                {
                    int cardValue = (int)cardValueName;
                    Deck.Add(new Card(cardValue, suit.ToString(), $"{cardValueName} of {suit}"));
                }
            }
        }

        public virtual Card DrawACardFromDeck()
        {
            Random rnd = new Random();
            return Deck[rnd.Next(0, (Deck.Count + 1))];
        }

        public virtual void DisplayDrawnCardValues()
        {
            Card card = DrawACardFromDeck();
            Console.WriteLine($"Your card is {card.CardName}. ");
        }

        public virtual List<Card> SortAwayUnwantedCards(List<Card> deck, string[] unwantedSuits, int[] unwantedValues)
        {
            List<Card> sortedDeck = new List<Card>();

            sortedDeck = deck.Where(card => (!unwantedSuits.Contains(card.Suit) && (!unwantedValues.Contains(card.CardValue)))).ToList();

            return sortedDeck;
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
