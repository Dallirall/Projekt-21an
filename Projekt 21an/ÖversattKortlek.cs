using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames;

namespace Projekt_21an
{
    public class ÖversattKortlek : CardDeck
    {
        private List<Card> _kortlek;
        public List<Card> Kortlek { get { return _kortlek; } set { _kortlek = value; } }

        protected override List<Card> DeckForVirtualMethods => Kortlek;
                

        public ÖversattKortlek()
        {
            InitializeDeck();            
        }

        public override void InitializeDeck()
        {
            Kortlek = new List<Card>();

            foreach (Kortsviter svit in Enum.GetValues(typeof(Kortsviter)))
            {
                foreach (Kortvalörer kortValör in Enum.GetValues(typeof(Kortvalörer)))
                {
                    int valör = (int)kortValör;
                    Kortlek.Add(new Card(valör, svit.ToString(), $"{svit} {kortValör}"));
                }
            }
        }

        public override void DisplayDrawnCardValues()
        {
            Card card = DrawACardFromDeck();
            Console.WriteLine($"Ditt kort är {card.CardName}. ");
        }



        public enum Kortsviter
        {
            Spader,
            Hjärter,
            Ruter,
            Klöver
        }

        public enum Kortvalörer
        {
            Ess = 1,
            Två,
            Tre,
            Fyra,
            Fem,
            Sex,
            Sju,
            Åtta,
            Nio,
            Tio,
            Knekt,
            Dam,
            Kung
        }
    }
}
