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
        //fixa textan snyggt...
        private string regler = $"I 21:an kommer du att spela mot datorn och försöka tvinga datorn att få över 21 poäng. Både du och datorn får poäng genom att dra kort, varje kort är värt 1 – 10 poäng. När spelet börjar dras två kort till både dig och datorn. Därefter får du dra hur många kort som du vill tills du är nöjd med din totalpoäng, du vill komma så nära 21 som möjligt utan att få mer än 21 poäng. När du inte vill dra fler kort så kommer datorn att dra kort tills den har mer eller lika många poäng som dig.\n\nDu vinner om datorn får mer än totalt 21 poäng när den håller på att dra kort. Datorn vinner om den har mer poäng än dig när spelet är slut så länge som datorn inte har mer än 21 poäng. Om det skulle bli lika i poäng så vinner datorn. Om du får mer än 21 poäng när du drar kort så har du förlorat.\n\n";
        public string Regler { get { return regler; } }
        public string SpeletsNamn {get { return speletsNamn; } }

        public void RunGame21an()
        {
            Console.WriteLine("Nu kommer två kort dras per spelare.");
            int dinPoäng = RandomCard();
            dinPoäng += RandomCard();
            int datornsPoäng = RandomCard();
            datornsPoäng += RandomCard();
            int nyttKort = 0;
            

            while (true)
            {
                Console.WriteLine($"Din poäng: {dinPoäng}");
                Console.WriteLine($"Datorna poäng: {datornsPoäng}");
                Console.WriteLine("Vill du ha ett till kort? (j/n)");
                if (Console.ReadLine() == "J" || Console.ReadLine() == "j")
                {
                    nyttKort = RandomCard();
                    dinPoäng += nyttKort;
                    Console.WriteLine($"Ditt nya kort är värt {nyttKort} poäng");
                    Console.WriteLine($"Din totalpoäng är {dinPoäng}");
                    if (ÄrPoängenÖver21(dinPoäng))
                    {
                        
                    }
                }
                else
                {

                }
            }


        }

        public int RandomCard()
        {
            Random slumpKort = new Random();
            return slumpKort.Next(1, 11);
        }

        public bool ÄrPoängenÖver21(int resultat)
        {
            if (resultat > 21)
            {
                return true;
            }
            return false;
        }
    }
}

// *
// * 
// * 21an metoden:

// 
// * 
// * if kollapoängen(dinPoäng)
// * break
// * 
// * else 
// * break
// * 
// * if kollapoängen (dinPoäng) == true 
// * CW Du har förlorat mm...
// * variabel vinnare = "Datorn" (Lagra i logfil)
// * else
// * (datorns tur) while datorns poäng < 21
// * nyttkort = rand
// * datorns poäng += nyttkort 
// * CW kort resultat
// * CW totalscore 
// * 
// * if datorn poäng > 21
// * CW Du har vunnit, skriv namn mm
// * RL lagra i logfile 
// * else if datorn poäng >= din poäng 
// * CW Du har förlorat mm...
// * variabel vinnare = "Datorn" (Lagra i logfil)