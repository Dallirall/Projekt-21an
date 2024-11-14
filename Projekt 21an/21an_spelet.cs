using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_21an
{
    public class _21an_spelet
    {
        private string speletsNamn = "21an";
        public string SpeletsNamn { get { return speletsNamn; } }
        
        private string regler = "I 21:an kommer du att spela mot datorn och försöka tvinga datorn att få över 21 poäng. Både du och datorn får poäng genom att dra kort, varje kort är värt 1 – 10 poäng. När spelet börjar dras två kort till både dig och datorn. Därefter får du dra hur många kort som du vill tills du är nöjd med din totalpoäng, du vill komma så nära 21 som möjligt utan att få mer än 21 poäng. När du inte vill dra fler kort så kommer datorn att dra kort tills den har mer eller lika många poäng som dig.\n\nDu vinner om datorn får mer än totalt 21 poäng när den håller på att dra kort. Datorn vinner om den har mer poäng än dig när spelet är slut så länge som datorn inte har mer än 21 poäng. Om det skulle bli lika i poäng så vinner datorn. Om du får mer än 21 poäng när du drar kort så har du förlorat.\n";
        public string Regler { get { return regler; } }

        private string pathToLogFile = @"C:\temp\21an_vinnarlog.txt";

        public string PathToLogFile { get { return pathToLogFile; } }

        private string? vinnare;
        public string? Vinnare 
        { 
            get
            {
                if (File.Exists(pathToLogFile))
                {
                    return File.ReadAllText(pathToLogFile);
                }
                else
                {
                    return null;
                }
            } 
        }
        private int kortMaxVärde = 10;
        public int KortMaxVärde { get {return kortMaxVärde;} }
        private int kortMinVärde = 1;
        public int KortMinVärde { get {return kortMinVärde;}}

        public void RunGame21an()
        {
            Console.WriteLine("Nu kommer två kort dras per spelare.");
            Console.ReadKey();
            int dinPoäng = RandomCard();
            dinPoäng += RandomCard();
            int datornsPoäng = RandomCard();
            datornsPoäng += RandomCard();
            int nyttKort = 0;
            bool draKort = true;
            bool förlorat = false;

            while (draKort)
            {
                Console.Write($"Din poäng:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{ dinPoäng}\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"Datorns poäng:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{datornsPoäng}\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Vill du ha ett till kort? (j/n)");   
                if (Console.ReadLine().ToLower() == "j")
                {
                    nyttKort = RandomCard();
                    dinPoäng += nyttKort;
                    Console.Write("Ditt nya kort är värt ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"{nyttKort}");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" poäng\n");
                    Console.WriteLine($"Din totalpoäng är ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{dinPoäng}\n");
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (ÄrPoängenÖver21(dinPoäng))
                    {
                        Console.WriteLine("Din poäng har överskridit 21.");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Du har förlorat.\n");
                        Console.ForegroundColor = ConsoleColor.Black;
                        vinnare = "Datorn";
                        RegistreraVinnaren(vinnare);
                        draKort = false;
                        förlorat = true;
                        Console.ReadKey();
                    }
                }
                else
                {
                    draKort = false;
                }
            }
            draKort = true;
            while (draKort && !förlorat)
            {
                Console.WriteLine("Nu drar datorn kort!");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                nyttKort = RandomCard();
                datornsPoäng += nyttKort;
                Console.Write($"\n\nDatorn drog ett kort värt ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{nyttKort}\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"Din poäng:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{dinPoäng}\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"Datorns poäng:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{datornsPoäng}\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ReadKey();
                if (ÄrPoängenÖver21(datornsPoäng))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Du har vunnit!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Skriv in ditt namn: ");
                    vinnare = Console.ReadLine();
                    RegistreraVinnaren(vinnare);
                    Console.WriteLine($"Grattis {vinnare}!");
                    draKort = false;
                    Console.ReadKey();
                }
                else if (datornsPoäng >= dinPoäng)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Datorn har vunnit!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    vinnare = "Datorn";
                    RegistreraVinnaren(vinnare);
                    draKort = false;
                    Console.ReadKey();
                }
            }


        }

        public int RandomCard()
        {
            Random slumpKort = new Random();
            return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
        }

        public bool ÄrPoängenÖver21(int resultat)
        {
            if (resultat > 21)
            {
                return true;
            }
            return false;
        }

        public void RegistreraVinnaren(string vinnare)
        {
            string path = @"C:\temp\21an_vinnarlog.txt";
            File.WriteAllText(path, vinnare);
        }
    }
}
