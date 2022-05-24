using Newtonsoft.Json;
using React.Poker.DataModel.Interfaces;
using System.Collections.Generic;

namespace React.Poker.DataModel.Models
{
    public class PokerGame : IPokerGame
    {
        /// <summary>
        /// Game ID
        /// </summary>
        public int GameId { get; }

        /// <summary>
        /// THe Deck from which we're dealing the hands
        /// </summary>
        [JsonIgnore]
        public IPokerDeck Deck { get; }

        /// <summary>
        /// The collection of Poker hands in this game
        /// </summary>
        public IList<PokerHand> Hands { get; }

        /// <summary>
        /// Helper to know if we've already dealt the hands
        /// </summary>
        [JsonIgnore]
        public bool CardsDealt => Hands.Count > 0;

        public PokerGame(int id, IList<string> players)
        {
            GameId = id;
            Deck = new PokerDeck();
            Deck.Shuffle();
            Hands = Deck.Deal(players);
        }
    }
}
