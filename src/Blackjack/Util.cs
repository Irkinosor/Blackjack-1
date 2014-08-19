using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public static class Util
    {
        public static CardMetaAttribute GetCardValue<T>(Enum card)
        {
            return typeof(T).GetMember(card.ToString())
                                            .First()
                                            .GetCustomAttribute<CardMetaAttribute>();
        }

        public static GameDecision ReadDecision()
        {
            Console.WriteLine("Please play your decision: [(S)tand (H)it]");
            ConsoleKeyInfo userOption = Console.ReadKey(true);
            while (userOption.Key != ConsoleKey.H && userOption.Key != ConsoleKey.S)
            {
                Console.WriteLine("Please choose a valid option: [(S)tand (H)it]");
                userOption = Console.ReadKey(true);
            }

            switch (userOption.Key)
            {
                case ConsoleKey.H: return GameDecision.Hit;
                default: return GameDecision.Stand;
            }
        }

        /// <summary>
        ///     Calculates the sum of all the cards in a list
        /// </summary>
        /// <param name="cards">The cards to calculate</param>
        /// <returns>The value of all the cards</returns>
        public static int GetBjValue(this List<Card> cards)
        {
            // Count the number of aces
            int totalAces = cards.Count(card => card.Face == Face.Ace);

            // Count the sum of every card excluding aces
            int cardsValue = cards.Where(card => card.Face != Face.Ace)
                                  .Select(card => card.Value)
                                  .Sum();

            // Calculate the total value of the aces
            int totalAcesValue = totalAces * 11;

            int totalCardsValue = cardsValue + totalAcesValue;

            // Get the best possible sum by taking in to account that aces can be soft.
            while (totalCardsValue > 21 && totalAces > 0)
            {
                totalCardsValue -= 10;
                totalAces -= 1;
            }
                               
            return totalCardsValue;
        }
    }
}
