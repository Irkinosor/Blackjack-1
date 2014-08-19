namespace Blackjack
{
    public enum Face
    {
        [CardMeta(Value = 11, Face = "A")]
        Ace,

        [CardMeta(Value = 2, Face = "2")]
        Two,

        [CardMeta(Value = 3, Face = "3")]
        Three,

        [CardMeta(Value = 4, Face = "4")]
        Four,

        [CardMeta(Value = 5, Face = "5")]
        Five,

        [CardMeta(Value = 6, Face = "6")]
        Six,

        [CardMeta(Value = 7, Face = "7")]
        Seven,

        [CardMeta(Value = 8, Face = "8")]
        Eight,

        [CardMeta(Value = 9, Face = "9")]
        Nine,

        [CardMeta(Value = 10, Face = "10")]
        Ten,

        [CardMeta(Value = 10, Face = "J")]
        Jack,

        [CardMeta(Value = 10, Face = "Q")]
        Queen,

        [CardMeta(Value = 10, Face = "K")]
        King,
    }
}