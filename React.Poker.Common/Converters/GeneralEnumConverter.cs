using React.Poker.Common.Utils;
using System;
using System.ComponentModel;
using System.Globalization;

namespace React.Poker.Common.Converters
{
    public class GeneralEnumTypeConverter : EnumConverter
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumType"></param>
		public GeneralEnumTypeConverter(Type enumType) : base(enumType)
		{
			UseDescription = true;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool UseDescription
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (UseDescription && value is string)
			{
				object enumValue = this.FindEnumValue(value as string);

				if (enumValue != null)
					return enumValue;
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (UseDescription && value != null && destinationType == typeof(string))
				return GetDescription(value);

			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="description"></param>
		/// <returns></returns>
		private object FindEnumValue(String description)
		{
			Array values = Enum.GetValues(EnumType);

			// find the value whose description matches the input string
			foreach (object entry in values)
			{
				if (GetDescription(entry) == description)
				{
					return entry;
				}
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumMember"></param>
		/// <returns></returns>
		private String GetDescription(Object enumMember)
		{
			if (enumMember == null)
				return null;

			return AttributeUtils.GetDescriptionFromField(this.EnumType, enumMember.ToString());
		}

		/// <summary>
		/// Returns the string representation of each value in <paramref name="enumType"/>
		/// using its <see cref="DescriptionAttribute"/> or if it doesn't have one, using
		/// its <see cref="object.ToString()"/> representation.
		/// </summary>
		/// <param name="enumType">An enumeration (<see cref="Enum"/>) type.</param>
		/// <returns>
		/// The description or built-in string representation of each value within the
		/// Enum type <paramref name="enumType"/>.
		/// </returns>
		public static string[] GetDescriptions(Type enumType)
		{
			GeneralEnumTypeConverter converter = new GeneralEnumTypeConverter(enumType);

			Array enumConstants = Enum.GetValues(enumType);

			string[] descriptions = new string[enumConstants.Length];

			for (int i = 0; i < enumConstants.Length; ++i)
			{
				descriptions[i] = converter.GetDescription(enumConstants.GetValue(i));
			}

			return descriptions;
		}
	}
}
