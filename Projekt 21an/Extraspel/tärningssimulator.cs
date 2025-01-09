using System;
using System.Collections.Generic;
using System.Text;


namespace Projekt_21an.Extraspel
{
    public static class Tärningssimulator
    {
        public static void RunDiceSimulator()
        {
            Console.WriteLine("Hur många tärningar vill du rulla? ");
            int valtAntalTärningar = int.Parse(Console.ReadLine());
            Console.WriteLine("Hur många sidor ska varje tärning ha? ");
            int valtAntalTärningssidor = int.Parse(Console.ReadLine());

            Random slumpgenerator = new Random();
            Console.WriteLine("Kastar... ");
            for (int i = 0; i < valtAntalTärningar; i++)
            {
                Console.WriteLine($"Tärning {i + 1}: {slumpgenerator.Next(1, valtAntalTärningssidor + 1)}");
            }


            Console.ReadKey();


        }
    }
}
