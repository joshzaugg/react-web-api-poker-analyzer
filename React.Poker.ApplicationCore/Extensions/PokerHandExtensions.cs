using React.Poker.DataModel.Interfaces;
using React.Poker.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace React.Poker.ApplicationCore.Extensions
{
    /// <summary>
    /// Static Poker Hand Extension methods 
    /// </summary>
    public static class PokerHandExtensions
    {
        /// <summary>
        /// Determine if a poker hand contains exactly 1 pair
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsOnePair(this IPokerHand hand)
        {
            return hand.Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 1;
        }

        /// <summary>
        /// Determine if a poker hand contains exactly 2 pairs
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsTwoPair(this IPokerHand hand)
        {
            return hand.Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 2;
        }

        /// <summary>
        /// Determine if a poker hand contains 3 of a kind
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsThreeOfAKind(this IPokerHand hand)
        {
            return hand.Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 3)
                .Any();
        }

        /// <summary>
        /// Determine if a poker hand contains a Straight
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsStraight(this IPokerHand hand)
        {
            var orderedRanks = hand.Cards.OrderBy(c => c.Rank).Select(c => c.Rank).ToArray();

            // Handle low straight as the one-off, as all other uses of Ace are as high card
            var lowStraightRankOrder = new List<PokerCardRank>() { PokerCardRank.Two, PokerCardRank.Three, PokerCardRank.Four, PokerCardRank.Five, PokerCardRank.Ace };

            if (orderedRanks.SequenceEqual(lowStraightRankOrder))
            {
                return true;
            }

            var straightStart = (int)orderedRanks.First();
            for (var i = 1; i < orderedRanks.Length; i++)
            {
                if ((int)orderedRanks[i] != straightStart + i)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determine if a poker hand contains a Flush
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsFlush(this IPokerHand hand)
        {
            return hand.Cards.GroupBy(h => h.Suit).Count() == 1;
        }

        /// <summary>
        /// Determine if a poker hand contains a Full House
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsFullHouse(this IPokerHand hand)
        {
            return IsOnePair(hand) && IsThreeOfAKind(hand);
        }

        /// <summary>
        /// Determine if a poker hand contains 4 of a kind
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsFourOfAKind(this IPokerHand hand)
        {
            return hand.Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 4)
                .Any();
        }

        /// <summary>
        /// Determines if a poker hand contains a Straight Flush
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsStraightFlush(this IPokerHand hand)
        {
            return IsStraight(hand) && IsFlush(hand);
        }

        /// <summary>
        /// Determine if a poker hand contains a Royal Flush
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static bool IsRoyalFlush(this IPokerHand hand)
        {
            return IsStraight(hand) && IsFlush(hand) && hand.Cards.Max(c => c.Rank) == PokerCardRank.Ace;
        }

        /// <summary>
        /// Retrieve the highest Poker Card in a Poker Hand
        /// </summary>
        /// <param name="hand"></param>
        /// <returns>bool</returns>
        public static IPokerCard GetHighCard(this IPokerHand hand)
        {
            return hand.Cards.OrderByDescending(c => c.Rank).First();
        }

        /// <summary>
        /// Calculate and return a summary of a hand.
        /// Compose an integer value to represent the overall value of the hand.
        /// We determine hand rank and normalize the hand order, then compose
        /// the byte nibbles into an integer that represents total value for the hand.
        /// This ensures we can sort and compare for all uses.
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public static IPokerHandSummary GetPokerHandSummary(this IPokerHand hand, int gameId)
        {
            PokerHandRank handRank = PokerHandRank.HighCard;
            IPokerCard[] normalizedHand = new PokerCard[5];
            int handValue;

            if (hand.IsRoyalFlush())
            {
                handRank = PokerHandRank.RoyalFlush;
                normalizedHand = hand.Cards.NormalizeStraightOrFlush();
            }
            else if (hand.IsStraightFlush())
            {
                handRank = PokerHandRank.StraightFlush;
                normalizedHand = hand.Cards.NormalizeStraightOrFlush();
            }
            else if (hand.IsFourOfAKind())
            {
                handRank = PokerHandRank.FourOfAKind;
                normalizedHand = hand.Cards.NormalizeFullHouseOrFourOfAKind();
            }
            else if (hand.IsFullHouse())
            {
                handRank = PokerHandRank.FullHouse;
                normalizedHand = hand.Cards.NormalizeFullHouseOrFourOfAKind();
            }
            else if (hand.IsFlush())
            {
                handRank = PokerHandRank.Flush;
                normalizedHand = hand.Cards.NormalizeStraightOrFlush();
            }
            else if (hand.IsStraight())
            {
                handRank = PokerHandRank.Straight;
                normalizedHand = hand.Cards.NormalizeStraightOrFlush();
            }
            else if (hand.IsThreeOfAKind())
            {
                handRank = PokerHandRank.ThreeOfAKind;
                normalizedHand = hand.Cards.NormalizeThreeOfAKindOrLess();
            }
            else if (hand.IsTwoPair())
            {
                handRank = PokerHandRank.TwoPair;
                normalizedHand = hand.Cards.NormalizeThreeOfAKindOrLess();
            }
            else if (hand.IsOnePair())
            {
                handRank = PokerHandRank.OnePair;
                normalizedHand = hand.Cards.NormalizeThreeOfAKindOrLess();
            }
            else
            {
                normalizedHand = hand.Cards.OrderByDescending(c => c.Rank).ToArray();
            }
            // Bit-shift magic to preserve our order of importance for ranking
            handValue = (byte)handRank << 20 | (byte)normalizedHand[0].Rank << 16 | (byte)normalizedHand[1].Rank << 12 | (byte)normalizedHand[2].Rank << 8 | (byte)normalizedHand[3].Rank << 4 | (byte)normalizedHand[4].Rank;
           
            return new PokerHandSummary(hand.PlayerName, gameId, normalizedHand, handRank, handValue);
        }

        /// <summary>
        /// Helper to normalize any straight or flush efficiently.
        /// Doesn't require grouping like the others do.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static IPokerCard[] NormalizeStraightOrFlush(this IEnumerable<IPokerCard> cards)
        {
            return cards.OrderByDescending(c => c.Rank).ToArray();
        }

        /// <summary>
        /// Helper to normalize Full House or 4 of a Kind.
        /// Requires less group sorting than the lesser hands.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static IPokerCard[] NormalizeFullHouseOrFourOfAKind(this IEnumerable<IPokerCard> cards)
        {
            return cards.GroupBy(g => g.Rank)
                .OrderByDescending(g => g.Count())
                .SelectMany(c => c.ToList())
                .ToArray();
        }

        /// <summary>
        /// Helper to normalize any hand less than a straight.
        /// We require additional group sorting to handle 3 of a Kind or less
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static IPokerCard[] NormalizeThreeOfAKindOrLess(this IEnumerable<IPokerCard> cards)
        {
            return cards.GroupBy(c => c.Rank)
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.Key)
                .SelectMany(g => g.ToList())
                .ToArray();
        }

    }
}
