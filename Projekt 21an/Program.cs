using System;
using System.IO;
using System.Collections.Generic;
using Projekt_21an;

namespace spel21an
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till 21:an!");
            string? senasteVinnaren = null;
            string pathToLogFile = @"C:\temp\21an_log.txt";
            if (File.Exists(pathToLogFile))
            {
                senasteVinnaren = File.ReadAllText(pathToLogFile);
            }
            bool programRunning = true;

            while (programRunning)
            {
                Console.WriteLine("Välj ett alternativ\r\n1. Spela 21:an\r\n2. Visa senaste vinnaren\r\n3. Spelets regler\r\n4. Avsluta programmet");
                string menyVal = Console.ReadLine();
                switch (menyVal)
                {
                    case "1":
                        break;

                    case "2":
                        if (senasteVinnaren != null)
                        {
                            Console.WriteLine($"Senaste vinnaren: {senasteVinnaren}");
                        }
                        else
                        {
                            Console.WriteLine("Det finns ännu ingen vinnare.");
                        }
                        break;
                    case "3":
                        Console.Write("I 21:an kommer du att spela mot datorn och försöka tvinga datorn att få över 21 poäng. Både du och datorn får poäng genom att dra kort, varje kort är värt 1 – 10 poäng. När spelet börjar dras två kort till både dig och datorn. Därefter får du dra hur många kort som du vill tills du är nöjd med din totalpoäng, du vill komma så nära 21 som möjligt utan att få mer än 21 poäng. När du inte vill dra fler kort så kommer datorn att dra kort tills den har mer eller lika många poäng som dig.\r\n\r\nDu vinner om datorn får mer än totalt 21 poäng när den håller på att dra kort. Datorn vinner om den har mer poäng än dig när spelet är slut så länge som datorn inte har mer än 21 poäng. Om det skulle bli lika i poäng så vinner datorn. Om du får mer än 21 poäng när du drar kort så har du förlorat.\n\n");
                        break;
                    case "4":
                        Console.WriteLine("Tack för att du spelade!");
                        programRunning = false;
                        break;
                    default:
                        break;
                }

            }


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