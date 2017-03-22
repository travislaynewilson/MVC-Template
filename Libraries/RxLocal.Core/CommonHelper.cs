using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using RxLocal.Core.ComponentModel;

namespace RxLocal.Core
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class CommonHelper
    {
        /// <summary>
        /// Sets the value of the string to be the first non-nullable parameter found for the strings provided.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns>The first non-null, non-empty string found.</returns>
        public static string Coalesce(params string[] strings)
        {
            return strings.FirstOrDefault(s => !string.IsNullOrEmpty(s)) ?? string.Empty;
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// Masks the end of a string with the provided mask (default: '*'), leaving a provided number of unmasked characters remaining (default: 4).
        /// </summary>
        /// <param name="value">The object to mask.</param>
        /// <param name="unmaskedLength">The number of characters to leave unmasked. Defaults to 4.</param>
        /// <param name="mask">The character used as the mask</param>
        /// <returns>The masked string, or the original string if there were not enough characters to leave unmasked.</returns>
        public static string Mask(object value, int unmaskedLength = 4, char mask = '*')
        {
            var result = value.ToString();
            if (result.Length <= unmaskedLength) unmaskedLength = result.Length;

            return result.Substring(result.Length - unmaskedLength).PadLeft(result.Length, mask);
        }

        #region Random Generators

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        #endregion

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length <= maxLength) return str;

            var result = str.Substring(0, maxLength);
            if (!string.IsNullOrEmpty(postfix))
            {
                result += postfix;
            }
            return result;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate) {
            var result = false;
            Array.ForEach(stringsToValidate, str => {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        /// <summary>
        /// Compare two arrasy
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a1">Array 1</param>
        /// <param name="a2">Array 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;

            return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
        }

        /// <summary>
        /// Sets a property on an object to a value.
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            var instanceType = instance.GetType();
            var pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new RxLocalException("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType);
            if (!pi.CanWrite)
                throw new RxLocalException("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType);
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        /// <summary>
        /// Gets any custom type converters used in To() type conversions.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The matching custom type converter, or the default type converter if no custom type is specified.</returns>
        public static TypeConverter GetRxLocalCustomTypeConverter(Type type)
        {
            if (type == typeof(bool))
                return new BooleanTypeConverter();

            if (type == typeof(IList<int>))
                return new GenericListTypeConverter<int>();

            if (type == typeof(IList<decimal>))
                return new GenericListTypeConverter<decimal>();

            if (type == typeof(IList<double>))
                return new GenericListTypeConverter<double>();

            if (type == typeof(IList<string>))
                return new GenericListTypeConverter<string>();

            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                var destinationConverter = GetRxLocalCustomTypeConverter(destinationType);
                var sourceConverter = GetRxLocalCustomTypeConverter(sourceType);

                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);

                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);

                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);

                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value, CultureInfo culture)
        {
            return (T)To(value, typeof(T), culture);
        }
        
        /// <summary>
        /// Convert enum for front-end display
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string GetEnumDisplayName(string str)
        {
            var result = string.Empty;
            var letters = str.ToCharArray();

            foreach (var c in letters)
            {
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            }
            result = result.Trim();

            return result;
        }

        /// <summary>
        /// Convert enum for front-end display
        /// </summary>
        /// <param name="enumValue">Enum value</param>
        /// <returns>Converted string</returns>
        public static string GetEnumDisplayName<T>(T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return GetEnumDisplayName(enumValue.ToString(CultureInfo.InvariantCulture));
        }
    }
}
