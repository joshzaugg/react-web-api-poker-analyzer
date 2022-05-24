using React.Poker.DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace React.Poker.DataModel.Models
{
    public class PokerDeck : IPokerDeck
    {
        private IList<PokerCard> _cards;

        /// <summary>
        /// The cards currently in the PokerDeck
        /// </summary>
        public IList<PokerCard> Cards => _cards;

        public PokerDeck()
        {
            InitializeDeck();
        }

        /// <summary>
        /// Helper to load the deck with all cards
        /// </summary>
        private void InitializeDeck()
        {
            var cards = new List<PokerCard>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (PokerCardRank rank in Enum.GetValues(typeof(PokerCardRank)))
                {
                    cards.Add(new PokerCard(rank, suit));
                }
            }
            _cards = cards;
        }

        /// <summary>
        /// Randomly (somewhat) shuffle the deck
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            int count = Cards.Count;
            while (count > 1)
            {
                count--;
                int k = random.Next(count + 1);
                PokerCard value = Cards[k];
                Cards[k] = Cards[count];
                Cards[count] = value;
            }
        }

        /// <summary>
        /// Deal the cards to the provided players
        /// </summary>
        /// <param name="playerList"></param>
        /// <returns></returns>
        public IList<PokerHand> Deal(IList<string> playerList)
        {
            var hands = new List<PokerHand>();

            for (int j = 0; j < playerList.Count; j++)
            {
                var currentCards = new List<PokerCard>();
                for (int k = 1; k <= 5; k++)
                {
                    var skipCount = (playerList.Count * (k - 1)) + j;
                    currentCards.Add(Cards.Skip(skipCount).Take(1).First());
                }
                hands.Add(new PokerHand(playerList[j], currentCards));
            }

            return hands;
        }
    }
}
