using spel21an;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using CardGames;

namespace Projekt_21an
{
    public class _21an_spelet : IKonsolKortspel
    {
        private string speletsNamn = "21an";
        public string SpeletsNamn { get { return speletsNamn; } }
        
        private string regler = $"I 21:an kommer du att spela mot datorn och försöka tvinga datorn att få över 21 poäng. Både du och datorn får poäng genom att dra kort. När spelet börjar dras kort till både dig och datorn. Därefter får du dra hur många kort som du vill tills du är nöjd med din totalpoäng, du vill komma så nära 21 som möjligt utan att få mer än 21 poäng. När du inte vill dra fler kort så kommer datorn att dra kort tills den har mer eller lika många poäng som dig.\n\nDu vinner om datorn får mer än totalt 21 poäng när den håller på att dra kort. Datorn vinner om den har mer poäng än dig när spelet är slut så länge som datorn inte har mer än 21 poäng. Om du får mer än 21 poäng när du drar kort så har du förlorat.\n";
        public string Regler { get { return regler; } }

        public ÖversattKortlek Kortspel { get; set; }


        public _21an_spelet()
        {
            Kortspel = new ÖversattKortlek();            
        }

        public void RunGame()
        {
            if (Spelinställningar.Svårighetsgrad == EnumVärden.Svårighetsgrader.Lätt || Spelinställningar.Svårighetsgrad == EnumVärden.Svårighetsgrader.Medel)
            {
                Kortspel.Kortlek = Kortspel.SortAwayUnwantedCards(Kortspel.Kortlek, new[] { "" }, new[] { 11, 12, 13 });
            }
            //Kortspel.Kortlek = Spelinställningar.SorteraKortlekEfterSvårighetsgrad(Spelinställningar.Svårighetsgrad, Kortspel);
            
            Spelare datorn = new Spelare("Datorn");
            Console.WriteLine("Skriv in ditt spelarnamn: ");
            Spelare spelare = new Spelare(Console.ReadLine());
            
            if (!SqlMetoder.ExistsInDatabaseCheck(spelare.Namn))
            {
                SqlMetoder.RegistreraNySpelareIDatabasen(spelare);
            }

            //Spelfas 1: Utdelning av starthand
            Console.WriteLine($"\nNu kommer {Spelinställningar.AntalKortAttBörjaMed} kort dras per spelare.");
            Console.ReadKey();
            spelare.Poäng = 0;
            datorn.Poäng = 0;
            for (int i = 0; i < Spelinställningar.AntalKortAttBörjaMed; i++)
            {
                spelare.Poäng += RandomCardTillSpelaren();
                Console.Write($"Din poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                Console.ReadKey();

                datorn.Poäng += RandomCardTillDatorn();
                Console.Write($"Datorns poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{datorn.Poäng}\n", ConsoleColor.Red);
                Console.ReadKey();
            }

            int nyttKort = 0;
            bool avgjort = false;
            bool duHarFörlorat = false;
            bool datornHarFörlorat = false;
            Func<int, bool> ärPoängenÖver21 = poäng => poäng > 21;

            //Spelfas 2: Spelaren drar kort
            while (!avgjort)
            {
                Console.Write($"Din poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                Console.Write($"Datorns poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{datorn.Poäng}\n", ConsoleColor.Red);
                duHarFörlorat = ärPoängenÖver21(spelare.Poäng);
                datornHarFörlorat = ärPoängenÖver21(datorn.Poäng);
                avgjort = HarÖverskridit21poäng(datornHarFörlorat, duHarFörlorat, spelare, datorn);
                if (avgjort)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Vill du ha ett till kort? (j/n)");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "j")
                    {
                        nyttKort = RandomCardTillSpelaren();
                        spelare.Poäng += nyttKort;
                        Console.Write("Ditt nya kort är värt ");
                        StringManipulationMethods.SkrivUtIFärg($"{nyttKort}", ConsoleColor.DarkCyan);
                        Console.Write(" poäng\n");
                        Console.Write($"Din totalpoäng är ");
                        StringManipulationMethods.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (avgjort)
            {

            }
            else
            {
                avgjort = checkaVinstConditions(datorn, spelare);
            }

            //Spelfas 3: Datorn drar kort
            while (!avgjort)
            {
                Console.WriteLine("\nNu drar datorn kort!");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                nyttKort = RandomCardTillDatorn();
                datorn.Poäng += nyttKort;
                Console.Write($"\n\nDatorn drog ett kort värt ");
                StringManipulationMethods.SkrivUtIFärg($"{nyttKort}\n", ConsoleColor.DarkCyan);
                Console.Write($"Din poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                Console.Write($"Datorns poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{datorn.Poäng}\n", ConsoleColor.Red);
                Console.ReadKey();
                datornHarFörlorat = ärPoängenÖver21(datorn.Poäng);
                avgjort = HarÖverskridit21poäng(datornHarFörlorat, duHarFörlorat, spelare, datorn);
                if (avgjort)
                {
                    break;
                }
                else
                {
                    avgjort = checkaVinstConditions(datorn, spelare);
                }   
            }
        }

        public bool HarÖverskridit21poäng(bool datornHarFörlorat, bool duHarFörlorat, Spelare spelaren, Spelare datorn)
        {
            if (duHarFörlorat && datornHarFörlorat)
            {
                if (Spelinställningar.MöjligtMedOavgjort)
                {
                    RegistreraVinnaren(EnumVärden.Resultat.BådaÖver21, spelaren, datorn);
                }
                else
                {
                    RegistreraVinnaren(EnumVärden.Resultat.PoängÖverskridit21, datorn, spelaren);
                }
                return true;
            }
            else if (duHarFörlorat)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängÖverskridit21, datorn, spelaren);
                return true;
            }
            else if (datornHarFörlorat)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängÖverskridit21, spelaren, datorn);
                return true;
            }
            return false;
        }

        public bool checkaVinstConditions(Spelare datorn, Spelare spelare)
        {
           
            if (datorn.Poäng > spelare.Poäng)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängNärmast21, datorn, spelare);
                return true;
            }
            else if (datorn.Poäng < Spelinställningar.DatornSlutarDraKortVid)
            {
                return false;
            }
            else if (datorn.Poäng < spelare.Poäng)
            {
                Console.WriteLine("\nDatorn väljer att inte dra fler kort. \n");
                RegistreraVinnaren(EnumVärden.Resultat.PoängNärmast21, spelare, datorn);
                return true;
            }
            else if (datorn.Poäng == spelare.Poäng)
            {
                if (Spelinställningar.MöjligtMedOavgjort)
                {
                    RegistreraVinnaren(EnumVärden.Resultat.BådaSammaPoäng, spelare, datorn);
                    return true; 
                }
                else
                {
                    RegistreraVinnaren(EnumVärden.Resultat.BådaSammaPoäng, datorn, spelare);
                    return true;
                }
            }
            return false;
        }

        public int RandomCardTillSpelaren()
        {
            Random slumpKort = new Random();
            
            
            //
            //cw du har dragit Card.Name
            //return Card.Value
            switch (Spelinställningar.Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Custom:

                    Card dragetKort = Kortspel.DrawACardFromDeck();
                    Console.WriteLine($"Du drog {dragetKort.CardName}. ");
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Lätt:
                    if (slumpKort.NextDouble() < 0.5)
                    {
                        return slumpKort.Next(Spelinställningar.KortMinVärde, 3);
                    }
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));

                case EnumVärden.Svårighetsgrader.Medel:
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Svår:
                    if (slumpKort.NextDouble() < 0.15)
                    {
                        return slumpKort.Next(6, (Spelinställningar.KortMaxVärde + 1));
                    }
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));

                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = slumpKort.NextDouble();
                    if (procent < 0.1)
                    {
                        return slumpKort.Next(8, (Spelinställningar.KortMaxVärde + 1));
                    }
                    else if (procent < 0.5)
                    {
                        return slumpKort.Next(6, (Spelinställningar.KortMaxVärde + 1));
                    }
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
                default:
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
            }
        }
        //svårighetsgrader: Lätt = 50% chans för mina kort att bli 1-2, 10% för datorn att bli över 3. Datorn drar kort till 18. Kortmaxvärde 10.
        //Medel = Defaultläge, slutardrakortvid 20
        //Svår = maxvärde kort: 13, slutardrakortvid 21,  15% chans för mina kort att bli över 5
        //Omöjlig = 50% mina kort över 5, 10% över 8, datorn 40% under 3, 70% under 5
        public int RandomCardTillDatorn()
        {
            Random slumpKort = new Random();

            switch (Spelinställningar.Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Custom:
                    Card dragetKort = Kortspel.DrawACardFromDeck();
                    Console.WriteLine($"Datorn drog {dragetKort.CardName}. ");
                    return dragetKort.CardValue;

                case EnumVärden.Svårighetsgrader.Lätt:
                    if (slumpKort.NextDouble() < 0.1)
                    {
                        return slumpKort.Next(4, (Spelinställningar.KortMaxVärde + 1));
                    }
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Medel:
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Svår:
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = slumpKort.NextDouble();
                    if (procent < 0.4)
                    {
                        return slumpKort.Next(Spelinställningar.KortMinVärde, 3);
                    }
                    else if (procent < 0.7)
                    {
                        return slumpKort.Next(Spelinställningar.KortMinVärde, 5);
                    }
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
                default:
                    return slumpKort.Next(Spelinställningar.KortMinVärde, (Spelinställningar.KortMaxVärde + 1));
            }
        }

        public void RegistreraVinnaren(EnumVärden.Resultat resultat, Spelare vinnare, Spelare förlorare)
        {
            bool oavgjort = false;  
            switch (resultat)
            {
                case EnumVärden.Resultat.PoängÖverskridit21:
                    if (förlorare.Namn == "Datorn")
                    {
                        Console.WriteLine("Datorns poäng har överskridit 21.");
                        StringManipulationMethods.SkrivUtIFärg($"Grattis {vinnare.Namn}! Du har vunnit!\n", ConsoleColor.DarkCyan);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Din poäng har överskridit 21.");
                        StringManipulationMethods.SkrivUtIFärg("Du har förlorat.\n", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                case EnumVärden.Resultat.PoängNärmast21:
                    if (vinnare.Namn == "Datorn")
                    {
                        Console.WriteLine("Datorns poäng är närmast 21. ");
                        StringManipulationMethods.SkrivUtIFärg("Datorn har vunnit!", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Din poäng är närmast 21. ");
                        StringManipulationMethods.SkrivUtIFärg($"Grattis {vinnare.Namn}! Du har vunnit!\n", ConsoleColor.DarkCyan);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                case EnumVärden.Resultat.BådaÖver21:
                    Console.WriteLine("Både din och datorns poäng har överskridit 21.");
                    StringManipulationMethods.SkrivUtIFärg("Det blev oavgjort.\n", ConsoleColor.DarkGray);
                    oavgjort = true;
                    SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    break;
                case EnumVärden.Resultat.BådaSammaPoäng:
                    if (Spelinställningar.MöjligtMedOavgjort)
                    {
                        Console.WriteLine("Du och datorn har landat på samma poäng.");
                        StringManipulationMethods.SkrivUtIFärg("Det blev oavgjort.\n", ConsoleColor.DarkGray);
                        oavgjort = true;
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Samma poäng. Då vinner ");
                        StringManipulationMethods.SkrivUtIFärg("datorn\n", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}

