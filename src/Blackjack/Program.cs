namespace Blackjack
{
    using System;

    internal class Program
    {
       
        private static void Main()
        {
            Console.Title = "♠♥♣♦ Blackjack Game by Niv Harel";

            Game game = new Game();

            game.Play();

            Console.ReadLine();
        }
    }
}