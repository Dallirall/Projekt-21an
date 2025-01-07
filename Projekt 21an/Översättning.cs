using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames;

namespace Projekt_21an
{
    public class Översättning:CardDeck
    {
        public IDictionary<CardSuits, Kortsviter> ÖversättningarDict {  get; set; }

        public Översättning()
        {
            ÖversättningarDict = new Dictionary<CardSuits, Kortsviter>()
            {
                { CardSuits.Spades, Kortsviter.Spader },
                { CardSuits.Hearts, Kortsviter.Hjärter },
                { CardSuits.Diamonds, Kortsviter.Ruter },
                { CardSuits.Clubs, Kortsviter.Klöver },
            };
        }


        public enum Kortsviter
        {
            Spader,
            Hjärter,
            Ruter,
            Klöver
        }
    }
}
