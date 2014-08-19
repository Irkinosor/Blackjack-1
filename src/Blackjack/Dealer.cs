namespace Blackjack
{
    using System.Collections.Generic;
    using System.Linq;

    public class Dealer : Player
    {
        #region Constructors and Destructor's

        public Dealer()
        {
            this.Cards = new List<Card>();
        }

        #endregion

        #region Public Properties

        public int FaceUpCardsValue
        {
            get
            {
                return this.Cards.First()
                           .Value;
            }
        }

        public bool HasAce
        {
            get
            {
                return this.Cards.First()
                           .Face == Face.Ace;
            }
        }

        #endregion

        #region Public Methods and Operators

        public GameDecision MakeDecision()
        {
            var totalValue = this.Cards.GetBjValue();

            if (totalValue >= 17)
            {
                return GameDecision.Stand;
            }

            return GameDecision.Hit;
        }

        public string ReadFaceUpCards()
        {
            return string.Format("[{0}] [Hidden]", this.Cards.First());
        }

        #endregion
    }

    public enum GameDecision
    {
        Hit, 

        Stand
    }
}