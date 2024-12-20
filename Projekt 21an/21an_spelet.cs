using spel21an;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;

namespace Projekt_21an
{
    public class _21an_spelet : IGame
    {
        private string speletsNamn = "21an";
        public string SpeletsNamn { get { return speletsNamn; } }
        
        private string regler = $"I 21:an kommer du att spela mot datorn och försöka tvinga datorn att få över 21 poäng. Både du och datorn får poäng genom att dra kort. När spelet börjar dras kort till både dig och datorn. Därefter får du dra hur många kort som du vill tills du är nöjd med din totalpoäng, du vill komma så nära 21 som möjligt utan att få mer än 21 poäng. När du inte vill dra fler kort så kommer datorn att dra kort tills den har mer eller lika många poäng som dig.\n\nDu vinner om datorn får mer än totalt 21 poäng när den håller på att dra kort. Datorn vinner om den har mer poäng än dig när spelet är slut så länge som datorn inte har mer än 21 poäng. Om du får mer än 21 poäng när du drar kort så har du förlorat.\n";
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
        public int KortMaxVärde { get { return kortMaxVärde; } set { kortMaxVärde = value; } }

        private int kortMinVärde = 1;
        public int KortMinVärde { get { return kortMinVärde; } set { kortMinVärde = value; } }

        private EnumVärden.Svårighetsgrader svårighetsgrad = EnumVärden.Svårighetsgrader.Lätt;
        public EnumVärden.Svårighetsgrader Svårighetsgrad { get { return svårighetsgrad; } set { svårighetsgrad = value; } }

        private int antalKortAttBörjaMed = 2;
        public int AntalKortAttBörjaMed { get { return antalKortAttBörjaMed; } set { antalKortAttBörjaMed = value; } }

        private bool möjligtMedOavgjort = false;
        public bool MöjligtMedOavgjort { get { return möjligtMedOavgjort; } set { möjligtMedOavgjort = value; } }

        public int datornSlutarDraKortVid = 21;

        public int DatornSlutarDraKortVid { get {return datornSlutarDraKortVid;} set {datornSlutarDraKortVid = value; } }



        public void Spelinställningar()
        {
            Console.WriteLine("Skriv siffra för att välja svårighetsgrad: \r\n1. Lätt \r\n2. Medel \r\n3. Svår \r\n4. Mer eller mindre omöjlig \r\nVill du inte ha en förinställd nivå utan justera inställningar själv, skriv '0'. ");
            
            Svårighetsgrad = (EnumVärden.Svårighetsgrader)int.Parse(Console.ReadLine());
            switch (Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Custom:
                    Console.WriteLine($"Välj antal kort som varje spelare ska dra i början av spelet (default 2, max {(int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed})");
                    AntalKortAttBörjaMed = int.Parse(Console.ReadLine());
                    if (AntalKortAttBörjaMed > (int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed)
                    {
                        Console.WriteLine($"Input överskred maxvärdet. Ställs in till {(int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed}");
                        AntalKortAttBörjaMed = (int)EnumVärden.SettingsMaxvärden.MaxLimit_AntalKortAttBörjaMed;
                    }

                    Console.WriteLine($"Välj maxvärdet på korten som dras (default 10, max {(int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde})");
                    KortMaxVärde = int.Parse(Console.ReadLine());
                    if (KortMaxVärde > (int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde)
                    {
                        Console.WriteLine($"Input överskred maxvärdet. Ställs in till {(int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde}");
                        KortMaxVärde = (int)EnumVärden.SettingsMaxvärden.MaxLimit_KortMaxVärde;
                    }

                    Console.WriteLine($"Välj det värde då datorn ska sluta dra kort (default 21, max {(int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid})");
                    DatornSlutarDraKortVid = int.Parse(Console.ReadLine());
                    if (DatornSlutarDraKortVid > (int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid)
                    {
                        Console.WriteLine($"Input överskred maxvärdet. Ställs in till {(int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid}");
                        DatornSlutarDraKortVid = (int)EnumVärden.SettingsMaxvärden.MaxLimit_DatornSlutarDraKortVid;
                    }
                    break;
                case EnumVärden.Svårighetsgrader.Lätt:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 18;
                    KortMaxVärde = 10;
                    break;
                case EnumVärden.Svårighetsgrader.Medel:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 20;
                    KortMaxVärde = 10;
                    break;
                case EnumVärden.Svårighetsgrader.Svår:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 21;
                    KortMaxVärde = 13;
                    break;
                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    AntalKortAttBörjaMed = 2;
                    DatornSlutarDraKortVid = 21;
                    KortMaxVärde = 13;
                    break;
                default: break;
            }
            Console.WriteLine("Skriv 'j' om du vill att det ska kunna bli oavgjort. Annars skriv 'n', så kommer datorn vinna vid lika resultat.");
            if (Console.ReadLine() == "j")
            {
                MöjligtMedOavgjort = true;
            }
            else
            {
                MöjligtMedOavgjort = false;
            }
            Console.WriteLine("Inställningar sparade!");
            Console.ReadKey();
        }


        public void RunGame()
        {
            Spelare datorn = new Spelare("Datorn");
            Console.WriteLine("Skriv in ditt spelarnamn: ");
            Spelare spelare = new Spelare(Console.ReadLine());
            if (!SqlMetoder.ExistsInDatabaseCheck(spelare.Namn))
            {
                SqlMetoder.RegistreraNySpelareIDatabasen(spelare);
            }

            Console.WriteLine($"\nNu kommer {AntalKortAttBörjaMed} kort dras per spelare.");
            Console.ReadKey();
            spelare.Poäng = 0;
            datorn.Poäng = 0;
            for (int i = 0; i < AntalKortAttBörjaMed; i++)
            {
                spelare.Poäng += RandomCardTillSpelaren();
                datorn.Poäng += RandomCardTillDatorn();
            }
            int nyttKort = 0;
            bool avgjort = false;
            bool duHarFörlorat = false;
            bool datornHarFörlorat = false;
            Func<int, bool> ärPoängenÖver21 = poäng => poäng > 21;

            while (!avgjort)
            {
                Console.Write($"Din poäng: ");
                Program.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                Console.Write($"Datorns poäng: ");
                Program.SkrivUtIFärg($"{datorn.Poäng}\n", ConsoleColor.Red);
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
                        Program.SkrivUtIFärg($"{nyttKort}", ConsoleColor.DarkCyan);
                        Console.Write(" poäng\n");
                        Console.Write($"Din totalpoäng är ");
                        Program.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
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
                Program.SkrivUtIFärg($"{nyttKort}\n", ConsoleColor.DarkCyan);
                Console.Write($"Din poäng: ");
                Program.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                Console.Write($"Datorns poäng: ");
                Program.SkrivUtIFärg($"{datorn.Poäng}\n", ConsoleColor.Red);
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
        // resultat jag 21, datorn 20 datornslutar20
        // resultat jag 20 datorn 20 datornslutar20
        // res jag 21 dator 21, datorslutar20
        //resultat jag 13, datorn 13
        // jag 21 datorn 20
        // if datorn mindre än mig men under slutardrakort
        //om datorn är under slutadrakort men != 21
        //if we are tied == , men inte likmed 21 och under datornsluteardrakort (20)

        //Jag drar 2 kort, datorn drar 2.
        //chacka vinst. Ex:
        //  Jag     datorn
        //  1       1
        //  1       5
        //  1       21
        //  5       1
        //  21      1
        //  21      20 (slutardrakort)
        //  21      21
        //  25      1
        //  1       25
        //  18      18 (slutar 20)

        public bool HarÖverskridit21poäng(bool datornHarFörlorat, bool duHarFörlorat, Spelare spelaren, Spelare datorn)
        {
            if (duHarFörlorat && datornHarFörlorat)
            {
                if (MöjligtMedOavgjort)
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
            else if (datorn.Poäng < DatornSlutarDraKortVid)
            {
                return false;
            }
            else if (datorn.Poäng < spelare.Poäng)
            {
                RegistreraVinnaren(EnumVärden.Resultat.PoängNärmast21, spelare, datorn);
                return true;
            }
            else if (datorn.Poäng == spelare.Poäng)
            {
                if (MöjligtMedOavgjort)
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
            
            switch (Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Custom:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Lätt:
                    if (slumpKort.NextDouble() < 0.5)
                    {
                        return slumpKort.Next(KortMinVärde, 3);
                    }
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Medel:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Svår:
                    if (slumpKort.NextDouble() < 0.15)
                    {
                        return slumpKort.Next(6, (KortMaxVärde + 1));
                    }
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = slumpKort.NextDouble();
                    if (procent < 0.1)
                    {
                        return slumpKort.Next(8, (KortMaxVärde + 1));
                    }
                    else if (procent < 0.5)
                    {
                        return slumpKort.Next(6, (KortMaxVärde + 1));
                    }
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                default:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
            }
        }
        //svårighetsgrader: Lätt = 50% chans för mina kort att bli 1-2, 10% för datorn att bli över 3. Datorn drar kort till 18. Kortmaxvärde 10.
        //Medel = Defaultläge, slutardrakortvid 20
        //Svår = maxvärde kort: 13, slutardrakortvid 21,  15% chans för mina kort att bli över 5
        //Omöjlig = 50% mina kort över 5, 10% över 8, datorn 40% under 3, 70% under 5
        public int RandomCardTillDatorn()
        {
            Random slumpKort = new Random();

            //Fixa enum
            switch (Svårighetsgrad)
            {
                case EnumVärden.Svårighetsgrader.Custom:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Lätt:
                    if (slumpKort.NextDouble() < 0.1)
                    {
                        return slumpKort.Next(4, (KortMaxVärde + 1));
                    }
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Medel:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Svår:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                case EnumVärden.Svårighetsgrader.Mer_eller_mindre_omöjlig:
                    double procent = slumpKort.NextDouble();
                    if (procent < 0.4)
                    {
                        return slumpKort.Next(KortMinVärde, 3);
                    }
                    else if (procent < 0.7)
                    {
                        return slumpKort.Next(KortMinVärde, 5);
                    }
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
                default:
                    return slumpKort.Next(KortMinVärde, (KortMaxVärde + 1));
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
                        Program.SkrivUtIFärg($"Grattis {vinnare.Namn}! Du har vunnit!\n", ConsoleColor.DarkCyan);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Din poäng har överskridit 21.");
                        Program.SkrivUtIFärg("Du har förlorat.\n", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                case EnumVärden.Resultat.PoängNärmast21:
                    if (vinnare.Namn == "Datorn")
                    {
                        Console.WriteLine("Datorns poäng är närmast 21. ");
                        Program.SkrivUtIFärg("Datorn har vunnit!", ConsoleColor.DarkRed);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Din poäng är närmast 21. ");
                        Program.SkrivUtIFärg($"Grattis {vinnare.Namn}! Du har vunnit!\n", ConsoleColor.DarkCyan);
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    break;
                case EnumVärden.Resultat.BådaÖver21:
                    Console.WriteLine("Både din och datorns poäng har överskridit 21.");
                    Program.SkrivUtIFärg("Det blev oavgjort.\n", ConsoleColor.DarkGray);
                    oavgjort = true;
                    SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    break;
                case EnumVärden.Resultat.BådaSammaPoäng:
                    if (MöjligtMedOavgjort)
                    {
                        Console.WriteLine("Du och datorn har landat på samma poäng.");
                        Program.SkrivUtIFärg("Det blev oavgjort.\n", ConsoleColor.DarkGray);
                        oavgjort = true;
                        SqlMetoder.RegistreraResultatIDatabasen(oavgjort, vinnare, förlorare);
                    }
                    else
                    {
                        Console.WriteLine("Samma poäng. Då vinner ");
                        Program.SkrivUtIFärg("datorn\n", ConsoleColor.DarkRed);
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

