﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames;

namespace Projekt_21an
{
    public class Översättning:CardDeck
    {
        public IDictionary<CardSuits, Kortsviter> SvitÖversättningDict { get; set; }

        public IDictionary<int, Kortvalörer> ValörÖversättningDict { get; set; }

        public struct Spelkort
        {
            public int Valör;
            public Kortsviter Svit;
            public string KortNamn;

            public Spelkort(int valör, Kortsviter svit, string kortNamn)
            {
                Valör = valör;
                Svit = svit;
                KortNamn = kortNamn;
            }
        }

        public Översättning()
        {
            SvitÖversättningDict = new Dictionary<CardSuits, Kortsviter>()
            {
                { CardSuits.Spades, Kortsviter.Spader },
                { CardSuits.Hearts, Kortsviter.Hjärter },
                { CardSuits.Diamonds, Kortsviter.Ruter },
                { CardSuits.Clubs, Kortsviter.Klöver },
            };

            ValörÖversättningDict = new Dictionary<int, Kortvalörer>()
            {
                { 1, Kortvalörer.Ess },
                { 2, Kortvalörer.Två },
                { 3, Kortvalörer.Tre },
                { 4, Kortvalörer.Fyra },
                { 5, Kortvalörer.Fem },
                { 6, Kortvalörer.Sex },
                { 7, Kortvalörer.Sju },
                { 8, Kortvalörer.Åtta },
                { 9, Kortvalörer.Nio },
                { 10, Kortvalörer.Tio },
                { 11, Kortvalörer.Knekt },
                { 12, Kortvalörer.Dam },
                { 13, Kortvalörer.Kung }
            };
        }

        public CardDeck ÖversättKortlek(CardDeck engelskKortlek)
        {
            List<Card> översattKortlek = new List<Card>();

            översattKortlek = engelskKortlek.Deck.Select(k => new
            {
                Svit = SvitÖversättningDict[k.Suit],
                Valör = ValörÖversättningDict[k.CardValue],
            });

            return 
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
