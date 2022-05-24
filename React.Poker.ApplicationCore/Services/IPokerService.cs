using React.Poker.DataModel.Interfaces;
using React.Poker.DataModel.Models;
using System.Collections.Generic;

namespace React.Poker.ApplicationCore.Services
{
    public interface IPokerService
    {
        IPokerGame DealNewGame(IList<string> players);
        bool DeleteGame(int id);
        IPokerGame GetExistingPokerGame(int id);
        IPokerHandSummary DetermineWinner(IList<PokerHand> hands, int gameId);
        IPokerHandSummary DetermineWinner(IPokerGame game);
        IPokerHandSummary DetermineWinner(int gameId);
    }
}
