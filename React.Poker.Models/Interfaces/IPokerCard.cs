using React.Poker.DataModel.Models;

namespace React.Poker.DataModel.Interfaces
{
    public interface IPokerCard
    {
        PokerCardRank Rank { get; }
        Suit Suit { get; }
        string Abbreviation { get; }
    }
}
