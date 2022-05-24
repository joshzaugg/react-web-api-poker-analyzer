using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using React.Poker.Common.Converters;
using System.ComponentModel;

namespace React.Poker.DataModel.Models
{
    /// <summary>
    /// PokerHand value rankings
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [TypeConverter(typeof(GeneralEnumTypeConverter))]
    public enum PokerHandRank : byte
    {
        [Description("High Card")]
        HighCard = 0x1,
        [Description("One Pair")]
        OnePair = 0x2,
        [Description("Two Pair")]
        TwoPair = 0x3,
        [Description("Three of a Kind")]
        ThreeOfAKind = 0x4,
        [Description("Straight")]
        Straight = 0x5,
        [Description("Flush")]
        Flush = 0x6,
        [Description("Full House")]
        FullHouse = 0x7,
        [Description("Four of a Kind")]
        FourOfAKind = 0x8,
        [Description("Straight Flush")]
        StraightFlush = 0x9,
        [Description("Royal Flush")]
        RoyalFlush = 0xA
    }
}
