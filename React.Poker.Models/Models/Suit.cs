using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using React.Poker.Common.Converters;
using System.ComponentModel;

namespace React.Poker.DataModel.Models
{
    /// <summary>
    /// Enum to represent the Card Suits
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [TypeConverter(typeof(GeneralEnumTypeConverter))]
    public enum Suit
    {
        [Description("C")]
        Clubs,
        [Description("D")]
        Diamonds,
        [Description("H")]
        Hearts,
        [Description("S")]
        Spades
    }
}
