using React.Poker.DataModel.Models;
using System.Collections.Generic;

namespace React.Poker.DataModel.Interfaces
{
    public interface IPokerGame
    {
        int GameId { get; }
        IPokerDeck Deck { get; }
        IList<PokerHand> Hands { get; }
        bool CardsDealt { get; }
    }
}
