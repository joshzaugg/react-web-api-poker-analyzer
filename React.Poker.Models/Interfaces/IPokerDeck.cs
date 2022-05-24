using React.Poker.DataModel.Models;
using System.Collections.Generic;

namespace React.Poker.DataModel.Interfaces
{
    public interface IPokerDeck
    {
        IList<PokerCard> Cards { get; }
        void Shuffle();
        IList<PokerHand> Deal(IList<string> playerList);
    }
}
