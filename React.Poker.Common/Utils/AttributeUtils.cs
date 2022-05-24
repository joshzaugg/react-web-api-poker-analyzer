using System;
using System.ComponentModel;

namespace React.Poker.Common.Utils
{
    public class AttributeUtils
    {
		/// <summary>
		/// Gets the description attribute from a field
		/// </summary>
		/// <param name="objectType"></param>
		/// <param name="fieldName"></param>
		/// <returns></returns>
		public static string GetDescriptionFromField(Type objectType, string fieldName)
		{
			if (objectType == null || fieldName == null)
				return null;

			DescriptionAttribute fieldDescription = Attribute.GetCustomAttribute(objectType.GetField(fieldName), typeof(DescriptionAttribute)) as DescriptionAttribute;

			if (fieldDescription == null)
				return fieldName;

			return fieldDescription.Description;
		}
	}
}
