using System;
using System.IO;
using System.Collections.Generic;
using System.Text;


namespace Projekt_21an.Extraspel
{
    public class Lyckohjul
    {
        public static void LyckohjulSpin()
        {
            Console.WriteLine("Välj ett tal mellan 1-10. ");
            int valtTal = int.Parse(Console.ReadLine());
            Console.WriteLine("Snurrar hjulet... ");

            Random slumptal = new Random();
            int resultat = slumptal.Next(1, 11);
            Console.WriteLine("Hjulet stannade på {0}.", resultat);
            if (valtTal == resultat)
            {
                Console.WriteLine("Grattis, du vann! ");
            }
            else
            {
                Console.WriteLine("Tyvärr ingen vinst. ");
            }
        }

    }
}
