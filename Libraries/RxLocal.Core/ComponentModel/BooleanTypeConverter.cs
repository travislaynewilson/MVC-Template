using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace RxLocal.Core.ComponentModel
{
    /// <summary>
    /// Smart boolean type converted
    /// </summary>
    public class BooleanTypeConverter : TypeConverter
    {
        protected readonly TypeConverter typeConverter;

        protected Dictionary<bool, string[]> valueMap { get; private set; } = new Dictionary<bool, string[]> {
            { true, new[] { "true", "1", "yes", "on", "active" }},
            { false, new[] { "false", "0", "no", "off", "inactive", string.Empty }}
        };

        /// <summary>
        /// Gets a value indicating whether this converter can        
        /// convert an object in the given source type to the native type of the converter
        /// using the context.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="sourceType">Source type</param>
        /// <returns>Result</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Converts the given object to the converter's native type.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="culture">Culture</param>
        /// <param name="value">Value</param>
        /// <returns>Result</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return false;

            var stringValue = value.ToString().Trim().ToLowerInvariant();

            foreach (var map in valueMap)
            {
                if (map.Value.Contains(stringValue, StringComparer.InvariantCultureIgnoreCase))
                {
                    return map.Key;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
