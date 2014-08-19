namespace Blackjack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Deck
    {
        private Queue<Card> Cards;

        public Deck()
        {
            this.AddNewDeck();
        }

        public void AddNewDeck()
        {
            var cards = new List<Card>();

            for (int cardFace = 0; cardFace < 13; cardFace++)
            {
                Card cardClub    = new Card { Suit = Suit.Club,    Face = (Face)cardFace };
                Card cardHeart   = new Card { Suit = Suit.Heart,   Face = (Face)cardFace };
                Card cardDiamond = new Card { Suit = Suit.Diamond, Face = (Face)cardFace };
                Card cardSpade   = new Card { Suit = Suit.Spade,   Face = (Face)cardFace };

                cards.Add(cardClub);
                cards.Add(cardHeart);
                cards.Add(cardDiamond);
                cards.Add(cardSpade);
            }

            var shuffledCards = cards.OrderBy(card => Guid.NewGuid()
                                                          .ToString());

            this.Cards = new Queue<Card>(shuffledCards); 
        }
            
        public Card DrawCard()
        {
            if (!this.Cards.Any())
            {
                this.AddNewDeck();
            }

            return this.Cards.Dequeue();
        }

        public int GetNumberOfRemainingCards()
        {
            return this.Cards.Count;
        }

        public void PrintDeck()
        {
            int i = 1;
            foreach (Card card in this.Cards)
            {
                Console.WriteLine("Card {0}: {1} of {2}. Value: {3}", i, card.Face, card.Suit, card.Value);
                i++;
            }
        }



    }
}