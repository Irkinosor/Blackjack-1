namespace Blackjack.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PlayerTests
    {
        #region Public Properties

        public Player Player { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void Calculating_SumOf_10_10_A_Equals_21()
        {
            this.Player.Cards.Add(new Card { Face = Face.Ace, Suit = Suit.Diamond });
            this.Player.Cards.Add(new Card { Face = Face.Ten, Suit = Suit.Diamond });
            this.Player.Cards.Add(new Card { Face = Face.Ten, Suit = Suit.Diamond });

            Assert.That(this.Player.Cards.GetBjValue() == 21);
        }

        [Test]
        public void Calculating_SumOf_10_A_Equals_21()
        {
            this.Player.Cards.Add(new Card { Face = Face.Ace, Suit = Suit.Diamond });
            this.Player.Cards.Add(new Card { Face = Face.Ten, Suit = Suit.Diamond });

            Assert.That(this.Player.Cards.GetBjValue() == 21);
        }

        [Test]
        public void HasBlackjack_WithBlackjack_Returns_True()
        {
            this.Player.Cards.Add(new Card { Face = Face.Ace, Suit = Suit.Diamond });
            this.Player.Cards.Add(new Card { Face = Face.Ten, Suit = Suit.Diamond });

            Assert.That(this.Player.HasBlackjack);
        }

        [Test]
        public void HasBlackjack_WithOutBlackjack_Returns_False()
        {
            this.Player.Cards.Add(new Card { Face = Face.Ace, Suit = Suit.Diamond });
            this.Player.Cards.Add(new Card { Face = Face.Ten, Suit = Suit.Diamond });
            this.Player.Cards.Add(new Card { Face = Face.Ten, Suit = Suit.Diamond });

            Assert.That(this.Player.HasBlackjack == false);
        }

        [SetUp]
        public void Init()
        {
            this.Player = new Player();
        }

        #endregion
    }
}