// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IPlayer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Blackjack.Abstractions
{
    using System.Collections.Generic;

    public interface IPlayer
    {
        #region Public Properties

        List<Card> Cards { get; set; }

        int CardsTotalValue { get; }

        decimal Chips { get; }

        bool HasBlackjack { get; }

        bool HasBusted { get; }

        #endregion

        #region Public Methods and Operators

        decimal Bet(decimal chips);

        string ReadCards();

        void Win(decimal chips);

        bool IsInsured { get; set; }

        #endregion
    }
}