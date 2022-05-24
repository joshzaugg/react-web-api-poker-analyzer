using React.Poker.DataModel.Models;
using System.Collections.Generic;

namespace React.Poker.DataModel.Interfaces
{
    public interface IPokerHand
    {
        string PlayerName { get; set; }
        IList<PokerCard> Cards { get; }
    }
}
