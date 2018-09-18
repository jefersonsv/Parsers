using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Parsers.Converter
{
    public class SlugmeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
           System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new Slugme(value as string);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
