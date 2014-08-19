namespace Blackjack
{
    public enum Suit
    {
        [CardMeta(Suit = "♥")]
        Heart,

        [CardMeta(Suit = "♦")]
        Diamond,

        [CardMeta(Suit = "♠")]
        Spade,

        [CardMeta(Suit = "♣")]
        Club
    }
}