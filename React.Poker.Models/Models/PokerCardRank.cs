using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using React.Poker.Common.Converters;
using System.ComponentModel;

namespace React.Poker.DataModel.Models
{
    /// <summary>
    /// Enum to represent the card rank values
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [TypeConverter(typeof(GeneralEnumTypeConverter))] 
    public enum PokerCardRank : byte
    {        
        [Description("2")]
        Two = 0x2,
        [Description("3")]
        Three = 0x3,
        [Description("4")]
        Four = 0x4,
        [Description("5")]
        Five = 0x5,
        [Description("6")]
        Six = 0x6,
        [Description("7")]
        Seven = 0x7,
        [Description("8")]
        Eight = 0x8,
        [Description("9")]
        Nine = 0x9,
        [Description("10")]
        Ten = 0xA,
        [Description("J")]
        Jack = 0xB,
        [Description("Q")]
        Queen = 0xC,
        [Description("K")]
        King = 0xD, 
        [Description("A")]
        Ace = 0xE
    }
}