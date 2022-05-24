using React.Poker.DataModel.Interfaces;
using System.Collections.Generic;

namespace React.Poker.DataModel.Models
{
    /// <summary>
    /// Poker Hand
    /// </summary>
    public class PokerHand : IPokerHand
    {
        /// <summary>
        /// Name of player, uniquely identifies hand
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Collection of cards for this hand, in this case only 5
        /// </summary>
		public IList<PokerCard> Cards { get; }

		public PokerHand(string player, IList<PokerCard> cards)
        {
            PlayerName = player;
			Cards = cards;
        }
	}
}
