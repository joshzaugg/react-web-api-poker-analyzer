using React.Poker.DataModel.Interfaces;
using React.Poker.Common.Extensions;

namespace React.Poker.ApplicationCore.Extensions
{
    public static class PokerCardExtensions
    {
        public static string GetCardSummary(this IPokerCard pokerCard)
        {
            return $"{pokerCard.Rank.GetDescriptionAttr()}{pokerCard.Suit.GetDescriptionAttr()}";
        }
    }
}
