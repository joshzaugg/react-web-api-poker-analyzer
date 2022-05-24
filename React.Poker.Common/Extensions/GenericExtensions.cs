using System.ComponentModel;
using System.Reflection;

namespace React.Poker.Common.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Helper method to retrieve the Description attribute from a given object.
        /// This is intended for Enums, but will work on any object.
        /// If no Description Attribute is found, returns ToString() of the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>Returns string</returns>
        public static string GetDescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return source.ToString();
            }
        }
    }
}
