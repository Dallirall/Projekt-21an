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

        //public IDictionary<CardSuits, Kortsviter> SvitÖversättningDict { get; set; }

        //public IDictionary<int, Kortvalörer> ValörÖversättningDict { get; set; }

        //public struct Spelkort
        //{
        //    public int Valör;
        //    public Kortsviter Svit;
        //    public string KortNamn;

        //    public Spelkort(int valör, Kortsviter svit, string kortNamn)
        //    {
        //        Valör = valör;
        //        Svit = svit;
        //        KortNamn = kortNamn;
        //    }
        //}

        public ÖversattKortlek()
        {
            InitializeDeck();

            //SvitÖversättningDict = new Dictionary<CardSuits, Kortsviter>()
            //{
            //    { CardSuits.Spades, Kortsviter.Spader },
            //    { CardSuits.Hearts, Kortsviter.Hjärter },
            //    { CardSuits.Diamonds, Kortsviter.Ruter },
            //    { CardSuits.Clubs, Kortsviter.Klöver },
            //};

            //ValörÖversättningDict = new Dictionary<int, Kortvalörer>()
            //{
            //    { 1, Kortvalörer.Ess },
            //    { 2, Kortvalörer.Två },
            //    { 3, Kortvalörer.Tre },
            //    { 4, Kortvalörer.Fyra },
            //    { 5, Kortvalörer.Fem },
            //    { 6, Kortvalörer.Sex },
            //    { 7, Kortvalörer.Sju },
            //    { 8, Kortvalörer.Åtta },
            //    { 9, Kortvalörer.Nio },
            //    { 10, Kortvalörer.Tio },
            //    { 11, Kortvalörer.Knekt },
            //    { 12, Kortvalörer.Dam },
            //    { 13, Kortvalörer.Kung }
            //};
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

        //public CardDeck ÖversättKortlek(CardDeck engelskKortlek)
        //{
        //    List<Card> översattKortlek = new List<Card>();

        //    översattKortlek = engelskKortlek.Deck.Select(k => new
        //    {
        //        Svit = SvitÖversättningDict[k.Suit],
        //        Valör = ValörÖversättningDict[k.CardValue],
        //    });

        //    return
        //}

        //public override Card DrawACardFromDeck()
        //{
        //    Random rnd = new Random();
        //    return Kortlek[rnd.Next(0, Kortlek.Count + 1)];
        //}

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
