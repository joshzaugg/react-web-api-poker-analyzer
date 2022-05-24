using React.Poker.Common.Extensions;
using React.Poker.DataModel.Interfaces;

namespace React.Poker.DataModel.Models
{
    public class PokerCard : IPokerCard
    {
        /// <summary>
        /// Numeric Rank of the PokerCard
        /// </summary>
        public PokerCardRank Rank { get; }

        /// <summary>
        /// Suit of the PokerCard
        /// </summary>
        public Suit Suit { get; }

        /// <summary>
        /// Abbreviation for summary text, etc.
        /// </summary>
        public string Abbreviation
        {
            get { return $"{Rank.GetDescriptionAttr()}{Suit.GetDescriptionAttr()}"; }
        }

        public PokerCard(PokerCardRank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }
    }
}
