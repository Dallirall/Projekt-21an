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
    public partial class _21an_spelet : IKonsolKortspel
    {
        private string speletsNamn = "21an";
        public string SpeletsNamn { get { return speletsNamn; } }
        
        private string regler = $"I 21:an kommer du att spela mot datorn och försöka tvinga datorn att få över 21 poäng. Både du och datorn får poäng genom att dra kort. När spelet börjar dras kort till både dig och datorn. Därefter får du dra hur många kort som du vill tills du är nöjd med din totalpoäng, du vill komma så nära 21 som möjligt utan att få mer än 21 poäng. När du inte vill dra fler kort så kommer datorn att dra kort tills den har mer eller lika många poäng som dig.\n\nDu vinner om datorn får mer än totalt 21 poäng när den håller på att dra kort. Datorn vinner om den har mer poäng än dig när spelet är slut så länge som datorn inte har mer än 21 poäng. Om du får mer än 21 poäng när du drar kort så har du förlorat.\n";
        public string Regler { get { return regler; } }

        public ÖversattKortlek Kortspel { get; set; } = new ÖversattKortlek();

                
        public void RunGame()
        {
            if (Spelinställningar.Svårighetsgrad == EnumVärden.Svårighetsgrader.Lätt || Spelinställningar.Svårighetsgrad == EnumVärden.Svårighetsgrader.Medel)
            {
                Kortspel.Kortlek = Kortspel.SortAwayUnwantedCards(Kortspel.Kortlek, new[] { "" }, new[] { 11, 12, 13 });
            }
            
            Spelare datorn = new Spelare("Datorn");
            Console.WriteLine("Skriv in ditt spelarnamn: ");
            Spelare spelare = new Spelare(Console.ReadLine());
            
            if (!SqlMetoder.ExistsInDatabaseCheck(spelare.Namn))
            {
                SqlMetoder.RegistreraNySpelareIDatabasen(spelare);
            }

            //Spelfas 1: Utdelning av starthand
            Console.WriteLine($"\nNu kommer {Spelinställningar.AntalKortAttBörjaMed} kort dras per spelare.\n");
            Console.ReadKey();
            spelare.Poäng = 0;
            datorn.Poäng = 0;
            for (int i = 0; i < Spelinställningar.AntalKortAttBörjaMed; i++)
            {
                spelare.Poäng += RandomCardTillSpelaren();
                Console.Write($"Din poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{spelare.Poäng}\n", ConsoleColor.Green);
                Console.WriteLine("");
                Console.ReadKey();

                datorn.Poäng += RandomCardTillDatorn();
                Console.Write($"Datorns poäng: ");
                StringManipulationMethods.SkrivUtIFärg($"{datorn.Poäng}\n", ConsoleColor.Red);
                Console.WriteLine("");
                Console.ReadKey();
            }

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
                    Console.WriteLine("");
                    if (choice == "j")
                    {                       
                        spelare.Poäng += RandomCardTillSpelaren();                        
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
                avgjort = CheckaVinstConditions(datorn, spelare);
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
                Console.WriteLine("");
                datorn.Poäng += RandomCardTillDatorn();
                Console.WriteLine("");
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
                    avgjort = CheckaVinstConditions(datorn, spelare);
                }   
            }
        }

    }
}

