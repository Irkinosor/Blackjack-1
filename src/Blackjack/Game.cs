namespace Blackjack
{
    using System;
    using System.Collections.Generic;

    using Blackjack.Abstractions;

    public class Game
    {
        #region Constructors and Destructor's

        public Game()
        {
            this.CardDeck = new Deck();
            this.Player = new Player();
            this.Dealer = new Dealer();
        }

        public Game(IBetLimit limit)
        {
            this.Limits = limit;
        }

        #endregion

        #region Public Properties

        public Deck CardDeck { get; private set; }

        public decimal CurrentBet { get; private set; }

        public Dealer Dealer { get; set; }

        public IPlayer Player { get; set; }

        public IBetLimit Limits { get; set; }
        #endregion

        #region Public Methods and Operators

        public void PlaceBets()
        {
            Console.WriteLine("Current Chips: {0}", this.Player.Chips);
            Console.WriteLine("How much would you like to bet? ({0} - {1})", this.Limits.Min, this.Limits.Max > this.Player.Chips ? this.Player.Chips : this.Limits.Max);

            string inputBet = this.ReadBetInput();

            decimal betAmount;

            while (this.IsBetValid(inputBet, out betAmount) == false)
            {
                Console.WriteLine("How much would you like to bet? (1 - {0})", this.Player.Chips);
                inputBet = this.ReadBetInput();
            }

            this.CurrentBet = this.Player.Bet(betAmount);
        }

        public void Play()
        {
            this.PlaceBets();
            this.DealHand();
            this.InsureBet();
            this.EvaluateHands();
            this.Cleanup();
            
            if (this.Player.Chips > this.Limits.Min)
            {
                this.Play();
            }
        }

        #endregion

        #region Methods

        private void Cleanup()
        {
            this.Dealer.Cards = new List<Card>();
            this.Player.Cards = new List<Card>();
            this.Player.IsInsured = false;
            this.CurrentBet = 0;
        }

        private void DealHand()
        {
            this.Player.Cards.Add(this.CardDeck.DrawCard());
            this.Dealer.Cards.Add(this.CardDeck.DrawCard());

            this.Player.Cards.Add(this.CardDeck.DrawCard());
            this.Dealer.Cards.Add(this.CardDeck.DrawCard());

            Console.WriteLine("Your cards: {0}", this.Player.ReadCards());
            Console.WriteLine(">>> Total: {0}\n", this.Player.CardsTotalValue);
            Console.WriteLine("Dealers cards: {0}", this.Dealer.ReadFaceUpCards());
            Console.WriteLine(">>> Total: {0}\n", this.Dealer.FaceUpCardsValue);
        }


        private void EvaluateHands()
        {
            if (this.Player.HasBlackjack && this.Dealer.HasBlackjack)
            {
                Console.WriteLine("It's a TIE! You both have Blackjacks!");

                decimal chipsWon = this.Player.IsInsured ? this.CurrentBet * 1.5M : this.CurrentBet;

                this.Player.Win(chipsWon);

                return;
            }

            if (this.Dealer.HasBlackjack)
            {
                Console.WriteLine("The dealer has won.");

                if (this.Player.IsInsured)
                {
                    this.Player.Win(this.CurrentBet);
                }

                return;
            }

            if (this.Player.HasBlackjack)
            {
                decimal chipsWon = this.Player.IsInsured ? this.CurrentBet * 2 : this.CurrentBet * 2.5M;

                this.Player.Win(chipsWon);

                return;
            }

            while (true)
            {
                GameDecision playerDecision = Util.ReadDecision();

                if (playerDecision == GameDecision.Hit)
                {
                    Card drawnCard = this.CardDeck.DrawCard();
                    this.Player.Cards.Add(drawnCard);

                    Console.WriteLine("HIT: You've received: {0}", drawnCard);
                    Console.WriteLine("Your cards: {0}", this.Player.ReadCards());
                    Console.WriteLine("Total: {0}\n", this.Player.CardsTotalValue);

                    if (this.Player.HasBusted)
                    {
                        Console.WriteLine("BUSTED!");
                        return;
                    }

                    continue;
                }

                if (playerDecision == GameDecision.Stand)
                {
                    Console.WriteLine("STAND: It's now the dealers turn.");
                    Console.WriteLine(this.Dealer.ReadCards());
                    while (this.Dealer.MakeDecision() == GameDecision.Stand)
                    {
                        Card dealerDrawnCard = this.CardDeck.DrawCard();
                        this.Dealer.Cards.Add(dealerDrawnCard);
                        Console.WriteLine("HIT: The dealer received {0}", dealerDrawnCard);
                        Console.WriteLine(this.Dealer.ReadCards());

                        if (!this.Dealer.HasBusted)
                        {
                            continue;
                        }

                        Console.WriteLine("The dealer has busted!");
                        return;
                    }

                    if (this.Dealer.CardsTotalValue > this.Player.CardsTotalValue)
                    {
                        Console.WriteLine("Dealer wins!");
                        return;
                    }

                    if (this.Dealer.CardsTotalValue == this.Player.CardsTotalValue)
                    {
                        Console.WriteLine("It's a tie!");
                        this.Player.Win(this.CurrentBet);
                        return;
                    }

                    if (this.Dealer.CardsTotalValue < this.Player.CardsTotalValue)
                    {
                        Console.WriteLine("You win!");
                        this.Player.Win(this.CurrentBet * 2);
                        return;
                    }
                }
            }
        }

        private void InsureBet()
        {
            decimal priceOfInsurance = this.CurrentBet / 2;

            if (!this.Dealer.HasAce)
            {
                return;
            }

            if (this.Player.Chips < priceOfInsurance)
            {
                return;
            }

            string userInput = string.Empty;

            while (userInput != "y" && userInput != "n")
            {
                Console.WriteLine("Insurance? (y / n)");
                userInput = (Console.ReadLine() ?? string.Empty).ToLowerInvariant();
            }

            this.Player.IsInsured = userInput == "y";

            Console.WriteLine("Insurance: {0}", this.Player.IsInsured ? "Accepted" : "Denied");
            this.Player.Bet(priceOfInsurance);
        }

        private bool IsBetValid(string betValue, out decimal betAmount)
        {
            bool isValidDecimalValue = decimal.TryParse(betValue, out betAmount);

            if (!isValidDecimalValue)
            {
                Console.WriteLine("Please provide a valid value");
                return false;
            }

            if (betAmount < this.Limits.Min)
            {
                Console.WriteLine("Your bet must be at least {0} chip(s)", this.Limits.Min);
                return false;
            }

            if (betAmount > this.Limits.Max)
            {
                Console.WriteLine("This table has a upper limit of {0} chips", this.Limits.Max);
                return false;
            }

            if (betAmount > this.Player.Chips)
            {
                Console.WriteLine("You can not bet more chips than the amount you have");
                return false;
            }

            return true;
        }

        private string ReadBetInput()
        {
            return (Console.ReadLine() ?? string.Empty).Trim()
                                                       .Replace(" ", string.Empty);
        }

        #endregion
    }
}