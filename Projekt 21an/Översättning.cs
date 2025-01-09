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
        public IDictionary<CardSuits, Kortsviter> SvitÖversättningDict {  get; set; }

        public IDictionary<CardValueName, Kortvalörer> ValörÖversättningDict { get; set; }

        public Översättning()
        {
            SvitÖversättningDict = new Dictionary<CardSuits, Kortsviter>()
            {
                { CardSuits.Spades, Kortsviter.Spader },
                { CardSuits.Hearts, Kortsviter.Hjärter },
                { CardSuits.Diamonds, Kortsviter.Ruter },
                { CardSuits.Clubs, Kortsviter.Klöver },
            };

            ValörÖversättningDict = new Dictionary<CardValueName, Kortvalörer>()
            {
                { CardValueName.Ace, Kortvalörer.Ess },
                { CardValueName.Two, Kortvalörer.Två },
                { CardValueName.Three, Kortvalörer.Tre },
                { CardValueName.Four, Kortvalörer.Fyra },
                { CardValueName.Five, Kortvalörer.Fem },
                { CardValueName.Six, Kortvalörer.Sex },
                { CardValueName.Seven, Kortvalörer.Sju },
                { CardValueName.Eight, Kortvalörer.Åtta },
                { CardValueName.Nine, Kortvalörer.Nio },
                { CardValueName.Ten, Kortvalörer.Tio },
                { CardValueName.Jack, Kortvalörer.Knekt },
                { CardValueName.Queen, Kortvalörer.Dam },
                { CardValueName.King, Kortvalörer.Kung }
            };
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
            Ess,
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
