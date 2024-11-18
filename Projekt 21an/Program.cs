using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;

namespace spel21an
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            _21an_spelet spelet = new _21an_spelet();
            bool programRunning = true;

            while (programRunning)
            {
                Console.Clear();
                SkrivUtIFärg($"Välkommen till {spelet.SpeletsNamn}!\n", ConsoleColor.Green);
                Console.WriteLine($"Välj ett alternativ\r\n1. Spela {spelet.SpeletsNamn}\r\n2. Visa senaste vinnaren\r\n3. Spelets regler\r\n4. Avsluta programmet");
                string menyVal = Console.ReadLine();
                Console.WriteLine("");
                switch (menyVal)
                {
                    case "1":
                        spelet.RunGame21an();
                        break;

                    case "2":
                        if (spelet.Vinnare != null)
                        {
                            Console.Write($"Senaste vinnaren: ");
                            SkrivUtIFärg($"{spelet.Vinnare}\n", ConsoleColor.Green);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Det finns ännu ingen vinnare.");
                            Console.ReadKey();
                        }
                        break;
                    case "3":
                        Console.WriteLine(spelet.Regler);
                        Console.ReadKey();
                        break;
                    case "4":
                        SkrivUtIFärg("Tack för att du spelade!", ConsoleColor.Green);
                        Console.ReadKey();
                        programRunning = false;
                        break;
                    default:
                        break;
                }

            }


        }
        public static void SkrivUtIFärg(string textAttSkrivaUt, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(textAttSkrivaUt);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}



/*
 * Meny: 
 *  välkomsthälsning
 *  whileloop för programmet, while (programRunning = true) 
 *  lagra menyval, gör string variable för senaste vinnaren, från log file?
 *  switch för valen
 * case 4 programRunning = false, break
 * case 3 Läs upp reglerna 
 * case 2 Hämta vinnartitel från log File, annars "ingen vinnare ännu"
 * case 1 Kör 21an metod?
 * 
 * metod KollaPoängen (int resultat)
 *:  if resultat > 21 return true
 * 
 * gör randomCard metod som returnar tal från 1 - 10 
 * 
 * 21an metoden:
 * CW "Nu kommer två kort dras per spelare"
 * int dinPoäng = RandomCard()
 * dinPoäng += ^
 * int datornsPoäng = ^ *2 
 * int nyttKort = 0
 * 
 * while true 
 * CW Vill du ha ett till kort? (j/n)
 * if ReadLine == j || J 
 * nyttKort = rand
 * CW nytt kort 
 * dinPoäng += nytt kort 
 * CW reslutat din och datorns
 * if kollapoängen(dinPoäng)
 * break
 * 
 * else 
 * break
 * 
 * if kollapoängen (dinPoäng) == true 
 * CW Du har förlorat mm...
 * variabel vinnare = "Datorn" (Lagra i logfil)
 * else
 * (datorns tur) while datorns poäng < 21
 * nyttkort = rand
 * datorns poäng += nyttkort 
 * CW kort resultat
 * CW totalscore 
 * 
 * if datorn poäng > 21
 * CW Du har vunnit, skriv namn mm
 * RL lagra i logfile 
 * else if datorn poäng >= din poäng 
 * CW Du har förlorat mm...
 * variabel vinnare = "Datorn" (Lagra i logfil)
 * 
 * 
 * 
 * 
 * 

*/
//Välkommen till 21:an!
//Välj ett alternativ
//1. Spela 21:an
//2. Visa senaste vinnaren
//3. Spelets regler
//4. Avsluta programmet

//Nu kommer två kort dras per spelare
//Din poäng: 4
//Datorns poäng: 15
//Vill du ha ett till kort? (j/n)
//J
//Ditt nya kort är värt 8 poäng
//Din totalpoäng är 12
//Din poäng: 12
//Datorns poäng: 15
//Vill du ha ett till kort? (j/n)
//j
//Ditt nya kort är värt 4 poäng
//Din totalpoäng är 16
//Din poäng: 16
//Datorns poäng: 15
//Vill du ha ett till kort? (j/n)
//j
//Ditt nya kort är värt 4 poäng
//Din totalpoäng är 20
//Din poäng: 20
//Datorns poäng: 15
//Vill du ha ett till kort? (j/n)
//n
//Datorn drog ett kort värt 8
//Din poäng: 20
//Datorns poäng: 23
//Du har vunnit!
//Skriv in ditt namn
//Simon

//Välj ett alternativ
//1. Spela 21:an
//2. Visa senaste vinnaren
//3. Spelets regler
//4. Avsluta programmet