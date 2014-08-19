// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Player type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Blackjack.Abstractions;

    public class Player : IPlayer
    {
        #region Fields

        #endregion

        #region Constructors and Destructors

        public Player()
        {
            this.Cards = new List<Card>();
            this.Chips = 100;
        }

        #endregion

        #region Public Properties

        public List<Card> Cards { get; set; }


        public int CardsTotalValue
        {
            get
            {
                return this.Cards.GetBjValue();
            }
        }

        public decimal Chips { get; private set; }

        public bool HasBlackjack
        {
            get
            {
                return this.Cards.Count == 2 && this.CardsTotalValue == 21;
            }
        }

        public bool HasBusted
        {
            get
            {
                return this.Cards.GetBjValue() > 21;
            }
        }

        public bool IsInsured { get; set; }
        #endregion

        #region Public Methods and Operators

        public decimal Bet(decimal chips)
        {
            if (chips < 1)
            {
                throw new InvalidOperationException("You can not bet a negative number of chips.");
            }

            if (chips > this.Chips)
            {
                throw new InvalidOperationException("You can not bet more chips than you own.");
            }

            this.Chips -= chips;

            return chips;
        }

        public string ReadCards()
        {
            StringBuilder builder = new StringBuilder();

            foreach (Card card in this.Cards)
            {
                builder.Append(@"[" + card + "]");
            }

            return builder.ToString();
        }

        public void Win(decimal chips)
        {
            this.Chips += chips;
        }

        #endregion
    }
}