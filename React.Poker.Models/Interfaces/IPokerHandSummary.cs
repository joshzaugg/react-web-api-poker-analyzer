using React.Poker.DataModel.Models;
using System.Collections.Generic;

namespace React.Poker.DataModel.Interfaces
{
    public interface IPokerHandSummary
    {
        string PlayerName { get; }
        int GameId { get; }
        IEnumerable<IPokerCard> NormalizedHand { get; }
        PokerHandRank HandRank { get; }
        int CalculatedValue { get; }
        string GetSummaryText { get; }

    }
}
