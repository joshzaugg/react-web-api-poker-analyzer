using React.Poker.Common.Extensions;
using React.Poker.DataModel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace React.Poker.DataModel.Models
{
    public class PokerHandSummary : IPokerHandSummary
    {
        /// <summary>
        /// Player who holds this hand
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        /// Game Id for this hand
        /// </summary>
        public int GameId { get; }

        /// <summary>
        /// Poker hand, normalized to present top cards together and rank
        /// </summary>
        public IEnumerable<IPokerCard> NormalizedHand { get; }

        /// <summary>
        /// Top hand type/category for this hand
        /// </summary>
        public PokerHandRank HandRank { get; }

        /// <summary>
        /// Calculated Value of this hand, based upon normalization and hand rank
        /// </summary>
        public int CalculatedValue { get; }
        
        /// <summary>
        /// Friendly summary description text
        /// </summary>
        /// <returns></returns>
        public string GetSummaryText
        {
            get { return $"Game {GameId}: Player '{PlayerName}' had {HandRank.GetDescriptionAttr()}: {string.Join("-", NormalizedHand.Select(n => n.Abbreviation))} "; }
        }

        public PokerHandSummary(string player, int gameId, IEnumerable<IPokerCard> normalizedHand, PokerHandRank rank, int calculatedValue)
        {
            PlayerName = player;
            GameId = gameId;
            NormalizedHand = normalizedHand;
            HandRank = rank;
            CalculatedValue = calculatedValue;
        }
    }
}
