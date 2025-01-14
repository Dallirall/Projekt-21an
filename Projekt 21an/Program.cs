﻿using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;
using static Projekt_21an._21an_spelet;
using Microsoft.Data.SqlClient;
using Dapper;
using CardGames;


namespace spel21an
{
    public class Program
    {
        static void Main(string[] args)
        {            
            //CardDeck cardDeck = new CardDeck();
            ////cardDeck.DisplayDrawnCardValues();

            //var kortlek = new ÖversattKortlek();
            //kortlek.DisplayDrawnCardValues();
            ////CardDeck sortedDeck = new();
            ////sortedDeck.Deck = cardDeck.SortAwayUnwantedCards(cardDeck.Deck, new string[] { CardGames.CardConstants.CardSuits.Spades.ToString(), CardGames.CardConstants.CardSuits.Clubs.ToString() }, new int[] { 11, 12, 13 });
            ////sortedDeck.DisplayDrawnCardValues();

            //Card card = new(1, "Hearts", "Ace of Hearts");

            //card = kortlek.DrawCardOfSpecificValues(new[] { 1, 2, 3 });
            //Console.WriteLine(card.CardName);
           


            Console.ReadKey();


            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            _21an_spelet spelet = new _21an_spelet();
            bool programRunning = true;

            while (programRunning)
            {
                Console.Clear();
                StringManipulationMethods.SkrivUtIFärg($"Välkommen till {spelet.SpeletsNamn}!\n", ConsoleColor.Green);
                Console.WriteLine($"Välj ett alternativ\r\n1. Spela {spelet.SpeletsNamn}\r\n2. Visa vinnarstatistik\r\n3. Spelets regler\r\n4. Inställningar\r\n5. Avsluta programmet");
                EnumVärden.StartmenyVal menyVal = (EnumVärden.StartmenyVal)int.Parse(Console.ReadLine());
                Console.WriteLine("");
                switch (menyVal)
                {
                    case EnumVärden.StartmenyVal.Val_spela_spelet:
                        while (true)
                        {
                            spelet.RunGame();
                            Console.WriteLine("\nSpela igen? (j/n): ");
                            if (Console.ReadLine() != "j")
                            {
                                break;
                            }
                        }
                        break;

                    case EnumVärden.StartmenyVal.Val_visa_vinnarstatistik:
                        SqlMetoder.DisplayaVinststatistik();
                        Console.ReadKey();
                        break;

                    case EnumVärden.StartmenyVal.Val_spelets_regler:
                        Console.WriteLine(spelet.Regler);
                        Console.ReadKey();
                        break;

                    case EnumVärden.StartmenyVal.Val_inställningar:
                        Spelinställningar.Inställningar(spelet);
                        break;

                    case EnumVärden.StartmenyVal.Val_avsluta_programmet:
                        StringManipulationMethods.SkrivUtIFärg("Tack för att du spelade!", ConsoleColor.Green);
                        Console.ReadKey();
                        programRunning = false;
                        break;

                    default:
                        break;
                }

            }


        }
        

        

    }    
}

