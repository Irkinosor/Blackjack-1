using System;

namespace Blackjack
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CardMetaAttribute : Attribute
    {


        #region All Constructors

        public CardMetaAttribute()
        {
        }

        #endregion

        #region Properties


        public int Value { get; set; }

        public string Suit { get; set; }

        public string Face { get; set; }

        #endregion
    }
}
