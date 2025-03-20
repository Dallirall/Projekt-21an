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

        protected virtual List<Card> DeckForVirtualMethods => Deck;


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
            return DeckForVirtualMethods[rnd.Next(0, (DeckForVirtualMethods.Count))];
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

        public virtual Card DrawCardOfSpecificValues(IEnumerable<int> cardValues)
        {            
            List<Card> cardsOfSpecValues = new List<Card>();
            foreach (int value in cardValues)
            {
                cardsOfSpecValues.AddRange(DeckForVirtualMethods.Where(card => card.CardValue == value));
            }
            
            Random rnd = new Random();
            return cardsOfSpecValues[rnd.Next(0, cardsOfSpecValues.Count)];
        }


    }
}

