namespace Blackjack
{
    public class Card
    {
        public Suit Suit { get; set; }

        public Face Face { get; set; }

        public int Value
        {
            get
            {
                CardMetaAttribute card = Util.GetCardValue<Face>(Face);
                return card.Value;
            }
        }

        public override string ToString()
        {
            var suitMeta = Util.GetCardValue<Suit>(this.Suit);
            var faceMeta = Util.GetCardValue<Face>(this.Face);

            return string.Format(" {0}{1} ", faceMeta.Face, suitMeta.Suit);
        }
    }
}